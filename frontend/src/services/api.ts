import type { MemberDto, GoalDto, CreateGoalRequest, UpdateMoodRequest, TeamStatsDto } from '../types'

const API_BASE = '/api'

async function handleResponse<T>(response: Response): Promise<T> {
  if (!response.ok) {
    throw new Error(`API error: ${response.status} ${response.statusText}`)
  }
  if (response.status === 204) {
    return undefined as T
  }
  return response.json()
}

export const api = {
  async getMembers(): Promise<MemberDto[]> {
    const response = await fetch(`${API_BASE}/members`)
    return handleResponse<MemberDto[]>(response)
  },

  async createGoal(request: CreateGoalRequest): Promise<GoalDto> {
    const response = await fetch(`${API_BASE}/goals`, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(request),
    })
    return handleResponse<GoalDto>(response)
  },

  async toggleGoal(id: number): Promise<GoalDto> {
    const response = await fetch(`${API_BASE}/goals/${id}/toggle`, {
      method: 'PUT',
    })
    return handleResponse<GoalDto>(response)
  },

  async deleteGoal(id: number): Promise<void> {
    const response = await fetch(`${API_BASE}/goals/${id}`, {
      method: 'DELETE',
    })
    return handleResponse<void>(response)
  },

  async updateMood(memberId: number, request: UpdateMoodRequest): Promise<void> {
    const response = await fetch(`${API_BASE}/members/${memberId}/mood`, {
      method: 'PUT',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(request),
    })
    return handleResponse<void>(response)
  },

  async getStats(): Promise<TeamStatsDto> {
    const response = await fetch(`${API_BASE}/stats`)
    return handleResponse<TeamStatsDto>(response)
  },
}
