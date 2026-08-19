SELECT 'CREATE DATABASE data_ingestor'
WHERE NOT EXISTS (
    SELECT
        FROM pg_database
        WHERE datname = 'data_ingestor'
)
\gexec

\connect data_ingestor

CREATE TABLE IF NOT EXISTS person
(
    id UUID PRIMARY KEY,
    cpf VARCHAR(11) NOT NULL,
    birth_date DATE NOT NULL,
    zip_code VARCHAR(8) NOT NULL
);