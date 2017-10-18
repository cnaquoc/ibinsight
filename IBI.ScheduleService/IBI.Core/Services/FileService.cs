using IBI.Core;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace IBI.Core
{
    class FileService : IFileService
    {
        const String PERSON_AVATAR_DIR = "avatars";
        const String COMPANY_LOGO_DIR = "logos";

        private readonly IAzureStorageBlobService _azureStorageBlobService;

        private bool IsAppMode { get; set; }
        private String DefaultAzureBlobConnectionString { get; set; }

        public FileService(IConfiguration configuration,
                            IAzureStorageBlobService azureStorageBlobService)
        {
            _azureStorageBlobService = azureStorageBlobService;

            IsAppMode = configuration.IsAppMode();
            DefaultAzureBlobConnectionString = configuration.GetDefaultAzureBlobConnectionString();
            _azureStorageBlobService.SetConnectionString(DefaultAzureBlobConnectionString);
        }

        private String GetDir(String dirPath)
        {
            return  (IsAppMode ? "" : "dev-") + dirPath;
        }

        private String GetFileName(Guid id)
        {
            return GetFileName(id, 0);
        }

        private String GetFileName(Guid id, int version)
        {
            return GetFileName(id.ToString(), version);
        }

        public String GetFileName(String rawName)
        {
            return GetFileName(rawName, 0);
        }

        public String GetFileName(String rawName, int version)
        {
            if (version < 0) return null;
            return rawName + ((version > 0) ? "_" + version : "");
        }

        public String GetFileName(IFileInfo fileInfo)
        {
            return GetFileName(fileInfo.RawName, fileInfo.Version);
        }

        #region Person
        public async Task<string> GetPersonAvatarAsync(Guid personId)
        {
            return await GetPersonAvatarAsync(personId, 0);
        }

        public async Task<string> GetPersonAvatarAsync(Guid personId, int version)
        {
            return await _azureStorageBlobService.GetUriAsync(GetFileName(personId), GetDir(PERSON_AVATAR_DIR));
        }

        public async Task<IEnumerable<string>> GetPersonAvatarAsync(IEnumerable<IFileInfo> files)
        {
            return await _azureStorageBlobService.GetUriAsync(
                files.Select(f => GetFileName(f.RawName, f.Version)), 
                GetDir(PERSON_AVATAR_DIR));
        }

        public async Task<IEnumerable<string>> GetAllPersonAvatarFileNameAsync()
        {
            return await _azureStorageBlobService.GetAllFileNameAsync(GetDir(PERSON_AVATAR_DIR));
        }
        
        public async  Task<string> UploadPersonAvatarAsync(Guid personId, IFormFile file)
        {
            return await UploadPersonAvatarAsync(personId, file, 0);
        }

        public async Task<string> UploadPersonAvatarAsync(Guid personId, IFormFile file, int version)
        {
            var uri = await _azureStorageBlobService.UploadAsync(file, GetFileName(personId, version), GetDir(PERSON_AVATAR_DIR));
            if (version > 0 && uri != null) // delete old file
            {
                await _azureStorageBlobService.DeleteAsync(GetFileName(personId, version - 1), GetDir(PERSON_AVATAR_DIR));
            }

            return uri;
        }
        #endregion

        #region Company
        public async Task<string> GetCompanyLogoAsync(Guid companyId)
        {
            return await GetCompanyLogoAsync(companyId, 0);
        }

        public async Task<string> GetCompanyLogoAsync(Guid companyId, int version)
        {            
            return await _azureStorageBlobService.GetUriAsync(GetFileName(companyId, version), GetDir(COMPANY_LOGO_DIR));
        }

        public async Task<IEnumerable<string>> GetCompanyLogoAsync(IEnumerable<IFileInfo> files)
        {
            return await _azureStorageBlobService.GetUriAsync(
                files.Select(f => GetFileName(f.RawName, f.Version)),
                GetDir(PERSON_AVATAR_DIR));
        }

        public async Task<IEnumerable<string>> GetAllCompanyLogoFileNameAsync()
        {
            return await _azureStorageBlobService.GetAllFileNameAsync(GetDir(COMPANY_LOGO_DIR));
        }

        public async Task<string> UploadCompanyLogoAsync(Guid companyId, IFormFile file)
        {
            return await UploadCompanyLogoAsync(companyId, file, 0);
        }

        public async Task<string> UploadCompanyLogoAsync(Guid companyId, IFormFile file, int version)
        {
            return await _azureStorageBlobService.UploadAsync(file, GetFileName(companyId, 0), GetDir(COMPANY_LOGO_DIR));
        }
        #endregion
    }

    public class FileInfo : IFileInfo
    {
        public string RawName { get; set; }
        public int Version { get; set; }
    }
}
