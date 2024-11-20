using StockMarketWatcher.Business.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMarketWatcher.Business
{
    public class Stock : IStock
    {
        private List<ISubscriber> _subscribers = new List<ISubscriber>();
        private double _price;

        public string Symbol { get; private set; }
        public double Price
        {
            get => _price;
            private set
            {
                if (_price != value)
                {
                    _price = value;
                    Notify();
                }
            }
        }

        public Stock(string symbol, double initialPrice)
        {
            Symbol = symbol;
            _price = initialPrice;
        }

        public void Attach(ISubscriber observer)
        {
            _subscribers.Add(observer);
        }

        public void Detach(ISubscriber observer)
        {
            _subscribers.Remove(observer);
        }

        public void Notify()
        {
            foreach (var subscriber in _subscribers)
            {
                subscriber.Update(this);
            }
        }

        public void UpdatePrice(double newPrice)
        {
            Price = newPrice;
        }
    }
}
