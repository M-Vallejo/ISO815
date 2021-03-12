<template>
    <v-card>
        <v-card-title>Proveedores</v-card-title>
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
                                                    v-model="item.nombre"
                                                    prepend-icon="person"
                                                    label="Nombre"
                                                    required
                                                    type="text"
                                                    maxlength="255"
                                                    id="nombre"
                                                />
                                            </v-col>
                                            <v-col cols="12" sm="6" md="6">
                                                <v-select
                                                    v-model="item.tipo_persona"
                                                    prepend-icon="person"
                                                    :items="personTypes"
                                                    item-value="id"
                                                    item-text="value"
                                                    id="tipo_persona"
                                                    required
                                                    label="Tipo Persona"
                                                >
                                                </v-select>
                                            </v-col>
                                            <v-col cols="12" sm="6" md="6">
                                                <v-select
                                                    v-model="item.tipo_documento"
                                                    prepend-icon="person"
                                                    :items="documentTypes"
                                                    item-value="id"
                                                    item-text="value"
                                                    id="tipo_documento"
                                                    required
                                                    label="Tipo Documento"
                                                >
                                                </v-select>
                                            </v-col>
                                            <v-col cols="12" sm="6" md="6">
                                                <v-text-field
                                                    v-model="item.numero_documento"
                                                    prepend-icon="person"
                                                    label="Numero Documento"
                                                    required
                                                    type="text"
                                                    maxlength="255"
                                                    id="numero_documento"
                                                />
                                            </v-col>
                                            <v-col cols="12" sm="6" md="6">
                                                <v-text-field
                                                    v-model="item.balance"
                                                    prepend-icon="person"
                                                    label="Balance"
                                                    required
                                                    type="number"
                                                    maxlength="255"
                                                    min="0"
                                                    id="balance"
                                                />
                                            </v-col>
                                            <v-col cols="12" sm="6" md="6">
                                                <v-text-field
                                                    v-model="item.numero_documento"
                                                    prepend-icon="person"
                                                    label="Numero Documento"
                                                    required
                                                    type="text"
                                                    maxlength="255"
                                                    id="numero_documento"
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
            <template v-slot:item.estado="{ item }">
                <Statusfortable :item="item.estado" />
            </template>
            <template v-slot:item.tipo_persona="{ item }">
                {{ personType(item.tipo_persona) }}
            </template>
            <template v-slot:item.tipo_documento="{ item }">
                {{ documentType(item.tipo_documento) }}
            </template>
        </v-data-table>
    </v-card>
</template>

<script>
import Statusfortable from "@/components/Statusfortable";
//import Swal from 'sweetalert2';

export default {
    name: 'Proveedor',
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
                nombre: "",
                tipo_persona: null,
                tipo_documento: null,
                numero_documento: "",
                balance: 0,
                estado: 1
            },
            headers: [
                {
                    text: "Nombre",
                    align: 'start',
                    value: "nombre"
                },
                {
                    text: "Tipo Persona",
                    value: "tipo_persona"
                },
                {
                    text: "Balance",
                    value: 'balance'
                },
                {
                    text: "Tipo Documento",
                    value: "tipo_documento"
                },
                {
                    text: "Numero Documento",
                    value: 'numero_documento'
                },
                {
                    text: "Estado",
                    value: "estado"
                }
            ],
            items: [],
            personTypes: [
                {
                    "id": 0,
                    "value": "Fisica"
                },
                {
                    "id": 1,
                    "value": "Juridica"
                }
            ],
            documentTypes: [
                {
                    "id": 0,
                    "value": "Cedula"
                },
                {
                    "id": 1,
                    "value": "RNC"
                },
                {
                    "id": 2,
                    "value": "Pasaporte"
                }
            ]
        }
    },
    computed: {
        formTitle() {
            const title = this.editedIndex === -1 ? "Crear" : "Editar";
            return `${title} Proveedor`;
        }
    },
    mounted() {
        this.getAllProveedores();
    },
    methods: {
        closeModal() {
            this.modal = false;
            this.item = Object.assign({}, {
                nombre: ""
            });
            this.$refs.form.resetValidation();
        },
        saveItem() {

        },
        getAllProveedores() {
            this.$http
                .get("proveedores/get")
                .then((response) => {
                    this.items = response.data;
                    this.isLoading = false;
                })
        },
        personType(type) {
            return (type == 0) ? "Fisica" : "Juridica";
        },
        documentType(type) {
            if (type == 0) return "Cedula"
            else if (type == 1) return "RNC"
            else return "Pasaporte"
        }
    }
}
</script>
