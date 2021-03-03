<template>
  <v-app>
    <v-app-bar app color="primary" dark>
      <v-toolbar-title>Listar Pagos</v-toolbar-title>
    </v-app-bar>
    <v-main>
      <ListPayments />
    </v-main>
  </v-app>
</template>

<script>
import ListPayments from './components/ListPayments';

export default {
  name: 'App',

  components: {
    ListPayments
  }
};
</script>
