using Moq;
using StockMarketWatcher.Business.Interface;
using StockMarketWatcher.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMarketWatcherTests
{
    [TestClass]
    public class StockTests
    {
        [TestMethod]
        public void Constructor_ShouldInitializeWithSymbolAndPrice()
        {
            // Arrange
            var symbol = "AAPL";
            var initialPrice = 150.00;

            // Act
            var stock = new Stock(symbol, initialPrice);

            // Assert
            Assert.AreEqual(symbol, stock.Symbol);
            Assert.AreEqual(initialPrice, stock.Price);
        }

        [TestMethod]
        public void Attach_ShouldAddSubscriber()
        {
            // Arrange
            var stock = new Stock("AAPL", 150.00);
            var subscriberMock = new Mock<ISubscriber>();

            // Act
            stock.Attach(subscriberMock.Object);

            // Assert
            var subscribers = GetSubscribers(stock);
            Assert.AreEqual(1, subscribers.Count);
            Assert.AreEqual(subscriberMock.Object, subscribers[0]);
        }

        [TestMethod]
        public void Detach_ShouldRemoveSubscriber()
        {
            // Arrange
            var stock = new Stock("AAPL", 150.00);
            var subscriberMock = new Mock<ISubscriber>();
            stock.Attach(subscriberMock.Object);

            // Act
            stock.Detach(subscriberMock.Object);

            // Assert
            var subscribers = GetSubscribers(stock);
            Assert.AreEqual(0, subscribers.Count);
        }

        [TestMethod]
        public void Notify_ShouldCallUpdateOnSubscribers()
        {
            // Arrange
            var stock = new Stock("AAPL", 150.00);
            var subscriberMock1 = new Mock<ISubscriber>();
            var subscriberMock2 = new Mock<ISubscriber>();

            stock.Attach(subscriberMock1.Object);
            stock.Attach(subscriberMock2.Object);

            // Act
            stock.Notify();

            // Assert
            subscriberMock1.Verify(s => s.Update(stock), Times.Once);
            subscriberMock2.Verify(s => s.Update(stock), Times.Once);
        }

        [TestMethod]
        public void UpdatePrice_ShouldChangePriceAndNotifySubscribers()
        {
            // Arrange
            var stock = new Stock("AAPL", 150.00);
            var subscriberMock = new Mock<ISubscriber>();
            stock.Attach(subscriberMock.Object);

            // Act
            stock.UpdatePrice(200.00);

            // Assert
            Assert.AreEqual(200.00, stock.Price);
        }

        [TestMethod]
        public void UpdatePrice_ShouldNotNotifySubscribersWhenPriceUnchanged()
        {
            // Arrange
            var stock = new Stock("AAPL", 150.00);
            var subscriberMock = new Mock<ISubscriber>();
            stock.Attach(subscriberMock.Object);

            // Act
            stock.UpdatePrice(150.00);

            // Assert
            subscriberMock.Verify(s => s.Update(It.IsAny<IStock>()), Times.Never);
        }

        private List<ISubscriber> GetSubscribers(Stock stock)
        {
            var fieldInfo = typeof(Stock).GetField("_subscribers", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            return (List<ISubscriber>)fieldInfo.GetValue(stock);
        }
    }
}
