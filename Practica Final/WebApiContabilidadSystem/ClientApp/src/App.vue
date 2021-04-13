<template>
    <v-app>
        <Sidebar />
        <Appbar />
        <v-main>
            <router-view />
        </v-main>
    </v-app>
</template>

<script>
import Sidebar from '@/components/Sidebar';
import Appbar from '@/components/Appbar';

export default {
    name: 'App',
    components: {
        Sidebar,
        Appbar
    }
};
</script>
