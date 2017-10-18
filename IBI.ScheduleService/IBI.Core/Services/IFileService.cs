using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IBI.Core
{
    public interface IFileService
    {
        String GetFileName(String rawName);
        String GetFileName(String rawName, int version);
        String GetFileName(IFileInfo fileInfo);

        Task<String> GetPersonAvatarAsync(Guid personId);
        Task<String> GetPersonAvatarAsync(Guid personId, int version);
        Task<String> UploadPersonAvatarAsync(Guid personId, IFormFile file);
        Task<String> UploadPersonAvatarAsync(Guid personId, IFormFile file, int version);
        Task<IEnumerable<string>> GetPersonAvatarAsync(IEnumerable<IFileInfo> files);
        Task<IEnumerable<string>> GetAllPersonAvatarFileNameAsync();

        Task<String> GetCompanyLogoAsync(Guid companyId);
        Task<String> GetCompanyLogoAsync(Guid companyId, int version);
        Task<IEnumerable<string>> GetCompanyLogoAsync(IEnumerable<IFileInfo> files);
        Task<String> UploadCompanyLogoAsync(Guid companyId, IFormFile file);
        Task<String> UploadCompanyLogoAsync(Guid companyId, IFormFile file, int version);
        Task<IEnumerable<string>> GetAllCompanyLogoFileNameAsync();
    }

    public interface IFileInfo
    {
        string RawName { get; set; }
        int Version { get; set; }
    }
}