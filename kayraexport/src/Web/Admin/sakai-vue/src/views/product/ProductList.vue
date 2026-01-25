<script setup>
import { ref, onMounted } from 'vue';
import { useRouter } from 'vue-router';
import { useToast } from 'primevue/usetoast';
import { useI18n } from 'vue-i18n';
import { getProducts, deleteProduct } from '@/service/ProductService';

const { t } = useI18n();

const router = useRouter();
const toast = useToast();
const products = ref([]);
const totalItems = ref(0);
const loading = ref(false);
const pageNumber = ref(1);
const pageSize = ref(10);
const searchTerm = ref('');
const deleteDialog = ref(false);
const selectedProduct = ref(null);

async function load() {
    loading.value = true;
    try {
        const res = await getProducts({
            searchTerm: searchTerm.value || undefined,
            pageNumber: pageNumber.value,
            pageSize: pageSize.value
        });
        products.value = res.products ?? [];
        totalItems.value = res.totalItems ?? 0;
    } catch (err) {
        const msg = err.response?.data?.error ?? err.message ?? t('product.loadingError');
        toast.add({ severity: 'error', summary: t('product.error'), detail: msg, life: 4000 });
    } finally {
        loading.value = false;
    }
}

onMounted(load);

function onSearch() {
    pageNumber.value = 1;
    load();
}

function onPage(event) {
    pageNumber.value = (event.page ?? 0) + 1;
    pageSize.value = event.rows ?? 10;
    load();
}

function goToAdd() {
    router.push({ name: 'product-add' });
}

function goToEdit(p) {
    router.push({ name: 'product-edit', params: { id: p.id } });
}

function confirmDelete(p) {
    selectedProduct.value = p;
    deleteDialog.value = true;
}

async function doDelete() {
    if (!selectedProduct.value) return;
    try {
        await deleteProduct(selectedProduct.value.id);
        deleteDialog.value = false;
        selectedProduct.value = null;
        toast.add({ severity: 'success', summary: t('product.success'), detail: t('product.deleted'), life: 3000 });
        await load();
    } catch (err) {
        const msg = err.response?.data?.error ?? err.message ?? t('product.deleteError');
        toast.add({ severity: 'error', summary: t('product.error'), detail: msg, life: 4000 });
    }
}
</script>

<template>
    <div>
        <div class="grid">
            <div class="col-12">
                <div class="card">
                    <div class="flex flex-wrap align-items-center justify-content-between gap-2 mb-3">
                        <h5 class="m-0">{{ t('product.list') }}</h5>
                        <Button :label="t('product.newProduct')" icon="pi pi-plus" @click="goToAdd" />
                    </div>
                    <div class="flex flex-wrap gap-2 mb-3">
                        <InputText
                            v-model="searchTerm"
                            :placeholder="t('product.searchPlaceholder')"
                            class="flex-1"
                            style="min-width: 200px"
                            @keyup.enter="onSearch"
                        />
                        <Button :label="t('product.search')" icon="pi pi-search" @click="onSearch" />
                    </div>
                    <DataTable
                        :value="products"
                        :loading="loading"
                        :paginator="true"
                        :first="(pageNumber - 1) * pageSize"
                        :rows="pageSize"
                        :totalRecords="totalItems"
                        lazy
                        paginator-template="FirstPageLink PrevPageLink PageLinks NextPageLink LastPageLink CurrentPageReport RowsPerPageDropdown"
                        :rows-per-page-options="[5, 10, 25, 50]"
                        current-page-report-template="{first} - {last} / {totalRecords}"
                        @page="onPage"
                        striped-rows
                        responsive-layout="scroll"
                    >
                        <Column field="id" :header="t('product.id')" style="width: 5rem" />
                        <Column field="name" :header="t('product.name')" />
                        <Column field="description" :header="t('product.description')" />
                        <Column field="price" :header="t('product.price')">
                            <template #body="{ data }">
                                {{ Number(data.price).toLocaleString('tr-TR') }} ₺
                            </template>
                        </Column>
                        <Column field="stock" :header="t('product.stock')" style="width: 6rem" />
                        <Column :header="t('product.operations')" style="width: 10rem">
                            <template #body="{ data }">
                                <div class="flex gap-1">
                                    <Button icon="pi pi-pencil" severity="secondary" text rounded size="small" v-tooltip.top="t('product.update')" @click="goToEdit(data)" />
                                    <Button icon="pi pi-trash" severity="danger" text rounded size="small" v-tooltip.top="t('product.deleteTooltip')" @click="confirmDelete(data)" />
                                </div>
                            </template>
                        </Column>
                    </DataTable>
                </div>
            </div>
        </div>

        <Dialog :visible="deleteDialog" :header="t('product.deleteDialogTitle')" modal :closable="true" :style="{ width: '400px' }" @update:visible="deleteDialog = $event">
            <p v-if="selectedProduct">
                <strong>{{ selectedProduct.name }}</strong> {{ t('product.deleteConfirm') }}
            </p>
            <template #footer>
                <Button :label="t('product.cancel')" severity="secondary" @click="deleteDialog = false" />
                <Button :label="t('common.delete')" severity="danger" @click="doDelete" />
            </template>
        </Dialog>
    </div>
</template>
