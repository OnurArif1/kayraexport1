<script setup>
import { ref, reactive, onMounted, computed } from 'vue';
import { useRouter, useRoute } from 'vue-router';
import { useToast } from 'primevue/usetoast';
import { getProductById, updateProduct } from '@/service/ProductService';

const router = useRouter();
const route = useRoute();
const toast = useToast();
const loading = ref(false);
const loadingProduct = ref(true);
const form = reactive({
    name: '',
    description: '',
    price: 0,
    stock: 0
});

const productId = computed(() => Number(route.params.id));

async function loadProduct() {
    if (!productId.value || Number.isNaN(productId.value)) {
        toast.add({ severity: 'error', summary: 'Hata', detail: 'Geçersiz ürün.', life: 4000 });
        router.replace({ name: 'product-list' });
        return;
    }
    loadingProduct.value = true;
    try {
        const p = await getProductById(productId.value);
        form.name = p.name ?? '';
        form.description = p.description ?? '';
        form.price = Number(p.price) ?? 0;
        form.stock = Number(p.stock) ?? 0;
    } catch (err) {
        const msg = err.response?.data?.error ?? err.message ?? 'Ürün yüklenirken hata oluştu.';
        toast.add({ severity: 'error', summary: 'Hata', detail: msg, life: 4000 });
        router.replace({ name: 'product-list' });
    } finally {
        loadingProduct.value = false;
    }
}

onMounted(loadProduct);

async function submit() {
    if (!form.name?.trim()) {
        toast.add({ severity: 'warn', summary: 'Uyarı', detail: 'Ürün adı zorunludur.', life: 3000 });
        return;
    }
    loading.value = true;
    try {
        await updateProduct(productId.value, {
            name: form.name.trim(),
            description: form.description?.trim() || undefined,
            price: Number(form.price) || 0,
            stock: Number(form.stock) || 0
        });
        toast.add({ severity: 'success', summary: 'Başarılı', detail: 'Ürün güncellendi.', life: 3000 });
        router.push({ name: 'product-list' });
    } catch (err) {
        const msg = err.response?.data?.error ?? err.response?.data?.message ?? err.message ?? 'Ürün güncellenirken hata oluştu.';
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
                <h5 class="mb-4">Ürün Düzenle</h5>
                <div v-if="loadingProduct" class="flex align-items-center justify-content-center py-6">
                    <ProgressSpinner style="width: 3rem; height: 3rem" />
                </div>
                <form v-else @submit.prevent="submit" class="flex flex-column gap-3">
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
                        <Button type="submit" label="Güncelle" icon="pi pi-check" :loading="loading" />
                        <Button type="button" label="İptal" severity="secondary" icon="pi pi-times" @click="cancel" />
                    </div>
                </form>
            </div>
        </div>
    </div>
</template>
