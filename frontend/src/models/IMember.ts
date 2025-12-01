import type { IGoal } from './IGoal'

export interface IMember {
  id: number
  name: string
  mood: string
  moodEmoji: string
  goals: IGoal[]
  completedCount: number
  totalCount: number
}
