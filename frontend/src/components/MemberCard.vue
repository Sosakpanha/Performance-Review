<script setup lang="ts">
import type { MemberDto } from '../types'
import GoalItem from './GoalItem.vue'

defineProps<{
  member: MemberDto
}>()

const emit = defineEmits<{
  toggleGoal: [goalId: number]
  deleteGoal: [goalId: number]
}>()
</script>

<template>
  <div class="card bg-base-100 shadow-md">
    <div class="card-body p-4">
      <div class="flex items-center justify-between">
        <h2 class="card-title text-lg">
          {{ member.name }}
        </h2>
        <span class="text-2xl" :title="member.mood">{{ member.moodEmoji }}</span>
      </div>

      <div class="text-sm text-gray-500">
        {{ member.completedCount }}/{{ member.totalCount }} completed
      </div>

      <div class="divider my-1"></div>

      <div v-if="member.goals.length === 0" class="text-sm text-gray-400 italic">
        No goals for today
      </div>

      <div v-else class="space-y-1">
        <GoalItem
          v-for="goal in member.goals"
          :key="goal.id"
          :goal="goal"
          @toggle="emit('toggleGoal', $event)"
          @delete="emit('deleteGoal', $event)"
        />
      </div>
    </div>
  </div>
</template>
