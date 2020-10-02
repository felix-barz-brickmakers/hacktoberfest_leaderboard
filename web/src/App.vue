<template>
  <div id="app">
    <router-view />
  </div>
</template>

<style lang="scss">
body {
  background-color: $BM-LightBlue;
  margin: 0;

  #app {
    -webkit-font-smoothing: antialiased;
    -moz-osx-font-smoothing: grayscale;

    font-family: "proxima-nova", Helvetica, Arial, sans-serif;
    font-size: 16px;
    font-weight: 500;
    line-height: 1.5;
    color: $BM-Darkblue;
    text-align: center;
    padding: 20px;

    @media screen and (max-width: 450px) {
      position: relative;
      width: 100vw;
      padding: 16px;
    }

    @media screen and (min-width: 451px) {
      font-size: 18px;
    }
  }
}
</style>
