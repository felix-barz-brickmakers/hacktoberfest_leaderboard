<template>
  <div id="app">
    <router-view />
  </div>
</template>

<style lang="scss">
body {
  background-color: $BM-LightBlue;
  padding: 20px;

  #app {
    -webkit-font-smoothing: antialiased;
    -moz-osx-font-smoothing: grayscale;

    font-family: "proxima-nova", Helvetica, Arial, sans-serif;
    font-size: 16px;
    font-weight: 500;
    line-height: 1.5;
    color: $BM-Darkblue;
    text-align: center;

    @media screen and (min-width: 421px) {
      font-size: 18px;
    }
  }
}
</style>
