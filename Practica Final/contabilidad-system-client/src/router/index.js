import Vue from 'vue'
import VueRouter from 'vue-router'
import Login from '@/views/Login'

Vue.use(VueRouter)

const routes = [
    {
		path: "*",
		redirect: "/login"
	},
	{
		path: '/login',
		name: "Login",
		component: Login
	}
];

const router = new VueRouter({
	mode: 'history',
	routes
});

router.beforeEach((to, _from, next) => {
	const currentUser = localStorage.getItem("token");
	const requiresAuth = to.matched.some(record => record.meta.requiresAuth);

	if (requiresAuth && !currentUser) next('login');
	else if (!requiresAuth && currentUser) next('home');
	else next();
});

export default router
