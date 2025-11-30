# Team Daily Goal Tracker Development Guidelines

Auto-generated from feature plans. Last updated: 2025-11-30

## Active Technologies

**Backend**:
- Language: C# / .NET 8
- Framework: ASP.NET Core Web API
- Data Access: Dapper (micro-ORM)
- Database: SQLite (local file)

**Frontend**:
- Language: TypeScript
- Framework: Vue 3
- Styling: Tailwind CSS + DaisyUI
- API Pattern: Composition API only

## Project Structure

```text
backend/
├── TeamGoalTracker.Api/
│   ├── Controllers/         # Thin controllers, delegate to services
│   ├── Services/            # Business logic layer
│   ├── Repositories/        # Data access with Dapper + explicit SQL
│   ├── Models/              # Database entities (internal)
│   ├── DTOs/                # API contracts (external)
│   └── Data/                # Database initialization

frontend/
├── src/
│   ├── components/          # Small, focused Vue components
│   ├── views/               # Page-level components
│   ├── services/            # API client
│   └── types/               # TypeScript interfaces
```

## Commands

### Backend

```bash
# Navigate to backend
cd backend/TeamGoalTracker.Api

# Restore dependencies
dotnet restore

# Run API server (http://localhost:5000)
dotnet run

# Build
dotnet build
```

### Frontend

```bash
# Navigate to frontend
cd frontend

# Install dependencies
npm install

# Run dev server (http://localhost:5173)
npm run dev

# Build for production
npm run build

# Type check
npm run type-check
```

## Code Style

### C# (.NET 8)

- Use Repository → Service → Controller pattern
- Explicit SQL only (no LINQ-to-SQL, no query builders)
- DTOs separate from database models
- Controllers must be thin - delegate to services immediately
- Use dependency injection for all services

### TypeScript/Vue 3

- Composition API only (Options API prohibited)
- Small, single-responsibility components
- Props down, events up
- Use TypeScript interfaces for all API types

## Constitution Principles

1. **Scope Discipline**: MVP only, no out-of-scope features
2. **Backend Architecture**: .NET 8, Dapper, SQLite, Repository+Service
3. **Frontend Architecture**: Vue 3 + TS, Composition API, Tailwind + DaisyUI
4. **API & Data Boundaries**: REST only, DTOs separate from DB models
5. **Quality Standards**: Follow spec exactly, clarity over cleverness

## Recent Changes

- **001-team-goal-tracker**: Initial feature - team daily goal tracker with mood sync

<!-- MANUAL ADDITIONS START -->
<!-- MANUAL ADDITIONS END -->
