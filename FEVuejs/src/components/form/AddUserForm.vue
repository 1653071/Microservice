<template>
  <div>
    <b-form class="form-login" @submit="onSubmit" @reset="onReset" v-if="show">
      <b-form-group
        id="input-group-1"
        label-for="input-1"
       
      >
        <b-form-input
          id="input-1"
          v-model="form.email"
          type="email"
          placeholder="Enter email"
          required
        ></b-form-input>
      </b-form-group>
      <b-form-group
        id="input-group-1"
        label-for="input-1"
       
      >
        <b-form-input
          id="input-1"
          v-model="form.name"
          type="text"
          placeholder="Enter name"
          required
        ></b-form-input>
      </b-form-group>
      <b-form-group
        id="input-group-1"
        label-for="input-1"
        
      >
        <b-form-input
          id="input-1"
          v-model="form.password"
          type="password"
          placeholder="Enter password"
          required
        ></b-form-input>
      </b-form-group>
      <b-form-group
        id="input-group-1"
        label-for="input-1"
        
      >
        <b-form-input
          id="input-1"
          v-model="form.confirmpassword"
          type="password"
          placeholder="Confirm Password"
          required
        ></b-form-input>
      </b-form-group>

     
    </b-form>
  </div>
</template>

<script>
export default {
  data() {
    return {
      form: {
        email: "",
        name: "",
        food: null,
        checked: []
      },
      foods: [
        { text: "Select One", value: null },
        "Carrots",
        "Beans",
        "Tomatoes",
        "Corn"
      ],
      show: true
    };
  },
  methods: {
    onSubmit(event) {
      event.preventDefault();
      alert(JSON.stringify(this.form));
      this.$router.push('/')
    },
    onReset(event) {
      eventeventDefault();
      // Reset.pr our form values
      this.form.email = "";
      this.form.name = "";
      this.form.food = null;
      this.form.checked = [];
      // Trick to reset/clear native browser form validation state
      this.show = false;
      this.$nextTick(() => {
        this.show = true;
      });
    }
  }
};
</script>
<style scoped>
   #input-group-1{
       margin-bottom: 30px;
   }
   .form-login{
       padding: 10px;
       
   }
   .button-login{
       margin-top: 30px ;
       width: 100%;
   }
</style>
