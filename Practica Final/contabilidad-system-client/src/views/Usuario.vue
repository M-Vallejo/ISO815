<template>
    <v-card>
        <v-card-title>Usuarios</v-card-title>
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
                                                    v-model="item.nombre_usuario"
                                                    prepend-icon="person"
                                                    label="Nombre de Usuario"
                                                    required
                                                    type="text"
                                                    maxlength="255"
                                                    id="nombre_usuario"
                                                />
                                            </v-col>
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
                                                <v-text-field
                                                    v-model="item.apellidos"
                                                    prepend-icon="person"
                                                    label="Apellidos"
                                                    required
                                                    type="text"
                                                    maxlength="255"
                                                    id="apellidos"
                                                />
                                            </v-col>
                                            <v-col cols="12" sm="6" md="6">
                                                <v-text-field
                                                    v-model="item.correo"
                                                    prepend-icon="email"
                                                    label="Correo"
                                                    required
                                                    type="email"
                                                    maxlength="255"
                                                    id="correo"
                                                />
                                            </v-col>
                                            <v-col cols="12" sm="6" md="6">
                                                <v-select
                                                    v-model="item.rol"
                                                    prepend-icon="mdi-script-text"
                                                    :items="roles"
                                                    item-value="id"
                                                    item-text="value"
                                                    id="rol"
                                                    required
                                                    label="Rol de Usuario"
                                                >
                                                </v-select>
                                            </v-col>
                                            <v-col cols="12" sm="6" md="6">
                                                <v-select
                                                    v-model="item.estado"
                                                    prepend-icon="mdi-check-decagram"
                                                    label="Estado"
                                                    required
                                                    :items="estados"
                                                    item-value="id"
                                                    item-text="value"
                                                    id="estado"
                                                />
                                            </v-col>
                                            <v-col cols="12" sm="6" md="6">
                                                <v-text-field
                                                    v-model="item.clave"
                                                    prepend-icon="vpn_key"
                                                    label="Clave"
                                                    required
                                                    type="password"
                                                    maxlength="255"
                                                    id="clave"
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
                <v-icon small @click="deleteItem(item.usuario_id)">delete</v-icon>
            </template>
        </v-data-table>
    </v-card>
</template>

<script>
import Statusfortable from "@/components/Statusfortable";
import Swal from 'sweetalert2';

export default {
    name: "Usuario",
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
                nombre_usuario: "",
                clave: "",
                nombre: "",
                apellidos: "",
                correo: "",
                rol: null,
                estado: 1
            },
            headers: [
                {
                    text: "Usuario",
                    align: 'start',
                    value: "nombre_usuario"
                },
                {
                    text: "Nombre",
                    value: "nombre"
                },
                {
                    text: "Apellidos",
                    value: "apellidos"
                },
                {
                    text: "Correo",
                    value: "correo"
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
            ],
            roles: [
                {
                    "id": 0,
                    "value": "Usuario"
                },
                {
                    "id": 1,
                    "value": "Administrador"
                }
            ]
        }
    },
    computed: {
        formTitle() {
            const title = this.editedIndex === -1 ? "Crear" : "Editar";
            return `${title} Usuario`;
        }
    },
    mounted() {
        this.getAllUsuarios();
    },
    methods: {
        getAllUsuarios() {
            this.$http
                .get("usuario/get")
                .then((response) => {
                    this.items = response.data;
                    this.isLoading = false;
                })
        },
        closeModal() {
            this.editedIndex = -1;
            this.modal = false;
            this.item = Object.assign({}, {
                nombre_usuario: "",
                clave: "",
                nombre: "",
                apellidos: "",
                correo: "",
                rol: null,
                estado: 1
            });
            this.$refs.form.resetValidation();
        },
        editItem(item) {
            this.editedIndex = this.items.indexOf(item);
            this.item = Object.assign({}, item);
            this.modal = true;
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
                                    .put(`usuario/ChangeEstadoUsuario?id=${item}&estado=2`)
                                    .then((result) => {
                                        if (result.status === 200) {
                                            Swal.fire({
                                                title: "Elemento Eliminado exitosamente",
                                                icon: 'success',
                                            });
                                            this.getAllUsuarios();
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
                });
        },
        validateData() {
            let nombre_usuario = document.getElementById("nombre_usuario");
            let nombre = document.getElementById("nombre");
            let apellidos = document.getElementById("apellidos");
            let correo = document.getElementById("correo");
            let rol = document.getElementById("rol");
            let estado = document.getElementById("estado");
            let clave = document.getElementById("clave");

            if (!nombre_usuario.checkValidity()) {
                nombre_usuario.focus();
                return false;
            }

            if (!nombre.checkValidity()) {
                nombre.focus();
                return false;
            }

            if (!apellidos.checkValidity()) {
                apellidos.focus();
                return false;
            }

            if (!correo.checkValidity()) {
                correo.focus();
                return false;
            }

            if (!rol.checkValidity()) {
                rol.focus();
                return false;
            }

            if (!estado.checkValidity()) {
                estado.focus();
                return false;
            }

            if (!clave.checkValidity()) {
                clave.focus();
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
                                .post('usuario/CreateUsuario', {
                                    nombre_usuario: this.item.nombre_usuario,
                                    clave: this.item.clave,
                                    nombre: this.item.nombre,
                                    apellidos: this.item.apellidos,
                                    correo: this.item.correo,
                                    rol: parseInt(this.item.rol),
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
                                                this.getAllUsuarios();
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
                                .put('usuario/EditUsuario', {
                                    usuario_id: parseInt(this.item.usuario_id),
                                    nombre_usuario: this.item.nombre_usuario,
                                    clave: this.item.clave,
                                    nombre: this.item.nombre,
                                    apellidos: this.item.apellidos,
                                    correo: this.item.correo,
                                    rol: parseInt(this.item.rol),
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
                                                this.getAllUsuarios();
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
        }
    }
}
</script>
