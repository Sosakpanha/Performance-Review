<!--
Sync Impact Report
==================
Version change: 0.0.0 → 1.0.0 (initial ratification)
Modified principles: N/A (new constitution)
Added sections:
  - Core Principles (5 principles)
  - Technical Stack Requirements
  - API & Data Guidelines
  - Governance
Removed sections: None
Templates requiring updates:
  - .specify/templates/plan-template.md ✅ (no changes needed - generic template)
  - .specify/templates/spec-template.md ✅ (no changes needed - generic template)
  - .specify/templates/tasks-template.md ✅ (no changes needed - generic template)
Follow-up TODOs: None
-->

# Team Daily Goal Tracker Constitution

## Core Principles

### I. Scope Discipline

The project MUST adhere to MVP-only development boundaries:

- Build MVP features only as explicitly specified
- Do NOT implement anything listed as "Out of Scope" in specifications
- Desktop-only UI - no mobile or responsive design work
- No authentication system
- No multi-day history or persistence beyond current day
- No analytics or reporting features

**Rationale**: Prevents scope creep and ensures focused delivery of core functionality.

### II. Backend Architecture

The backend MUST follow these non-negotiable technical constraints:

- Stack: .NET 8 Web API exclusively
- Data access: Dapper only - Entity Framework is prohibited
- Database: SQLite with local file storage
- Repository pattern MUST be used for data access
- Service layer MUST handle business logic
- Controllers MUST be thin - delegate to services immediately
- SQL MUST be explicit and handwritten - no query builders or ORMs
- Avoid over-engineering - simplest solution that meets requirements

**Rationale**: Ensures consistent, maintainable backend with clear separation of concerns and predictable data access patterns.

### III. Frontend Architecture

The frontend MUST follow these non-negotiable technical constraints:

- Stack: Vue 3 with TypeScript only
- Composition API exclusively - Options API is prohibited
- Styling: Tailwind CSS with DaisyUI components
- Components MUST be small and focused on single responsibility
- No external UI component libraries beyond DaisyUI
- Keep component logic readable and straightforward

**Rationale**: Ensures consistent, modern frontend patterns with minimal dependencies and predictable component behavior.

### IV. API & Data Boundaries

All API and data handling MUST follow these rules:

- REST endpoints only - no GraphQL, WebSocket, or other protocols
- Clear DTO (Data Transfer Object) boundaries between layers
- Database models MUST NOT be exposed to API consumers
- DTOs MUST be separate from database entities
- Use simple, descriptive naming conventions
- API responses MUST be consistent and predictable

**Rationale**: Maintains clean separation between internal data structures and external contracts, enabling independent evolution.

### V. Quality Standards

All development work MUST adhere to these quality rules:

- Follow the specification exactly as written
- Ask clarifying questions when requirements are unclear - do not assume
- Prefer clarity over cleverness in all code
- Code MUST be readable by developers unfamiliar with the codebase
- Avoid premature optimization or abstraction
- Each change MUST be traceable to a specification requirement

**Rationale**: Ensures predictable, maintainable code that matches stakeholder expectations.

## Technical Stack Requirements

**Backend**:
- Runtime: .NET 8
- Framework: ASP.NET Core Web API
- Data Access: Dapper (micro-ORM)
- Database: SQLite (local file)

**Frontend**:
- Framework: Vue 3
- Language: TypeScript
- Styling: Tailwind CSS + DaisyUI
- API Pattern: Composition API only

**Environment**:
- Local development only
- No containerization required for MVP
- No CI/CD pipeline required for MVP

## API & Data Guidelines

### Endpoint Conventions

- Use RESTful resource naming (nouns, not verbs)
- Use appropriate HTTP methods (GET, POST, PUT, DELETE)
- Return consistent response structures
- Use appropriate HTTP status codes

### Data Layer Rules

- Repository classes handle all database operations
- Service classes contain business logic
- Controllers only handle HTTP concerns
- DTOs transfer data between layers
- No direct database model exposure to API

## Governance

This constitution supersedes all other development practices for this project:

- All code changes MUST verify compliance with these principles
- Complexity beyond MVP scope MUST be explicitly justified and approved
- Amendments to this constitution require documentation and explicit approval
- Version updates follow semantic versioning:
  - MAJOR: Principle removals or incompatible redefinitions
  - MINOR: New principles or material expansions
  - PATCH: Clarifications and non-semantic refinements

**Compliance Review**: Before any feature implementation begins, verify alignment with all five core principles.

**Version**: 1.0.0 | **Ratified**: 2025-11-30 | **Last Amended**: 2025-11-30
