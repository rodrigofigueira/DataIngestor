namespace DataIngestor.Domain;

public class Person
{
    public Guid Id { get; private set; }
    public string Cpf { get; private set; }
    public DateOnly BirthDate { get; private set; }
    public string ZipCode { get; private set; }

    public Person(
        string cpf,
        DateOnly birthDate,
        string zipCode)
    {
        Id = Guid.NewGuid();
        Cpf = cpf;
        BirthDate = birthDate;
        ZipCode = zipCode;
    }
}