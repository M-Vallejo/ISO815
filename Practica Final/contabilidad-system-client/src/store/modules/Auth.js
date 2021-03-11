import http from '@/plugins/axios';

const state = {
    user: {},
    isLoggedIn: !!localStorage.getItem("token")
};

const getters = {
    getLoggedStatus: (state) => state.isLoggedIn,
    getUser: (state) => state.user
};

const actions = {
    login({ commit }, user) {
        return new Promise((resolve, reject) => {
            http
                .post('auth/login', {
                    username: user.username,
                    password: user.password
                })
                .then((response) => {
                    localStorage.setItem('token', response.data.access_token);
                    commit('setUser', response.data.user);
                    resolve();
                })
                .catch((err) => {
                    console.log(err);
                    reject();
                })
        })
    },
    logout() {
        return new Promise((resolve) => {
            localStorage.clear();
            location.reload();
            resolve();
        });
    },
    fetchUser({ commit }) {
        http
            .get('auth/checkUser')
            .then((response) => commit('setUser', response.data))
    }
};

const mutations = {
    setUser: (state, user) => {
        state.user = user;
        state.isLoggedIn = true;
    }
};

export default {
    state, getters, actions, mutations
}
