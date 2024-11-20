using StockMarketWatcher.Business.Interface;
using StockMarketWatcher.Business;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StockMarketWatcher.Business;
using StockMarketWatcher.Business.Interface;
using System.Collections.Generic;
using System.Linq;

namespace StockMarketWatcherTests
{
    [TestClass]
    public class StockMarketTests
    {
        private StockMarket _stockMarket;
        private List<IStock> _mockStockDb;

        [TestInitialize]
        public void Setup()
        {
            _mockStockDb = new List<IStock>();
            _stockMarket = new StockMarket(_mockStockDb);
        }

        [TestMethod]
        public void AddStock_ShouldAddNewStock()
        {
            // Arrange
            var stockMock = new Mock<IStock>();
            stockMock.Setup(s => s.Symbol).Returns("AAPL");

            // Act
            _stockMarket.AddStock(stockMock.Object);

            // Assert
            Assert.AreEqual(1, _mockStockDb.Count);
            Assert.AreEqual("AAPL", _mockStockDb[0].Symbol);
        }

        [TestMethod]
        public void AddStock_ShouldNotAddDuplicateStock()
        {
            // Arrange
            var stockMock = new Mock<IStock>();
            stockMock.Setup(s => s.Symbol).Returns("AAPL");
            _mockStockDb.Add(stockMock.Object);

            // Act
            _stockMarket.AddStock(stockMock.Object);

            // Assert
            Assert.AreEqual(1, _mockStockDb.Count);
        }

        [TestMethod]
        public void UpdateStockPrice_ShouldUpdateStockPrice()
        {
            // Arrange
            var stockMock = new Mock<IStock>();
            stockMock.Setup(s => s.Symbol).Returns("AAPL");
            stockMock.Setup(s => s.UpdatePrice(It.IsAny<double>()));
            _mockStockDb.Add(stockMock.Object);

            // Act
            _stockMarket.UpdateStockPrice("AAPL", 160.00);

            // Assert
            stockMock.Verify(s => s.UpdatePrice(160.00), Times.Once);
        }

        [TestMethod]
        public void UpdateStockPrice_ShouldNotThrowIfStockNotFound()
        {
            // Act & Assert
            _stockMarket.UpdateStockPrice("INVALID", 160.00);
        }

        [TestMethod]
        public void Subscribe_ShouldAttachSubscriberToStock()
        {
            // Arrange
            var stockMock = new Mock<IStock>();
            var subscriberMock = new Mock<ISubscriber>();
            subscriberMock.Setup(s => s.Name).Returns("John Doe");

            stockMock.Setup(s => s.Symbol).Returns("AAPL");
            stockMock.Setup(s => s.Attach(subscriberMock.Object));
            _mockStockDb.Add(stockMock.Object);

            // Act
            _stockMarket.Subscribe("AAPL", subscriberMock.Object);

            // Assert
            stockMock.Verify(s => s.Attach(subscriberMock.Object), Times.Once);
        }

        /*[TestMethod]
        public void Subscribe_ShouldNotAttachToNonexistentStock()
        {
            // Arrange
            var subscriberMock = new Mock<ISubscriber>();
            subscriberMock.Setup(s => s.Name).Returns("John Doe");

            // Act & Assert
            var exception = Record.Exception(() => _stockMarket.Subscribe("INVALID", subscriberMock.Object));
            Assert.IsNull(exception);
        }*/

        [TestMethod]
        public void Unsubscribe_ShouldDetachSubscriberFromStock()
        {
            // Arrange
            var stockMock = new Mock<IStock>();
            var subscriberMock = new Mock<ISubscriber>();
            subscriberMock.Setup(s => s.Name).Returns("John Doe");

            stockMock.Setup(s => s.Symbol).Returns("AAPL");
            stockMock.Setup(s => s.Detach(subscriberMock.Object));
            _mockStockDb.Add(stockMock.Object);

            // Act
            _stockMarket.Unsubscribe("AAPL", subscriberMock.Object);

            // Assert
            stockMock.Verify(s => s.Detach(subscriberMock.Object), Times.Once);
        }

        /*[TestMethod]
        public void Unsubscribe_ShouldNotThrowIfStockNotFound()
        {
            // Arrange
            var subscriberMock = new Mock<ISubscriber>();
            subscriberMock.Setup(s => s.Name).Returns("John Doe");

            // Act & Assert
            var exception = Record.Exception(() => _stockMarket.Unsubscribe("INVALID", subscriberMock.Object));
            Assert.IsNull(exception);
        }*/

        /*[TestMethod]
        public void Unsubscribe_ShouldNotThrowIfSubscriberNotAttached()
        {
            // Arrange
            var stockMock = new Mock<IStock>();
            var subscriberMock = new Mock<ISubscriber>();
            subscriberMock.Setup(s => s.Name).Returns("John Doe");

            stockMock.Setup(s => s.Symbol).Returns("AAPL");
            _mockStockDb.Add(stockMock.Object);

            // Act & Assert
            var exception = Record.Exception(() => _stockMarket.Unsubscribe("AAPL", subscriberMock.Object));
            Assert.IsNull(exception);
        }*/
    }
}