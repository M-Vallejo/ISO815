<template>
    <v-chip dark :color="getStatus(item)">
        <v-avatar>
            <v-icon v-if="item === 1">check</v-icon>
            <v-icon v-else-if="item === 0">close</v-icon>
            <v-icon v-else>delete</v-icon>
        </v-avatar>
    </v-chip>
</template>

<script>
export default {
    name: 'Statusfortable',
    props: {
        item: {
            type: Number,
            required: true
        }
    },
    methods:  {
        getStatus() {
            return (this.item === 1) ? "green": "red";
        }
    }
}
</script>
