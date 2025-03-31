<template>
  <div class="container">
    <div class="header">
      <h1>Records</h1>
      <div class="actions">
        <input type="text" placeholder="Search..." v-model="search" />
        <router-link to="/create" class="btn add">+ Add New</router-link>
        <button @click="fetchExternalData" class="btn btn-secondary">Fetch External Data</button>
      </div>
    </div>

    <table v-if="filteredRecords.length">
      <thead>
        <tr>
          <th>ID</th>
          <th>Title</th>
          <th>Description</th>
          <th>Actions</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="record in filteredRecords" :key="record.id">
          <td>{{ record.id }}</td>
          <td>{{ record.title }}</td>
          <td>{{ record.description }}</td>
          <td>
            <div class="actions-column">
              <router-link :to="`/edit/${record.id}`" class="btn edit">Edit</router-link>
              <button class="btn delete" @click="deleteRecord(record.id)">Delete</button>
            </div>
          </td>
        </tr>
      </tbody>
    </table>

    <p v-if="errorMessage" class="error">{{ errorMessage }}</p>
    <p v-if="!filteredRecords.length && !errorMessage">No records found.</p>
  </div>
</template>

<script lang="ts">
import { defineComponent, onMounted, ref, computed } from 'vue'
import axios from 'axios'

interface Record {
  id: number
  title: string
  description: string
}

export default defineComponent({
  setup() {
    const records = ref<Record[]>([])
    const errorMessage = ref('')
    const search = ref('')

    const loadRecords = async () => {
      try {
        const response = await axios.get('http://localhost:5257/api/data')
        records.value = response.data
      } catch {
        errorMessage.value = 'Failed to load records.'
      }
    }

    const deleteRecord = async (id: number) => {
      try {
        await axios.delete(`http://localhost:5257/api/data/${id}`)
        await loadRecords()
      } catch {
        alert('Failed to delete record.')
      }
    }

    const fetchExternalData = async () => {
      try {
        await axios.post('http://localhost:5257/api/data/fetch-external')
        await loadRecords()
        alert('External data fetched successfully!')
      } catch (error) {
        alert('Failed to fetch external data.')
        console.error(error)
      }
    }

    const filteredRecords = computed(() =>
      records.value.filter(
        r =>
          r.title.toLowerCase().includes(search.value.toLowerCase()) ||
          r.description.toLowerCase().includes(search.value.toLowerCase())
      )
    )

    onMounted(() => {
      loadRecords()
    })

    return {
      records,
      errorMessage,
      search,
      loadRecords,
      deleteRecord,
      fetchExternalData,
      filteredRecords
    }
  }
})
</script>

<style scoped>
.container {
  max-width: 1000px;
  margin: 40px auto;
  padding: 30px;
  background: #fff;
  border-radius: 12px;
  box-shadow: 0 2px 8px rgba(0, 0, 0, 0.06);
  overflow-x: auto;
}

.header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 24px;
  flex-wrap: wrap;
  gap: 12px;
}

.actions {
  display: flex;
  gap: 12px;
  flex-wrap: wrap;
}

input[type="text"] {
  padding: 8px;
  border-radius: 6px;
  border: 1px solid #ccc;
}

table {
  width: 100%;
  border-collapse: collapse;
  table-layout: fixed;
}

th,
td {
  padding: 10px;
  text-align: left;
  vertical-align: top;
  border-bottom: 1px solid #ddd;
  word-wrap: break-word;
  overflow-wrap: break-word;
}

td:nth-child(1) {
  width: 60px;
}
td:nth-child(2) {
  width: 150px;
}
td:nth-child(4) {
  width: 180px;
}

.actions-column {
  display: flex;
  flex-wrap: wrap;
  gap: 8px;
}

.btn {
  padding: 6px 10px;
  border-radius: 6px;
  border: none;
  cursor: pointer;
  font-weight: bold;
}

.btn.add {
  background-color: #007bff;
  color: white;
}

.btn.btn-secondary {
  background-color: #6c757d;
  color: white;
}

.btn.edit {
  background-color: #ffc107;
  color: black;
}

.btn.delete {
  background-color: #dc3545;
  color: white;
}

.error {
  color: red;
  margin-top: 12px;
}
</style>
