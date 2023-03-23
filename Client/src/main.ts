import axios from 'axios';
import 'bootstrap';
import 'bootstrap/dist/css/bootstrap.min.css';
import { createApp } from 'vue';
import vueAxios from 'vue-axios';

import App from './app/App.vue';
import router from './app/router/index';

/**
 * Create app, configure it and mount.
 */
const app = createApp(App);

app.use(router);
app.use(vueAxios, axios);

app.mount('#app');
