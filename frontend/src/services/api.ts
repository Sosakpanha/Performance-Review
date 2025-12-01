import type { IMember, IGoal, ICreateGoalRequest, IUpdateMoodRequest, ITeamStats } from '../models'
import { API_BASE_URL } from '../constants'

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
  async getMembers(): Promise<IMember[]> {
    const response = await fetch(`${API_BASE_URL}/members`)
    return handleResponse<IMember[]>(response)
  },

  async createGoal(request: ICreateGoalRequest): Promise<IGoal> {
    const response = await fetch(`${API_BASE_URL}/goals`, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(request),
    })
    return handleResponse<IGoal>(response)
  },

  async toggleGoal(id: number): Promise<IGoal> {
    const response = await fetch(`${API_BASE_URL}/goals/${id}/toggle`, {
      method: 'PUT',
    })
    return handleResponse<IGoal>(response)
  },

  async deleteGoal(id: number): Promise<void> {
    const response = await fetch(`${API_BASE_URL}/goals/${id}`, {
      method: 'DELETE',
    })
    return handleResponse<void>(response)
  },

  async updateMood(memberId: number, request: IUpdateMoodRequest): Promise<void> {
    const response = await fetch(`${API_BASE_URL}/members/${memberId}/mood`, {
      method: 'PUT',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(request),
    })
    return handleResponse<void>(response)
  },

  async getStats(): Promise<ITeamStats> {
    const response = await fetch(`${API_BASE_URL}/stats`)
    return handleResponse<ITeamStats>(response)
  },
}
