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
                                                <v-select
                                                    v-model="item.estado"
                                                    prepend-icon="person"
                                                    label="Estado"
                                                    required
                                                    :items="estados"
                                                    item-value="id"
                                                    item-text="value"
                                                    id="estado"
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
            <template v-slot:item.actions="{ item }">
                <v-icon small class="mr-2" @click="editItem(item)">edit</v-icon>
                <v-icon small @click="deleteItem(item.proveedor_id)">delete</v-icon>
            </template>
        </v-data-table>
    </v-card>
</template>

<script>
import Statusfortable from "@/components/Statusfortable";
import Swal from 'sweetalert2';

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
                },
                {
                    text: "Acciones",
                    value: "actions",
                    sortable: false
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
            ],
            estados: [
                {
                    "id": 0,
                    "value": "Inactivo"
                },
                {
                    "id": 1,
                    "value": "Activo"
                },
                {
                    "id": 2,
                    "value": "Eliminado"
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
            this.editedIndex = -1;
            this.modal = false;
            this.item = Object.assign({}, {
                nombre: "",
                tipo_persona: null,
                tipo_documento: null,
                numero_documento: "",
                balance: 0,
                estado: 1
            });
            this.$refs.form.resetValidation();
        },
        editItem(item) {
            this.editedIndex = this.items.indexOf(item);
            this.item = Object.assign({}, item);
            this.modal = true;
        },
        validateData() {
            let nombre = document.getElementById("nombre");
            let tipo_persona = document.getElementById("tipo_persona");
            let tipo_documento = document.getElementById("tipo_documento");
            let numero_documento = document.getElementById("numero_documento");
            let balance = document.getElementById("balance");
            let estado = document.getElementById("estado");

            if (!nombre.checkValidity()) {
                nombre.focus();
                return false;
            }

            if (!tipo_persona.checkValidity()) {
                tipo_persona.focus();
                return false;
            }

            if (!tipo_documento.checkValidity()) {
                tipo_documento.focus();
                return false;
            }

            if (!numero_documento.checkValidity()) {
                numero_documento.focus();
                return false;
            }

            if (!balance.checkValidity()) {
                balance.focus();
                return false;
            }

            if (!estado.checkValidity()) {
                estado.focus();
                return false;
            }

            return true;
        },
        saveItem() {
            if (this.validateData()) {
                Swal.fire({
                    title: "Espere por favor",
                    allowEscapeKey: false,
                    allowOutsideClick: false,
                    showConfirmButton: false,
                    didOpen: () => {
                        Swal.showLoading();

                        //create item
                        if (this.editedIndex === -1)
                        {
                            this.$http
                                .post('proveedores/CreateProveedor', {
                                    nombre: this.item.nombre,
                                    tipo_persona: parseInt(this.item.tipo_persona),
                                    tipo_documento: parseInt(this.item.tipo_documento),
                                    numero_documento: this.item.numero_documento,
                                    balance: parseFloat(this.item.balance)
                                })
                                .then((response) => {
                                    if (response.status === 200 && response)
                                    {
                                         Swal.fire({
                                            title:"Elemento creado exitosamente",
                                            icon: 'success',
                                            preConfirm: () => {
                                                this.closeModal();
                                                this.getAllProveedores();
                                            }
                                        });
                                    }
                                    else
                                    {
                                        Swal.fire({
                                            title:"ha ocurrido un error en el servidor",
                                            icon: 'danger'
                                        });
                                    }
                                });
                        }
                        else
                        {
                            this.$http
                                .put('proveedores/EditProveedor', {
                                    proveedor_id: parseInt(this.item.proveedor_id),
                                    nombre: this.item.nombre,
                                    tipo_persona: parseInt(this.item.tipo_persona),
                                    tipo_documento: parseInt(this.item.tipo_documento),
                                    numero_documento: this.item.numero_documento,
                                    balance: parseFloat(this.item.balance),
                                    estado: parseInt(this.item.estado)
                                })
                                .then((response) => {
                                    if (response.status === 200 && response)
                                    {
                                         Swal.fire({
                                            title:"Elemento actualizado exitosamente",
                                            icon: 'success',
                                            preConfirm: () => {
                                                this.closeModal();
                                                this.getAllProveedores();
                                            }
                                        });
                                    }
                                    else
                                    {
                                        Swal.fire({
                                            title:"ha ocurrido un error en el servidor",
                                            icon: 'danger'
                                        });
                                    }
                                });
                        }
                    }
                });
            }
        },
        deleteItem(item) {
            Swal
                .fire({
                    title: "Desea eliminar este elemento?",
                    icon: 'warning',
                    showCancelButton: true,
                    allowEscapeKey: false,
                    allowOutsideClick: false,
                    confirmButtonText: "Si",
                    cancelButtonText: "No",
                    reverseButtons: true
                })
                .then((result) => {
                    if (result.value) {
                        Swal.fire({
                            title: "Espere por favor...",
                            allowEscapeKey: false,
                            allowOutsideClick: false,
                            showConfirmButton: false,
                            didOpen: () => {
                                Swal.showLoading();
                                this.$http
                                    .put(`proveedores/ChangeEstadoProveedores?id=${item}&estado=2`)
                                    .then((result) => {
                                        if (result.status === 200) {
                                            Swal.fire({
                                                title: "Elemento Eliminado exitosamente",
                                                icon: 'success',
                                            });
                                            this.getAllProveedores();
                                        }
                                        else {
                                            Swal.fire({
                                                title: "Ha ocurrido un error",
                                                icon: 'danger'
                                            });
                                        }
                                    })
                            }
                        })
                    }
                })
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
