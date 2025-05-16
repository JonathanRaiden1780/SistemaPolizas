using Microsoft.AspNetCore;

namespace Policies.Api
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {

            AppContext.SetSwitch("System.Drawing.EnableUnixSupport", true);
            var builder = WebHost.CreateDefaultBuilder(args).UseStartup<Startup>();
            return builder;
        }
    }
}