<template>
  <main>
    <h1>Vue3 Application</h1>
    <SignIn />
  </main>
</template>

<script lang="ts">
  import SignIn from './components/Auth/SignIn.vue';

  export default {
    components: {
      SignIn,
    },
  };
</script>

<style>
  #app {
    font-family: Avenir, Helvetica, Arial, sans-serif;
    -webkit-font-smoothing: antialiased;
    -moz-osx-font-smoothing: grayscale;
    text-align: center;
    color: #2c3e50;
  }

  nav {
    padding: 30px;
  }

  nav a {
    font-weight: bold;
    color: #2c3e50;
  }

  nav a.router-link-exact-active {
    color: #42b983;
  }
</style>
