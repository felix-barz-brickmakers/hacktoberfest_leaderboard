// eslint-disable-next-line import/no-extraneous-dependencies
import { Wrapper, mount, createLocalVue } from '@vue/test-utils';
import { CombinedVueInstance, VueConstructor } from 'vue/types/vue.d';
import VueRouter from 'vue-router';
import VueCompositionAPI from '@vue/composition-api';

const localVue = createLocalVue();

localVue.use(VueRouter);
localVue.use(VueCompositionAPI);

export const render = (
  component: VueConstructor<Vue>,
  options?: Record<string, any>,
): [any, Wrapper<CombinedVueInstance<any, object, object, object, Record<never, any>>>] => {
  const wrapper = mount(component, { localVue, ...options });
  const { vm } = wrapper as any;

  return [vm, wrapper];
};
