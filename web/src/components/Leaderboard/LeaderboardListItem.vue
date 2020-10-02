<template>
  <div class="leaderboardListItem">
    {{ entry.name }} - {{ entry.prCount }}
  </div>
</template>

<script lang="ts">
import { LeaderboardEntry } from '@/models/LeaderboardEntry';
import { defineComponent } from '@vue/composition-api';

export default defineComponent({
  name: 'LeaderboardListItem',
  props: {
    entry: {
      type: Object as () => LeaderboardEntry,
      required: true,
    },
  },
});
</script>
