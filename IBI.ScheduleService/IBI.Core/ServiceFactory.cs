using System;
using System.Collections.Generic;
using System.Text;

namespace IBI.Core
{
    public static class  ServiceFactory
    {
        private static IAzureStorageBlobService _azureStorageBlobService;
        public static IAzureStorageBlobService CreateAzureStorageBlobService()
        {
            if (_azureStorageBlobService  == null) _azureStorageBlobService = new AzureStorageBlobService();
            return _azureStorageBlobService;
        }
    }
}
