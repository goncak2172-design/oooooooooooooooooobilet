using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

// IHostedService'ten kalıtım alan bu sınıf, uygulamanın arka planında çalışır
public class ArkaPlanGorevi : BackgroundService
{
    // Loglama servisini enjekte ediyoruz
    private readonly ILogger<ArkaPlanGorevi> _logger;

    public ArkaPlanGorevi(ILogger<ArkaPlanGorevi> logger)
    {
        _logger = logger;
    }

    // Bu metot, uygulama çalıştığı sürece sürekli çağrılır
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Kuyruk İşleyicisi (Arka Plan Görevi) başlatıldı.");
        
        // Uygulama kapanana kadar çalışacak sonsuz döngü
        while (!stoppingToken.IsCancellationRequested)
        {
            // KUYRUK SİSTEMİ SİMÜLASYONU:
            // Gerçek bir senaryoda bu noktada kuyruktan bir mesaj (örn. 'Toplu Fiyat Güncelleme İsteği') çekilirdi.
            _logger.LogInformation("KUYRUK: Toplu Fiyat Güncelleme İşlemi Çalışıyor... (Simülasyon)");
            
            // İşlemin tamamlanması için bir süre bekliyoruz (Simülasyon süresi)
            await Task.Delay(TimeSpan.FromSeconds(15), stoppingToken); 

            _logger.LogInformation("KUYRUK: Toplu Fiyat Güncelleme İşlemi tamamlandı. Yeni döngü bekleniyor.");
        }

        _logger.LogInformation("Kuyruk İşleyicisi durduruldu.");
    }
}