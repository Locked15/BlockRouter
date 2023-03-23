<template>
  <div class="login-parent">
    <h2>Authorization Process</h2>
    <form class="login-form justify-content-center">
      <div class="form-outline">
        <input id="userLogin" type="text" class="form-control" v-model="user.userName" />
        <label class="form-label" for="userLogin">Login</label>
      </div>
      <div class="form-outline">
        <input id="userPassword" type="password" class="form-control" v-model="user.password" />
        <label class="form-label" for="userPassword">Password</label>
      </div>

      <div class="row">
        <div class="col d-flex justify-content-center">
          <div class="form-check">
            <input id="rememberMe" class="form-check-input" type="checkbox" value="false" />
            <label class="form-check-label" for="rememberMe">Remember Me?</label>
          </div>
        </div>
        <div class="col">
          <a href="#" @click="helpWithPassword">Forget Password?</a>
        </div>
      </div>

      <button type="button" class="btn btn-primary btn-block submit-button" @click="tryToAuthorize">Sign IN</button>
    </form>
  </div>
</template>

<script lang="ts">
  import User from '@/models/User';
  import axios from 'axios';
  import Swal from 'sweetalert2';
  import { Vue } from 'vue-class-component';

  import appData from '../../data';

  export default class SignIn extends Vue {
    /**
     * Model for current component.
     * Contains information about user properties, such as Login and Password.
     */
    public user = User.getDefaultUser();

    // #region Component-Specific Functions

    // eslint-disable-next-line class-methods-use-this
    public created(): void {
      document.title = 'Sign In — Block Router';
    }

    public data(): User {
      return this.user;
    }
    // #endregion Component-Specific Functions.

    // #region Authorization Functions

    public tryToAuthorize() {
      if (this.validateModelAndNotify()) {
        const apiQuery = axios.get(
          `${appData.apiAddress + appData.signInApiQuery}${this.user.userName}\\${this.user.password}`,
        );

        apiQuery.then((response) => {
          if (response.status === 200) {
            localStorage.setItem(appData.tokenKeyName, JSON.stringify(response.data.token));
            localStorage.setItem(appData.currentUserKeyName, JSON.stringify(this.user));

            Swal.fire('Authorization is successful!', 'Success!', 'success');
            this.$router.push({ name: 'DashBoard' });
          }
        });
        apiQuery.catch((error) => {
          if (error.response === undefined || error.response.status === undefined) {
            Swal.fire('Whoops, looks like API not available now.\n\nPlease, stand by.', 'Not Available', 'error');
          } else if (error.response.status === 404) {
            Swal.fire('Are you sure about data, that you provided?', 'Login or/and Password is Incorrect', 'error');
          }
        });
      }
    }

    private validateModelAndNotify() {
      if (!this.user.userName) {
        Swal.fire('Provide login to authorize.', 'Login is Missed', 'error');
      } else if (!this.user.password) {
        Swal.fire('Provide password to authorize.', 'Password is Missed', 'error');
      } else {
        return true;
      }

      return false;
    }

    public helpWithPassword() {
      if (!this.user.userName) {
        Swal.fire('Are you forget password, or your login, by the way?', 'Are you?', 'question');
      } else {
        Swal.fire('We need to be sure, that YOU is really YOU.', "Where's proof?", 'question');
      }
    }
    // #endregion Authorization Functions.
  }
</script>

<style scoped>
  .login-parent {
    display: flex;
    flex-direction: column;
  }

  .login-parent > h2 {
    margin-bottom: 5%;
  }

  .login-form {
    display: flex;
    flex-direction: column;

    width: 65%;
    align-self: center;
  }

  .login-form .form-outline {
    align-self: center;
    max-width: 65%;
  }

  .login-form input {
    text-align: center;
  }

  .submit-button {
    margin-top: 3.14%;
    width: 32%;

    align-self: center;
  }
</style>
