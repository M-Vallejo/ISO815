<template>
    <v-card>
        <v-card-title>Tipos de Documentos</v-card-title>
    </v-card>
</template>

<script>
export default {
    name: "tipodocumento",
    data: () => {
        return {
            isLoading: true,
            search: "",
            modal: false,
            editedIndex: -1,
            item: {
                descripcion: "",
                estado: 1
            },
            headers: [
                {
                    text: "Descripcion",
                    align: 'start',
                    value: "descripcion"
                },
                {
                    text: "Estado",
                    value: "estado"
                },
                {
                    text: "Acciones",
                    value: "actions",
                    sortable: false
                }
            ],
            items: [],
            estados: [
                {
                    "id": 1,
                    "value": "Activo"
                },
                {
                    "id": 0,
                    "value": "Inactivo"
                },
            ]
        }
    }
}
</script>
