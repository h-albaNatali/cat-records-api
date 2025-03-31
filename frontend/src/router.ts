import { createRouter, createWebHistory } from 'vue-router'
import HomeView from './views/HomeView.vue'
import CreateView from './views/CreateView.vue'
import EditView from './views/EditView.vue'

const routes = [
  { path: '/', component: HomeView },
  { path: '/create', component: CreateView },
  { path: '/edit/:id', component: EditView, props: true }
]

const router = createRouter({
  history: createWebHistory(),
  routes
})

export default router