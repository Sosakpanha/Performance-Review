# Research: Team Daily Goal Tracker with Mood Sync

**Date**: 2025-11-30
**Feature**: 001-team-goal-tracker

## Technology Decisions

### Backend Stack

**Decision**: ASP.NET Core Web API with .NET 8

**Rationale**: Mandated by constitution. Provides robust REST API capabilities with minimal configuration. Built-in dependency injection supports the required Repository → Service → Controller pattern.

**Alternatives Considered**:
- None - Stack is mandated by constitution

---

### Data Access Layer

**Decision**: Dapper with explicit SQL

**Rationale**: Mandated by constitution. Dapper provides lightweight data access without ORM overhead. Explicit SQL ensures full control over queries and aligns with the "no query builders" requirement.

**Implementation Pattern**:
```csharp
// Repository pattern with Dapper
public async Task<IEnumerable<Member>> GetAllAsync()
{
    using var connection = new SqliteConnection(_connectionString);
    return await connection.QueryAsync<Member>(
        "SELECT Id, Name, Mood FROM Members");
}
```

**Alternatives Considered**:
- Entity Framework - Explicitly prohibited by constitution

---

### Database

**Decision**: SQLite with local file storage

**Rationale**: Mandated by constitution. Ideal for local-only development with no external dependencies. Single file database simplifies setup and distribution.

**Configuration**:
- Database file: `teamgoals.db` in application root
- Connection string: `Data Source=teamgoals.db`
- Schema initialization on startup

**Alternatives Considered**:
- None - Database is mandated by constitution

---

### Frontend Framework

**Decision**: Vue 3 with TypeScript and Composition API

**Rationale**: Mandated by constitution. Composition API provides better TypeScript integration and code organization. Single-file components keep related logic together.

**Implementation Pattern**:
```typescript
// Composition API pattern
const members = ref<Member[]>([])
const loading = ref(false)

async function fetchMembers() {
  loading.value = true
  members.value = await api.getMembers()
  loading.value = false
}
```

**Alternatives Considered**:
- Options API - Explicitly prohibited by constitution

---

### Styling

**Decision**: Tailwind CSS with DaisyUI components

**Rationale**: Mandated by constitution. DaisyUI provides pre-built component classes that integrate seamlessly with Tailwind. Reduces custom CSS while maintaining consistency.

**Key Components to Use**:
- `card` - Team member cards
- `checkbox` - Goal completion toggles
- `btn` - Form buttons
- `select` - Member dropdowns
- `input` - Goal text input
- `stats` - Statistics panel

**Alternatives Considered**:
- Other UI libraries - Prohibited by constitution

---

## Architecture Decisions

### API Design

**Decision**: RESTful API with resource-based endpoints

**Rationale**: Mandated by constitution. REST provides predictable, stateless API design. Resource-based URLs align with CRUD operations on entities.

**Endpoints**:
| Method | Endpoint | Purpose |
|--------|----------|---------|
| GET | /api/members | List all members with goals |
| GET | /api/members/{id} | Get single member with goals |
| PUT | /api/members/{id}/mood | Update member mood |
| GET | /api/goals | List all goals |
| POST | /api/goals | Create new goal |
| PUT | /api/goals/{id}/toggle | Toggle goal completion |
| DELETE | /api/goals/{id} | Delete goal |
| GET | /api/stats | Get team statistics |

---

### DTO Separation

**Decision**: Separate DTOs from database models

**Rationale**: Mandated by constitution. Prevents database schema from leaking to API consumers. Allows independent evolution of API contracts and database structure.

**Model vs DTO Example**:
```csharp
// Database Model (internal)
public class Member
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Mood { get; set; }
}

// API DTO (external)
public class MemberDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Mood { get; set; }
    public string MoodEmoji { get; set; }
    public List<GoalDto> Goals { get; set; }
    public int CompletedCount { get; set; }
    public int TotalCount { get; set; }
}
```

---

### Mood Representation

**Decision**: Store mood as string identifier, map to emoji in DTO layer

**Rationale**: Simplifies database storage while providing rich display data. Allows easy extension of mood options without schema changes.

**Mood Mapping**:
| Identifier | Emoji | Label |
|------------|-------|-------|
| happy | 😊 | Happy |
| neutral | 😐 | Neutral |
| sad | 😢 | Sad |
| stressed | 😰 | Stressed |
| excited | 🎉 | Excited |

---

### Single-Day Scope

**Decision**: No date field on goals; data represents "today" only

**Rationale**: Per spec requirement FR-008, system tracks goals for current day only. Simplifies data model by avoiding date-based filtering.

**Implementation**:
- Goals table has no date column
- Data can be manually cleared for new day (out of scope for MVP)
- Pre-seeded data represents today's state

---

## Frontend Architecture

### Component Structure

**Decision**: Small, focused components with single responsibility

**Rationale**: Mandated by constitution. Each component handles one concern for maintainability and testability.

**Component Breakdown**:
- `Dashboard.vue` - Page layout, fetches data, orchestrates children
- `MemberCard.vue` - Displays single member with goals
- `GoalItem.vue` - Single goal with checkbox and delete
- `AddGoalForm.vue` - Form to add new goal
- `UpdateMoodForm.vue` - Form to update member mood
- `StatsPanel.vue` - Shows completion % and mood counts

---

### State Management

**Decision**: Component-local state with prop drilling

**Rationale**: Simplest approach for MVP scope. No need for Vuex/Pinia given small component tree and straightforward data flow. Dashboard holds state, passes to children via props.

**Data Flow**:
```
Dashboard (state holder)
├── StatsPanel (receives stats)
├── AddGoalForm (emits goal-added)
├── UpdateMoodForm (emits mood-updated)
└── MemberCard[] (receives member)
    └── GoalItem[] (emits toggle, delete)
```

---

### API Service Layer

**Decision**: Single api.ts service module with typed functions

**Rationale**: Centralizes API calls, provides type safety, makes testing easier.

**Pattern**:
```typescript
export const api = {
  async getMembers(): Promise<MemberDto[]> { ... },
  async createGoal(request: CreateGoalRequest): Promise<GoalDto> { ... },
  async toggleGoal(id: number): Promise<void> { ... },
  async deleteGoal(id: number): Promise<void> { ... },
  async updateMood(memberId: number, mood: string): Promise<void> { ... },
  async getStats(): Promise<TeamStatsDto> { ... }
}
```

---

## Database Schema

**Decision**: Two tables - Members and Goals

**Rationale**: Minimal schema that supports all functional requirements. Foreign key ensures referential integrity.

**Schema**:
```sql
CREATE TABLE Members (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Name TEXT NOT NULL,
    Mood TEXT NOT NULL DEFAULT 'neutral'
);

CREATE TABLE Goals (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    MemberId INTEGER NOT NULL,
    Description TEXT NOT NULL,
    IsCompleted INTEGER NOT NULL DEFAULT 0,
    FOREIGN KEY (MemberId) REFERENCES Members(Id) ON DELETE CASCADE
);
```

---

## Seed Data

**Decision**: Pre-seed 5 team members on database initialization

**Rationale**: Per FR-015, system must provide pre-seeded data for demonstration. Five members provides good variety without overwhelming the UI.

**Seed Members**:
1. Alice - happy
2. Bob - neutral
3. Carol - excited
4. David - stressed
5. Eve - neutral

---

## CORS Configuration

**Decision**: Enable CORS for localhost frontend origin

**Rationale**: Frontend (Vite dev server) runs on different port than backend. CORS required for cross-origin API calls during development.

**Configuration**:
```csharp
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});
```

---

## Summary

All technical decisions align with constitution requirements. No clarifications needed - the stack and patterns are fully specified by project governance. Ready to proceed to Phase 1 design.
