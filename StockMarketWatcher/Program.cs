using Microsoft.Extensions.DependencyInjection;
using StockMarketWatcher.Business;
using StockMarketWatcher.Business.Interface;
using StockMarketWatcher.Data;
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
            var stocks = StockDb.GetStocks();

            //Register             
            services.AddScoped<StockMarketWatcher.StockMarketWatcher>();
            services.AddSingleton<List<IStock>>(stocks);
            services.AddScoped<IStockMarket, StockMarket>();

            return services.BuildServiceProvider();
        }

    }
}
