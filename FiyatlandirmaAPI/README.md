Obilet & İTÜ ATOM Hackathon: Fiyatlandırma API Çözümü

Bu proje, Obilet & İTÜ ATOM Hackathon'u için geliştirilmiş, kurallara dayalı, ölçeklenebilir ve izlenebilir bir bilet fiyatlandırma RESTful API çözümüdür.

✅ Teslimat Durumu ve Kriter Özeti

Proje, tüm zorunlu kriterleri ve 4 BONUS görevin 4'ünü de başarıyla tamamlamıştır.

Kriter Alanı

Kriter

Durum

Açıklama

Zorunlu

Kuralların Uygulanabilirliği

✅ TAMAM

Fiyat hesaplama mantığı kurallara uygun olarak Controller'da uygulanmıştır.

Zorunlu

API Tasarım ve Performansı

✅ TAMAM

Temiz, tek sorumluluklu GET /api/fiyat/hesapla endpoint'i kullanılmıştır.

Zorunlu

Kod Kalitesi ve Mimari

✅ TAMAM

Dependency Injection (DI), ILogger ve IConfiguration kullanılarak .NET Best Practices'e uygun mimari kurulmuştur.

BONUS

Yapılandırılabilir Kurallar

✅ TAMAM

Fiyatlandırma yüzdeleri ve limitleri appsettings.json üzerinden okunur.

BONUS

Detaylı Loglama ve İzlenebilirlik

✅ TAMAM

ILogger kullanılarak fiyat hesaplama sürecinin her adımı loglanmıştır.

BONUS

Docker ile Konteynerizasyon

✅ TAMAM

Uygulama, Dockerfile ile konteynerize edilmiştir.

BONUS

Kuyruk Tabanlı İşleme

✅ TAMAM

IHostedService ile toplu fiyat güncelleme işini simüle eden Arka Plan Görevi (Queue Worker) eklenmiştir.

ÖZGÜNLÜK

Kullanıcı Dostu Çıktı

✅ TAMAM

API çıktısına "Erken Alım Fırsatı" ve "Doluluk İndirimi" gibi etiketler eklenmiştir.

🛠️ RESTful API Kurulumu ve Testi

Proje, Docker konteyneri içinde çalışmak üzere yapılandırılmıştır.

1. Docker ile Çalıştırma (Önerilen)

Docker kurulu olan bir ortamda, proje klasöründe (FiyatlandirmaAPI dizininde) sırayla aşağıdaki komutlar çalıştırılır:

Docker İmajını Oluşturma:

docker build -t obilet-fiyatlandirma .


Konteyneri Çalıştırma: (8081 portuna yönlendirildi)

docker run -d -p 0.0.0.0:8081:80 --name fiyat-servisi obilet-fiyatlandirma


2. API Testi ve Dokümantasyon

Uygulama çalıştıktan sonra, testler Swagger veya Terminal üzerinden yapılabilir:

A. Swagger (Web Arayüzü) Testi

Tarayıcınızda şu adrese gidin:
http://localhost:8081/swagger

B. Terminal (Kanıt) Testi

Bu komut, kodun çalıştığına dair kesin JSON çıktısını terminale döker:

# Kanıt Komutu: Temel Fiyat 1000 TL ve Erken Rezervasyon (true) ile sorgulama
docker exec fiyat-servisi curl http://localhost/api/fiyat/hesapla?temelFiyat=1000&erkenRezervasyon=true


C. Detaylı Loglama Kontrolü (Bonus 2 ve 4 Kanıtı)

Hesaplama adımlarını ve arka plan kuyruk görevini (her 15 saniyede bir çalışır) kontrol etmek için:

docker logs fiyat-servisi


💡 Problem Çözüm Yaklaşımı ve Mimari

1. Teknik Mimari

Teknoloji: .NET 8.0 Web API (RESTful)

Bağımlılık Yönetimi: Dependency Injection (IConfiguration, ILogger) kullanılarak kodun sürdürülebilirliği sağlanmıştır.

İzlenebilirlik: ILogger (Detaylı Loglama) ile her fiyat kuralının başlangıç, bitiş ve uygulanan indirim miktarı adım adım loglanmıştır.

Taşınabilirlik: Dockerfile (Konteynerizasyon) ile uygulama, bağımsız bir konteyner içinde çalışacak şekilde paketlenmiştir.

2. Vizyon ve Ölçeklenebilirlik

Bu çözüm, gelecekteki genişlemeler için hazırdır:

Kuyruk Sistemi (IHostedService): ArkaPlanGorevi sınıfı, dışarıdan gelen uzun süreli veya toplu işleri (örn. tüm fiyatları günlük güncelleme) arka plana atarak API'nin performansını ve yanıt süresini korur. Bu, çözümün yüksek trafikli senaryolara uyarlanabileceğini gösterir.

Kural Yönetimi: Tüm kurallar appsettings.json'da tutulduğundan, yeni bir indirim yüzdesi veya kural limiti, kod derlenmeden canlı ortamda değiştirilebilir.

Adaptasyon: Mimari, otobüs, uçak veya diğer hizmetler için kolayca yeni Controller'lar eklenerek farklı sektörlere hızla adapte edilebilir.
