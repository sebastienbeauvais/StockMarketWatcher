using StockMarketWatcher.Business;
using StockMarketWatcher.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMarketWatcher
{
    public class StockMarketWatcher
    { 
        public StockMarketWatcher() { }
        public void StartWatching()
        {
            var stocks = StockDb.GetStocks();
            var stockMarket = new StockMarket(stocks);

            // Initialize stocks
            var amdComputers = new Stock("AMDC", 150.00);
            var intel = new Stock("INTC", 700.00);
            stockMarket.AddStock(amdComputers);
            stockMarket.AddStock(intel);

            // Create subscribers
            var subscriber1 = new Subscriber("Alice");
            var subscriber2 = new Subscriber("Bob");

            // Subscribe to stocks
            stockMarket.Subscribe("AMDC", subscriber1);
            stockMarket.Subscribe("INTC", subscriber2);

            // Update stock prices
            stockMarket.UpdateStockPrice("AMDC", 155.00);
            stockMarket.UpdateStockPrice("INTC", 710.00);

            // Unsubscribe and update again
            stockMarket.Unsubscribe("AMDC", subscriber1);
            stockMarket.UpdateStockPrice("INTC", 160.00);
        }
    }
}
