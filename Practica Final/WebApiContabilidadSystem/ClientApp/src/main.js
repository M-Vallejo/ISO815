import Vue from 'vue'
import App from './App.vue'
import vuetify from './plugins/vuetify'
import router from './router'
import 'material-design-icons-iconfont/dist/material-design-icons.css'
import http from './plugins/axios';

Vue.config.productionTip = false
Vue.prototype.$http = http

new Vue({
    vuetify,
    router,
    render: h => h(App)
}).$mount('#app')
