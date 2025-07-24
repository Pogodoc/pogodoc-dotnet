using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Pogodoc.Utils
{
    /// <summary>
    /// Utility class for S3 operations.
    /// </summary>
    public static class S3Utils
    {
        /// <summary>
        /// Uploads a payload to an S3 bucket using a presigned URL.
        /// </summary>
        /// <param name="presignedUrl">The presigned URL of the S3 bucket.</param>
        /// <param name="payload">The payload to upload.</param>
        /// <param name="payloadLength">The length of the payload.</param>
        /// <param name="contentType">The content type of the payload.</param>
        public static async Task UploadToS3WithUrlAsync(
            string presignedUrl,
            Stream payload,
            long payloadLength,
            string contentType
        )
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
