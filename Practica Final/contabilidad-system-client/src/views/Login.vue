<template>
    <v-container class="fill-height" fluid>
        <v-row align="center" justify="center">
            <v-col cols="10" sm="8" md="4">
                <v-card class="elevation-12">
                    <v-form autocomplete="off" @submit.prevent="handleSubmit">
                        <v-toolbar color="indigo" dark flat>
                            <v-toolbar-title>Iniciar sesion</v-toolbar-title>
                        </v-toolbar>
                        <v-card-text>
                            <v-text-field
                                id="username"
                                label="Usuario"
                                prepend-icon="person"
                                type="text"
                                required
                                v-model="username"
                                autofocus
                            />
                            <v-text-field
                                id="password"
                                label="Clave"
                                prepend-icon="lock"
                                type="password"
                                required
                                v-model="password"
                            />
                        </v-card-text>
                        <v-card-actions>
                            <v-btn type="submit" block color="primary">
                                <v-icon left>input</v-icon> Iniciar Sesion
                            </v-btn>
                        </v-card-actions>
                    </v-form>
                </v-card>
            </v-col>
        </v-row>
    </v-container>
</template>

<script>
import Swal from 'sweetalert2';

export default {
    name: "Login",
    data: () => {
        return {
            username: "",
            password: ""
        }
    },
    methods: {
        validateData() {
            let username = document.getElementById("username");
            let password = document.getElementById("password");

            if (!username.checkValidity()) {
                username.focus();
                return false;
            }

            if (!password.checkValidity()) {
                password.focus();
                return false;
            }

            return true;
        },
        handleSubmit() {
            if (this.validateData())
            {
                Swal.fire({
                    title: "Espere por favor",
                    allowEscapeKey: false,
                    allowOutsideClick: false,
                    showConfirmButton: false,
                    didOpen: () => {
                        Swal.showLoading();

                        this.$http
                            .post("account/Login", {
                                Username: this.username,
                                Password: this.password
                            })
                            .then((response) => {
                                if (response.status === 200) {
                                    const token = response.data.token;
                                    localStorage.setItem("token", token);

                                    const user = token.split(".")[1];
                                    localStorage.setItem("user", atob(user));
                                    location.reload();
                                }
                            })
                            .catch(() => {
                                Swal.fire({
                                    icon: 'error',
                                    title: 'Oops...',
                                    text: "ha ocurrido un error con los datos, intentelo nuevamente"
                                });
                                this.password = "";
                            })
                    }
                });
            }
        }
    }
}
</script>

<style>

</style>
