using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

// FiyatlandirmaAPI ad alanını kullanıyoruz.
using FiyatlandirmaAPI;

var builder = WebApplication.CreateBuilder(args);

// Servisleri ekle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Swagger servisi: Controller'ları bulması için Assembly bilgisini kullanıyoruz
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "FiyatlandirmaAPI", Version = "v1" });
});

// Arka plan servisini kaydet (BONUS 4)
builder.Services.AddHostedService<ArkaPlanGorevi>();

var app = builder.Build();

// Swagger'ı etkinleştir
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    // Swagger'a, JSON dosyasını kendi ana portundan (Docker'da 80) çekmesini söylüyoruz.
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "FiyatlandirmaAPI v1");
    
    // Rota ön ekini ayarla
    c.RoutePrefix = "swagger";
});

// Port zorlamasını KALDIRIYORUZ.
// Docker/Kestrel, otomatik olarak çevreyi (80 portu) kullanacaktır.
// app.Urls.Add("http://*:5268"); // ARTIK YOK

app.MapControllers();

// Uygulamayı çalıştır
app.Run();
