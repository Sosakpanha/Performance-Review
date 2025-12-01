import type { IMoodCount } from './IMoodCount'

export interface ITeamStats {
  totalGoals: number
  completedGoals: number
  completionPercentage: number
  moodCounts: IMoodCount[]
}
