<template>
    <v-app-bar app color="indigo" dark>
        <v-toolbar-title>Cuentas por Pagar</v-toolbar-title>
        <v-spacer />
        <v-menu left bottom :close-on-content-click="false" v-if="getLoggedStatus">
            <template v-slot:activator="{ on }">
                <v-btn icon v-on="on">
                    <v-icon>more_vert</v-icon>
                </v-btn>
            </template>
            <v-list>
                <v-list-item link @click="handleLogout">
                    <v-list-item-icon>
                        <v-icon>directions_run</v-icon>
                    </v-list-item-icon>
                    <v-list-item-content>Cerrar Sesion</v-list-item-content>
                </v-list-item>
            </v-list>
        </v-menu>
    </v-app-bar>
</template>

<script>
import Swal from 'sweetalert2'

export default {
    name: 'AppBar',
    data: () => {
        return {
            getLoggedStatus: !!localStorage.getItem("token")
        }
    },
    methods: {
        handleLogout() {
            Swal.fire({
                title: "Espere por favor...",
                allowEscapeKey: false,
                allowOutsideClick: false,
                showConfirmButton: false,
                didOpen: () => {
                    Swal.showLoading();
                    localStorage.clear();
                    location.reload();
                }
            })
        }
    }
}
</script>
