using Logic = StockMarketWatcher.Business.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockMarketWatcher.Business.Interface;

namespace StockMarketWatcher.Business
{
    public class StockMarket : IStockMarket
    {
        private Dictionary<string, IStock> _stocks = new Dictionary<string, IStock>(); //Update to dependency Injection

        public void AddStock(IStock stock)
        {
            _stocks[stock.Symbol] = stock;
        }

        public void UpdateStockPrice(string symbol, double newPrice)
        {
            if (_stocks.TryGetValue(symbol, out var stock)) //This can be updated to dependency injection of the Db of Stocks.
            {
                stock.UpdatePrice(newPrice);
            }
        }

        public void Subscribe(string symbol, ISubscriber subscriber) //use the model here instead of business class 'subscriber.name'
        {
            if (_stocks.TryGetValue(symbol, out var stock))
            {
                stock.Attach(subscriber);
                Console.WriteLine($"{subscriber} subscribed to {symbol}.");
            }
        }

        public void Unsubscribe(string symbol, ISubscriber subscriber) //use the model here instead of business class 'subscriber.name'
        {
            if (_stocks.TryGetValue(symbol, out var stock))
            {
                stock.Detach(subscriber);
                Console.WriteLine($"{subscriber} unsubscribed from {symbol}.");
            }
        }
    }
}
