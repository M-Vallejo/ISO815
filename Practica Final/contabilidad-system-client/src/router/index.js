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
			requiresAuth: true,
            userRole: "0"
		}
	},
    {
        path: "/proveedores",
		name: "Proveedores",
		component: Proveedor,
		meta: {
			requiresAuth: true,
            userRole: "0"
		}
    },
    {
        path: "/conceptos",
		name: "Conceptos",
		component: Conceptos,
		meta: {
			requiresAuth: true,
            userRole: "0"
		}
    },
    {
        path: '/usuarios',
        name: "Usuarios",
        component: Usuarios,
        meta: {
            requiresAuth: true,
            userRole: "1"
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
    const currentUserData = (localStorage.getItem("user") == null) ? null : parseInt(JSON.parse(localStorage.getItem("user")).rol);
    const userRole = parseInt(to.meta.userRole);

	if (requiresAuth && !currentUser) next('login');
	else if (!requiresAuth && currentUser) next('home');
    else if (currentUserData != null && currentUserData != userRole) next("home");
	else next();
});

export default router
