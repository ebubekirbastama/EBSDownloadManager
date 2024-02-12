# DownloadManager DLL

**DownloadManager DLL**, çeşitli dosya indirme yöntemlerini destekleyen bir C# kütüphanesidir.

## Kurulum

Bu DLL'yi projenize eklemek için şu adımları izleyebilirsiniz:

1. **Proje Dizininde DLL Ekleyin:** `DownloadManager.dll` dosyasını projenizin dizinindeki bir klasöre ekleyin.
2. **Referans Ekleyin:** Visual Studio veya benzeri bir IDE kullanıyorsanız, projenize referans olarak ekleyin.

## Kullanım Örnekleri

### WebClient Kullanarak Dosya İndirme
```csharp
DownloadManager.DownloadWithWebClient("https://example.com/file.zip", "C:\\Downloads\\file.zip");
```
### HttpClient Kullanarak Dosya İndirme (Async)
```csharp
await DownloadManager.DownloadWithHttpClientAsync("https://example.com/file.zip", "C:\\Downloads\\file.zip");
```
### WebRequest Kullanarak Dosya İndirme
```csharp
DownloadManager.DownloadWithWebRequest("https://example.com/file.zip", "C:\\Downloads\\file.zip");
```
### HttpClient Kullanarak Dosya İndirme (Async ve Dosyaya Yazma)
```csharp
await DownloadManager.DownloadWithHttpClientFileAsync("https://example.com/file.zip", "C:\\Downloads\\file.zip");
```
### CMD Kullanarak Dosya İndirme
```csharp
DownloadManager.DownloadWithCMD("https://example.com/file.zip", "C:\\Downloads\\file.zip");
```
### Dosya Bütünlüğü Doğrulama
```csharp
bool isMD5Valid = DownloadManager.VerifyFileIntegrityMD5("C:\\Downloads\\file.zip", "expected_md5_hash");
bool isSHA256Valid = DownloadManager.VerifyFileIntegritySHA256("C:\\Downloads\\file.zip", "expected_sha256_hash");
```
### Hata Kontrolü
```csharp
try
{
    // İndirme işlemi buraya
}
catch (Exception ex)
{
    Console.WriteLine($"Hata oluştu: {ex.Message}");
}
```
Her indirme yönteminin kendi hata kontrolü bulunmaktadır. Hatalar, konsola yazdırılarak kullanıcıya bilgi verilir. 
## Lisans

Bu proje [MIT Lisansı](LICENSE) altında lisanslanmıştır.

---

🚀 İyi kodlamalar!
