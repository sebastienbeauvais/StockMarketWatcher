using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMarketWatcher.Business.Interface
{
    public interface IStock
    {
        string Symbol { get; }
        double Price { get; }
        void Attach(ISubscriber observer);
        void Detach(ISubscriber observer);
        void Notify();
        void UpdatePrice(double price);
    }
}
