using StockMarketWatcher.Business;
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
            var stockMarket = new StockMarket();

            // Initialize stocks
            var apple = new Stock("AAPL", 150.00);
            var tesla = new Stock("TSLA", 700.00);
            stockMarket.AddStock(apple);
            stockMarket.AddStock(tesla);

            // Create subscribers
            var subscriber1 = new Subscriber("Alice");
            var subscriber2 = new Subscriber("Bob");

            // Subscribe to stocks
            stockMarket.Subscribe("AAPL", subscriber1);
            stockMarket.Subscribe("TSLA", subscriber2);

            // Update stock prices
            stockMarket.UpdateStockPrice("AAPL", 155.00);
            stockMarket.UpdateStockPrice("TSLA", 710.00);

            // Unsubscribe and update again
            stockMarket.Unsubscribe("AAPL", subscriber1);
            stockMarket.UpdateStockPrice("AAPL", 160.00);
        }
    }
}
