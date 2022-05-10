<template>
  <div id="app">
     

    <router-view/>
  </div>
</template>

<script>
import Navigation from "./components/navigation/Navigation"
import Sidebar from "./components/menu/Sidebar"
import Modal from "./components/modal/Modal.vue"
export default {
  name: 'App',
  components:{
    Navigation,Sidebar,Modal
  },
  

}
</script>

<style scoped>
#app {
  height: 100%;
  overflow: auto;
  /* font-family: Verdana, sans-serif;;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  background-color: #F0E8F0;
  color: #2c3e50;
  font-size:16px;
  padding :0;
  overflow: scroll */
 
 
  
}
@import'~bootstrap/dist/css/bootstrap.css';
</style>
