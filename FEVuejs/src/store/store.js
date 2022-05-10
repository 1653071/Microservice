import Vue from 'vue'
import Vuex from 'vuex'
import axios from 'axios'

Vue.use(Vuex)

export default new Vuex.Store({
  state: {
    accessToken: null,
    permission:[]
  },
  mutations: {
    loginStart: state => state.loggingIn = true,
    loginStop: (state, errorMessage) => {
      state.loggingIn = false;
      state.loginError = errorMessage;
    },  
    updateAccessToken: (state, accessToken) => {
      state.accessToken = accessToken;
    }
  },
  getters: {
    fullName: function (state) {
      return state.permission;
    }
  }
  
})