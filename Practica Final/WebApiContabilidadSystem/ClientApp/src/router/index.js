import Vue from 'vue'
import VueRouter from 'vue-router'
import Login from '@/views/Login'
import Proveedor from '@/views/Proveedor'
import Home from '@/views/Home'
import Conceptos from '@/views/Conceptos'
import Usuarios from '@/views/Usuario'
import TipoDocumento from '@/views/TipoDocumento'

Vue.use(VueRouter)

const routes = [
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
            userRole: 0
		}
	},
    {
        path: "/proveedores",
		name: "Proveedores",
		component: Proveedor,
		meta: {
			requiresAuth: true,
            userRole: 0
		}
    },
    {
        path: "/conceptos",
		name: "Conceptos",
		component: Conceptos,
		meta: {
			requiresAuth: true,
            userRole: 0
		}
    },
    {
        path: '/usuarios',
        name: "Usuarios",
        component: Usuarios,
        meta: {
            requiresAuth: true,
            userRole: 1
        }
    },
    {
        path: '/tipodocumento',
        name: "Tipo Documento",
        component: TipoDocumento,
        meta: {
            requiresAuth: true,
            userRole: 0
        }
    }
];

const router = new VueRouter({
	mode: 'history',
	routes
});

router.beforeEach((to, _from, next) => {
	const currentUser = localStorage.getItem("token");
    const user = JSON.parse(localStorage.getItem('user'));

	if (to.meta.requiresAuth && !currentUser) next('login');
	else if (!to.meta.requiresAuth && currentUser) next('home');
    else if ((user) && to.meta.userRole != parseInt(user.rol)) next('home');
	else next();
});

export default router
