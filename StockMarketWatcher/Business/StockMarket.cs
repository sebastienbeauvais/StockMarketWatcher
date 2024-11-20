using Logic = StockMarketWatcher.Business.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockMarketWatcher.Business.Interface;
using System.Linq.Expressions;

namespace StockMarketWatcher.Business
{
    public class StockMarket : IStockMarket
    {
        private readonly List<IStock> _stocksDb;
        public StockMarket(List<IStock> stocksDb)
        {
            _stocksDb = stocksDb;
        }

        public void AddStock(IStock stock)
        {
            if (!_stocksDb.Any(s => s.Symbol == stock.Symbol))
            {
                _stocksDb.Add(stock);
            }
        }

        public void UpdateStockPrice(string symbol, double newPrice)
        {
            var stock = _stocksDb.FirstOrDefault(s => s.Symbol == symbol);
            if (stock != null)
            {
                stock.UpdatePrice(newPrice);
            }
        }

        public void Subscribe(string symbol, ISubscriber subscriber)
        {
            var stock = _stocksDb.FirstOrDefault(s => s.Symbol == symbol);
            if (stock != null)
            {
                stock.Attach(subscriber);
                Console.WriteLine($"{subscriber.Name} subscribed to {symbol}.");
            }
        }

        public void Unsubscribe(string symbol, ISubscriber subscriber)
        {
            var stock = _stocksDb.FirstOrDefault(s => s.Symbol == symbol);
            if (stock != null)
            {
                stock.Detach(subscriber);
                Console.WriteLine($"{subscriber.Name} unsubscribed from {symbol}.");
            }
        }
    }
}
