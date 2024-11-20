using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMarketWatcher.Business.Interface
{
    public interface ISubscriber
    {
        string Name { get; }
        void Update(IStock stock);
    }
}
