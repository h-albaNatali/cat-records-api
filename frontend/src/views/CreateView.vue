<template>
  <div class="container">
    <div class="header">
      <h1>Create Record</h1>
    </div>
    <form @submit.prevent="createRecord">
      <label for="title">Title</label>
      <input id="title" v-model="record.title" required />

      <label for="description">Description</label>
      <input id="description" v-model="record.description" required />

      <button type="submit" :disabled="!isFormValid">Save</button>
    </form>
    <p v-if="errorMessage" class="error">{{ errorMessage }}</p>
    <p v-if="successMessage" class="success">{{ successMessage }}</p>
  </div>
</template>

<script lang="ts">
import { defineComponent, reactive, ref, computed } from 'vue'
import axios from 'axios'
import { useRouter } from 'vue-router'

export default defineComponent({
  setup() {
    const router = useRouter()
    const record = reactive({ title: '', description: '' })
    const errorMessage = ref('')
    const successMessage = ref('')

    const isFormValid = computed(() => {
      return record.title.trim() !== '' && record.description.trim() !== ''
    })

    const createRecord = async () => {
      try {
        await axios.post('http://localhost:5257/api/data', record)
        successMessage.value = 'Record created successfully.'
        errorMessage.value = ''
        setTimeout(() => router.push('/'), 1000)
      } catch {
        errorMessage.value = 'Failed to create record.'
        successMessage.value = ''
      }
    }

    return {
      record,
      errorMessage,
      successMessage,
      createRecord,
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

.header {
  margin-bottom: 24px;
  text-align: center;
}

form {
  display: flex;
  flex-direction: column;
}

label {
  margin-top: 12px;
  margin-bottom: 4px;
  font-weight: bold;
}

input {
  padding: 10px;
  border: 1px solid #ccc;
  border-radius: 6px;
}

button {
  margin-top: 20px;
  padding: 10px;
  background-color: #28a745;
  color: white;
  font-weight: bold;
  border: none;
  border-radius: 6px;
  cursor: pointer;
}

button:disabled {
  background-color: #ccc;
  cursor: not-allowed;
}

.error {
  color: red;
  margin-top: 12px;
}

.success {
  color: green;
  margin-top: 12px;
}
</style>
