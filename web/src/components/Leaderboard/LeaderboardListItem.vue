<template>
  <div class="leaderboardListItem">
    <div
      class="avatar"
      :style="{ backgroundImage: `url('${entry.avatarUrl}')` }"
    />
    <div class="username">
      <h3 class="heading">
        {{ entry.name }}
      </h3>
      <div class="subHeading">
        {{ entry.username }}
      </div>
    </div>
    <LeaderboardPrCount :pr-count="entry.prCount" />
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

  @media screen and (max-width: 450px) {
    width: 100%;

    .heading {
      width: 70%;
      text-overflow: ellipsis;
      overflow: hidden;
      white-space: nowrap;
    }
  }

  .avatar {
    min-width: 50px;
    width: 50px;
    height: 50px;
    border-radius: 50%;
    background-color: $BM-LightGray;
    margin-right: 16px;
    background-size: contain;
    background-repeat: no-repeat;
  }

  .username {
    display: flex;
    flex-direction: column;
    align-items: flex-start;
  }
}
</style>
