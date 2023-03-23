<template>
  <input v-model="user.userName" />
  <input v-model="user.password" />

  <button class="btn btn-primary" type="button" v-on:click="tryToAuthorize">Click Me!</button>
</template>

<script lang="ts">
  import User from '@/models/User';
  import axios from 'axios';
  import swal from 'sweetalert2';
  import { Vue } from 'vue-class-component';

  import appData from '../../data';

  export default class SignIn extends Vue {
    public user = User.getDefaultUser();

    public data(): User {
      this.user = User.getDefaultUser();
      return this.user;
    }

    public tryToAuthorize() {
      if (this.validateModelAndNotify()) {
        const apiQuery = axios.get(
          `${appData.apiAddress + appData.signInApiQuery}${this.user.userName}\\${this.user.password}`,
        );

        apiQuery.then((response) => {
          if (response.status === 200) {
            localStorage.setItem(appData.tokenKeyName, JSON.stringify(response.data.token));
            localStorage.setItem(appData.currentUserKeyName, JSON.stringify(this.user));

            swal.fire('Authorization is successful!');
          }
        });
        apiQuery.catch((error) => {
          if (error.response.status === 404) {
            swal.fire('Login or/and password is incorrect!');
          }
        });
      }
    }

    private validateModelAndNotify() {
      if (!this.user.userName) {
        swal.fire('Provide login to authorize.');
      } else if (!this.user.password) {
        swal.fire('Provide password to authorize.');
      } else {
        return true;
      }

      return false;
    }
  }
</script>

<style></style>
