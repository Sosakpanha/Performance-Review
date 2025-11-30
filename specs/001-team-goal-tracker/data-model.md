# Data Model: Team Daily Goal Tracker with Mood Sync

**Date**: 2025-11-30
**Feature**: 001-team-goal-tracker

## Entity Relationship Diagram

```
┌─────────────────┐       ┌─────────────────┐
│     Member      │       │      Goal       │
├─────────────────┤       ├─────────────────┤
│ Id (PK)         │──────<│ Id (PK)         │
│ Name            │       │ MemberId (FK)   │
│ Mood            │       │ Description     │
└─────────────────┘       │ IsCompleted     │
                          └─────────────────┘
```

**Relationship**: One Member has many Goals (1:N)

---

## Entities

### Member

Represents a team member who can have goals and a current mood.

| Field | Type | Constraints | Description |
|-------|------|-------------|-------------|
| Id | INTEGER | PRIMARY KEY, AUTOINCREMENT | Unique identifier |
| Name | TEXT | NOT NULL | Display name of team member |
| Mood | TEXT | NOT NULL, DEFAULT 'neutral' | Current mood identifier |

**Valid Mood Values**: `happy`, `neutral`, `sad`, `stressed`, `excited`

**Validation Rules**:
- Name must be non-empty
- Mood must be one of the valid mood values

**Lifecycle**:
- Created at database initialization (seed data)
- Updated when mood changes
- Never deleted (member management out of scope)

---

### Goal

Represents a daily task assigned to a team member.

| Field | Type | Constraints | Description |
|-------|------|-------------|-------------|
| Id | INTEGER | PRIMARY KEY, AUTOINCREMENT | Unique identifier |
| MemberId | INTEGER | NOT NULL, FOREIGN KEY → Member(Id) | Owning team member |
| Description | TEXT | NOT NULL | Goal text content |
| IsCompleted | INTEGER | NOT NULL, DEFAULT 0 | Completion status (0=false, 1=true) |

**Validation Rules**:
- Description must be non-empty
- MemberId must reference existing Member
- IsCompleted must be 0 or 1

**Lifecycle**:
- Created when user adds a goal
- Updated when completion status toggled
- Deleted when user removes goal

---

## Database Schema (SQLite)

```sql
-- Members table
CREATE TABLE IF NOT EXISTS Members (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Name TEXT NOT NULL,
    Mood TEXT NOT NULL DEFAULT 'neutral'
);

-- Goals table
CREATE TABLE IF NOT EXISTS Goals (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    MemberId INTEGER NOT NULL,
    Description TEXT NOT NULL,
    IsCompleted INTEGER NOT NULL DEFAULT 0,
    FOREIGN KEY (MemberId) REFERENCES Members(Id) ON DELETE CASCADE
);

-- Index for faster goal lookups by member
CREATE INDEX IF NOT EXISTS idx_goals_member ON Goals(MemberId);
```

---

## Seed Data

Initial team members inserted on first startup:

```sql
INSERT INTO Members (Name, Mood) VALUES
    ('Alice', 'happy'),
    ('Bob', 'neutral'),
    ('Carol', 'excited'),
    ('David', 'stressed'),
    ('Eve', 'neutral');
```

---

## DTOs (Data Transfer Objects)

### MemberDto

Response DTO for member with embedded goals.

```typescript
interface MemberDto {
  id: number
  name: string
  mood: string         // identifier: 'happy', 'neutral', etc.
  moodEmoji: string    // display emoji: '😊', '😐', etc.
  goals: GoalDto[]
  completedCount: number
  totalCount: number
}
```

### GoalDto

Response DTO for a single goal.

```typescript
interface GoalDto {
  id: number
  memberId: number
  description: string
  isCompleted: boolean
}
```

### CreateGoalRequest

Request DTO for creating a new goal.

```typescript
interface CreateGoalRequest {
  memberId: number
  description: string
}
```

**Validation**:
- memberId: Required, must exist
- description: Required, non-empty, max 500 characters

### UpdateMoodRequest

Request DTO for updating member mood.

```typescript
interface UpdateMoodRequest {
  mood: string
}
```

**Validation**:
- mood: Required, must be valid mood identifier

### TeamStatsDto

Response DTO for aggregate team statistics.

```typescript
interface TeamStatsDto {
  totalGoals: number
  completedGoals: number
  completionPercentage: number
  moodCounts: MoodCountDto[]
}

interface MoodCountDto {
  mood: string
  emoji: string
  count: number
}
```

---

## Mood Mapping Reference

| Identifier | Emoji | Label |
|------------|-------|-------|
| happy | 😊 | Happy |
| neutral | 😐 | Neutral |
| sad | 😢 | Sad |
| stressed | 😰 | Stressed |
| excited | 🎉 | Excited |

---

## Query Patterns

### Get All Members with Goals

```sql
-- Step 1: Get all members
SELECT Id, Name, Mood FROM Members ORDER BY Name;

-- Step 2: Get all goals (or filter by member)
SELECT Id, MemberId, Description, IsCompleted
FROM Goals
ORDER BY MemberId, Id;
```

### Get Team Statistics

```sql
-- Total and completed goals
SELECT
    COUNT(*) as TotalGoals,
    SUM(IsCompleted) as CompletedGoals
FROM Goals;

-- Mood distribution
SELECT
    Mood,
    COUNT(*) as Count
FROM Members
GROUP BY Mood;
```

### Add Goal

```sql
INSERT INTO Goals (MemberId, Description, IsCompleted)
VALUES (@MemberId, @Description, 0);
```

### Toggle Goal

```sql
UPDATE Goals
SET IsCompleted = CASE WHEN IsCompleted = 0 THEN 1 ELSE 0 END
WHERE Id = @Id;
```

### Delete Goal

```sql
DELETE FROM Goals WHERE Id = @Id;
```

### Update Mood

```sql
UPDATE Members SET Mood = @Mood WHERE Id = @Id;
```
