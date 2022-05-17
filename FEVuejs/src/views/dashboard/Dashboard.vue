<template>
 
    <b-row class="row" no-gutters>
      <b-col class="col col-sidebar" cols="12" md="12" sm="12" lg="2"><Sidebar /> </b-col>
      <b-col class="col col-content" cols="12" md="12" sm="12" lg="10"><Navigation /><router-view class="view"/></b-col>
    </b-row>
    
 
</template>
<script>
import Sidebar from '../../components/menu/Sidebar.vue'
import Navigation from '../../components/navigation/Navigation.vue'
export default {
  name: "Dashboard",
  components: {
    Sidebar,
    Navigation
  },
};
</script>
<style scoped>
.col{
  
  padding: 0;
  margin: 0;
}
.col-content{
  height: 100%;
  background-color:#F6F6F6;
  overflow: scroll;
}
.col-sidebar{
   height: 100%;
 }
.row{
  padding: 0;
  margin: 0;
  height: 100%;
}
@media screen and (max-width:992px) {
 .col-sidebar{
   height: 20%;
 }
 .col-content{
   overflow: none;
 }
}
</style>