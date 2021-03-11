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
                                id="email"
                                label="Email"
                                prepend-icon="person"
                                type="email"
                                required
                                :rules="[rules.required, rules.email]"
                                v-model="email"
                                autofocus
                            />
                            <v-text-field
                                id="password"
                                label="Clave"
                                prepend-icon="lock"
                                type="password"
                                required
                                :rules="[rules.required]"
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
            email: "",
            password: "",
            errors: []
        }
    },
    computed: {
        rules() {
            return this.$store.getters.getRules;
        }
    },
    methods: {
        validateData() {
            let email = document.getElementById("email");
            let password = document.getElementById("password");

            if (!email.checkValidity()) {
                email.focus();
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

                        const user = {
                            email: this.email.trim().toLowerCase(),
                            password: this.password.trim()
                        };

                        this.$store
                            .dispatch('login', user)
                            .then(() => {
                                this.$router.replace('home');
                                location.reload();
                                Swal.close();
                            })
                            .catch(() => {
                                Swal.fire({
                                    icon: 'error',
                                    title: 'Oops...',
                                    text: "Credenciales Invalidas"
                                });
                                this.password = "";
                            });
                    }
                });
            }
        }
    }
}
</script>

<style>

</style>
