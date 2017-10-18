using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace IBI.Core
{
    internal class AzureStorageBlobService : IAzureStorageBlobService
    {
        protected String ConnectionString { get; set; }

        public void SetConnectionString(string connectionString)
        {
            ConnectionString = connectionString;
        }

        private async Task<CloudBlobContainer> GetContainerAsync(String containerName)
        {
            // Retrieve storage account from connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConnectionString);

            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();            

            // Retrieve a reference to a container.
            CloudBlobContainer container = blobClient.GetContainerReference(containerName);

            // Create the container if it doesn't already exist.
            if (await container.CreateIfNotExistsAsync())
            {
                // Set access permission to public
                await container.SetPermissionsAsync(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });
            }

            return container;
        }

        private async Task<CloudBlockBlob> GetBlockBlob(string blobName, string containerName)
        {
            var container = await GetContainerAsync(containerName);

            return container.GetBlockBlobReference(blobName);            
        }

        public async Task<String> UploadAsync(Stream uploadFileStream, string fileName, string contentType, string containerName)
        {
            try
            {
                var blockBlob = await GetBlockBlob(fileName, containerName);
                blockBlob.Properties.ContentType = contentType;
                await blockBlob.UploadFromStreamAsync(uploadFileStream);

                return blockBlob.Uri.ToString();
            }
            catch
            {
                return null;
            }
        }

        //public async Task<String> UploadAsync(IFormFile uploadFile, string fileName, string containerName)
        //{
        //    return await UploadAsync(uploadFile.OpenReadStream(), fileName, uploadFile.ContentType, containerName);
        //}

        public async Task<bool> DeleteAsync(string fileName, string containerName)
        {
            var blockBlob = await GetBlockBlob(fileName, containerName);
            return await blockBlob.DeleteIfExistsAsync();
        }

        public async Task<String> GetUriAsync(string fileName, string containerName)
        {
            if (String.IsNullOrWhiteSpace(fileName)) return null;

            try
            {
                var cloudBlockBlob = await GetBlockBlob(fileName, containerName);
                if (await cloudBlockBlob.ExistsAsync()) return cloudBlockBlob.Uri.ToString();

                return null;
            }
            catch
            {
                return null;
            }
        }

        public async Task<IEnumerable<String>> GetUriAsync(IEnumerable<string> fileNames, string containerName)
        {
            try
            {
                var container = await GetContainerAsync(containerName);
                return await Task.WhenAll(fileNames.Select(async name => {
                    if (String.IsNullOrWhiteSpace(name)) return null;

                    CloudBlockBlob blockBlob = container.GetBlockBlobReference(name);
                    if (await blockBlob.ExistsAsync()) return blockBlob.Uri.ToString();

                    return null;
                }));
            }
            catch
            {
                return null;
            }
        }

        public async Task<IEnumerable<String>> GetAllFileNameAsync(string containerName)
        {
            IList<String> names = new List<String>();

            var container = await GetContainerAsync(containerName);
            BlobContinuationToken token = null;
            do
            {
                BlobResultSegment resultSegment = await container.ListBlobsSegmentedAsync(token);
                token = resultSegment.ContinuationToken;

                foreach (IListBlobItem item in resultSegment.Results)
                {
                    if (item.GetType() == typeof(CloudBlockBlob))
                    {
                        CloudBlockBlob blob = (CloudBlockBlob)item;
                        names.Add(blob.Name);
                    }
                }
            } while (token != null);

            return names;
        }
    }
}