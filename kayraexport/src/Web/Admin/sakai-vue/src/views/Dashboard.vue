<script setup>
import { useRouter } from 'vue-router';

const router = useRouter();
</script>

<template>
    <div class="grid">
        <div class="col-12">
            <div class="card">
                <h5 class="mb-3">Hoş Geldiniz</h5>
                <p class="m-0 text-color-secondary">
                    Ürün ekleme ve listeleme uygulamasına hoş geldiniz. Soldan <strong>Ürün Listesi</strong> veya
                    <strong>Ürün Ekle</strong> ile devam edebilirsiniz.
                </p>
                <div class="flex gap-2 mt-4">
                    <Button label="Ürün Listesi" icon="pi pi-list" @click="router.push('/products')" />
                    <Button label="Ürün Ekle" icon="pi pi-plus" @click="router.push('/products/new')" />
                </div>
            </div>
        </div>
    </div>
</template>
