<template>
  <div class="navbar">
    <b-navbar class="navbar-block">
    

  

    <b-collapse id="nav-collapse" is-nav>
     

      <!-- Right aligned nav items -->
      <b-navbar-nav class="ml-auto">
        <b-avatar :src="user.avatar"></b-avatar>
      
       <b-nav-item class="welcome">Hi,{{user.username}}</b-nav-item>
      
        <b-nav-item-dropdown right>
          <!-- Using 'button-content' slot -->
          <template #button-content>
              
          </template>
         
          <b-dropdown-item @click="logout">Sign Out</b-dropdown-item>
        </b-nav-item-dropdown>

       
        
      </b-navbar-nav>
    </b-collapse>
  </b-navbar>
  </div>
</template>

<script>
import {getUser} from "../../service/repo/repository"
export default {
  name: "Navigation",
  data() {
    return {
      user:getUser()
    };
  },
  methods: {
    logout() {
      localStorage.removeItem("accessToken");
      localStorage.removeItem("currentUser");
      this.$router.push("/login");
    }
  }
};
</script>
<style scoped>
.navbar {
  background: white;
  
}
.navbar-block{
  width: 100%;
  background-color: white;
}
.welcome{
  font-weight: 700;
}
</style>

