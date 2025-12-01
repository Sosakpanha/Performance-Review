<script setup lang="ts">
import { ref, onMounted } from 'vue'
import type { ITeamStats } from '../models'
import { api } from '../services/api'

const stats = ref<ITeamStats | null>(null)
const loading = ref<boolean>(true)
const error = ref<string | null>(null)

async function fetchStats() {
  try {
    loading.value = true
    error.value = null
    stats.value = await api.getStats()
  } catch (e) {
    error.value = 'Failed to load statistics'
    console.error(e)
  } finally {
    loading.value = false
  }
}

defineExpose({ refresh: fetchStats })

onMounted(fetchStats)
</script>

<template>
  <div class="card bg-base-200 shadow-md">
    <div class="card-body p-4">
      <h3 class="card-title text-lg">Team Statistics</h3>

      <div v-if="loading" class="flex justify-center py-4">
        <span class="loading loading-spinner"></span>
      </div>

      <div v-else-if="error" class="text-error text-sm">
        {{ error }}
      </div>

      <div v-else-if="stats" class="space-y-4">
        <div class="stats stats-vertical lg:stats-horizontal shadow w-full">
          <div class="stat">
            <div class="stat-title">Completion</div>
            <div class="stat-value text-primary">{{ stats.completionPercentage }}%</div>
            <div class="stat-desc">{{ stats.completedGoals }} of {{ stats.totalGoals }} goals</div>
          </div>
        </div>

        <div>
          <h4 class="text-sm font-semibold mb-2">Team Mood</h4>
          <div class="flex flex-wrap gap-2">
            <div
              v-for="mood in stats.moodCounts"
              :key="mood.mood"
              class="badge badge-lg gap-1"
            >
              <span>{{ mood.emoji }}</span>
              <span>{{ mood.count }}</span>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>
