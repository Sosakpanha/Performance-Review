<script setup lang="ts">
import type { GoalDto } from '../types'

defineProps<{
  goal: GoalDto
}>()

const emit = defineEmits<{
  toggle: [id: number]
  delete: [id: number]
}>()
</script>

<template>
  <div class="flex items-center gap-2 py-1">
    <input
      type="checkbox"
      class="checkbox checkbox-sm"
      :checked="goal.isCompleted"
      @change="emit('toggle', goal.id)"
    />
    <span
      class="flex-1 text-sm"
      :class="{ 'line-through text-gray-400': goal.isCompleted }"
    >
      {{ goal.description }}
    </span>
    <button
      class="btn btn-ghost btn-xs text-error"
      @click="emit('delete', goal.id)"
    >
      ✕
    </button>
  </div>
</template>
