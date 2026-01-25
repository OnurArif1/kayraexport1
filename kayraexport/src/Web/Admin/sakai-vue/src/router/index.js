import AppLayout from '@/layout/AppLayout.vue';
import { createRouter, createWebHistory } from 'vue-router';

const router = createRouter({
    history: createWebHistory(),
    routes: [
        {
            path: '/',
            component: AppLayout,
            children: [
                {
                    path: '/',
                    name: 'dashboard',
                    component: () => import('@/views/Dashboard.vue')
                },
                {
                    path: '/products',
                    name: 'product-list',
                    component: () => import('@/views/product/ProductList.vue')
                },
                {
                    path: '/products/new',
                    name: 'product-add',
                    component: () => import('@/views/product/ProductAdd.vue')
                },
                {
                    path: '/products/:id/edit',
                    name: 'product-edit',
                    component: () => import('@/views/product/ProductEdit.vue'),
                    props: true
                }
            ]
        }
    ]
});

export default router;
