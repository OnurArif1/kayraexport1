<script setup>
import { useRouter } from 'vue-router';
import { useI18n } from 'vue-i18n';

const router = useRouter();
const { t } = useI18n();
</script>

<template>
    <div class="grid">
        <div class="col-12">
            <div class="card">
                <h5 class="mb-3">{{ t('dashboard.welcome') }}</h5>
                <p class="m-0 text-color-secondary" v-html="t('dashboard.welcomeMessage')">
                </p>
                <div class="flex gap-2 mt-4">
                    <Button :label="t('dashboard.productList')" icon="pi pi-list" @click="router.push('/products')" />
                    <Button :label="t('dashboard.addProduct')" icon="pi pi-plus" @click="router.push('/products/new')" />
                </div>
            </div>
        </div>
    </div>
</template>
