<template>
  <div class="leaderboardListItem">
    <div class="avatar" />
    <div class="username">
      <h3 class="heading">
        {{ entry.name }}
      </h3>
      <div class="subHeading">
        username
      </div>
    </div>
    <leaderboard-pr-count :pr-count="entry.prCount" />
  </div>
</template>

<script lang="ts">
import { LeaderboardEntry } from '@/models/LeaderboardEntry';
import { defineComponent } from '@vue/composition-api';
import LeaderboardPrCount from './LeaderboardPrCount.vue';

export default defineComponent({
  name: 'LeaderboardListItem',
  components: {
    LeaderboardPrCount,
  },
  props: {
    entry: {
      type: Object as () => LeaderboardEntry,
      required: true,
    },
  },
});
</script>

<style lang="scss" scoped>
.leaderboardListItem {
  position: relative;
  box-sizing: border-box;
  width: 450px;
  height: 80px;
  background-color: $white;
  margin-bottom: 8px;
  padding: 16px;
  display: flex;
  align-items: center;

  .avatar {
    width: 50px;
    height: 50px;
    border-radius: 50%;
    background-color: $BM-LightGray;
    margin-right: 16px;
    background-image: url("https://github.com/sabifa.png");
    background-size: contain;
  }

  .username {
    display: flex;
    flex-direction: column;
    align-items: flex-start;
  }
}
</style>
