# KayraExport – Ürün API + Vue Admin

Controller–Service–Repository mimarisinde .NET Product API ve Vue.js Admin frontend.

## Mimari

- **KayraExport.API** – Ürün CRUD API (PostgreSQL, EF Core, Swagger)
- **Vue Admin** (sakai-vue) – Ürün listeleme ve ekleme (Vue 3, PrimeVue, TailwindCSS)

Identity, Gateway ve Log.Api **kullanılmaz**; sadece Product API ve Admin vardır.

## Gereksinimler

- .NET 9
- Node.js 18+
- PostgreSQL 15 (veya Docker)
- Docker & Docker Compose (isteğe bağlı)

## Docker ile Çalıştırma

Docker’da sadece **PostgreSQL** ve **pgAdmin** çalışır. Product API ve Vue Admin yerelde çalıştırılır.

```bash
docker compose up -d
```

Eski container’lar kaldıysa: `docker compose down --remove-orphans` ardından `docker compose up -d`.

Çalışan servisler:

- **PostgreSQL** – `localhost:5432` (db: `kayraexportdb`, user: `kayraexportuser`, pass: `12345`)
- **pgAdmin** – `http://localhost:8080` (email: `admin@kayraexport.com`, pass: `admin123`)

## Manuel Çalıştırma

### 1. PostgreSQL

```bash
docker compose up -d postgres
```

Veya yerel PostgreSQL: `kayraexportdb`, `kayraexportuser` / `12345`.

### 2. Product API

```bash
cd src/KayraExport.API
dotnet run
```

API: `http://localhost:6165`, Swagger: `http://localhost:6165/swagger`.

### 3. Vue Admin

```bash
cd src/Web/Admin/sakai-vue
npm install --legacy-peer-deps
npm run dev
```

Frontend: **http://localhost:5173**. API adresi için `.env` / `.env.local`:

**404 alıyorsan:** `npm run dev` mutlaka **sakai-vue** klasörü içinden çalıştırılmalı (`cd src/Web/Admin/sakai-vue`). `src/Web/Admin` üzerinden çalıştırırsan farklı proje ayağa kalkar ve 404 hatası alırsın.

```env
VITE_API_URL=http://localhost:6165
```

Varsayılan zaten `http://localhost:6165`’tir.

## Portlar

| Servis      | Port  |
|------------|-------|
| PostgreSQL | 5432  |
| pgAdmin    | 8080  |
| Product API| 6165 (yerel `dotnet run`) |
| Admin (Vue)| 5173 (yerel `npm run dev`) |

## API Özeti

- `GET /api/products` – Liste (sayfalama, arama)
- `GET /api/products/{id}` – Tekil ürün
- `POST /api/products` – Ürün ekle
- `PUT /api/products/{id}` – Güncelle
- `DELETE /api/products/{id}` – Sil

## VS Code

- **Launch:** “KayraExport.API (Product API)” ile API’yi debug’da çalıştırın.
- **Tasks:** `docker-compose-up` / `docker-compose-down` ile altyapıyı yönetin.
