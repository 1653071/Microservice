<template>
  <div class="sidebar">
    <Logo  />
    
    <div class="item-sidebar">
      <a class="item"
        ><b-icon class="icon-menu" icon="house-door" aria-hidden="true"></b-icon>
        <router-link class="link" to="/dashboard/home">Home</router-link></a
      >
      <a class="item" v-if="isAdmin == true || isManager==true"
        ><b-icon class="icon-menu" icon="people" aria-hidden="true"></b-icon>
        <router-link class="link" to="/dashboard/users">User</router-link></a
      >
      <a class="item" v-if="isAdmin == true"
        ><b-icon class="icon-menu" icon="key" aria-hidden="true"></b-icon>
        <router-link class="link" to="/dashboard/permissions"
          >Permission</router-link
        ></a
      >
      
    </div>
  </div>
</template>
<script>
import Logo from "../logo/Logo.vue";

import {checkPermission,getPermission} from "../../service/permissionservice"
export default {
  name: "Sidebar",
  data() {
    return {
      isManager:false,
      isAdmin:false,
      isUser:false,
      per:[]
    };
  },
  components: { Logo },
  async created(){
    this.per = await getPermission()
    this.checkPer()
    
  },
  
  methods:{
    async checkPer () {
      this.isManager = await checkPermission(this.per,[2])
      this.isAdmin = await checkPermission(this.per,[1])
      this.isUser = await checkPermission(this.per,[3])
    }
  }
};
</script>
<style scoped>
.sidebar {
  height: 100%;
  background-color: #464BE1;

  width: 100%;
}
.sidebar .item-sidebar {
  background-color: #464BE1;
  cursor: pointer;
  font-weight: 400;
}
.sidebar .item-sidebar .item {
  display: block;
  color: #d2d2d2;
  padding: 15px;
  text-decoration: none;
}
.sidebar .item-sidebar .item .link {
  color: #A6A6A6;
  text-decoration: none;
  font-weight: 700;
}
.sidebar .item-sidebar .item:hover {
  background-color: #9B89FF;
  background-image:
    linear-gradient(to right,
      #9B89FF, #5E5FFF
    );

    color: white;
}
.sidebar .item-sidebar .item .link:hover {
  color: white;
  font-weight: 800;
}
.sidebar .item-sidebar .item .link:hover {
}
.sidebar .item-sidebar .item .icon-menu {
  margin-right: 15px;
  margin-left: 15px;
}
@media screen and (max-width: 992x) {
  .sidebar .item-sidebar .item {
    display: inline-block;
    color: #d2d2d2;
    padding: 15px;
    text-decoration: none;
  }.sidebar {
    display: inline-block;
    color: #d2d2d2;
    padding: 15px;
    text-decoration: none;
   
  }
}
</style>
