using Microsoft.Extensions.DependencyInjection;
using StockMarketWatcher.Business;
using StockMarketWatcher.Business.Interface;
using System;

namespace Program
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var serviceProvider = ConfigureServices();

            var stockMarket = serviceProvider.GetRequiredService<StockMarketWatcher.StockMarketWatcher>();

            stockMarket.StartWatching();
        }
        private static ServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            //Register Services
            services.AddScoped<StockMarketWatcher.StockMarketWatcher>();

            return services.BuildServiceProvider();
        }

    }
}
