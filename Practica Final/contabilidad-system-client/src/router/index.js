import Vue from 'vue'
import VueRouter from 'vue-router'
import Login from '@/views/Login'
import Proveedor from '@/views/Proveedor'
import Home from '@/views/Home'
import Conceptos from '@/views/Conceptos'
import Usuarios from '@/views/Usuario'

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
	},
    {
		path: '/home',
		name: 'Home',
		component: Home,
		meta: {
			requiresAuth: true
		}
	},
    {
        path: "/proveedores",
		name: "Proveedores",
		component: Proveedor,
		meta: {
			requiresAuth: true
		}
    },
    {
        path: "/conceptos",
		name: "Conceptos",
		component: Conceptos,
		meta: {
			requiresAuth: true
		}
    },
    {
        path: '/usuarios',
        name: "Usuarios",
        component: Usuarios,
        meta: {
            requiresAuth: true
        }
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
