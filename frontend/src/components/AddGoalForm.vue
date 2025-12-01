<script setup lang="ts">
import { ref } from 'vue'
import type { IMember } from '../models'

defineProps<{
  members: IMember[]
}>()

const emit = defineEmits<{
  submit: [memberId: number, description: string]
}>()

const selectedMemberId = ref<number | null>(null)
const description = ref<string>('')
const errorMessage = ref<string>('')

function handleSubmit() {
  errorMessage.value = ''

  if (!selectedMemberId.value) {
    errorMessage.value = 'Please select a team member'
    return
  }

  if (!description.value.trim()) {
    errorMessage.value = 'Please enter a goal description'
    return
  }

  emit('submit', selectedMemberId.value, description.value.trim())
  description.value = ''
}
</script>

<template>
  <div class="card bg-base-200 shadow-md">
    <div class="card-body p-4">
      <h3 class="card-title text-lg">Add New Goal</h3>

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
            <span class="label-text">Goal Description</span>
          </label>
          <input
            v-model="description"
            type="text"
            placeholder="Enter goal description"
            class="input input-bordered w-full"
          />
        </div>

        <div v-if="errorMessage" class="text-error text-sm">
          {{ errorMessage }}
        </div>

        <button type="submit" class="btn btn-primary w-full">
          Add Goal
        </button>
      </form>
    </div>
  </div>
</template>
