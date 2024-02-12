using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Threading.Tasks;

public class DownloadManager
{
    public static void DownloadWithWebClient(string url, string destinationPath)
    {
        try
        {
            using (WebClient client = new WebClient())
            {
                client.DownloadFile(url, destinationPath);
                Console.WriteLine("Dosya başarıyla indirildi (WebClient).");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Hata oluştu (WebClient): {ex.Message}");
        }
    }
    public static async Task DownloadWithHttpClientAsync(string url, string destinationPath)
    {
        try
        {
            using (HttpClient httpClient = new HttpClient())
            {
                byte[] fileData = await httpClient.GetByteArrayAsync(url);
                File.WriteAllBytes(destinationPath, fileData);
                Console.WriteLine("Dosya başarıyla indirildi (HttpClient - GetByteArrayAsync).");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Hata oluştu (HttpClient - GetByteArrayAsync): {ex.Message}");
        }
    }
    public static void DownloadWithWebRequest(string url, string destinationPath)
    {
        try
        {
            WebRequest request = WebRequest.Create(url);

            using (WebResponse response = request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (FileStream fileStream = File.Create(destinationPath))
            {
                byte[] buffer = new byte[4096];
                int bytesRead;

                while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    fileStream.Write(buffer, 0, bytesRead);
                }

                Console.WriteLine("Dosya başarıyla indirildi (WebRequest).");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Hata oluştu (WebRequest): {ex.Message}");
        }
    }
    public static async Task DownloadWithHttpClientFileAsync(string url, string destinationPath)
    {
        try
        {
            using (HttpClient httpClient = new HttpClient())
            {
                HttpResponseMessage response = await httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    using (Stream fileStream = File.Create(destinationPath))
                    using (Stream contentStream = await response.Content.ReadAsStreamAsync())
                    {
                        await contentStream.CopyToAsync(fileStream);
                        Console.WriteLine("Dosya başarıyla indirildi (HttpClient - FileAsync).");
                    }
                }
                else
                {
                    Console.WriteLine($"Dosya indirme sırasında hata oluştu. HTTP durumu: {response.StatusCode}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Hata oluştu (HttpClient - FileAsync): {ex.Message}");
        }
    }
    public static void DownloadWithCMD(string url, string destinationPath)
    {
        try
        {
            using (Process process = new Process())
            {
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    RedirectStandardInput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                process.StartInfo = startInfo;
                process.Start();

                // CMD'ye curl benzeri komutu yaz
                string command = $"curl -o {destinationPath} {url}";
                process.StandardInput.WriteLine(command);
                process.StandardInput.Flush();
                process.StandardInput.Close();

                process.WaitForExit();

                Console.WriteLine("Dosya başarıyla indirildi (CMD).");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Hata oluştu (CMD): {ex.Message}");
        }
    }
    public bool VerifyFileIntegrityMD5(string filePath, string expectedHash)
    {
        using (FileStream fileStream = File.OpenRead(filePath))
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] hashBytes = md5.ComputeHash(fileStream);
                string actualHash = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();

                return string.Equals(expectedHash, actualHash, StringComparison.OrdinalIgnoreCase);
            }
        }
    }
    public bool VerifyFileIntegritySHA256(string filePath, string expectedHash)
    {
        using (FileStream fileStream = File.OpenRead(filePath))
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(fileStream);
                string actualHash = BitConverter.ToString(hashBytes).Replace("-", "");

                return string.Equals(expectedHash, actualHash, StringComparison.OrdinalIgnoreCase);
            }
        }
    }
}


