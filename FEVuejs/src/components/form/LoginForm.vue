<template>
  <div>
    <b-form class="form-login" @submit="onSubmit" @reset="onReset" v-if="show">
      <!-- <p v-if="errors.length">
    <b class="error-title">Please correct the following error(s):</b>
    <ul>
      <li v-for="error in errors" :key="error.id">{{ error }}</li>
    </ul>
  </p> -->
      <b-alert
        show
        v-if="errors.length > 0 "
        dismissible
        @dismissed="dismissCountDown = 0"
        @dismiss-count-down="countDownChanged"
        variant = "danger"  
      >
        <div v-if="errors.length>0">
          <span v-for="error in errors" :key="error">
            {{error}}
          </span>
          
        </div>
      </b-alert>
      <b-form-group id="input-group-1" label-for="input-1">
        <b-form-input
          id="input-1"
          v-model="form.email"
          type="text"
          placeholder="Enter email"
        ></b-form-input>
      </b-form-group>
      <b-form-group id="input-group-1" label-for="input-1">
        <b-form-input
          id="input-1"
          v-model="form.password"
          type="password"
          placeholder="Enter password"
        ></b-form-input>
      </b-form-group>
      
      <b-button pill class="button-login" type="submit" variant="primary"
        >Login</b-button
      >
    </b-form>
  </div>
</template>

<script>
import authrepo from "../../service/impl/authrepo";
import {mapState} from "vuex"
import {getPermission,checkPermission} from "../../service/permissionservice"
export default {
  data() {
    return {
      form: {
        email: "",
        password: "",
      },
      errors: [],
      dismissSecs: 5,
      dismissCountDown: 0,
      loginerror:null,
      show: true,
      permission:[]
    };
  },
  methods: {
    checkForm: function (e) {
      this.errors = [];

      if (!this.form.email) {
        this.errors.push("Email required.");
      } 
      if (!this.form.password) {
        this.errors.push("Password required.");
      }
       
      if (!this.errors.length) {
        return true;
      }

      return false;
    },
    async onSubmit(event) {
      let self = this;
      if (this.checkForm() === false) {
        self.showAlert();
        event.preventDefault();
      }

      const data = {
        username: this.form.email,
        password: this.form.password,
      };
      await authrepo
        .login(data)
        .then(function (response) {
          if (response.data.success == true) {
            self.errors = [];
            localStorage.setItem("accessToken", response.data.data.token);
            localStorage.setItem(
              "currentUser",
              JSON.stringify(response.data.data.user)
            );
            self.$store.state.permission = getPermission();

            self.$router.go("/dashboard/home");
          }
          if(response.data.success == false){
            self.errors.push("Invalid username or password. Please check again.");
          } 
        })
        .catch((error) => {
          console.log(error);
        });
    },
    onReset(event) {
      event.preventDefault();
      // Reset our form values
      this.form.email = "";
      this.form.name = "";
      this.form.food = null;
      this.form.checked = [];
      // Trick to reset/clear native browser form validation state
      this.show = false;
      this.$nextTick(() => {
        this.show = true;
      });
    },
    countDownChanged(dismissCountDown) {
      this.dismissCountDown = dismissCountDown;
    },
    showAlert() {
      this.dismissCountDown = this.dismissSecs;
    },
  },
};
</script>
<style scoped>
#input-group-1 {
  margin-top: 30px;
}
.form-login {
  padding: 30px;
}
.button-login {
  margin-top: 30px;
  width: 100%;
  font-weight: 600;
}
.error-title {
  color: red;
}
::placeholder {
  /* Chrome, Firefox, Opera, Safari 10.1+ */
  color: #d2d2d2;

  opacity: 0.6; /* Firefox */
}
#input-1 {
  border: none;
  border-bottom: 1px solid #1890ff;
  border-radius: 0;
}
#input-1:focus {
  box-shadow: none;
  border-color: none;
  color: black;
}
[placeholder]:focus::-webkit-input-placeholder {
  transition: text-indent 0.4s 0.4s ease;
  text-indent: -100%;
  opacity: 1;
}
.error{
  color: red;
  margin-top: 20px;

}
</style>
