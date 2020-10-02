<template>
  <div class="leaderboard">
    <h1 class="h1-darkblue">
      Leaderboard
    </h1>

    <div v-if="loading">
      <LoadingSpinner />
    </div>
    <div v-else-if="error">
      Error while loading leaderboard
    </div>
    <div v-else-if="result">
      <LeaderboardListItem
        v-for="user in result"
        :key="user.name"
        :entry="user"
      />
    </div>
  </div>
</template>

<script lang="ts">
import LeaderboardListItem from '@/components/Leaderboard/LeaderboardListItem.vue';
import httpClient from '@/services/httpClient/httpClient';
import { defineComponent, onBeforeMount } from '@vue/composition-api';
import { LeaderboardEntry } from '@/models/LeaderboardEntry';
import LoadingSpinner from '@/components/LoadingSpinner/LoadingSpinner.vue';
import usePromise from '../composables/usePromise';

export default defineComponent({
  name: 'Leaderboard',
  components: {
    LeaderboardListItem,
    LoadingSpinner,
  },
  setup() {
    const {
      createPromise,
      result,
      loading,
      error,
    } = usePromise(async () => httpClient.get<LeaderboardEntry[]>('leaderboard'));

    onBeforeMount(async () => {
      await createPromise();
    });

    return {
      result,
      loading,
      error,
    };
  },
});
</script>

<style lang="scss" scoped>
.leaderboard {
  display: flex;
  flex-direction: column;
  align-items: center;
}
</style>
