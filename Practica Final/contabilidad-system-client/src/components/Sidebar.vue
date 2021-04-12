<template>
    <v-navigation-drawer permanent app expand-on-hover v-if="getLoggedStatus">
        <v-list dense>
            <v-list-item v-for="(link, index) in links" :key="index" link :to="link.to" :replace="false">
                <v-list-item-action>
                    <v-icon>{{ link.icon }}</v-icon>
                </v-list-item-action>
                <v-list-item-content>
                    <v-list-item-title>{{ link.text }}</v-list-item-title>
                </v-list-item-content>
            </v-list-item>
        </v-list>
    </v-navigation-drawer>
</template>

<script>
export default {
    name: "Sidebar",
    data: () => {
        return {
            getLoggedStatus: !!localStorage.getItem("token"),
            user: JSON.parse(localStorage.getItem("user"))
        }
    },
    computed: {
        links() {
            return [
                {
                    to: 'home',
                    text: "Dashboard",
                    icon: 'mdi-home',
                    role: 0
                },
                {
                    to: 'proveedores',
                    text: "Proveedores",
                    icon: 'mdi-bag-personal',
                    role: 0
                },
                {
                    to: 'conceptos',
                    text: "Conceptos de Pago",
                    icon: 'mdi-newspaper',
                    role: 0
                },
                {
                    to: 'usuarios',
                    text: "Usuarios",
                    icon: 'group',
                    role: 1
                }
            ]
        }
    },
}
</script>
