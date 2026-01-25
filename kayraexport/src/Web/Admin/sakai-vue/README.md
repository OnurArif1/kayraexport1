# Ürün Admin (Vue.js)

Ürün ekleme ve listeleme uygulaması. Backend Product API ile iletişim kurar.

## Gereksinimler

- Node.js 18+
- Backend Product API (örn. `http://localhost:6165`)

## Kurulum

**Önemli:** Komutları `src/Web/Admin/sakai-vue` klasörü içinden çalıştırın.

```bash
cd src/Web/Admin/sakai-vue
npm install --legacy-peer-deps
```

## Yapılandırma (12 Factor App)

Backend API adresi ortam değişkeni ile yönetilir. Proje kökünde `.env` veya `.env.local` oluşturun:

```env
VITE_API_URL=http://localhost:6165
```

- **Yerel geliştirme:** Varsayılan `http://localhost:6165`. API’yi `dotnet run` veya Docker ile 6165’te çalıştırın. Ön yüz Docker’da çalıştırılmaz; her zaman `npm run dev` ile yerelde ayağa kaldırılır.

## Geliştirme

```bash
cd src/Web/Admin/sakai-vue
npm run dev
```

Tarayıcıda `http://localhost:5173`. Backend API’nin `http://localhost:6165` üzerinde çalışıyor olması gerekir.

## Derleme

```bash
npm run build
```

## Sayfalar

- **Gösterge Paneli** (`/`) – Hoş geldiniz ve kısayollar
- **Ürün Listesi** (`/products`) – GET /api/products ile ürünler, arama, sayfalama, silme
- **Ürün Ekle** (`/products/new`) – Form ile POST /api/products
