import axios from 'axios';

const baseURL = (import.meta.env.VITE_API_URL || 'http://localhost:6165').replace(/\/$/, '') + '/api';

const client = axios.create({
    baseURL,
    timeout: 30000,
    headers: { 'Content-Type': 'application/json' }
});

/**
 * Ürün listesini getirir (sayfalama ve arama destekli).
 * @param {{ searchTerm?: string, pageNumber?: number, pageSize?: number }} params
 * @returns {Promise<{ products: Array<{id:number,name:string,description:string,price:number,stock:number}>, totalItems: number, pageNumber: number, pageSize: number }>}
 */
export async function getProducts(params = {}) {
    const { data } = await client.get('/products', { params });
    return data;
}

/**
 * Id ile tek ürün getirir.
 * @param {number} id
 */
export async function getProductById(id) {
    const { data } = await client.get(`/products/${id}`);
    return data;
}

/**
 * Yeni ürün ekler.
 * @param {{ name: string, description?: string, price: number, stock: number }} payload
 * @returns {Promise<{ id: number }>}
 */
export async function createProduct(payload) {
    const res = await client.post('/products', payload);
    return res.data;
}

/**
 * Ürün günceller.
 * @param {number} id
 * @param {{ name: string, description?: string, price: number, stock: number }} payload
 */
export async function updateProduct(id, payload) {
    await client.put(`/products/${id}`, payload);
}

/**
 * Ürün siler.
 * @param {number} id
 */
export async function deleteProduct(id) {
    await client.delete(`/products/${id}`);
}

export default { getProducts, getProductById, createProduct, updateProduct, deleteProduct };
