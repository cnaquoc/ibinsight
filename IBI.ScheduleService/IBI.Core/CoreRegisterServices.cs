using IBI.Core.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;


namespace IBI.Core
{
    public static class CoreRegisterServices
    {
        public static void UseAzureStorageBlob(this IServiceCollection services)
        {
            services.AddSingleton<IAzureStorageBlobService>(new AzureStorageBlobService());
        }

        public static void UseFileService(this IServiceCollection services)
        {
            services.UseAzureStorageBlob();
            services.AddSingleton<IFileService>((factory) => new FileService(
                factory.GetRequiredService<IConfiguration>(),
                factory.GetRequiredService<IAzureStorageBlobService>()));
        }

        public static void UseViewRenderService(this IServiceCollection services)
        {
            services.AddScoped<IViewRenderService, ViewRenderService>();
        }
    }
}
