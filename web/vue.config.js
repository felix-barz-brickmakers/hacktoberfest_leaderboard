module.exports = {
  css: {
    loaderOptions: {
      sass: {
        prependData: `
          @import "@/scss/fonts.scss";
          @import "@/scss/colors.scss";
          @import "@/scss/main.scss";
        `,
      },
    },
  },
  devServer: {
    watchOptions: {
      poll: true
    },
  }
};
