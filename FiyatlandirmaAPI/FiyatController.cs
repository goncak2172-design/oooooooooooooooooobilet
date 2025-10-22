using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic; // List için gerekli

namespace FiyatlandirmaAPI 
{
    [ApiController]
    [Route("api/fiyat")] 
    public class FiyatController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<FiyatController> _logger; 

        public FiyatController(IConfiguration configuration, ILogger<FiyatController> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        [HttpGet("hesapla")] 
        public IActionResult Hesapla(decimal temelFiyat, bool erkenRezervasyon = false) 
        {
            _logger.LogInformation("Fiyat hesaplama başladı. Temel Fiyat: {TemelFiyat}", temelFiyat);
            
            decimal nihaiFiyat = temelFiyat;
            decimal toplamIndirim = 0;
            
            // Kullanıcıya gösterilecek etiketler (Özgünlük kriteri)
            var uygulananEtiketler = new List<string>();

            // KURAL 1: Sezon İndirimi (appsettings.json'dan okunur)
            var sezonIndirimOrani = _configuration.GetValue<int>("FiyatlandirmaKurallari:SezonIndirimiYuzde");
            var maksimumIndirim = _configuration.GetValue<decimal>("FiyatlandirmaKurallari:MaksimumIndirimTutari");
            
            decimal indirim1 = nihaiFiyat * (sezonIndirimOrani / 100m);
            
            if (indirim1 > maksimumIndirim)
            {
                indirim1 = maksimumIndirim;
                uygulananEtiketler.Add("Doluluk İndirimi");
                _logger.LogInformation("Kural 1: Maksimum Tutar ({MaksTutar} TL) ile sınırlandı. İndirim: {Indirim} TL.", maksimumIndirim, indirim1); 
            }
            else if (indirim1 > 0)
            {
                uygulananEtiketler.Add("Sezon İndirimi");
                _logger.LogInformation("Kural 1: Sezon İndirimi (%{Oran}) uygulandı. İndirim: {Indirim} TL.", sezonIndirimOrani, indirim1); 
            }
            
            nihaiFiyat -= indirim1;
            toplamIndirim += indirim1;
            _logger.LogInformation("Kural 1 Sonrası Güncel Fiyat: {NihaiFiyat} TL", nihaiFiyat);


            // KURAL 2: Erken Rezervasyon İndirimi (Şartlı Kural)
            if (erkenRezervasyon)
            {
                var ekstraIndirimOrani = _configuration.GetValue<int>("FiyatlandirmaKurallari:ErkenRezervasyonEkstraYuzde");
                decimal indirim2 = nihaiFiyat * (ekstraIndirimOrani / 100m);
                
                nihaiFiyat -= indirim2;
                toplamIndirim += indirim2;

                uygulananEtiketler.Add("Erken Alım Fırsatı");
                _logger.LogInformation("Kural 2: Erken Rezervasyon İndirimi (%{Oran}) uygulandı. İndirim: {Indirim} TL.", ekstraIndirimOrani, indirim2); 
            }

            _logger.LogInformation("Fiyat hesaplama tamamlandı. Nihai Fiyat: {NihaiFiyat} TL", nihaiFiyat); 


            return Ok(new 
            {
                IstekTemelFiyat = temelFiyat,
                UygulananToplamIndirim = toplamIndirim,
                NihaiFiyat = nihaiFiyat, 
                Etiketler = uygulananEtiketler, // Yeni etiketler listesi
                Durum = "Başarılı"
            });
        }
    }
}
