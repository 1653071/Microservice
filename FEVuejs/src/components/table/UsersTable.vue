<template>
  <div class="userstable container">
    <b-table
       
      hover
      :items="items"
      :fields="fields"
      class="users-table"
      id="users-table"
      :per-page="perPage"
      :current-page="currentPage"
    >
      <template #cell(actions)="row">
        <b-button
          variant="info"
          size="sm"
          @click="update(row.index, row.item, $event.target)"
          class="mr-1"
        >
          <b-icon icon="pen" aria-hidden="true"></b-icon>
        </b-button>
        <b-button
          variant="danger"
          size="sm"
          @click="info(row.index, row.item, $event.target)"
          class="mr-1"
        >
          <b-icon icon="trash" aria-hidden="true"></b-icon>
        </b-button>

        <!-- As `row.showDetails` is one-way, we call the toggleDetails function on @change -->
      </template>
    </b-table>
    <b-pagination
      v-model="currentPage"
      :total-rows="rows"
      :per-page="perPage"
      aria-controls="users-table"
    ></b-pagination>
    <b-modal :id="infoModal.id" title="Notification" @ok="handleDeleteOk">
      <div class="d-block text-center">
        <h4>Confirm Delete User {{ this.infoModal.content.id }} ?</h4>
      </div>
    </b-modal>
    <b-modal id="update-table" title="Thay đổi thông tin" class="update">
      <b-form class="update-form" @submit="onUpdate">
        <b-form-group id="input-group-1" label-for="input-1">
          <b-form-input
            id="input-1"
            v-model="form.email"
            type="email"
            placeholder="Enter email"
            required
          ></b-form-input>
        </b-form-group>
        <b-form-group id="input-group-1" label-for="input-1">
          <b-form-input
            id="input-1"
            v-model="form.username"
            type="text"
            placeholder="Enter name"
            required
          ></b-form-input>
        </b-form-group>

        <b-button type="submit" variant="">Update</b-button>
      </b-form>
    </b-modal>
  </div>
</template>

<script>

import repository from "../../service/repo/repository";
export default {
  name: "UsersTable",
  components: {},
  data() {
    return {
      fields: [
        { key: "username", thClass: "bg-white text-dark" },
        { key: "email", thClass: "bg-blue text-dark" },
        { key: "actions", thClass: "bg-white text-dark" }
      ],
      items: [],
      infoModal: {
        id: "info111",
        title: "",
        content: {}
      },
      form: {
        id: 0,
        email: "",
        username: "",
        password: ""
      },
      perPage: 5,
      currentPage: 1
    };
  },
  created() {
    repository
      .get(`/user/list-user`)
      .then(response => {
        this.items = response.data.data;
      })
      .catch(e => {
        this.errors.push(e);
      });
  },
  computed: {
      rows() {
        return this.items.length
      }
  },

  methods: {
    async getAllUser() {
      //get all user in database

      await repository
        .get("/user/list-user")
        .then(function(response) {
          this.items = response.data.data;
          console.log("jdhjf");
        })
        .catch(error => {
          console.log(error);
        });
    },
    async handleDeleteOk() {
      let self = this;
      const data = { Id: parseInt(this.infoModal.content.id) };
      await repository
        .delete(`/user/list-user/delete`, { data: data })
        .then(function(response) {
          self.getAllUser();
        })
        .catch(error => {
          console.log(error);
        });
    },
    info(index, item, button) {
      this.infoModal.content = item;
      this.$root.$emit("bv::show::modal", this.infoModal.id, button);
    },
    onUpdate() {},
    update(index, item, button) {
      this.$root.$emit("bv::show::modal", "update-table", button);
    }
  }
};
</script>
<style>
.users-table th {
  background-color: #ccccd0 !important;
}
.mr-1 {
  margin-right: 20px;
}
.mr-1:hover {
  transform: scale(1.3, 1.3);
  -webkit-transform: scale(1.3, 1.3);
  -moz-transform: scale(1.3, 1.3);
}
</style>
