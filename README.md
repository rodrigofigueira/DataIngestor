# DataIngestor

## v0.1.0 — Synchronous CSV Ingestion Baseline

This is the first functional version of **DataIngestor**, a learning project focused on understanding how software architecture evolves in response to real limitations.

Rather than starting with a complex or production-ready architecture, the project deliberately begins with the simplest solution that satisfies the initial requirement.

The goal is to progressively **stress, break, observe, understand, and evolve** the application.

---

## Initial Goal

The first version solves a deliberately simple problem:

> Receive person data from a CSV file and persist it to a relational database.

The CSV currently contains:

* CPF
* Birth date
* ZIP code

Example:

```csv
cpf,birthDate,zipCode
12345678901,1985-04-21,01310100
98765432100,1990-08-15,30140071
```

---

## Current Architecture

The entire ingestion process happens synchronously during the HTTP request.

```text
CSV File
   ↓
HTTP Multipart Upload
   ↓
.NET API
   ↓
CSV Parsing
   ↓
Person Creation
   ↓
PostgreSQL
```

The client sends the CSV file to the API.

The API reads the file, creates the corresponding `Person` entities and persists each record directly to PostgreSQL before completing the request.

---

## Technology Stack

* .NET 10
* ASP.NET Core Minimal API
* PostgreSQL 17
* Npgsql
* Docker
* Docker Compose
* Ubuntu 24.04 / WSL 2

---

## Infrastructure

PostgreSQL runs locally inside a Docker container.

Docker Compose is responsible for configuring the database container, port mapping, persistent storage and initial database setup.

The application itself currently runs directly in the Linux development environment provided by WSL 2.

---

## Intentional Limitations

This version intentionally does **not** attempt to solve problems that have not yet been observed.

There is currently no:

* Background processing
* Messaging or queues
* Worker processes
* Horizontal scaling strategy
* Retry mechanism
* Dead Letter Queue
* Caching
* Distributed processing
* Observability stack
* Performance optimization
* Large-file optimization

These are not missing features of `v0.1.0`.

They are deliberately excluded from the baseline.

---

## Learning Strategy

Architectural changes in this project should be driven by demonstrated problems rather than anticipated requirements.

The development cycle will follow this approach:

```text
Build
  ↓
Stress
  ↓
Break
  ↓
Observe
  ↓
Understand
  ↓
Change the architecture
  ↓
Measure again
```

Future versions will introduce new architectural concepts only when the current implementation provides a concrete reason for them to exist.

Examples of future experiments may include larger files, concurrent uploads, constrained CPU and memory, failures during processing and increasing workloads.

The solution to those problems is intentionally **not defined yet**.

---

## Purpose of v0.1.0

`v0.1.0` establishes the experimental baseline.

It represents the simplest functional architecture against which future versions can be compared.

As the application is intentionally stressed and its limitations become measurable, each significant architectural evolution will be documented in subsequent releases together with:

* The problem observed
* How the problem was reproduced
* Measurements and evidence
* Alternatives considered
* The architectural decision
* The resulting impact

The objective is not simply to build a data ingestion platform.

The objective is to understand **why its architecture needs to evolve**.
