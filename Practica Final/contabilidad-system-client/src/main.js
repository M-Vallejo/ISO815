import Vue from 'vue'
import App from './App.vue'
import vuetify from './plugins/vuetify'
import store from './store'
import router from './router'
import 'material-design-icons-iconfont/dist/material-design-icons.css'

Vue.config.productionTip = false

new Vue({
    vuetify,
    store,
    router,
    mounted: () => { if (localStorage.getItem('token')) store.dispatch("fetchUser"); },
    render: h => h(App)
}).$mount('#app')
