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
                                                    v-model="item.numero_factura"
                                                    prepend-icon="person"
                                                    label="Numero de Factura*"
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
                                                                label="Fecha Documento*"
                                                                prepend-icon="mdi-calendar"
                                                                readonly
                                                                v-bind="attrs"
                                                                v-on="on"
                                                                id="fecha_documento"
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
                                                    label="Monto*"
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
                                                    label="Tipo de Documento*"
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
                                                    label="Proveedor*"
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
            
            <template v-slot:item.fecha_documento="{ item }">
                <span>{{ item.fecha_documento ? new Date(item.fecha_documento).toDateString() : null }}</span>
            </template>

            <template v-slot:item.fecha_proceso="{ item }">
                <span>{{ item.fecha_proceso ? new Date(item.fecha_proceso).toDateString() : null }}</span>
            </template>

            <template v-slot:item.estado="{ item }">
                <Statusfortable :item="item.estado"/>
            </template>
        </v-data-table>
    </v-card>
</template>

<script>
import Statusfortable from "@/components/Statusfortable";
import Swal from 'sweetalert2';

export default {
    name: "entradadocumento",
    components: {
        Statusfortable
    },
    data: () => {
        return {
            isLoading: true,
            search: "",
            modal: false,
            editedIndex: -1,
            item: {
                numero_factura: null,
                numero_cheque: null,
                fecha_documento: null,
                monto: 0,
                tipo_documento_id: null,
                proveedor_id: null,
                condiciones: null,
                estado: 1,
                fecha_proceso: null,
            },
            headers: [
                {
                    text: "No. documento",
                    align: 'start',
                    value: "numero_documento"
                },
                {
                    text: "No. Factura",
                    value: "numero_factura"
                },
                {
                    text: "No. cheque",
                    value: 'numero_cheque'
                },
                {
                    text: "Fecha documento",
                    value: "fecha_documento"
                },
                {
                    text: "Monto",
                    value: 'monto'
                },
                {
                    text: "Tipo documento",
                    value: 'tipo_documento.descripcion'
                },
                {
                    text: "Proveedor",
                    value: "proveedor.nombre"
                },
                {
                    text: "Estado",
                    value: "estado"
                },
                {
                    text: "Fecha proceso",
                    value: "fecha_proceso"
                }
            ],
            items: [],
            tiposDocumentos: [],
            proveedores: [],
            estados: [
                {
                    "id": 0,
                    "value": "Pendiente"
                },
                {
                    "id": 1,
                    "value": "Pagado"
                }
            ],
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
            let numero_factura = document.getElementById("numero_factura");
            let fecha_documento = document.getElementById("fecha_documento");
            let monto = document.getElementById("monto");
            let tipo_documento_id = document.getElementById("tipo_documento_id");
            let proveedor_id = document.getElementById("proveedor_id");

            if(!this.item.numero_factura) {
                numero_factura.focus();
                return false;
            }
            if(!fecha_documento.checkValidity) {
                fecha_documento.focus();
                return false;
            }
            if(!monto.checkValidity || this.item.monto < 0) {
                monto.focus();
                return false;
            }
            if(this.item.tipo_documento_id <= 0) {
                tipo_documento_id.focus();
                return false;
            }
            if(this.item.proveedor_id <= 0) {
                proveedor_id.focus();
                return false;
            }

            return true;
        },
        saveItem() {
            if (this.validateData()) {
                const data = {
                    numero_factura: parseInt(this.item.numero_factura),
                    numero_cheque: parseInt(this.item.numero_cheque),
                    fecha_documento: this.item.fecha_documento,
                    monto: parseFloat(this.item.monto),
                    tipo_documento_id: parseInt(this.item.tipo_documento_id),
                    proveedor_id: parseInt(this.item.proveedor_id),
                    condiciones: this.item.condiciones
                };

                Swal.fire({
                    title: "Espere por favor...",
                    allowEscapeKey: false,
                    allowOutsideClick: false,
                    showConfirmButton: false,
                    didOpen: () => {
                        Swal.showLoading();
                        this.$http
                        .post("EntraDeDocumentos/CreateEntradaDocumento", data)
                        .then((response) => {
                            this.isLoading = false;
                            if(response?.status == 200) {
                                Swal.fire({
                                    title: "Entrada creada exitosamente",
                                    icon: 'success',
                                });
                                this.getEntradaDocumentos();
                                this.closeModal();
                            } else {
                                Swal.fire({
                                    title: "Ha ocurrido un error",
                                    icon: 'danger'
                                });
                            }
                        }).catch(error => {
                            Swal.fire({
                                title: `Ha ocurrido un error: ${error.message}`,
                                icon: 'danger'
                            });
                        });
                    }
                });
            } 
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
