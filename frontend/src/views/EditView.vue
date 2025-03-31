<template>
  <div class="container">
    <h1>Edit Record</h1>
    <form @submit.prevent="updateRecord">
      <div class="form-group">
        <label for="title">Title</label>
        <input id="title" v-model="record.title" type="text" required />
      </div>
      <div class="form-group">
        <label for="description">Description</label>
        <input id="description" v-model="record.description" type="text" required />
      </div>
      <div class="actions">
        <button type="submit" :disabled="!isFormValid">Save</button>
        <button type="button" @click="cancel">Cancel</button>
      </div>
    </form>
    <p v-if="errorMessage" class="error">{{ errorMessage }}</p>
  </div>
</template>

<script lang="ts">
import { defineComponent, onMounted, ref, computed } from 'vue'
import { useRouter, useRoute } from 'vue-router'
import axios from 'axios'

interface Record {
  id: number
  title: string
  description: string
}

export default defineComponent({
  setup() {
    const record = ref<Record>({ id: 0, title: '', description: '' })
    const router = useRouter()
    const route = useRoute()
    const errorMessage = ref('')

    const isFormValid = computed(() => {
      return record.value.title.trim() !== '' && record.value.description.trim() !== ''
    })

    const loadRecord = async () => {
      try {
        const response = await axios.get(`http://localhost:5257/api/data/${route.params.id}`)
        record.value = response.data
      } catch {
        errorMessage.value = 'Failed to load record.'
      }
    }

    const updateRecord = async () => {
      try {
        await axios.put(`http://localhost:5257/api/data/${record.value.id}`, record.value)
        router.push('/')
      } catch {
        errorMessage.value = 'Failed to update record.'
      }
    }

    const cancel = () => {
      router.push('/')
    }

    onMounted(() => {
      loadRecord()
    })

    return {
      record,
      errorMessage,
      updateRecord,
      cancel,
      isFormValid
    }
  }
})
</script>

<style scoped>
.container {
  max-width: 600px;
  margin: 40px auto;
  padding: 30px;
  background: #fff;
  border-radius: 12px;
  box-shadow: 0 2px 8px rgba(0, 0, 0, 0.06);
}

h1 {
  margin-bottom: 24px;
}

.form-group {
  margin-bottom: 20px;
}

label {
  display: block;
  margin-bottom: 8px;
  font-weight: 600;
}

input {
  width: 100%;
  padding: 10px;
  border-radius: 6px;
  border: 1px solid #ccc;
}

.actions {
  display: flex;
  gap: 12px;
}

button {
  padding: 10px 18px;
  border: none;
  border-radius: 6px;
  cursor: pointer;
  font-weight: bold;
}

button[type='submit'] {
  background-color: #007bff;
  color: white;
}

button[type='button'] {
  background-color: #6c757d;
  color: white;
}

button:disabled {
  background-color: #ccc;
  cursor: not-allowed;
}

.error {
  color: red;
  margin-top: 16px;
}
</style>