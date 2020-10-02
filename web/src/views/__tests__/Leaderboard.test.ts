import httpClient from '@/services/httpClient/httpClient';
import { render } from '@/utils/testUtils';
import { LeaderboardEntry } from '@/models/LeaderboardEntry';
import Leaderboard from '../Leaderboard.vue';
import LeaderboardListItem from '../../components/Leaderboard/LeaderboardListItem.vue';

describe('Leaderboard', () => {
  it('fetches leaderboard on created and renders ListItems', async () => {
    const mockList: LeaderboardEntry[] = [
      { name: 'Fabien Potencier', prCount: 0 },
      { name: 'Andrew Nesbitt', prCount: 2 },
    ];
    jest.spyOn(httpClient, 'get').mockResolvedValue(mockList);

    const [vm, wrapper] = render(Leaderboard);
    await vm.$nextTick();
    await vm.$nextTick();
    await vm.$nextTick();

    expect(wrapper.findAllComponents(LeaderboardListItem).length).toBe(mockList.length);
    // ListItem content
    expect(wrapper.text().includes(mockList[0].name)).toBeTruthy();
    expect(wrapper.text().includes(mockList[0].prCount.toString())).toBeTruthy();
    expect(wrapper.text().includes(mockList[1].name)).toBeTruthy();
    expect(wrapper.text().includes(mockList[1].prCount.toString())).toBeTruthy();
  });
});
