<template>
  <div class="welcome-page">
    <div class="page-title">
      <span class="group-name">Profile</span>
    </div>

    <div class="information-container">
      <b-card class="inforcation-card" no-body style="max-width: 100%">
        <b-row no-gutters style="height: 100%">
          <b-col
            md="4"
            style="
              height: 100%;
              display: flex;
              align-items: center;
              justify-content: center;
            "
          >
            <div class="avatar" style="height: 100%">
              <b-avatar text="BV" size="10em" style="margin: auto"></b-avatar>
              <div class="username" style="text-align: center">
                {{ user.username }}
              </div>
            </div>
          </b-col>
          <b-col md="8" style="padding:20px 20px">
            <b-row>
              <b-col lg="6" class="col">
                <span class="label">Email</span>
              </b-col>
              <b-col lg="6" class="col"> 
                <span class="email">{{ user.email }}</span>
              </b-col>
              <b-col lg="6" class="col">
                <span class="label">Phone</span>
              </b-col>
              <b-col lg="6" class="col">
                <span class="phone">{{ user.phone }}</span>
              </b-col>
              <b-col lg="6" class="col">
                <span class="label">Address:</span>
              </b-col>
              <b-col lg="6" class="col">
                <span class="address">{{ user.address}}</span>
              </b-col>
              
            </b-row>
          </b-col>
        </b-row>
      </b-card>
    </div>
  </div>
</template>

<script>
import { getUser } from "../service/repo/repository";
export default {
  name: "HomeScreen",
  data() {
    return {
      user: getUser(),
    };
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
.welcome-page {
  height: 100%;
  padding: 0px 20px;
}
.information-container {
  width: 100%;
  padding: 0px 20px;
 
}
.inforcation-card{
  padding: 20px 20px;
}
.avatar {
  padding-top: 20px;
}

.avatar .username {
  color: black;
  font-weight: 700;
  padding-top: 20px;
}
.col{
  margin-bottom: 30px;
}
.label{
  color: #D5D5D5;
}
.email, .phone, .address{
  font-weight: 600;
}
</style>