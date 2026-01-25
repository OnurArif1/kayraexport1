<script setup>
import { computed } from 'vue';
import { useI18n } from 'vue-i18n';
import AppMenuItem from './AppMenuItem.vue';

const { t } = useI18n();

const model = computed(() => [
    {
        label: t('menu.mainMenu'),
        items: [
            { label: t('menu.dashboard'), icon: 'pi pi-fw pi-home', to: '/' },
            { label: t('menu.productList'), icon: 'pi pi-fw pi-list', to: '/products' },
            { label: t('menu.addProduct'), icon: 'pi pi-fw pi-plus', to: '/products/new' }
        ]
    }
]);
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
