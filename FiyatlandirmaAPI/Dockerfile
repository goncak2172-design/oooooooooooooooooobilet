# ----------------------------------------------------
# 1. BUILD AŞAMASI: Projeyi derleme
# mcr.microsoft.com/dotnet/sdk:8.0 imajını kullan, bu .NET SDK araçlarını içerir.
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Proje dosyasını kopyala ve bağımlılıkları yükle (hızlı derleme için)
COPY ["FiyatlandirmaAPI.csproj", "."]
RUN dotnet restore

# Kalan tüm dosyaları kopyala ve yayımla (publish)
COPY . .
RUN dotnet publish "FiyatlandirmaAPI.csproj" -c Release -o /app/publish

# ----------------------------------------------------
# 2. FINAL AŞAMASI: Uygulamayı çalıştırma
# Sadece .NET çalışma zamanını (runtime) içeren daha küçük imajı kullan.
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Yayımlanmış dosyaları FINAL imajına kopyala
COPY --from=build /app/publish .

# Varsayılan portu 80 olarak ayarla (Docker'da standarttır)
ENV ASPNETCORE_URLS=http://+:80 

# Konteyner başlatıldığında çalışacak komut
ENTRYPOINT ["dotnet", "FiyatlandirmaAPI.dll"]