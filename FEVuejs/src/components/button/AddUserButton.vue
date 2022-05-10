<template>
    <b-button class="add-user" variant="primary" >Add User</b-button>
</template>
<script>


export default {
    name:"AddUserButton",
    methods: {
      showModal() {
        this.$refs['my-modal'].show()
      },
      hideModal() {
        this.$refs['my-modal'].hide()
      },
      
    }
}
</script>
<style>
   .add-user{
       margin-bottom: 10px;
   }

</style>
