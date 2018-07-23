import Vue from 'vue'
import Router from 'vue-router'

import Dashboard from '../components/pages/Dashboard'
import Skills from '../components/pages/Skills'
import Offers from '../components/pages/Offers'
import Employees from '../components/pages/Employees' 

Vue.use(Router)

export default new Router({
  routes: [
    {
      path: '/',
      name: 'Dashboard',
      component: Dashboard,
    },
    {
      path: '/skills',
      name: 'Skills',
      component: Skills
    },
    {
      path: '/offers',
      name: 'Offers',
      component: Offers
    },
    {
      path: '/employees',
      name: 'Employees',
      component: Employees
    }
  ],
  mode: 'history'
})
