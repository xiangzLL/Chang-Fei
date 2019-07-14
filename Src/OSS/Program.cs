using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace OSS
{
    /// <summary>
    /// 对象存储服务，主要保存文件对象，支持横向扩展
    /// </summary>
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();

            return WebHost.CreateDefaultBuilder(args).UseConfiguration(config)
                .UseUrls($"http://{config["ServiceIP"]}:{config["ServicePort"]}")
                .UseStartup<Startup>();
        }
    }
}
