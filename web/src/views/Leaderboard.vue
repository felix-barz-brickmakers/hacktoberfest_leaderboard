<template>
  <div class="leaderboard">
    Leaderboard

    <div v-if="result">
      <leaderboard-list-item
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
import usePromise from '../composables/usePromise';

export default defineComponent({
  name: 'Leaderboard',
  components: {
    LeaderboardListItem,
  },
  setup() {
    const {
      createPromise,
      result,
    } = usePromise(async () => httpClient.get<LeaderboardEntry[]>('leaderboard'));

    onBeforeMount(async () => {
      await createPromise();
    });

    return {
      result,
    };
  },
});
</script>

<style scoped>
</style>
