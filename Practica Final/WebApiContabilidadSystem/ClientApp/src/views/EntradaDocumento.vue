<template>
    <v-card>
        <v-card-title>Entrada de Documentos</v-card-title>
        <v-card-text>
            <v-text-field
                append-icon="search"
                label="Buscar"
                single-line
                hide-details
                class="px-10"
                v-model="search"
                autocomplete="off"
            />
        </v-card-text>
        <v-data-table
            class="elevation-1"
            :loading="isLoading"
            loading-text="Por favor espere..."
            :search="search"
            :items="items"
            :headers="headers"
        >

        </v-data-table>
    </v-card>
</template>

<script>
export default {
    name: "entradadocumento",
    data: () => {
        return {
            isLoading: true,
            search: "",
            modal: false,
            editedIndex: -1,
            item: {
                numero_documento: null,
                numero_factura: null,
                numero_cheque: null,
                fecha_documento: null,
                monto: null,
                tipo_documento_id: null,
                proveedor_id: null,
                condiciones: null
            },
            headers: [

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
            ],
            tiposDocumentos: []
        }
    },
    mounted() {
        this.getTiposDocumentos();
    },
    computed: {
        formTitle() {
            const title = this.editedIndex === -1 ? "Crear" : "Editar";
            return `${title} Entrada de Documento`;
        }
    },
    methods: {
        getTiposDocumentos() {
            this.$http
                .get("TipoDocumento/GetTipoDocumentoByEstatus?Estado=1")
                .then((response) => {
                    this.tiposDocumentos = response.data;
                });
        }
    }
}
</script>

<style>

</style>
