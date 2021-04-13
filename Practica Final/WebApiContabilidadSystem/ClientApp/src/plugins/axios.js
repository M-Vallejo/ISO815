import axios from 'axios';
import Swal  from 'sweetalert2';

//setup axios
const http = axios.create({
    baseURL: process.env.VUE_APP_API_URL,
    headers: {
        'Accept': 'application/json',
        'Content-Type': 'application/json',
    }
});

//check if token is available on localstorage
//if is active, set it to be sent as a Bearer token
const token = localStorage.getItem('token');

if (token) http.defaults.headers.common['Authorization'] = `Bearer ${token}`;

//create interceptor to redirect to login if
//api returns 401 and Unauthenticated.
http.interceptors.response.use(
    (response) => { return response; },
    (error) => {
        if (error.response.status === 401) {
            Swal
                .fire({
                    title: "Sesion Expirada",
                    text: "Por favor vuelva a iniciar sesion",
                    allowEscapeKey: false,
                    allowOutsideClick: false,
                    confirmButtonText: "Ok"
                })
                .then(() => {
                    localStorage.clear();
                    location.reload();
                });
        }
    }
);

export default http;
