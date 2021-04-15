<template>
    <v-card>
        <v-card-title>Asientos Contable</v-card-title>
        <v-card-text>
            <v-row>
                <v-col cols="12" sm="5" md="5">
                    <template>
                        <v-menu
                            :close-on-content-click="false"
                            transition="scale-transition"
                            offset-y
                            min-width="auto">
                            <template v-slot:activator="{ on, attrs }">
                                <v-text-field
                                    v-model="filtro.desde"
                                    label="Desde"
                                    prepend-icon="mdi-calendar"
                                    readonly
                                    v-bind="attrs"
                                    v-on="on"
                                ></v-text-field>
                            </template>
                            <v-date-picker
                                ref="picker"
                                v-model="filtro.desde"
                                :max="currentDate"
                                min="1950-01-01">
                            </v-date-picker>
                        </v-menu>
                    </template>
                </v-col>

                 <v-col cols="12" sm="5" md="5">
                    <template>
                        <v-menu
                            :close-on-content-click="false"
                            transition="scale-transition"
                            offset-y
                            min-width="auto">
                            <template v-slot:activator="{ on, attrs }">
                                <v-text-field
                                    v-model="filtro.hasta"
                                    label="Hasta"
                                    prepend-icon="mdi-calendar"
                                    readonly
                                    v-bind="attrs"
                                    v-on="on"
                                ></v-text-field>
                            </template>
                            <v-date-picker
                                ref="picker"
                                v-model="filtro.hasta"
                                :max="currentDate"
                                min="1950-01-01">
                            </v-date-picker>
                        </v-menu>
                    </template>
                </v-col>
                <v-col cols="12" sm="2" md="2">
                    <template>
                        <v-btn color="warning" dark class="mb-2" @click="search">
                            <v-icon>search</v-icon> Buscar
                        </v-btn>
                    </template>
                </v-col>
            </v-row>
        </v-card-text>
        <v-data-table
            class="elevation-1"
            :loading="isLoading"
            loading-text="Por favor espere..."
            :items="items"
            :headers="headers"
        >
        <template v-slot:item.fecha_transaccion="{ item }">
            <span>{{ item.fecha_transaccion ? new Date(item.fecha_transaccion).toDateString() : null }}</span>
        </template>
        </v-data-table>
        <v-card-text>
            <v-row>
                <v-col rows="12" md="10" sm="10"></v-col>
                <v-col rows="12" md="2" sm="2">
                    <template>
                        <v-btn color="primary" dark class="mb-2" @click="validateAndSave">
                            <v-icon>check</v-icon> Contabilizar
                        </v-btn>
                    </template>
                </v-col>
            </v-row>
        </v-card-text>
    </v-card>
</template>

<script>
import Swal from 'sweetalert2';

export default {
    name: "asientoscontable",
    data: () => {
        return {
            isLoading: true,
            filtro: {
                desde: new Date(),
                hasta: new Date()
            },
            headers: [
                {
                    text: "No. documento",
                    align: 'start',
                    value: "id_transaccion"
                },
                {
                    text: "Descripción",
                    value: "descripcion"
                },
                {
                    text: "Fecha transaccion",
                    value: "fecha_transaccion"
                },
                {
                    text: "Monto",
                    value: 'monto'
                }
            ],
            items: [],
            currentDate: null
        }
    },
    mounted() {
        this.search();
        this.filtro.desde = this.setToday();
        this.filtro.hasta = this.setToday();
        this.currentDate = this.setToday();
    },
    methods: {
        search() {
           this.$http
                .post("EntraDeDocumentos/search", this.filtro)
                .then((response) => {
                    this.items = response.data;
                    this.isLoading = false;
                });
        },
        validateAndSave() {
            if(this.items.length) {
                Swal
                    .fire({
                        title: "¿Deseas contabilizar estos elementos?",
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
                                    const ids = this.items.map(x=> x.id_transaccion);
                                    this.$http
                                        .post("EntraDeDocumentos/Contabilizar", {
                                            transactions: ids
                                        })
                                        .then((response) => {
                                            this.isLoading = false;
                                            if(response?.status == 200) {
                                                this.clear();
                                                Swal.fire({
                                                    title: "Elementos contabilizados exitosamente",
                                                    icon: 'success',
                                                });
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
                            })
                        }
                    })
            } else {
                Swal
                .fire({
                    title: "No hay elementos que contabilizar",
                    icon: 'warning',
                    allowEscapeKey: false,
                    allowOutsideClick: false,
                    confirmButtonText: "Aceptar",
                    reverseButtons: true
                });
            }
        },
        clear() {
            this.items = [];
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
