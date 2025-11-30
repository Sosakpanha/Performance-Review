# Implementation Plan: Team Daily Goal Tracker with Mood Sync

**Branch**: `001-team-goal-tracker` | **Date**: 2025-11-30 | **Spec**: [spec.md](./spec.md)
**Input**: Feature specification from `/specs/001-team-goal-tracker/spec.md`

## Summary

Build a minimal full-stack web application that allows small teams to track daily goals and team morale in one place. The application displays team member cards with goals, completion status, and mood indicators. Users can add/delete goals, toggle completion, update moods, and view aggregate statistics (completion percentage and mood summary). Single-day scope with pre-seeded team members, no authentication.

## Technical Context

**Language/Version**: Backend: C# / .NET 8 | Frontend: TypeScript
**Primary Dependencies**: Backend: ASP.NET Core Web API, Dapper | Frontend: Vue 3, Tailwind CSS, DaisyUI
**Storage**: SQLite (local file)
**Testing**: Backend: xUnit (if needed) | Frontend: Vitest (if needed)
**Target Platform**: Desktop web browser (local development)
**Project Type**: Web application (frontend + backend)
**Performance Goals**: Dashboard load <2s, goal operations <1s, toggle <500ms
**Constraints**: Desktop-only, no auth, single-day data, no external services
**Scale/Scope**: 5-10 team members, local single-user access

## Constitution Check

*GATE: Must pass before Phase 0 research. Re-check after Phase 1 design.*

| Principle | Gate | Status |
|-----------|------|--------|
| I. Scope Discipline | MVP only, no out-of-scope features | PASS - Only implementing spec requirements |
| II. Backend Architecture | .NET 8, Dapper, SQLite, Repository+Service pattern | PASS - Will use prescribed stack |
| III. Frontend Architecture | Vue 3 + TS, Composition API, Tailwind + DaisyUI | PASS - Will use prescribed stack |
| IV. API & Data Boundaries | REST only, DTOs separate from DB models | PASS - Will implement REST API with DTOs |
| V. Quality Standards | Follow spec exactly, clarity over cleverness | PASS - Implementing per spec |

**Gate Result**: ALL GATES PASS - Proceed to Phase 0

## Project Structure

### Documentation (this feature)

```text
specs/001-team-goal-tracker/
├── plan.md              # This file
├── spec.md              # Feature specification
├── research.md          # Phase 0 output
├── data-model.md        # Phase 1 output
├── quickstart.md        # Phase 1 output
├── contracts/           # Phase 1 output (OpenAPI spec)
└── tasks.md             # Phase 2 output (/speckit.tasks command)
```

### Source Code (repository root)

```text
backend/
├── TeamGoalTracker.Api/
│   ├── Controllers/
│   │   ├── MembersController.cs
│   │   └── GoalsController.cs
│   ├── Services/
│   │   ├── IMemberService.cs
│   │   ├── MemberService.cs
│   │   ├── IGoalService.cs
│   │   └── GoalService.cs
│   ├── Repositories/
│   │   ├── IMemberRepository.cs
│   │   ├── MemberRepository.cs
│   │   ├── IGoalRepository.cs
│   │   └── GoalRepository.cs
│   ├── Models/
│   │   ├── Member.cs
│   │   └── Goal.cs
│   ├── DTOs/
│   │   ├── MemberDto.cs
│   │   ├── GoalDto.cs
│   │   ├── CreateGoalRequest.cs
│   │   ├── UpdateMoodRequest.cs
│   │   └── TeamStatsDto.cs
│   ├── Data/
│   │   └── DatabaseInitializer.cs
│   ├── Program.cs
│   └── appsettings.json

frontend/
├── src/
│   ├── components/
│   │   ├── MemberCard.vue
│   │   ├── GoalItem.vue
│   │   ├── AddGoalForm.vue
│   │   ├── UpdateMoodForm.vue
│   │   └── StatsPanel.vue
│   ├── views/
│   │   └── Dashboard.vue
│   ├── services/
│   │   └── api.ts
│   ├── types/
│   │   └── index.ts
│   ├── App.vue
│   └── main.ts
├── index.html
├── package.json
├── vite.config.ts
├── tailwind.config.js
└── tsconfig.json
```

**Structure Decision**: Web application structure with separate `backend/` and `frontend/` directories. Backend follows Repository → Service → Controller pattern per constitution. Frontend uses Vue 3 component-based architecture with Composition API.

## Complexity Tracking

> No constitution violations requiring justification. Design follows all principles.

| Violation | Why Needed | Simpler Alternative Rejected Because |
|-----------|------------|-------------------------------------|
| None | N/A | N/A |
