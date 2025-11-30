export interface GoalDto {
  id: number
  memberId: number
  description: string
  isCompleted: boolean
}

export interface MemberDto {
  id: number
  name: string
  mood: string
  moodEmoji: string
  goals: GoalDto[]
  completedCount: number
  totalCount: number
}

export interface CreateGoalRequest {
  memberId: number
  description: string
}

export interface UpdateMoodRequest {
  mood: string
}

export interface TeamStatsDto {
  totalGoals: number
  completedGoals: number
  completionPercentage: number
  moodCounts: MoodCountDto[]
}

export interface MoodCountDto {
  mood: string
  emoji: string
  count: number
}

export const MOOD_OPTIONS = [
  { id: 'happy', emoji: '😊', label: 'Happy' },
  { id: 'neutral', emoji: '😐', label: 'Neutral' },
  { id: 'sad', emoji: '😢', label: 'Sad' },
  { id: 'stressed', emoji: '😰', label: 'Stressed' },
  { id: 'excited', emoji: '🎉', label: 'Excited' },
] as const
