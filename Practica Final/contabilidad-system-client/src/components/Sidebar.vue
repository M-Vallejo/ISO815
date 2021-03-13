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
            getLoggedStatus: !!localStorage.getItem("token")
        }
    },
    computed: {
        links() {
            return [
                {
                    to: 'home',
                    text: "Dashboard",
                    icon: 'mdi-home'
                },
                {
                    to: 'proveedores',
                    text: "Proveedores",
                    icon: 'mdi-bag-personal'
                },

                {
                    to: 'conceptos',
                    text: "Conceptos de Pago",
                    icon: 'mdi-newspaper'
                }
            ]
        }
    },
}
</script>
