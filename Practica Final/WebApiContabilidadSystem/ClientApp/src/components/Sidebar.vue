<template>
    <v-navigation-drawer permanent app expand-on-hover v-if="getLoggedStatus">
        <v-list dense>
            <v-list-item v-for="(link, index) in linksForUsers" :key="index" link :to="link.to" :replace="false">
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
            user: JSON.parse(localStorage.getItem("user")),
            links: [
                {
                    to: 'home',
                    text: "Dashboard",
                    icon: 'mdi-home',
                    role: [0, 1]
                },
                {
                    to: 'proveedores',
                    text: "Proveedores",
                    icon: 'mdi-bag-personal',
                    role: [0, 1]
                },
                {
                    to: 'conceptos',
                    text: "Conceptos de Pago",
                    icon: 'mdi-newspaper',
                    role: [0, 1]
                },
                {
                    to: 'usuarios',
                    text: "Usuarios",
                    icon: 'group',
                    role: [1]
                },
                {
                    to: 'tipodocumento',
                    text: "Tipos de Documentos",
                    icon: 'description',
                    role: [0, 1]
                },
                {
                    to: 'entradadocumento',
                    text: "Entrada de Documentos",
                    icon: 'description',
                    role: [0, 1]
                }
            ]
        }
    },
    computed: {
        linksForUsers: function() {
            return this.links.filter(link => link.role.includes(parseInt(this.user.rol)));
        }
    },
}
</script>
