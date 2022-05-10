import Vue from 'vue'
import Router from 'vue-router'
import UsersScreen from '../views/UsersScreen.vue'
import LoginScreen from '../views/LoginScreen.vue'
import Dashboard from '../views/dashboard/Dashboard.vue'
import HomeScreen from '../views/HomeScreen.vue'
import Permission1 from '../views/Permission1.vue'

Vue.use(Router)

const router = new Router({
  routes: [
    {
      path: '/dashboard',
      name: 'Dashboard',
      component: Dashboard,
      children:[
        {
          path: '/dashboard/users',
           name: 'UsersScreen',
           component: UsersScreen,
           
        },
        {
          path: '/dashboard/permissions',
           name: 'Permission1',
           component: Permission1,
           
        },
        {
          path: '/dashboard/home',
           name: 'HomeScreen',
           component: HomeScreen,
           
        },
        
      ]
    },
    
    
    {
      path: '/login',
      name: 'LoginScreen',
      component: LoginScreen
    },
    
  ],
  
});
router.beforeEach((to, from, next) => {
  
 
  if (to.fullPath === '/login') {
    if (localStorage.getItem("accessToken")) {
      next('/dashboard/home');
    }
    else{
      next();
    }
  }
  else{
    if (!localStorage.getItem("accessToken")) {
      next('/login');
    }
    else{
      
      next()
    
    }
  }
  
});

export default router;