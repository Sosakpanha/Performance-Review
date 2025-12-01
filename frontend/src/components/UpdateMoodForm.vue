<script setup lang="ts">
import { ref } from 'vue'
import type { IMember } from '../models'
import { MOOD_OPTIONS } from '../constants'

defineProps<{
  members: IMember[]
}>()

const emit = defineEmits<{
  submit: [memberId: number, mood: string]
}>()

const selectedMemberId = ref<number | null>(null)
const selectedMood = ref<string>('')
const errorMessage = ref<string>('')

function handleSubmit() {
  errorMessage.value = ''

  if (!selectedMemberId.value) {
    errorMessage.value = 'Please select a team member'
    return
  }

  if (!selectedMood.value) {
    errorMessage.value = 'Please select a mood'
    return
  }

  emit('submit', selectedMemberId.value, selectedMood.value)
}
</script>

<template>
  <div class="card bg-base-200 shadow-md">
    <div class="card-body p-4">
      <h3 class="card-title text-lg">Update Mood</h3>

      <form @submit.prevent="handleSubmit" class="space-y-3">
        <div class="form-control">
          <label class="label">
            <span class="label-text">Team Member</span>
          </label>
          <select
            v-model="selectedMemberId"
            class="select select-bordered w-full"
          >
            <option :value="null" disabled>Select a member</option>
            <option
              v-for="member in members"
              :key="member.id"
              :value="member.id"
            >
              {{ member.name }}
            </option>
          </select>
        </div>

        <div class="form-control">
          <label class="label">
            <span class="label-text">Mood</span>
          </label>
          <div class="flex gap-2 flex-wrap">
            <button
              v-for="mood in MOOD_OPTIONS"
              :key="mood.id"
              type="button"
              class="btn btn-circle text-2xl"
              :class="{ 'btn-primary': selectedMood === mood.id, 'btn-ghost': selectedMood !== mood.id }"
              :title="mood.label"
              @click="selectedMood = mood.id"
            >
              {{ mood.emoji }}
            </button>
          </div>
        </div>

        <div v-if="errorMessage" class="text-error text-sm">
          {{ errorMessage }}
        </div>

        <button type="submit" class="btn btn-primary w-full">
          Update Mood
        </button>
      </form>
    </div>
  </div>
</template>
