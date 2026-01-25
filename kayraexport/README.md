# KayraExport – Ürün API + Vue Admin

Controller–Service–Repository mimarisinde .NET Product API ve Vue.js Admin frontend.

## Mimari

- **KayraExport.API** – Ürün CRUD API (PostgreSQL, EF Core, Swagger)
- **Vue Admin** (sakai-vue) – Ürün listeleme ve ekleme (Vue 3, PrimeVue, TailwindCSS, i18n)

## Gereksinimler

- .NET 9 SDK
- Node.js 18+ ve npm
- PostgreSQL 15 (veya Docker)
- Docker & Docker Compose (isteğe bağlı)

## Projeyi Çalıştırma

### Adım 1: PostgreSQL'i Başlat

Docker ile PostgreSQL ve pgAdmin'i başlatın:

```bash
cd kayraexport
docker compose up -d
```

Eski container'lar varsa önce temizleyin:

```bash
docker compose down --remove-orphans
docker compose up -d
```

Çalışan servisler:

- **PostgreSQL** – `localhost:5432` 
  - Database: `kayraexportdb`
  - User: `kayraexportuser`
  - Password: `12345`
- **pgAdmin** – `http://localhost:8080`
  - Email: `admin@kayraexport.com`
  - Password: `admin123`

### Adım 2: Veritabanı Migration'larını Çalıştır

API projesine gidin ve migration'ları uygulayın:

```bash
cd src/KayraExport.API
dotnet ef database update
```

**Not:** İlk çalıştırmada migration'lar otomatik olarak uygulanır, ancak manuel olarak da çalıştırabilirsiniz.

### Adım 3: API'yi Başlat

#### Yöntem 1: Terminal'den Çalıştırma

```bash
cd src/KayraExport.API
dotnet run
```

#### Yöntem 2: VS Code ile Debug Modda Çalıştırma

1. VS Code'da projeyi açın
2. Debug panelinden (F5) **"KayraExport.API (Product API)"** konfigürasyonunu seçin
3. Veya `launch.json` dosyasındaki profil ile çalıştırın

API çalıştığında:
- **API:** `http://localhost:6165`
- **Swagger UI:** `http://localhost:6165/swagger`

### Adım 4: Frontend'i Başlat

Vue Admin uygulamasını başlatın:

```bash
cd src/Web/Admin/sakai-vue
npm install
npm run dev
```

**Önemli:** `npm run dev` komutunu mutlaka **sakai-vue** klasörü içinden çalıştırın. `src/Web/Admin` üzerinden çalıştırırsanız farklı proje ayağa kalkar ve 404 hatası alırsınız.

Frontend çalıştığında:
- **Admin Panel:** `http://localhost:5173`

### Adım 5: Dil Değiştirme

Uygulama İngilizce ve Türkçe dil desteğine sahiptir. Sağ üstteki dil seçici ile dil değiştirebilirsiniz.

## Portlar

| Servis      | Port  | Açıklama                    |
|------------|-------|------------------------------|
| PostgreSQL | 5432  | Veritabanı                   |
| pgAdmin    | 8080  | Veritabanı yönetim arayüzü   |
| Product API| 6165  | Backend API (HTTP)           |
| Product API| 7165  | Backend API (HTTPS)          |
| Admin (Vue)| 5173  | Frontend uygulaması          |

## Ortam Değişkenleri

Frontend için API URL'ini değiştirmek isterseniz `.env` veya `.env.local` dosyası oluşturun:

```env
VITE_API_URL=http://localhost:6165
```

Varsayılan değer zaten `http://localhost:6165`'tir.

## Portlar

| Servis      | Port  |
|------------|-------|
| PostgreSQL | 5432  |
| pgAdmin    | 8080  |
| Product API| 6165 (yerel `dotnet run`) |
| Admin (Vue)| 5173 (yerel `npm run dev`) |

## API Endpoints

- `GET /api/products` – Ürün listesi (sayfalama, arama desteği)
- `GET /api/products/{id}` – Tekil ürün detayı
- `POST /api/products` – Yeni ürün ekle
- `PUT /api/products/{id}` – Ürün güncelle
- `DELETE /api/products/{id}` – Ürün sil

## Sorun Giderme

### PostgreSQL Bağlantı Hatası

- PostgreSQL container'ının çalıştığından emin olun: `docker compose ps`
- Container'ı yeniden başlatın: `docker compose restart postgres`

### API Çalışmıyor

- Migration'ların uygulandığından emin olun: `dotnet ef database update`
- Port 6165'in kullanılmadığından emin olun
- `appsettings.json` ve `appsettings.Development.json` dosyalarındaki connection string'i kontrol edin

### Frontend Çalışmıyor

- `node_modules` klasörünü silip yeniden yükleyin:
  ```bash
  rm -rf node_modules package-lock.json
  npm install
  ```
- Port 5173'in kullanılmadığından emin olun
- `sakai-vue` klasörü içinden çalıştırdığınızdan emin olun

### Migration Hataları

- Veritabanını sıfırlamak için:
  ```bash
  dotnet ef database drop
  dotnet ef database update
  ```

## Geliştirme Notları

- **i18n Desteği:** Uygulama İngilizce ve Türkçe dil desteğine sahiptir. Çeviriler `src/Web/Admin/sakai-vue/src/i18n/locales/` klasöründe bulunur.
- **VS Code Debug:** `.vscode/launch.json` ve `.vscode/tasks.json` dosyaları hazırlanmıştır. F5 ile debug modda çalıştırabilirsiniz.
- **Hot Reload:** Frontend'de değişiklik yaptığınızda otomatik olarak yenilenir.
- **Swagger:** API dokümantasyonu için `http://localhost:6165/swagger` adresini kullanın.
