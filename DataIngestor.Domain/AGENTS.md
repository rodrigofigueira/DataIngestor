# DataIngestor - Agent Instructions

## Objective

This project is intentionally being developed in small and naive iterations for architectural learning.

Implement only what is explicitly requested.

## Rules

* Do not anticipate future architectural needs.
* Do not introduce patterns, abstractions, libraries, services, or infrastructure unless explicitly requested.
* Do not optimize for scalability, performance, resiliency, observability, or distributed processing unless explicitly requested.
* Prefer the simplest implementation that satisfies the current requirement.
* Do not introduce CQRS, MediatR, messaging, queues, background workers, caching, repositories, unit of work, domain events, value objects, mapping libraries, or generic abstractions unless explicitly requested.
* Do not refactor unrelated code.
* Do not redesign the current architecture.
* Do not add functionality beyond the requested scope.
* Do not create speculative extension points for future requirements.
* Keep the implementation easy to read and easy to intentionally stress or break later.

## Current Architecture

The current solution contains:

* DataIngestor.Api
* DataIngestor.Domain
* DataIngestor.Infrastructure
* PostgreSQL running through Docker Compose

The current domain contains a simple `Person` entity.

## Development Principle

The architecture must evolve only after a concrete limitation or failure is demonstrated.

If you identify a possible future improvement, do not implement it. Mention it only if necessary to explain a limitation in the requested implementation.
