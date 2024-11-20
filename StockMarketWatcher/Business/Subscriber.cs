using StockMarketWatcher.Business.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMarketWatcher.Business
{
    public class Subscriber : ISubscriber
    {
        private string _name;

        public Subscriber(string name)
        {
            _name = name;
        }

        public void Update(IStock stock)
        {
            Console.WriteLine($"{_name} received update: {stock.Symbol} is now {stock.Price:C}");
        }
    }
}
