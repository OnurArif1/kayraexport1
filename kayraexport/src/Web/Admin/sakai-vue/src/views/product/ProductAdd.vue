<script setup>
import { ref, reactive } from 'vue';
import { useRouter } from 'vue-router';
import { useToast } from 'primevue/usetoast';
import { createProduct } from '@/service/ProductService';

const router = useRouter();
const toast = useToast();
const loading = ref(false);
const form = reactive({
    name: '',
    description: '',
    price: 0,
    stock: 0
});

async function submit() {
    if (!form.name?.trim()) {
        toast.add({ severity: 'warn', summary: 'Uyarı', detail: 'Ürün adı zorunludur.', life: 3000 });
        return;
    }
    loading.value = true;
    try {
        await createProduct({
            name: form.name.trim(),
            description: form.description?.trim() || undefined,
            price: Number(form.price) || 0,
            stock: Number(form.stock) || 0
        });
        toast.add({ severity: 'success', summary: 'Başarılı', detail: 'Ürün eklendi.', life: 3000 });
        router.push({ name: 'product-list' });
    } catch (err) {
        const msg = err.response?.data?.error ?? err.response?.data?.message ?? err.message ?? 'Ürün eklenirken hata oluştu.';
        toast.add({ severity: 'error', summary: 'Hata', detail: msg, life: 4000 });
    } finally {
        loading.value = false;
    }
}

function cancel() {
    router.push({ name: 'product-list' });
}
</script>

<template>
    <div class="grid">
        <div class="col-12 md:col-8 lg:col-6">
            <div class="card">
                <h5 class="mb-4">Yeni Ürün Ekle</h5>
                <form @submit.prevent="submit" class="flex flex-column gap-3">
                    <div class="flex flex-column gap-2">
                        <label for="name">Ürün Adı <span class="text-red-500">*</span></label>
                        <InputText id="name" v-model="form.name" placeholder="Örn. Laptop" required />
                    </div>
                    <div class="flex flex-column gap-2">
                        <label for="description">Açıklama</label>
                        <Textarea id="description" v-model="form.description" rows="3" placeholder="Kısa açıklama" />
                    </div>
                    <div class="flex flex-column gap-2">
                        <label for="price">Fiyat (₺)</label>
                        <InputNumber id="price" v-model="form.price" :min-fraction-digits="2" :max-fraction-digits="2" :min="0" />
                    </div>
                    <div class="flex flex-column gap-2">
                        <label for="stock">Stok</label>
                        <InputNumber id="stock" v-model="form.stock" :min="0" integer-only />
                    </div>
                    <div class="flex gap-2 mt-2">
                        <Button type="submit" label="Kaydet" icon="pi pi-check" :loading="loading" />
                        <Button type="button" label="İptal" severity="secondary" icon="pi pi-times" @click="cancel" />
                    </div>
                </form>
            </div>
        </div>
    </div>
</template>
