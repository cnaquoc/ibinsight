using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace IBI.Core
{
    public interface IAzureStorageBlobService
    {
        void SetConnectionString(string connectionString);

        Task<string> UploadAsync(Stream uploadFileStream, String fileName, String contentType, string containerName);
        //Task<string> UploadAsync(IFormFile uploadFile, string fileName, string containerName);

        Task<bool> DeleteAsync(string fileName, string containerName);

        Task<string> GetUriAsync(string fileName, string containerName);
        Task<IEnumerable<string>> GetUriAsync(IEnumerable<string> fileNames, string containerName);

        Task<IEnumerable<String>> GetAllFileNameAsync(string containerName);
    }
}