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
            <template v-slot:top>
                <v-toolbar flat color="white">
                    <v-spacer />
                    <v-dialog v-model="modal" max-width="900px" persistent>
                        <template v-slot:activator="{ on }">
                            <v-btn v-on="on" color="primary" dark class="mb-2">
                                <v-icon>add</v-icon> Crear
                            </v-btn>
                        </template>
                        <v-card>
                            <v-card-title>
                                <span class="headline">{{ formTitle }}</span>
                            </v-card-title>
                            <v-card-text>
                                <v-container>
                                    <v-form autocomplete="off" ref="form">
                                        <v-row>
                                            <v-col cols="12" sm="6" md="6">
                                                <v-text-field
                                                    v-model="item.numero_documento"
                                                    prepend-icon="person"
                                                    label="Numero de Documento"
                                                    required
                                                    type="number"
                                                    min='1'
                                                    id="numero_documento"
                                                />
                                            </v-col>
                                            <v-col cols="12" sm="6" md="6">
                                                <v-text-field
                                                    v-model="item.numero_factura"
                                                    prepend-icon="person"
                                                    label="Numero de Factura"
                                                    required
                                                    type="number"
                                                    min='1'
                                                    id="numero_factura"
                                                />
                                            </v-col>
                                            <v-col cols="12" sm="6" md="6">
                                                <v-text-field
                                                    v-model="item.numero_cheque"
                                                    prepend-icon="person"
                                                    label="Numero de cheque"
                                                    required
                                                    type="number"
                                                    min='1'
                                                    id="numero_cheque"
                                                />
                                            </v-col>
                                            <v-col cols="12" sm="6" md="6">
                                                <template>
                                                    <v-menu
                                                        :close-on-content-click="false"
                                                        transition="scale-transition"
                                                        offset-y
                                                        min-width="auto"
                                                    >
                                                        <template v-slot:activator="{ on, attrs }">
                                                            <v-text-field
                                                                v-model="item.fecha_documento"
                                                                label="Fecha Documento"
                                                                prepend-icon="mdi-calendar"
                                                                readonly
                                                                v-bind="attrs"
                                                                v-on="on"
                                                            ></v-text-field>
                                                        </template>
                                                        <v-date-picker
                                                            ref="picker"
                                                            v-model="item.fecha_documento"
                                                            :max="currentDate"
                                                            min="1950-01-01"
                                                        >
                                                        </v-date-picker>
                                                    </v-menu>
                                                </template>
                                            </v-col>
                                            <v-col cols="12" sm="6" md="6">
                                                <v-text-field
                                                    v-model="item.monto"
                                                    prepend-icon="person"
                                                    label="Monto"
                                                    required
                                                    type="number"
                                                    min="1"
                                                    id="monto"
                                                />
                                            </v-col>
                                            <v-col cols="12" sm="6" md="6">
                                                <v-select
                                                    v-model="item.tipo_documento_id"
                                                    prepend-icon="mdi-check-decagram"
                                                    label="Tipo de Documento"
                                                    required
                                                    :items="tiposDocumentos"
                                                    item-value="tipo_documento_id"
                                                    item-text="descripcion"
                                                    id="tipo_documento_id"
                                                />
                                            </v-col>
                                            <v-col cols="12" sm="6" md="6">
                                                <v-select
                                                    v-model="item.proveedor_id"
                                                    prepend-icon="mdi-check-decagram"
                                                    label="Proveedores"
                                                    required
                                                    :items="proveedores"
                                                    item-value="proveedor_id"
                                                    item-text="nombre"
                                                    id="proveedor_id"
                                                />
                                            </v-col>
                                            <v-col cols="12" sm="6" md="6">
                                                <v-text-field
                                                    v-model="item.condiciones"
                                                    prepend-icon="person"
                                                    label="Condiciones"
                                                    type="text"
                                                    id="condiciones"
                                                />
                                            </v-col>
                                        </v-row>
                                    </v-form>
                                </v-container>
                            </v-card-text>
                            <v-card-actions>
                                <v-btn color="red darken-1" text @click="closeModal">Cancelar</v-btn>
                                <v-spacer />
                                <v-btn color="blue darken-1" text @click="saveItem">Guardar</v-btn>
                            </v-card-actions>
                        </v-card>
                    </v-dialog>
                </v-toolbar>
            </template>
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
            tiposDocumentos: [],
            proveedores: [],
            currentDate: null
        }
    },
    mounted() {
        this.getTiposDocumentos();
        this.getProveedores();
        this.getEntradaDocumentos();
        this.item.fecha_documento = this.setToday();
        this.currentDate = this.setToday();
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
                .get("TipoDocumento/GetTipoDocumentoByEstatus/1")
                .then((response) => {
                    this.tiposDocumentos = response.data;
                });
        },
        getProveedores() {
            this.$http
                .get("proveedores/GetProveedoresByEstatus/1")
                .then((response) => {
                    this.proveedores = response.data;
                });
        },
        getEntradaDocumentos() {
            this.$http
                .get("EntraDeDocumentos/get")
                .then((response) => {
                    this.items = response.data;
                    this.isLoading = false;
                });
        },
        closeModal() {
            this.editedIndex = -1;
            this.modal = false;
            this.item = Object.assign({}, {
                numero_documento: null,
                numero_factura: null,
                numero_cheque: null,
                fecha_documento: this.setToday(),
                monto: null,
                tipo_documento_id: null,
                proveedor_id: null,
                condiciones: null
            });
            this.$refs.form.resetValidation();
        },
        editItem(item) {
            this.editedIndex = this.items.indexOf(item);
            this.item = Object.assign({}, item);
            this.modal = true;
        },
        validateData() {
            
        },
        saveItem() {

        },
        setToday() {
            let today = new Date();
            var dd = today.getDate();
            var mm = today.getMonth() + 1;
            var yyyy = today.getFullYear();
            if (dd < 10) dd = `0${dd}`;
            if (mm < 10) mm = `0${mm}`;
            return `${yyyy}-${mm}-${dd}`;
        }
    }
}
</script>

<style>

</style>
