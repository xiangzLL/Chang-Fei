using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

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

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
