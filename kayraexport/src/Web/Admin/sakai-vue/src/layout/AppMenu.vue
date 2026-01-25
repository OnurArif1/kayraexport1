<script setup>
import AppMenuItem from './AppMenuItem.vue';

const model = [
    {
        label: 'Ana Menü',
        items: [
            { label: 'Gösterge Paneli', icon: 'pi pi-fw pi-home', to: '/' },
            { label: 'Ürün Listesi', icon: 'pi pi-fw pi-list', to: '/products' },
            { label: 'Ürün Ekle', icon: 'pi pi-fw pi-plus', to: '/products/new' }
        ]
    }
];
</script>

<template>
    <ul class="layout-menu">
        <template v-for="(item, i) in model" :key="item.label">
            <app-menu-item v-if="!item.separator" :item="item" :index="i"></app-menu-item>
            <li v-if="item.separator" class="menu-separator"></li>
        </template>
    </ul>
</template>

<style lang="scss" scoped></style>
