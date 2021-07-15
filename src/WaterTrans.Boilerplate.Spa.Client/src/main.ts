import { createApp } from 'vue';
import App from './App.vue';
import router from './router';

import './assets/css/layout.scss';

createApp(App).use(router).mount('#app');
