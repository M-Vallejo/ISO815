<template>
  <v-container>
    <v-row>
      <v-col cols="6">
        <v-text-field v-model="matricula" label="Matricula"></v-text-field>
      </v-col>
      <v-col cols="6">
        <v-btn block color="primary" @click="searchStudent">
          Buscar
        </v-btn>
      </v-col>
    </v-row> 
    <v-row>
      <v-col cols="6">
        <v-text-field v-model="cabecera.numeroCuenta" readonly label="Numero de Cuenta"></v-text-field>
        <v-text-field v-model="cabecera.rnc" readonly label="RNC"></v-text-field>
        <v-text-field v-model="cabecera.montoTotal" readonly label="Monto Total"></v-text-field>
      </v-col>
      <v-col cols="6">
        <v-text-field v-model="cabecera.cantidadRegistros" readonly label="Cantidad de Registros"></v-text-field>
        <v-text-field v-model="cabecera.fechaPago" readonly label="Fecha de Pago"></v-text-field>
      </v-col>
    </v-row>
    <v-data-table :items-per-page="10" :headers="headers" :items="detalles" :loading="isLoading" loading-text="Cargando...">
    </v-data-table>
  </v-container>
</template>

<script>
  import axios from 'axios';

  export default {
    name: 'ListPayments',
    data() {
      return {
        detalles: [],
        cabecera: {
          numeroCuenta: null,
          rnc: null,
          montoTotal: null,
          cantidadRegistros: null,
          fechaPago: null
        },
        headers: [
          {
            text: 'Tipo Documento',
            value: 'tipoDocumento'
          },
          {
            text: 'Numero Documento',
            value: 'numeroDocumento'
          },
          {
            text: 'Matricula',
            value: 'matricula'
          },
          {
            text: 'Monto',
            value: 'monto'
          },
          {
            text: 'Tipo de Pago',
            value: 'tipoPago'
          }
        ],
        matricula: null,
        isLoading: true
      }
    },
    mounted() {
      axios
        .get("https://localhost:44319/api/payment/getpayments")
        .then(response => {
          this.detalles = response.data.detalles;
          this.cabecera = response.data.cabecera;
          this.isLoading = false;
        })
    },
    methods: {
      searchStudent: function() {
        if (this.matricula !== null) {
          this.isLoading = true;
          axios
            .get(`https://localhost:44319/api/payment/GetPaymentsByStudentId?matricula=${this.matricula}`)
            .then(response => {
              this.detalles = response.data.detalles;
              this.cabecera = response.data.cabecera;
              this.isLoading = false;
            });
          this.matricula = null;
        }
      }
    }
  }
</script>
