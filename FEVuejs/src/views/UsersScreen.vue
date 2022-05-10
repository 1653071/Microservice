<template>
  <div class="screen">
    <b-row class="row">
      <div class="page-title">
        <span class="group-name">Users</span>
        <div class="operation-permission">
          <b-button
            id="show-btn"
            variant="primary"
            class="button-add"
            @click="showModal"
            ><b-icon icon="person-plus" aria-hidden="true"></b-icon> Add
            User</b-button
          >
        </div>
      </div>
    </b-row>
    <div class="filter-container input-group mb-3">
      <b-form-input
        class="filter-input"
        v-model="textfilter"
        @change="onFilterChange"
        placeholder="Enter username or email"
      ></b-form-input>
      <div class="clear-button">
        <b-button>Clear</b-button>
      </div>
    </div>
    <div class="user-table">
      <div class="d-flex justify-content-between panel-header"></div>

      <div class="userstable">
        <b-table
          hover
          :items="items"
          :fields="fields"
          class="users-table mt-3"
          id="users-table"
          :per-page="perPage"
          :current-page="currentPage"
          :filter="textfilter"
        >
          <template #cell(actions)="row">
            <b-button
              style="background-color: #464be1"
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

        <b-modal
          ref="delete-modal"
          centered
          title="Notification"
          @ok="handleDeleteOk"
        >
          <div class="d-block text-center">
            <h6>
              Confirm delete user with id {{ this.infoModal.content.id }} and
              name is {{ this.infoModal.content.name }} ?
            </h6>
          </div>
        </b-modal>
        <b-modal
          ref="update-table"
          title="Thay đổi thông tin"
          class="update"
          v-b-modal.modal-lg
          hide-footer
        >
          <div class="button" v-if="!edit">
            <b-button variant="success" v-show="!edit" @click="editform"
              >Edit</b-button
            >
            <div class="container information">
              <b-row>
                <b-col class="image">
                  <b-img
                    sizes="6em"
                    :src="infoModal.content.avatar"
                    fluid
                    alt="Fluid image"
                  ></b-img>
                </b-col>
                <b-col class="user">
                  <b-row>
                    <b-col lg="6">
                      <span class="label">Username</span>
                    </b-col>
                    <b-col lg="6" class="info">
                      <span>{{ infoModal.content.username }}</span>
                    </b-col>
                    <b-col lg="6">
                      <span class="label">Email</span>
                    </b-col>
                    <b-col lg="6" class="info">
                      <span>{{ infoModal.content.email }}</span>
                    </b-col>
                    <b-col lg="6">
                      <span class="label">Address</span>
                    </b-col>
                    <b-col lg="6" class="info">
                      <span class="info">{{ infoModal.content.address }}</span>
                    </b-col>
                    <b-col lg="6">
                      <span class="label">Phone</span>
                    </b-col>
                    <b-col lg="6" class="info">
                      <span class="info">{{ infoModal.content.phone }}</span>
                    </b-col>
                  </b-row>
                </b-col>
              </b-row>
            </div>
          </div>
          <div class="button" v-else>
            <b-form @submit="onUpdate">
              <b-button v-show="edit" v-on:click="edit = !edit"
                >Cancel</b-button
              >
              <b-button class="button-update" type="submit" variant="primary"
                ><b-spinner
                  small
                  variant="light"
                  class="loading"
                  v-show="loading"
                  type="submit"
                ></b-spinner
                >Update</b-button
              >
              <b-form-group
                class="update-form"
                v-show="edit"
                id="input-group-1"
                label-for="input-1"
              >
                <b-form-input
                  id="input-1"
                  v-model="updateform.username"
                  type="text"
                  placeholder="Enter email"
                  disabled="1"
                  required
                ></b-form-input>
              </b-form-group>
              <b-form-group
                v-show="edit"
                id="input-group-1"
                label-for="input-1"
              >
                <b-form-input
                  id="input-1"
                  v-model="updateform.email"
                  type="text"
                  disabled="1"
                  placeholder="Enter name"
                  required
                ></b-form-input>
              </b-form-group>
              <b-form-group
                v-show="edit"
                id="input-group-1"
                label-for="input-1"
              >
                <b-form-input
                  id="input-1"
                  v-model="updateform.phone"
                  type="number"
                  placeholder="Enter Phone"
                  required
                ></b-form-input>
              </b-form-group>
              <b-form-group
                v-show="edit"
                id="input-group-1"
                label-for="input-1"
              >
                <b-form-input
                  id="input-1"
                  v-model="updateform.address"
                  type="text"
                  placeholder="Enter address"
                  required
                ></b-form-input>
              </b-form-group>
            </b-form>
          </div>
        </b-modal>
      </div>
      <div
        class="mt-3 pagination-user"
        style="float: right; padding-right: 20px"
      >
        <b-pagination
          v-model="currentPage"
          :total-rows="rows"
          :per-page="perPage"
          aria-controls="users-table"
          pills
          page-class="customPagination"
          class="customPagination"
        ></b-pagination>
      </div>
    </div>

    <b-modal
      ref="add-modal"
      class="add-modal"
      size="lg"
      hide-footer
      hide-header
      centered
      title="Add New User"
    >
      <div class="d-block text-center container">
        <div class="add-modal-header">Add new user</div>
        <b-alert v-model="showDismissibleAlert" dismissible variant="danger">
          <div v-if="addform.errors.length > 0">
            <span v-for="error in addform.errors" :key="error">
              {{ error }}
            </span>
          </div>
        </b-alert>

        <b-form class="form-login" @submit="onSubmit">
          <b-form-group id="input-group-1" label-for="input-1">
            <b-form-input
              id="input-1"
              v-model="addform.username"
              type="text"
              placeholder="Enter username"
            ></b-form-input>
            <div class="error" v-if="addform.errors.username">
              <span> {{ addform.errors.username }}</span>
            </div>
          </b-form-group>
          <b-form-group id="input-group-1" label-for="input-1">
            <b-form-input
              id="input-1"
              v-model="addform.email"
              type="email"
              placeholder="Enter email"
            ></b-form-input>
          </b-form-group>
          <div class="error" v-if="addform.errors.email">
            <span> {{ addform.errors.email }}</span>
          </div>
          <b-form-group id="input-group-1" label-for="input-1">
            <b-form-input
              id="input-1"
              v-model="addform.password"
              type="password"
              placeholder="Enter password"
            ></b-form-input>
          </b-form-group>
          <div class="error" v-if="addform.errors.password">
            <span> {{ addform.errors.password }}</span>
          </div>
          <b-form-group id="input-group-1" label-for="input-1">
            <b-form-input
              id="input-1"
              v-model="addform.phone"
              type="number"
              placeholder="Enter phone"
            ></b-form-input>
          </b-form-group>

          <b-form-group id="input-group-1" label-for="input-1">
            <b-form-input
              id="input-1"
              v-model="addform.address"
              type="text"
              placeholder="Enter address"
            ></b-form-input>
          </b-form-group>
          <b-form-group id="input-group-1" label-for="input-1">
            <b-form-input
              id="input-1"
              v-model="addform.avatar"
              type="text"
              placeholder="Enter avatar"
            ></b-form-input>
          </b-form-group>

          <b-button class="add-user-button" type="submit" variant="primary"
            ><b-spinner
              small
              variant="light"
              class="loading"
              v-show="loading"
            ></b-spinner
            >Confirm Add</b-button
          >
        </b-form>
      </div>
    </b-modal>
  </div>
</template>
<script>
import Sidebar from "../components/menu/Sidebar.vue";
import Modal from "../components/modal/Modal.vue";
import UsersTable from "../components/table/UsersTable.vue";
import AddUserButton from "../components/button/AddUserButton.vue";
import PageHeader from "../components/header/PageHeader.vue";
import TableHeader from "../components/header/TableHeader.vue";
import AddUserForm from "../components/form/AddUserForm.vue";
import Navigation from "../components/navigation/Navigation.vue";
import repository from "../service/repo/repository";
import JjUploader from "jj-uploader";

export default {
  name: "UsersScreen",

  components: {
    "jj-uploader": JjUploader,
    Sidebar,
    Modal,
    UsersTable,
    AddUserButton,
    PageHeader,
    TableHeader,
    AddUserForm,
    Navigation,
  },
  data() {
    return {
      showDismissibleAlert: false,
      imageData: [],
      username: "",
      email: "",
      password: "",
      textfilter: "",
      addform: {
        email: "",
        username: "",
        password: "",
        address: "",
        phone: null,
        avatar: null,
        errors: [],
      },
      dismissSecs: 5,
      dismissCountDown: 0,
      loading: false,
      fields: [
        {
          key: "username",
          thClass: "h-table",
          thStyle: { backgroundColor: "#FCFCFC", border: "1px" },
          tdStyle: {},
        },
        {
          key: "email",
          thClass: "h-table",
          thStyle: { backgroundColor: "#FCFCFC", border: "1px" },
        },
        {
          key: "actions",
          thClass: "h-table",
          thStyle: { backgroundColor: "#FCFCFC", border: "1px" },
        },
      ],
      items: [],
      infoModal: {
        id: 0,
        title: "",
        content: {},
      },
      updateform: {
        email: "",
        username: "",
        password: "",
        address: "",
        phone: 0,
        avatar: "",
      },
      form: {
        id: 0,
        email: "",
        username: "",
        password: "",
      },
      perPage: 7,
      currentPage: 1,
      edit: false,
    };
  },
  created() {
    this.getAllUser();
  },
  computed: {
    rows() {
      return this.items.length;
    },
  },
  watch: {},
  methods: {
    onFilterChange: function () {
      this.currentPage = 1;
    },
    checkForm: function () {
      this.addform.errors = [];
      const filter =
        /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
      if (this.addform.email == "") {
        this.addform.errors.push("Email required.");
      } else {
        if (!filter.test(this.addform.email)) {
          this.addform.errors.push("Invalid Email.");
        }
      }
      if (this.addform.password == "") {
        this.addform.errors.push("Password required.");
      }
      else{
        if (this.addform.password.length < 6 ) {
        this.addform.errors.push("Password required less than 6 words.");
      }
      }

      if (this.addform.phone == null) {
        this.addform.errors.push("Phone required.");
      } else {
        if (this.addform.phone.toString().length != 10) {
          this.addform.errors.push("Phone required 10 number.");
        }
      }
      if (this.addform.address == "") {
        this.addform.errors.push("Address required.");
      }

      if (!this.addform.errors.length) {
        return true;
      }

      return false;
    },

    errorOccured(event) {
      console.log(event);
    },
    success(event) {
      console.log(event);
    },
    previewImage(event, img) {
      this.imageData = event.target.files[0];
    },
    showViewCreateUser() {
      this.modalCreateUser = true;
    },
    showModal() {
      this.$refs["add-modal"].show();
    },
    hideModal() {
      this.$refs["add-modal"].hide();
    },
    showModalUpdate() {
      this.$refs["update-table"].show();
    },
    hideModalUpdate() {
      this.$refs["update-table"].hide();
    },
    showModalDelete() {
      this.$refs["delete-modal"].show();
    },
    hideModalDelete() {
      this.$refs["delete-modal"].hide();
    },
    countDownChanged(dismissCountDown) {
      this.dismissCountDown = dismissCountDown;
    },
    onSubmit: async function (event) {
      let self = this;
      if (self.checkForm() == false) {
        self.showDismissibleAlert = true;
        event.preventDefault();
        return false;
      }
      self.showLoading();

      const data = {
        Email: this.addform.email,
        Username: this.addform.username,
        Password: this.addform.password,
        Role: 3,
        Avatar: this.addform.avatar,
        Phone: this.addform.phone,
        Address: this.addform.address,
      };
      data.Phone = data.Phone.toString();
      await repository
        .post(`https://localhost:44334/user/list-user/create`, data)
        .then(function (response) {
          if (response.data.success == true) {
            self.hideModal();
            self.getAllUser();
            self.hideLoading();
            self.resetAddForm();
          } else {
          }
        })
        .catch((error) => {
          console.log(error);
          self.hideModal();
        });
    },
    showLoading: function (event) {
      this.loading = true;
    },

    hideLoading() {
      this.loading = false;
    },
    resetAddForm() {
      this.addform.username = "";
      this.addform.email = "";
      this.addform.password = "";
      this.addform.phone = "";
      this.addform.address = "";
      this.addform.avatar = "";
    },
    async getAllUser() {
      //get all user in database
      let self = this;
      await repository
        .get("/user/list-user")
        .then(function (response) {
          self.items = response.data.data;
          console.log("jdhjf");
        })
        .catch((error) => {
          console.log(error);
        });
    },
    async handleDeleteOk(event) {
      let self = this;
      self.showLoading();

      const data = JSON.stringify({ id: self.infoModal.content.id });
      await repository
        .delete(`/user/list-user/delete`, { data: data })
        .then(function (response) {
          self.getAllUser();
          self.hideModalDelete();

          self.hideLoading();
        })
        .catch((error) => {
          console.log(error);
        });
    },
    editform() {
      this.updateform.username = this.infoModal.content.username;
      this.updateform.email = this.infoModal.content.email;
      this.updateform.phone = this.infoModal.content.phone;
      this.updateform.address = this.infoModal.content.address;
      this.updateform.avatar = this.infoModal.content.avatar;
      this.edit = !this.edit;
    },
    info(index, item, button) {
      this.infoModal.content = item;
      this.showModalDelete();
    },
    async onUpdate(event) {
      event.preventDefault();
      const self = this;
      self.showLoading();

      let data = {};
      data.id = this.infoModal.content.id;
      data.username = this.updateform.username;
      data.email = this.updateform.email;
      data.phone = this.updateform.phone.toString();
      data.address = this.updateform.address;
      data.avatar = this.updateform.avatar;

      await repository
        .put(`/user/list-user/update`, data)
        .then(function (response) {
          if (response.status == 200 || response.status == 204) {
            self.getAllUser();
            self.hideLoading();
            self.hideModalUpdate();
            self.edit = !self.edit;
          }
        })
        .catch((error) => {
          console.log(error);
          self.hideModal();
        });
    },
    update(index, item, button) {
      this.infoModal.content = item;
      this.showModalUpdate();
    },
  },
};
</script>
<style scoped>
.screen {
  padding: 0px 10px;
}
.screen .row {
  padding: 0;
  margin: 0;
}
.screen .row .operation-permission {
  float: right;
}
.page-title {
  padding: 20px 20px;
  width: 100%;
  background-color: #64c5b1;
  margin-bottom: 20px;
  font-family: Verdana, sans-serif;
  font-size: 20px;
  font-weight: 700;
  background-image: linear-gradient(to right, #464be1, #6c40f7);
}
.page-title .group-name {
  color: whitesmoke;
  font-weight: 500;
}
.col {
  padding: 0;
}
.user-table {
  border-radius: 10px;
  background-color: white;
  box-shadow: 0px 10px 10px #888888;
  padding-bottom: 50px;
  margin-top: 30px;
  border: none;
  height: 700px;
  padding-right: 20px;
  padding-left: 20px;
}
::v-deep .customPagination .page-link {
  background: #1e1e1e;
  color: white;
  border: #363636 1px solid;
}

.card-user {
  height: 200px;
}
.button-col {
  text-align: left;
}
.panel-header {
  padding: 24px;
}
#input-group-1 {
  margin-bottom: 30px;
}
.form-login {
  padding: 10px;
}
.button-login {
  margin-top: 30px;
  width: 100%;
}
.button-add {
  font-size: 14px;
  background-color: #4045ca;
  border: none;
}

.button-add:hover {
  transform: scale(1.1, 1.1);
  -webkit-transform: scale(1.1, 1.1);
  -moz-transform: scale(1.1, 1.1);
}
#input-1 {
  border: none;
  border-radius: 0px;
  border-bottom: 1px solid #e6e6e6;
  transition: border-width 0.1s linear;
}
#input-1:focus {
  box-shadow: none;

  border-bottom: 3px solid #4045ca;
}
.loading {
  margin-right: 10px;
}
.h-table {
  border: none;
  background-color: yellow !important;
}

.mr-1 {
  margin-right: 20px;
}
.mr-1:hover {
  transform: scale(1.3, 1.3);
  -webkit-transform: scale(1.3, 1.3);
  -moz-transform: scale(1.3, 1.3);
}
#update-table .button {
  margin-right: 20px;
  margin-bottom: 20px;
}
.add-user-button {
  background-color: #4045ca;
}
.error {
  color: red;
}
.add-modal-header {
  font-size: 30px;
  font-weight: 700;
}
[placeholder]:focus::-webkit-input-placeholder {
  transition: text-indent 0.4s 0.4s ease;
  text-indent: -100%;
  opacity: 1;
}
::placeholder {
  /* Chrome, Firefox, Opera, Safari 10.1+ */
  color: #e6e6e6;
  opacity: 1; /* Firefox */
  font-weight: 600;
}
.information {
  margin-top: 20px;
  padding: 20px;
}

.information .user {
  padding: 0px 20px;
}
.information .label {
  font-weight: 600;
  margin-bottom: 10px;
}
.information .info {
  overflow-wrap: break-word;
  margin-bottom: 10px;
}
.update-form {
  margin-top: 10px;
}
.users-table {
}
.userstable {
  height: 500px;
}
.filter-container {
  background-color: white;
  padding: 20px 20px;
  border-radius: 10px;
  width: 100%;
}
.filter-container .filter-input {
  float: left;
  width: 80%;
}
.filter-container .clear-button {
  width: 10%;
  padding-left: 10px;
}
.filter-container .clear-button button {
  width: 100%;
}

.filter-container .filter-input:focus {
  box-shadow: none;
}
</style>
