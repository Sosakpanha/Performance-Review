# Tasks: Team Daily Goal Tracker with Mood Sync

**Input**: Design documents from `/specs/001-team-goal-tracker/`
**Prerequisites**: plan.md (required), spec.md (required), data-model.md, contracts/openapi.yaml

**Tests**: Tests are NOT included (not explicitly requested in feature specification).

**Organization**: Tasks are grouped by user story to enable independent implementation and testing of each story.

## Format: `[ID] [P?] [Story?] Description`

- **[P]**: Can run in parallel (different files, no dependencies)
- **[Story]**: Which user story this task belongs to (US1, US2, etc.)
- Include exact file paths in descriptions

---

## Phase 1: Setup

**Purpose**: Project initialization and basic structure

- [x] T001 Create backend project structure with `dotnet new webapi -n TeamGoalTracker.Api -o backend/TeamGoalTracker.Api`
- [x] T002 Add Dapper and SQLite NuGet packages to `backend/TeamGoalTracker.Api/TeamGoalTracker.Api.csproj`
- [x] T003 [P] Create frontend project with `npm create vite@latest frontend -- --template vue-ts`
- [x] T004 [P] Install frontend dependencies: `cd frontend && npm install && npm install -D tailwindcss postcss autoprefixer daisyui`
- [x] T005 Configure Tailwind CSS in `frontend/tailwind.config.js` with DaisyUI plugin
- [x] T006 Configure PostCSS in `frontend/postcss.config.js`
- [x] T007 Add Tailwind directives to `frontend/src/style.css`
- [x] T008 Configure Vite proxy for API in `frontend/vite.config.ts`

---

## Phase 2: Foundational (Blocking Prerequisites)

**Purpose**: Core infrastructure that MUST be complete before ANY user story can be implemented

**CRITICAL**: No user story work can begin until this phase is complete

- [x] T009 Create Member database model in `backend/TeamGoalTracker.Api/Models/Member.cs`
- [x] T010 [P] Create Goal database model in `backend/TeamGoalTracker.Api/Models/Goal.cs`
- [x] T011 Create DatabaseInitializer with schema and seed data in `backend/TeamGoalTracker.Api/Data/DatabaseInitializer.cs`
- [x] T012 Configure SQLite connection string in `backend/TeamGoalTracker.Api/appsettings.json`
- [x] T013 Register DatabaseInitializer in `backend/TeamGoalTracker.Api/Program.cs` and configure CORS
- [x] T014 [P] Create MemberDto in `backend/TeamGoalTracker.Api/DTOs/MemberDto.cs`
- [x] T015 [P] Create GoalDto in `backend/TeamGoalTracker.Api/DTOs/GoalDto.cs`
- [x] T016 [P] Create TypeScript types in `frontend/src/types/index.ts` matching API DTOs
- [x] T017 Create API service module in `frontend/src/services/api.ts` with base fetch configuration

**Checkpoint**: Foundation ready - user story implementation can now begin

---

## Phase 3: User Story 1 - View Team Dashboard (Priority: P1)

**Goal**: Display all team members with their goals and moods on a dashboard

**Independent Test**: Load dashboard and verify all 5 seeded team members appear with names, mood emojis, and goal lists

### Implementation for User Story 1

- [ ] T018 [US1] Create IMemberRepository interface in `backend/TeamGoalTracker.Api/Repositories/IMemberRepository.cs`
- [ ] T019 [US1] Implement MemberRepository with GetAllAsync in `backend/TeamGoalTracker.Api/Repositories/MemberRepository.cs`
- [ ] T020 [P] [US1] Create IGoalRepository interface in `backend/TeamGoalTracker.Api/Repositories/IGoalRepository.cs`
- [ ] T021 [P] [US1] Implement GoalRepository with GetByMemberIdAsync in `backend/TeamGoalTracker.Api/Repositories/GoalRepository.cs`
- [ ] T022 [US1] Create IMemberService interface in `backend/TeamGoalTracker.Api/Services/IMemberService.cs`
- [ ] T023 [US1] Implement MemberService with GetAllMembersWithGoalsAsync in `backend/TeamGoalTracker.Api/Services/MemberService.cs`
- [ ] T024 [US1] Create MembersController with GET /api/members endpoint in `backend/TeamGoalTracker.Api/Controllers/MembersController.cs`
- [ ] T025 [US1] Register repositories and services in DI container in `backend/TeamGoalTracker.Api/Program.cs`
- [ ] T026 [US1] Add getMembers function to `frontend/src/services/api.ts`
- [ ] T027 [US1] Create GoalItem component in `frontend/src/components/GoalItem.vue`
- [ ] T028 [US1] Create MemberCard component in `frontend/src/components/MemberCard.vue`
- [ ] T029 [US1] Create Dashboard view in `frontend/src/views/Dashboard.vue`
- [ ] T030 [US1] Update App.vue to render Dashboard in `frontend/src/App.vue`

**Checkpoint**: User Story 1 complete - Dashboard displays all team members with goals and moods

---

## Phase 4: User Story 2 - Add Goals to Team Members (Priority: P2)

**Goal**: Allow users to add new goals to any team member via form

**Independent Test**: Select member from dropdown, enter goal text, submit, and verify goal appears on that member's card

### Implementation for User Story 2

- [ ] T031 [US2] Create CreateGoalRequest DTO in `backend/TeamGoalTracker.Api/DTOs/CreateGoalRequest.cs`
- [ ] T032 [US2] Add CreateAsync method to IGoalRepository in `backend/TeamGoalTracker.Api/Repositories/IGoalRepository.cs`
- [ ] T033 [US2] Implement CreateAsync in GoalRepository in `backend/TeamGoalTracker.Api/Repositories/GoalRepository.cs`
- [ ] T034 [US2] Create IGoalService interface in `backend/TeamGoalTracker.Api/Services/IGoalService.cs`
- [ ] T035 [US2] Implement GoalService with CreateGoalAsync in `backend/TeamGoalTracker.Api/Services/GoalService.cs`
- [ ] T036 [US2] Create GoalsController with POST /api/goals endpoint in `backend/TeamGoalTracker.Api/Controllers/GoalsController.cs`
- [ ] T037 [US2] Register GoalService in DI container in `backend/TeamGoalTracker.Api/Program.cs`
- [ ] T038 [US2] Add createGoal function to `frontend/src/services/api.ts`
- [ ] T039 [US2] Create AddGoalForm component in `frontend/src/components/AddGoalForm.vue`
- [ ] T040 [US2] Integrate AddGoalForm into Dashboard in `frontend/src/views/Dashboard.vue`

**Checkpoint**: User Story 2 complete - Users can add goals to any team member

---

## Phase 5: User Story 3 - Mark Goals as Completed (Priority: P3)

**Goal**: Allow users to toggle goal completion by clicking checkbox

**Independent Test**: Click checkbox on goal, verify visual state changes and completion count updates

### Implementation for User Story 3

- [ ] T041 [US3] Add ToggleAsync method to IGoalRepository in `backend/TeamGoalTracker.Api/Repositories/IGoalRepository.cs`
- [ ] T042 [US3] Implement ToggleAsync in GoalRepository in `backend/TeamGoalTracker.Api/Repositories/GoalRepository.cs`
- [ ] T043 [US3] Add ToggleGoalAsync to IGoalService in `backend/TeamGoalTracker.Api/Services/IGoalService.cs`
- [ ] T044 [US3] Implement ToggleGoalAsync in GoalService in `backend/TeamGoalTracker.Api/Services/GoalService.cs`
- [ ] T045 [US3] Add PUT /api/goals/{id}/toggle endpoint to GoalsController in `backend/TeamGoalTracker.Api/Controllers/GoalsController.cs`
- [ ] T046 [US3] Add toggleGoal function to `frontend/src/services/api.ts`
- [ ] T047 [US3] Add toggle handler to GoalItem component in `frontend/src/components/GoalItem.vue`
- [ ] T048 [US3] Wire toggle event from MemberCard to Dashboard in `frontend/src/views/Dashboard.vue`

**Checkpoint**: User Story 3 complete - Users can toggle goal completion status

---

## Phase 6: User Story 4 - Update Team Member Mood (Priority: P4)

**Goal**: Allow users to update any team member's mood via emoji selector

**Independent Test**: Select member, choose mood emoji, submit, and verify emoji changes on card

### Implementation for User Story 4

- [ ] T049 [US4] Create UpdateMoodRequest DTO in `backend/TeamGoalTracker.Api/DTOs/UpdateMoodRequest.cs`
- [ ] T050 [US4] Add UpdateMoodAsync method to IMemberRepository in `backend/TeamGoalTracker.Api/Repositories/IMemberRepository.cs`
- [ ] T051 [US4] Implement UpdateMoodAsync in MemberRepository in `backend/TeamGoalTracker.Api/Repositories/MemberRepository.cs`
- [ ] T052 [US4] Add UpdateMoodAsync to IMemberService in `backend/TeamGoalTracker.Api/Services/IMemberService.cs`
- [ ] T053 [US4] Implement UpdateMoodAsync in MemberService in `backend/TeamGoalTracker.Api/Services/MemberService.cs`
- [ ] T054 [US4] Add PUT /api/members/{id}/mood endpoint to MembersController in `backend/TeamGoalTracker.Api/Controllers/MembersController.cs`
- [ ] T055 [US4] Add updateMood function to `frontend/src/services/api.ts`
- [ ] T056 [US4] Create UpdateMoodForm component with emoji selector in `frontend/src/components/UpdateMoodForm.vue`
- [ ] T057 [US4] Integrate UpdateMoodForm into Dashboard in `frontend/src/views/Dashboard.vue`

**Checkpoint**: User Story 4 complete - Users can update team member moods

---

## Phase 7: User Story 5 - View Team Statistics (Priority: P5)

**Goal**: Display completion percentage and mood summary counts

**Independent Test**: View stats panel, verify completion % and mood counts match actual data

### Implementation for User Story 5

- [ ] T058 [US5] Create TeamStatsDto and MoodCountDto in `backend/TeamGoalTracker.Api/DTOs/TeamStatsDto.cs`
- [ ] T059 [US5] Add GetStatsAsync method to IGoalRepository in `backend/TeamGoalTracker.Api/Repositories/IGoalRepository.cs`
- [ ] T060 [US5] Implement GetStatsAsync in GoalRepository in `backend/TeamGoalTracker.Api/Repositories/GoalRepository.cs`
- [ ] T061 [US5] Add GetMoodCountsAsync to IMemberRepository in `backend/TeamGoalTracker.Api/Repositories/IMemberRepository.cs`
- [ ] T062 [US5] Implement GetMoodCountsAsync in MemberRepository in `backend/TeamGoalTracker.Api/Repositories/MemberRepository.cs`
- [ ] T063 [US5] Create IStatsService interface in `backend/TeamGoalTracker.Api/Services/IStatsService.cs`
- [ ] T064 [US5] Implement StatsService with GetTeamStatsAsync in `backend/TeamGoalTracker.Api/Services/StatsService.cs`
- [ ] T065 [US5] Create StatsController with GET /api/stats endpoint in `backend/TeamGoalTracker.Api/Controllers/StatsController.cs`
- [ ] T066 [US5] Register StatsService in DI container in `backend/TeamGoalTracker.Api/Program.cs`
- [ ] T067 [US5] Add getStats function to `frontend/src/services/api.ts`
- [ ] T068 [US5] Create StatsPanel component in `frontend/src/components/StatsPanel.vue`
- [ ] T069 [US5] Integrate StatsPanel into Dashboard in `frontend/src/views/Dashboard.vue`

**Checkpoint**: User Story 5 complete - Statistics panel shows completion % and mood counts

---

## Phase 8: User Story 6 - Delete Goals (Priority: P6)

**Goal**: Allow users to delete goals

**Independent Test**: Click delete on goal, verify it is removed and stats update

### Implementation for User Story 6

- [ ] T070 [US6] Add DeleteAsync method to IGoalRepository in `backend/TeamGoalTracker.Api/Repositories/IGoalRepository.cs`
- [ ] T071 [US6] Implement DeleteAsync in GoalRepository in `backend/TeamGoalTracker.Api/Repositories/GoalRepository.cs`
- [ ] T072 [US6] Add DeleteGoalAsync to IGoalService in `backend/TeamGoalTracker.Api/Services/IGoalService.cs`
- [ ] T073 [US6] Implement DeleteGoalAsync in GoalService in `backend/TeamGoalTracker.Api/Services/GoalService.cs`
- [ ] T074 [US6] Add DELETE /api/goals/{id} endpoint to GoalsController in `backend/TeamGoalTracker.Api/Controllers/GoalsController.cs`
- [ ] T075 [US6] Add deleteGoal function to `frontend/src/services/api.ts`
- [ ] T076 [US6] Add delete button and handler to GoalItem component in `frontend/src/components/GoalItem.vue`
- [ ] T077 [US6] Wire delete event from MemberCard to Dashboard in `frontend/src/views/Dashboard.vue`

**Checkpoint**: User Story 6 complete - Users can delete goals

---

## Phase 9: Polish & Cross-Cutting Concerns

**Purpose**: Final refinements and verification

- [ ] T078 Add loading states to Dashboard in `frontend/src/views/Dashboard.vue`
- [ ] T079 Add empty state handling when no team members in `frontend/src/views/Dashboard.vue`
- [ ] T080 Add form validation error messages to AddGoalForm in `frontend/src/components/AddGoalForm.vue`
- [ ] T081 Style goal text with strikethrough when completed in `frontend/src/components/GoalItem.vue`
- [ ] T082 Verify all API error responses return appropriate status codes in all controllers
- [ ] T083 Run quickstart.md verification checklist

---

## Dependencies & Execution Order

### Phase Dependencies

- **Setup (Phase 1)**: No dependencies - can start immediately
- **Foundational (Phase 2)**: Depends on Setup completion - BLOCKS all user stories
- **User Stories (Phase 3-8)**: All depend on Foundational phase completion
  - US1 (View Dashboard) must complete first - provides base for all other stories
  - US2-US6 can proceed after US1 (they build on dashboard)
- **Polish (Phase 9)**: Depends on all user stories being complete

### User Story Dependencies

- **User Story 1 (P1)**: After Foundational - Core dashboard, required by all other stories
- **User Story 2 (P2)**: After US1 - Needs dashboard to show added goals
- **User Story 3 (P3)**: After US1 - Needs goals displayed to toggle them
- **User Story 4 (P4)**: After US1 - Needs member cards to show mood updates
- **User Story 5 (P5)**: After US1 - Needs dashboard layout for stats panel
- **User Story 6 (P6)**: After US1 - Needs goals displayed to delete them

### Within Each User Story

- Backend: Repository → Service → Controller
- Frontend: API function → Component → Dashboard integration
- Models/DTOs before services

### Parallel Opportunities

**Setup Phase**:
- T003, T004 can run in parallel (frontend setup)

**Foundational Phase**:
- T009, T010 can run in parallel (models)
- T014, T015, T016 can run in parallel (DTOs/types)

**User Story 1**:
- T020, T021 can run in parallel (goal repository)

---

## Implementation Strategy

### MVP First (User Story 1 Only)

1. Complete Phase 1: Setup
2. Complete Phase 2: Foundational
3. Complete Phase 3: User Story 1
4. **STOP and VALIDATE**: Test dashboard displays all members
5. Can demo basic viewing capability

### Incremental Delivery

1. Setup + Foundational → Foundation ready
2. Add User Story 1 → Dashboard works (MVP!)
3. Add User Story 2 → Can add goals
4. Add User Story 3 → Can complete goals
5. Add User Story 4 → Can update moods
6. Add User Story 5 → Can see statistics
7. Add User Story 6 → Can delete goals
8. Polish → Production ready

---

## Notes

- [P] tasks = different files, no dependencies
- [Story] label maps task to specific user story
- Each user story should be independently completable after US1
- Commit after each task or logical group
- Stop at any checkpoint to validate story independently
- Backend runs on port 5000, frontend on port 5173
