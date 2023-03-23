<template>
  <main>
    <h1>BlockRouter Application</h1>
    <router-view class="content" />
  </main>
</template>

<script lang="ts">
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

  .content {
    margin-top: 16%;
  }
</style>
