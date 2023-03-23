import { RouteRecordRaw, createRouter, createWebHistory } from 'vue-router';

import SignIn from '../components/Auth/SignIn.vue';
import BrandEdit from '../components/Flows/Edit/BrandEdit.vue';
import DashBoard from '../components/Flows/DashBoard.vue';
import ModelEdit from '../components/Flows/Edit/ModelEdit.vue';
import data from '../data';

const NotFound = () => import('../components/Shared/NotFound.vue');

const routes: Array<RouteRecordRaw> = [
  {
    path: '/',
    alias: '/login',
    name: 'SignIn',
    component: SignIn,
  },
  {
    path: '/board',
    alias: ['/dashboard', '/overview'],
    name: 'DashBoard',
    component: DashBoard,
  },
  {
    path: '/edit/brand',
    alias: '/editBrand',
    name: 'EditBrand',
    component: BrandEdit,
  },
  {
    path: '/edit/model',
    alias: '/editModel',
    name: 'EditModel',
    component: ModelEdit,
  },
  {
    path: '/:pathMatch(.*)*',
    alias: '/notFound',
    name: 'NotFound',
    component: NotFound,
  },
];
const router = createRouter({
  history: createWebHistory(),
  routes,
});

router.beforeEach((to, from, next) => {
  const isAuthRequired = !data.publicPages.includes(to.path);
  const isLoggedIn = localStorage.getItem(data.tokenKeyName);

  if (isAuthRequired && !isLoggedIn) {
    next('/login');
  } else {
    next();
  }
});

export default router;
