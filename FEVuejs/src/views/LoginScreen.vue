<template>
  <div class="login-container">
    <div class="form-container">
          <div class="login-title">Login</div>
       
          <LoginForm />
       
    </div>
  </div>
</template>
<script>
import ImageAdmin from "../components/image/ImageAdmin.vue";

import UsersTable from "../components/table/UsersTable.vue";
import LoginForm from "../components/form/LoginForm.vue";

export default {
  name: "LoginScreen",
  components: {
    UsersTable,
    ImageAdmin,
    LoginForm,

  },
  beforeCreate() {
    if (this.userInfo) {
        this.$router.push('/dashboard');
    }
  },
};
</script>
<style>

.login-container {
  background-image: url("https://hkuspace-plk.hku.hk/f/programme/194/1536c696/HD%20-%20Management%20Studies%20m.jpg");
  background-color: #cccccc;
  height: 100vh;
  widows: 100px;
}

.form-container {
  position: absolute;
  top: 100px;
  right: 150px;
  background-color: white;
  border-radius: 10px;
  width: 400px;
  height: 500px;


}
.form-container .login-title{
  font-size: 24px;
  text-align: center;
  margin-top: 40px;
  font-weight: 900;
}
.ttt:not(.active){
  color: black !important;
}
.ttt:focus{
    color: white;
    background-color: rgb(55, 133, 206) !important;
    font-weight: 700 !important;
}
</style>
