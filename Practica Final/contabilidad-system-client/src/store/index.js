import Vue from 'vue'
import Vuex from 'vuex'

import Auth from '@/store/modules/Auth'
import Util from '@/store/modules/Util'

Vue.use(Vuex)

export default new Vuex.Store({
    modules: {
        Auth,
        Util
    }
})
