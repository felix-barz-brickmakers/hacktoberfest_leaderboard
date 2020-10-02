<template>
  <div class="pr">
    <div class="h1-darkblue">
      {{ prCount }}
    </div>
    <div class="pr-count">
      / 4
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent } from '@vue/composition-api';

export default defineComponent({
  name: 'LeaderboardPrCount',
  props: {
    prCount: {
      type: Number,
      required: true,
    },
  },
});
</script>

<style lang="scss" scoped>
.pr {
  position: absolute;
  right: 16px;
  top: 0;
  height: 100%;
  display: flex;
  align-items: center;

  .h1-darkblue {
    font-size: 40px;
    padding: 0;
    margin-right: 2px;
  }

  .pr-count {
    margin-top: 16px;
    color: $BM-Pastel-Plum-2;
  }
}
</style>
