<template>
    <v-card>
        <v-card-title>Conceptos de Pago</v-card-title>
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
                                                    v-model="item.descripcion"
                                                    prepend-icon="person"
                                                    label="Descripcion"
                                                    required
                                                    type="text"
                                                    maxlength="255"
                                                    id="descripcion"
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
            <template v-slot:item.actions="{ item }">
                <v-icon small class="mr-2" @click="editItem(item)">edit</v-icon>
                <v-icon small @click="deleteItem(item.concepto_pago_id)">delete</v-icon>
            </template>
        </v-data-table>
    </v-card>
</template>

<script>
import Statusfortable from "@/components/Statusfortable";
import Swal from 'sweetalert2';

export default {
    name: "Conceptos",
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
            return `${title} Concepto de Pago`;
        }
    },
    mounted() {
        this.getAllConceptos();
    },
    methods: {
        closeModal() {
            this.editedIndex = -1;
            this.modal = false;
            this.item = Object.assign({}, {
                descripcion: "",
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
            let descripcion = document.getElementById("descripcion");
            let estado = document.getElementById("estado");

            if (!descripcion.checkValidity()) {
                descripcion.focus();
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
                                .post('ConceptoPago/CreateConceptoPago', {
                                    descripcion: this.item.descripcion,
                                    estado: parseInt(this.item.estado)
                                })
                                .then((response) => {
                                    if (response.status === 200 && response)
                                    {
                                         Swal.fire({
                                            title:"Elemento creado exitosamente",
                                            icon: 'success',
                                            preConfirm: () => {
                                                this.closeModal();
                                                this.getAllConceptos();
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
                                .put('ConceptoPago/EditConceptoPago', {
                                    descripcion: this.item.descripcion,
                                    estado: parseInt(this.item.estado),
                                    concepto_pago_id: parseInt(this.item.concepto_pago_id)
                                })
                                .then((response) => {
                                    if (response.status === 200 && response)
                                    {
                                         Swal.fire({
                                            title:"Elemento actualizado exitosamente",
                                            icon: 'success',
                                            preConfirm: () => {
                                                this.closeModal();
                                                this.getAllConceptos();
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
                                    .put(`ConceptoPago/ChangeEstadoConceptoPago?id=${item}&estado=2`)
                                    .then((result) => {
                                        if (result.status === 200) {
                                            Swal.fire({
                                                title: "Elemento Eliminado exitosamente",
                                                icon: 'success',
                                            });
                                            this.getAllConceptos();
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
        getAllConceptos() {
            this.$http
                .get("ConceptoPago/get")
                .then((response) => {
                    this.items = response.data;
                    this.isLoading = false;
                })
        }
    }
}
</script>
