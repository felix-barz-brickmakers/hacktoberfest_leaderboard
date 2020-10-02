/* eslint-disable @typescript-eslint/no-empty-function */
import httpClient from '@/services/httpClient/httpClient';
import { render } from '@/utils/testUtils';
import { LeaderboardEntry } from '@/models/LeaderboardEntry';
import Leaderboard from '../Leaderboard.vue';
import LoadingSpinner from '../../components/LoadingSpinner/LoadingSpinner.vue';
import LeaderboardListItem from '../../components/Leaderboard/LeaderboardListItem.vue';

describe('Leaderboard', () => {
  it('fetches leaderboard on created and renders ListItems', async () => {
    const mockList: LeaderboardEntry[] = [
      {
        name: 'Fabien Potencier',
        prCount: 0,
        avatarUrl: '',
        username: 'username',
      },
      {
        name: 'Andrew Nesbitt',
        prCount: 2,
        avatarUrl: '',
        username: 'username2',
      },
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
    expect(wrapper.text().includes(mockList[0].username)).toBeTruthy();
    expect(wrapper.text().includes(mockList[1].name)).toBeTruthy();
    expect(wrapper.text().includes(mockList[1].prCount.toString())).toBeTruthy();
    expect(wrapper.text().includes(mockList[1].username)).toBeTruthy();
  });

  it('shows loading on promise loading', async () => {
    jest.spyOn(httpClient, 'get').mockImplementation(() => new Promise(() => {}));

    const [vm, wrapper] = render(Leaderboard);
    await vm.$nextTick();
    await vm.$nextTick();
    await vm.$nextTick();

    expect(wrapper.findAllComponents(LeaderboardListItem).length).toBe(0);
    expect(wrapper.findComponent(LoadingSpinner).exists()).toBeTruthy();
  });

  it('shows error on promise error', async () => {
    jest.spyOn(httpClient, 'get').mockRejectedValue(new Error('some error'));

    const [vm, wrapper] = render(Leaderboard);
    await vm.$nextTick();
    await vm.$nextTick();
    await vm.$nextTick();

    expect(wrapper.findAllComponents(LeaderboardListItem).length).toBe(0);
    expect(wrapper.text().includes('Error while loading leaderboard')).toBeTruthy();
  });
});
