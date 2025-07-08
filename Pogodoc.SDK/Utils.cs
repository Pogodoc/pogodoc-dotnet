using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Pogodoc.SDK.Utils

{
    public static class S3Utils
    {
        public static async Task UploadToS3WithUrlAsync(string presignedUrl, Stream payload, long payloadLength, string contentType)
        {
            using (var httpClient = new HttpClient())
            using (var streamContent = new StreamContent(payload))
            {
                streamContent.Headers.Add("Content-Type", contentType);
                streamContent.Headers.Add("Content-Length", payloadLength.ToString());

                var response = await httpClient.PutAsync(presignedUrl, streamContent);
                response.EnsureSuccessStatusCode();
            }
        }
    }
} 