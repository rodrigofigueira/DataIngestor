using System.Globalization;
using DataIngestor.Domain;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton(_ => NpgsqlDataSource.Create(
    builder.Configuration.GetConnectionString("PostgreSql")
    ?? throw new InvalidOperationException("Connection string 'PostgreSql' was not configured.")));

var app = builder.Build();

app.UseHttpsRedirection();

app.MapPost("/people/import", async (IFormFile file, NpgsqlDataSource dataSource) =>
{
    using var reader = new StreamReader(file.OpenReadStream());

    var header = await reader.ReadLineAsync();
    if (header != "cpf,birthDate,zipCode")
    {
        return Results.BadRequest(new { message = "The CSV header must be: cpf,birthDate,zipCode." });
    }

    await using var connection = await dataSource.OpenConnectionAsync();
    await using var transaction = await connection.BeginTransactionAsync();

    var importedCount = 0;
    while (await reader.ReadLineAsync() is { } line)
    {
        if (string.IsNullOrWhiteSpace(line))
        {
            continue;
        }

        var columns = line.Split(',');
        if (columns.Length != 3)
        {
            return Results.BadRequest(new { message = $"Invalid CSV record at line {importedCount + 2}." });
        }

        if (!DateOnly.TryParseExact(
                columns[1],
                "yyyy-MM-dd",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out var birthDate))
        {
            return Results.BadRequest(new { message = $"Invalid birthDate at line {importedCount + 2}." });
        }

        var person = new Person(columns[0], birthDate, columns[2]);

        await using var command = new NpgsqlCommand(
            """
            INSERT INTO person (id, cpf, birth_date, zip_code)
            VALUES ($1, $2, $3, $4)
            """,
            connection,
            transaction);
        command.Parameters.AddWithValue(person.Id);
        command.Parameters.AddWithValue(person.Cpf);
        command.Parameters.AddWithValue(person.BirthDate);
        command.Parameters.AddWithValue(person.ZipCode);

        await command.ExecuteNonQueryAsync();
        importedCount++;
    }

    await transaction.CommitAsync();

    return Results.Ok(new
    {
        message = "Import completed.",
        importedRecords = importedCount
    });
})
.DisableAntiforgery()
.WithName("ImportPeople");

app.Run();
