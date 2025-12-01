export const MoodType = {
  Happy: 'happy',
  Neutral: 'neutral',
  Sad: 'sad',
  Stressed: 'stressed',
  Excited: 'excited'
} as const

export type MoodType = typeof MoodType[keyof typeof MoodType]
