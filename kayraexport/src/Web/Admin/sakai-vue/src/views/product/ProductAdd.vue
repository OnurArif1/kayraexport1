<script setup>
import { ref, reactive } from 'vue';
import { useRouter } from 'vue-router';
import { useToast } from 'primevue/usetoast';
import { useI18n } from 'vue-i18n';
import { createProduct } from '@/service/ProductService';

const { t } = useI18n();

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
        toast.add({ severity: 'warn', summary: t('product.warning'), detail: t('product.nameRequired'), life: 3000 });
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
        toast.add({ severity: 'success', summary: t('product.success'), detail: t('product.added'), life: 3000 });
        router.push({ name: 'product-list' });
    } catch (err) {
        const msg = err.response?.data?.error ?? err.response?.data?.message ?? err.message ?? t('product.addError');
        toast.add({ severity: 'error', summary: t('product.error'), detail: msg, life: 4000 });
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
                <h5 class="mb-4">{{ t('product.add') }}</h5>
                <form @submit.prevent="submit" class="flex flex-column gap-3">
                    <div class="flex flex-column gap-2">
                        <label for="name">{{ t('product.nameLabel') }} <span class="text-red-500">*</span></label>
                        <InputText id="name" v-model="form.name" :placeholder="t('product.namePlaceholder')" required />
                    </div>
                    <div class="flex flex-column gap-2">
                        <label for="description">{{ t('product.descriptionLabel') }}</label>
                        <Textarea id="description" v-model="form.description" rows="3" :placeholder="t('product.descriptionPlaceholder')" />
                    </div>
                    <div class="flex flex-column gap-2">
                        <label for="price">{{ t('product.priceLabel') }}</label>
                        <InputNumber id="price" v-model="form.price" :min-fraction-digits="2" :max-fraction-digits="2" :min="0" />
                    </div>
                    <div class="flex flex-column gap-2">
                        <label for="stock">{{ t('product.stockLabel') }}</label>
                        <InputNumber id="stock" v-model="form.stock" :min="0" integer-only />
                    </div>
                    <div class="flex gap-2 mt-2">
                        <Button type="submit" :label="t('product.save')" icon="pi pi-check" :loading="loading" />
                        <Button type="button" :label="t('product.cancel')" severity="secondary" icon="pi pi-times" @click="cancel" />
                    </div>
                </form>
            </div>
        </div>
    </div>
</template>
