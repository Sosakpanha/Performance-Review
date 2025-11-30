# Quickstart: Team Daily Goal Tracker with Mood Sync

## Prerequisites

- .NET 8 SDK
- Node.js 18+ and npm
- A code editor (VS Code recommended)

## Setup

### 1. Clone and Navigate

```bash
cd team-goal-tracker
```

### 2. Backend Setup

```bash
# Navigate to backend
cd backend/TeamGoalTracker.Api

# Restore dependencies
dotnet restore

# Run the API (starts on http://localhost:5000)
dotnet run
```

The API will:
- Create SQLite database (`teamgoals.db`) on first run
- Seed 5 team members automatically
- Enable CORS for frontend development

### 3. Frontend Setup (new terminal)

```bash
# Navigate to frontend
cd frontend

# Install dependencies
npm install

# Run development server (starts on http://localhost:5173)
npm run dev
```

### 4. Open Application

Visit http://localhost:5173 in your browser.

## Verification Checklist

| Feature | How to Verify |
|---------|---------------|
| Dashboard loads | See 5 team member cards with names and mood emojis |
| Add goal | Use form to add goal, see it appear on member card |
| Toggle goal | Click checkbox, see strikethrough and count update |
| Delete goal | Click delete icon, see goal removed |
| Update mood | Use mood form, see emoji change on member card |
| View stats | See completion % and mood counts in stats panel |

## API Endpoints

Base URL: `http://localhost:5000/api`

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | /members | Get all members with goals |
| PUT | /members/{id}/mood | Update member mood |
| POST | /goals | Create new goal |
| PUT | /goals/{id}/toggle | Toggle goal completion |
| DELETE | /goals/{id} | Delete goal |
| GET | /stats | Get team statistics |

## Test with curl

```bash
# Get all members
curl http://localhost:5000/api/members

# Add a goal
curl -X POST http://localhost:5000/api/goals \
  -H "Content-Type: application/json" \
  -d '{"memberId": 1, "description": "Test goal"}'

# Toggle goal (replace {id})
curl -X PUT http://localhost:5000/api/goals/1/toggle

# Update mood
curl -X PUT http://localhost:5000/api/members/1/mood \
  -H "Content-Type: application/json" \
  -d '{"mood": "excited"}'

# Get stats
curl http://localhost:5000/api/stats
```

## Troubleshooting

### Port conflicts

Backend default: 5000, Frontend default: 5173

If ports are in use:
- Backend: Edit `launchSettings.json`
- Frontend: Run `npm run dev -- --port 3000`

### CORS errors

Ensure backend is running before frontend. CORS is configured for `http://localhost:5173`.

### Database reset

Delete `teamgoals.db` file in backend directory and restart API to re-seed.

## Project Structure

```
team-goal-tracker/
├── backend/
│   └── TeamGoalTracker.Api/
│       ├── Controllers/      # API endpoints
│       ├── Services/         # Business logic
│       ├── Repositories/     # Data access
│       ├── Models/           # Database entities
│       ├── DTOs/             # API contracts
│       └── Data/             # DB initialization
├── frontend/
│   └── src/
│       ├── components/       # Vue components
│       ├── views/            # Page components
│       ├── services/         # API client
│       └── types/            # TypeScript types
└── specs/
    └── 001-team-goal-tracker/
        ├── spec.md           # Requirements
        ├── plan.md           # Implementation plan
        ├── data-model.md     # Database design
        ├── contracts/        # OpenAPI spec
        └── quickstart.md     # This file
```
