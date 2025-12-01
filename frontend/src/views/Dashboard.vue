<script setup lang="ts">
import { ref, onMounted } from 'vue'
import type { IMember } from '../models'
import { api } from '../services/api'
import MemberCard from '../components/MemberCard.vue'
import AddGoalForm from '../components/AddGoalForm.vue'
import UpdateMoodForm from '../components/UpdateMoodForm.vue'
import StatsPanel from '../components/StatsPanel.vue'

const members = ref<IMember[]>([])
const loading = ref<boolean>(true)
const error = ref<string | null>(null)
const statsPanel = ref<InstanceType<typeof StatsPanel> | null>(null)

async function fetchMembers() {
  try {
    loading.value = true
    error.value = null
    members.value = await api.getMembers()
  } catch (e) {
    error.value = 'Failed to load team members'
    console.error(e)
  } finally {
    loading.value = false
  }
}

async function handleToggleGoal(goalId: number) {
  try {
    await api.toggleGoal(goalId)
    await fetchMembers()
    statsPanel.value?.refresh()
  } catch (e) {
    console.error('Failed to toggle goal:', e)
  }
}

async function handleDeleteGoal(goalId: number) {
  try {
    await api.deleteGoal(goalId)
    await fetchMembers()
    statsPanel.value?.refresh()
  } catch (e) {
    console.error('Failed to delete goal:', e)
  }
}

async function handleAddGoal(memberId: number, description: string) {
  try {
    await api.createGoal({ memberId, description })
    await fetchMembers()
    statsPanel.value?.refresh()
  } catch (e) {
    console.error('Failed to add goal:', e)
  }
}

async function handleUpdateMood(memberId: number, mood: string) {
  try {
    await api.updateMood(memberId, { mood })
    await fetchMembers()
    statsPanel.value?.refresh()
  } catch (e) {
    console.error('Failed to update mood:', e)
  }
}

onMounted(fetchMembers)
</script>

<template>
  <div class="container mx-auto p-4">
    <h1 class="text-3xl font-bold mb-6">Team Daily Goal Tracker</h1>

    <div v-if="loading" class="flex justify-center py-8">
      <span class="loading loading-spinner loading-lg"></span>
    </div>

    <div v-else-if="error" class="alert alert-error">
      {{ error }}
    </div>

    <div v-else-if="members.length === 0" class="text-center py-8 text-gray-500">
      No team members found
    </div>

    <div v-else class="space-y-6">
      <div class="grid grid-cols-1 md:grid-cols-3 gap-4">
        <AddGoalForm :members="members" @submit="handleAddGoal" />
        <UpdateMoodForm :members="members" @submit="handleUpdateMood" />
        <StatsPanel ref="statsPanel" />
      </div>

      <div class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-4">
        <MemberCard
          v-for="member in members"
          :key="member.id"
          :member="member"
          @toggle-goal="handleToggleGoal"
          @delete-goal="handleDeleteGoal"
        />
      </div>
    </div>
  </div>
</template>
