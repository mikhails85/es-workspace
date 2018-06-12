import Vue from 'vue'
import VueRouter from 'vue-router'
import App from './App.vue'
import TodoList from './Components/Todo/TodoList.vue'
import ForecastList from './Components/ForecastList/ForecastList.vue'
import BootstrapVue from 'bootstrap-vue'

Vue.config.productionTip = false
Vue.use(VueRouter)

Vue.use(BootstrapVue)

const routes = [
  { 
    path: '/', 
    name: 'Home',
    component: App,
    children: [
        {
          path: 'todo',
          name: 'ToDo',
          component: TodoList
        },
        {
          name: 'ForeCast',  
          path: 'forecast',
          component: ForecastList
        }
    ]
  }
]

const router = new VueRouter({
  routes,
  mode: 'history'
})

new Vue({
  el: '#app',
  template: "<div><router-view></router-view></div>",
  router
})