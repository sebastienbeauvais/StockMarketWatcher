using StockMarketWatcher.Business.Interface;
using StockMarketWatcher.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMarketWatcher.Data
{
    public static class StockDb
    {
        public static List<IStock> GetStocks()
        {
            return new List<IStock>
            {
                new Stock("AAPL", 150.00),
                new Stock("GOOG", 2800.00),
                new Stock("AMZN", 3300.00),
                new Stock("MSFT", 299.99),
                new Stock("TSLA", 800.00)
            };
        }
    }
}
