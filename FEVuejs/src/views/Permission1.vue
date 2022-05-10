<template>
  <div class="permission-screen">
    <b-row class="row">
      <div class="page-title">
        <b-button class="group-button" @click="showGroupModal"
          ><b-icon icon="people" scale="1.5"></b-icon
        ></b-button>
        <span class="group-name">{{
          group.groupname
            ? `${
                group.groupname.charAt(0).toUpperCase() +
                group.groupname.slice(1)
              }`
            : `Please select 1 group`
        }}</span>

        <div v-if="!isEmpty(group)" class="operation-permission">
          <b-button class="create-permission" @click="showUsersModal"
            >Add person</b-button
          >
        </div>
        <div v-else class="operation-permission">
          <b-button class="create-permission" @click="showAddGroupsModal"
            >Add group</b-button
          >
        </div>
      </div>
    </b-row>
    <b-row class="row">
      <b-col cols="12" sm="9">
        <b-overlay :show="spinning1" rounded="sm">
          <div class="user-in-group">
            <div class="header-container">
              <div class="header">
                <span class="icon-header"><b-icon icon="person"></b-icon></span
                >Users in groups
              </div>
            </div>
            <div class="row-user" style="margin-top: 20px">
              <b-row>
                <b-col
                  class="user"
                  v-for="item in itemsForList"
                  :key="item.user.id"
                  lg="4"
                >
                  <b-card
                    no-body
                    class="overflow-hidden user-card"
                    style="max-width: 540px"
                  >
                    <b-row no-gutters>
                      <b-col
                        lg="5"
                        style="
                          display: flex;
                          justify-content: center;
                          position: relative;
                        "
                      >
                        <b-avatar
                          :src="item.user.avatar"
                          size="5em"
                          style="
                            position: absolute;
                            top: 50%;
                            left: 50%;
                            transform: translate(-50%, -50%);
                          "
                        ></b-avatar>
                      </b-col>
                      <b-col lg="5">
                        <b-card-body>
                          <div class="user-name">
                            {{
                              item.user.username.charAt(0).toUpperCase() +
                              item.user.username.slice(1)
                            }}
                          </div>
                          <b-card-text class="user-email">
                            {{ item.user.email }}
                          </b-card-text>
                        </b-card-body>
                      </b-col>
                      <b-col class="col-delete" lg="2">
                        <div
                          class="remove-button"
                          @click="
                            removeUserFromGroup(item.user.id, group.groupid)
                          "
                        >
                          <b-icon icon="trash" aria-hidden="true"></b-icon>
                        </div>
                      </b-col>
                    </b-row>
                  </b-card>
                </b-col>
              </b-row>
            </div>
            <div
              class="mt-3 pagination-user"
              style="float: right; padding-right: 20px"
            >
              <b-pagination
                class="customPagination"
                v-model="currentPage"
                pills
                :total-rows="rows"
                :per-page="perPage"
                aria-controls="row-user"
              ></b-pagination>
            </div>
          </div>
        </b-overlay>
      </b-col>
      <b-col cols="12" sm="3">
        <b-overlay :show="spinning1" rounded="sm">
          <div class="permission-in-group">
            <div class="permission-in-group-box">
              <div class="header-container">
                <div class="header">
                  <span class="icon-header"><b-icon icon="key"></b-icon></span>
                  Permission
                </div>
              </div>
              <div
                class="permission-card-box"
                v-for="item in permisioningroup"
                :key="item.per.perid"
              >
                <div class="permission-card">
                  <div class="permission-card-name">
                    {{
                      item.per.pername.charAt(0).toUpperCase() +
                      item.per.pername.slice(1)
                    }}
                  </div>
                  <div
                    class="permission-card-remove"
                    @click="removePermission(item.gr.groupid, item.per.perid)"
                  >
                    <p class="h4">
                      <b-icon
                        class="icon-remove"
                        icon="plus-circle-fill"
                        variant="secondary"
                        style="float: right; color: #COCOCO"
                      ></b-icon>
                    </p>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </b-overlay>
        <b-overlay :show="spinning1" rounded="sm">
          <div class="permission-not-in-group">
            <div class="permission-in-group-box">
              <div class="header-container">
                <div class="header">
                  <span class="icon-header"
                    ><b-icon icon="x-circle"></b-icon></span
                  >Not Permission
                </div>
              </div>
              <div
                class="permission-card-box"
                v-for="item in permissionnotingroup"
                :key="item.per.perid"
              >
                <div class="permission-card1">
                  <div class="permission-card-name">
                    {{
                      item.per.pername.charAt(0).toUpperCase() +
                      item.per.pername.slice(1)
                    }}
                  </div>
                  <div class="permission-card-add">
                    <p class="h4">
                      <b-icon
                        icon="plus-circle-fill"
                        class="icon-add"
                        style="float: right; color: #6006a8"
                        @click="addPermission(group.groupid, item.per.perid)"
                      ></b-icon>
                    </p>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </b-overlay>
      </b-col>
    </b-row>
    <b-modal
      centered
      ref="modal-group"
      hide-footer
      title="Select groups"
      size="xl"
    >
      <div class="d-block">
        <b-row>
          <b-col
            cols="6"
            class="col-group"
            v-for="item in groups"
            :key="item.id"
          >
            <b-card class="group-card">
              <b-row no-gutters>
                <b-card-body>
                  <b-row>
                    <b-col md="12">
                      <p class="group-name">{{ item.groupname }}</p>
                      <p class="group-note">{{ item.note }}</p>
                    </b-col>
                    <b-col md="6">
                      <b-button
                        @click="select(item)"
                        class="delete-group"
                        variant="primary"
                        >Delete</b-button
                      >
                    </b-col>
                    <b-col md="6">
                      <b-button
                        @click="select(item)"
                        class="select-group"
                        variant="primary"
                        >Select</b-button
                      >
                    </b-col>
                  </b-row>
                </b-card-body>
              </b-row>
            </b-card>
          </b-col>
        </b-row>
      </div>
    </b-modal>
    <b-modal
      id="modal-xl modal-user"
      ref="modal-user"
      hide-footer
      title="Users"
      size="xl"
      centered
    >
      <b-overlay :show="spinnerloading" rounded="sm">
        <b-container class="bv-example-row">
          <b-row>
            <b-col
              class="modal-col"
              v-for="item in usernotingroup"
              :key="item.id"
              lg="4"
            >
              <b-card no-body class="overflow-hidden" style="max-width: 540px">
                <b-row no-gutters>
                  <b-col
                    md="6"
                    style="
                      display: flex;
                      justify-content: center;
                      position: relative;
                    "
                  >
                    <b-avatar
                      :src="item.user.avatar"
                      size="6em"
                      style="
                        position: absolute;
                        top: 50%;
                        left: 50%;
                        transform: translate(-50%, -50%);
                      "
                    ></b-avatar>
                  </b-col>
                  <b-col md="6">
                    <b-card-body>
                      <div class="modal-name">
                        {{
                          item.user.username.charAt(0).toUpperCase() +
                          item.user.username.slice(1)
                        }}
                      </div>
                      <b-card-text> This is </b-card-text>
                      <b-button
                        class=""
                        @click="addUserToGroup(item.user.id, group.groupid)"
                        style="
                          background-color: #c4c15a;
                          border: none;
                          text-transform: uppercase;
                          font-weight: 700;
                        "
                        >Add</b-button
                      >
                    </b-card-body>
                  </b-col>
                </b-row>
              </b-card>
            </b-col>
          </b-row>
        </b-container>
      </b-overlay>
    </b-modal>
    <b-modal
      id="modal-1 modal-user"
      ref="modal-add-group"
      class="modal-add-group"
      hide-footer
      hide-header
      title="Groups"
      size="xl"
      centered
    >
      <b-overlay :show="spinnerloading" rounded="sm">
        <div class="d-block text-center container">
          <div class="add-modal-header">Add new group</div>

          <b-form class="form-login" @submit="onAddGroupSubmit">
            <b-form-group id="input-group-1" label-for="input-1">
              <b-form-input
                id="input-1"
                v-model="groupname"
                type="text"
                placeholder="Enter group"
                required
              ></b-form-input>

              <div class="d-flex">
                <div>
                  <span class="error" v-if="addform.errors.groupname">{{
                    addform.errors.groupname
                  }}</span>
                </div>
              </div>
            </b-form-group>
            <b-form-group id="input-group-1" label-for="input-1">
              <b-form-input
                id="input-1"
                v-model="note"
                type="text"
                placeholder="Enter note"
                required
              ></b-form-input>

              <div class="d-flex">
                <div>
                  <span class="error" v-if="addform.errors.note">{{
                    addform.errors.note
                  }}</span>
                </div>
              </div>
            </b-form-group>

            <b-button class="" type="submit" variant="primary"
              ><b-spinner
                small
                variant="light"
                class="loading"
                v-show="loading"
              ></b-spinner
              >Add</b-button
            >
          </b-form>
        </div>
      </b-overlay>
    </b-modal>
  </div>
</template>
<script>
import repository, { Color } from "../service/repo/repository";

import userrepo from "../service/impl/userrepo";
export default {
  name: "Permission",
  data() {
    return {
      mes: "aaaa1111",
      perPage: 6,
      currentPage: 1,
      groupname: "",
      note: "",
      addform: {
        groupname: null,
        active: true,
        note: null,
        errors: [],
      },
      groups: [],
      group: {},
      useringroup: [],
      useringroupofpage: [],
      loading: false,
      spinnerloading: false,
      permisioningroup: [],
      permissionnotingroup: [],
      usernotingroup: [],
      spinning1: true,
      color: Color,
    };
  },
  created() {
    this.getAllGroup();
  },
  computed: {
    mes1() {
      this.mes = "dsadasdasdasdasdsa";
      return this.mes + "1222222";
    },
    rows() {
      return this.useringroup.length;
    },
    itemsForList() {
      return this.useringroup.slice(
        (this.currentPage - 1) * this.perPage,
        this.currentPage * this.perPage
      );
    },

    randomColor() {
      let rand = Math.random() * 3;

      return Math.floor(rand);
    },
  },
  watch: {
    groupname(value) {
      // binding this to the data value in the email input
      this.addform.groupname = value;
      this.validateGroupname(value);
    },
    note(value) {
      this.addform.note = value;
      this.validateNote(value);
    },
  },
  methods: {
    validateNote(value) {
      if (value.length < 10) {
        this.addform.errors["note"] = "Note must have less than 10 words";
      } else {
        this.addform.errors["note"] = null;
      }
    },
    validateGroupname(value) {
      if (value.length > 0) {
        this.addform.errors["groupname"] = null;
      } else {
        this.addform.errors["groupname"] = "Note must have not blank";
      }
    },

    async getAllGroup() {
      let self = this;
      await repository
        .get("/group")
        .then(function (response) {
          self.groups = response.data.data;
        })
        .catch((error) => {
          console.log(error);
        });
    },
    isEmpty(obj) {
      for (let key in obj) {
        if (obj.hasOwnProperty(key)) return false;
      }
      return true;
    },
    showGroupModal() {
      this.$refs["modal-group"].show();
    },
    showUsersModal() {
      let self = this;
      repository
        .get(`userpermission/getUserNotInGroup/${this.group.groupid}`)
        .then((res) => {
          self.usernotingroup = res.data.data;
        })
        .catch((e) => {});
      this.$refs["modal-user"].show();
    },
    hideUserModal() {
      this.$refs["modal-user"].hide();
      this.spinnerloading = false;
    },
    showAddGroupsModal() {
      this.$refs["modal-add-group"].show();
    },
    hideAddGroupModal() {
      this.$refs["modal-add-group"].hide();
    },
    hideModal() {
      this.$refs["modal-group"].hide();
    },
    toggleModal() {
      // We pass the ID of the button that we want to return focus to
      // when the modal has hidden
      this.$refs["my-modal"].toggle("#toggle-btn");
    },
    select(item) {
      let self = this;
      this.$refs["modal-group"].hide();
      this.group = item;

      Promise.all([
        repository.get(`/userpermission/getUserInGroup/${item.groupid}`),
        repository.get(`/permission/getPerMissionByGroupId/${item.groupid}`),
        repository.get(`/permission/getPerMissionNotInGroup/${item.groupid}`),
      ]).then((res) => {
        self.useringroup = res[0].data.data;
        self.permisioningroup = res[1].data.data;
        self.permissionnotingroup = res[2].data.data;
        self.useringroupofpage = self.useringroup.slice(
          (self.currentPage - 1) * self.perPage,
          self.perPage
        );
        self.spinning1 = false;

        self.hideModal();
      });
    },
    onAddGroupSubmit() {
      let self = this;
      if (
        this.addform.errors["groupname"] != null ||
        this.addform.errors["note"] != null
      ) {
        return false;
      }
      const data = {
        groupname: self.addform.groupname,
        active: self.addform.active,
        note: self.addform.note,
      };
      repository
        .post(`group`, data)
        .then((res) => {
          self.hideAddGroupModal();
        })
        .catch((e) => {});
    },

    addPermission(groupid, perid) {
      let self = this;
      const data = {
        groupid: groupid,
        perid: perid,
      };
      repository
        .post(`grouppermission`, data)
        .then((res) => {
          Promise.all([
            repository.get(
              `/permission/getPerMissionByGroupId/${self.group.groupid}`
            ),
            repository.get(
              `/permission/getPerMissionNotInGroup/${self.group.groupid}`
            ),
          ]).then((res) => {
            self.permisioningroup = res[0].data.data;
            self.permissionnotingroup = res[1].data.data;
          });
        })
        .catch((e) => {});
    },
    removePermission(groupid, perid) {
      let self = this;
      const data = {
        groupid: groupid,
        perid: perid,
      };
      repository
        .post(`/grouppermission/removeGroupPermission`, data)
        .then((res) => {
          Promise.all([
            repository.get(
              `/permission/getPerMissionByGroupId/${self.group.groupid}`
            ),
            repository.get(
              `/permission/getPerMissionNotInGroup/${self.group.groupid}`
            ),
          ]).then((res) => {
            self.permisioningroup = res[0].data.data;
            self.permissionnotingroup = res[1].data.data;
          });
        })
        .catch((e) => {});
    },
    removeUserFromGroup(id, groupid) {
      let self = this;
      let data = {
        usid: id,
        groupid: groupid,
      };
      repository.post(`userpermission/remove`, data).then((res) => {
        userrepo.getAllUserInGroup(self.group.groupid).then((res) => {
          self.useringroup = res.data.data;
        });
      });
    },
    addUserToGroup(id, groupid) {
      let self = this;
      self.spinnerloading = true;
      let data = {
        usid: id,
        groupid: groupid,
      };
      repository.post(`userpermission/insert`, data).then((res) => {
        userrepo.getAllUserInGroup(self.group.groupid).then((res) => {
          self.useringroup = res.data.data;
          self.spinnerloading = false;
          self.hideUserModal();
        });
      });
    },
  },
};
</script>

<style scoped>
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
.page-title .group-button {
  background-color: #407cf7;
  color: white;
  border: none;
  margin-right: 10px;
}
.page-title .group-button:hover {
  filter: brightness(150%);
}

.permission-screen {
  padding: 0px 10px;
}
.permission-screen .row {
  padding: 0;
  margin: 0;
}
.user-in-group .header,
.permission-in-group .header,
.permission-not-in-group .header {
  font-size: 20px;
  padding: 10px;
  color: white;
  font-weight: 500;
  font-family: "Roboto", sans-serif;
}
.user-in-group .icon-header,
.permission-in-group .icon-header,
.permission-not-in-group .icon-header {
  padding-right: 10px;
}
.user-in-group .header-container {
  background-color: #3ea0ed;
  width: 100%;
  padding: 10px;
  border-radius: 10px 10px 0px 0px;
  background-image: linear-gradient(to right, #3ea0ed, #68bbf0);
}
.permission-not-in-group .header-container {
  background-color: #a0a0a0;
  width: 100%;
  padding: 10px;
  border-radius: 10px 10px 0px 0px;
  background-image: linear-gradient(to right, #a0a0a0, #b7b7b7);
}

.permission-in-group .header-container {
  width: 100%;
  padding: 10px;
  border-radius: 10px 10px 0px 0px;
  background-image: linear-gradient(to right, #973eed, #464be1);
}

.permission-in-group {
  width: 100%;
  height: 400px;
  padding-bottom: 10px;
  border-radius: 20px;
}
.permission-in-group-box {
  background-color: white;
  box-shadow: 0px 10px 10px #888888;
  height: 100%;
  border-radius: 10px;
}
.permission-not-in-group {
  width: 100%;
  padding-top: 10px;
  border-radius: 10px;
  height: 400px;
}
.permission-screen .user-in-group {
  width: 100%;
  height: 800px;
  box-shadow: 0px 10px 10px #888888;
  background-color: white;
  margin: 0 auto;
  border-radius: 10px;
}
.row-user {
  height: 600px;
  width: 100%;
}
.permission-card-box {
  padding: 10px;
}
.permission-card {
  padding: 10px;
  background-color: #9778c4;
  display: flex;

  color: aliceblue;
}
.permission-card1 {
  padding: 10px;
  background-color: #c0c0c0;
  display: flex;
  color: aliceblue;
}

.permission-card-name {
  width: 60%;
  float: left;
}
.permission-card-add,
.permission-card-remove {
  width: 40%;
  float: right;
  text-align: center;
}
.icon-add:hover,
.icon-remove:hover {
  filter: brightness(150%);
}

.operation-permission {
  float: right;
}
.operation-permission .create-permission {
  background-color: #3ea0ed;
  border: none;
}
.operation-permission .create-permission:hover {
  filter: brightness(150%);
}
.operation-permission .add-user {
  background-color: #f5c927;
  border: none;
}
.operation-permission .add-user:hover {
  filter: brightness(120%);
}
.group-card {
  background-color: #0fa8a0;
  margin-top: 10px;
}
.group-card .group-name {
  font-size: 18px;

  color: white;
  font-family: Verdana, sans-serif;
  font-size: 20px;
  font-weight: 500;
}
.group-card .group-note {
  color: white;
}
.user-card {
  border-radius: 10px;
  width: 100%;

  text-align: center;
  box-shadow: 0px 1px 10px #888888;
  margin-bottom: 30px;
}
.user-card:hover {
  box-shadow: 2px 2px 10px #888888;
  filter: brightness(105%);
}
.user .user-name {
  font-weight: 900;
  font-size: 18px;
  overflow: hidden;
  width: 100%;
  height: 30px;
}
.user .user-email {
  overflow: hidden;
  width: 100%;
  height: 30px;
}
.user .remove-button {
  display: flex;
  justify-content: center;
  align-items: center;
  background-color: red;
  color: black;
  width: 100%;
  height: 100%;
  cursor: pointer;
  transition: font-size 1s;
}
.user .remove-button:hover {
  font-size: 25px;
}

.col-user {
  padding: 35px;
  height: 150px;
}
.col-user .name {
  text-align: left;
  font-size: 20px;
  font-weight: 500;
  margin: 0;
  color: #6006a8;
}
.col-user .email {
  text-align: left;
  margin: 0;
  color: #c0c0c0;
}
.col-user .col-delete {
  padding: 0;
  border-radius: 10px 10px 0 0;
}
.user-card .content {
  display: inline-block;
}
.user-card .content .user-name {
  margin-top: 10px;
}

.select-group {
  background-color: white;
  width: 100%;
  color: black;
  border: none;
  font-weight: 600;
}
.delete-group {
  background-color: #03fe9e;
  width: 100%;
  color: white;
  border: none;
  font-weight: 600;
}
.spinning {
  width: 100%;
  height: 100%;
  display: flex;

  border: none;
  justify-content: center;
  align-items: center;
  text-align: center;
}
.add-user-button {
  background-color: #0fa8a0;
  border: none;
}
.spinner-loading {
  margin-right: 1em;
}
.pagination-user {
  height: 50px;
}
.customPagination > li > a {
  color: red;
}
.customPagination > li.active > a,
.customPagination > li > a:hover {
  color: white;
  background-color: green !important;
}
.avatar-div {
  display: flex;
  justify-content: center;
  align-items: center;
  padding-top: 15px;
}
.avatar {
}
@media screen and (max-width: 99px) {
  .permission-in-group {
    margin-top: 10px;
  }
  .col-user {
    height: 100px;
  }
}

.modal-name {
  font-weight: 900;
  font-size: 18px;
  overflow: hidden;
  width: 100%;
  height: 30px;
}
.modal-col {
  margin-bottom: 20px;
}

.add-modal-header {
  font-size: 30px;
  font-weight: 700;
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
.error {
  color: red;
  text-align: left;
  margin-top: 10px;
}
</style>
