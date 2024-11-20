using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StockMarketWatcher.Business;
using StockMarketWatcher.Business.Interface;
using System;

namespace StockMarketWatcherTests
{
    [TestClass]
    public class SubscriberTests
    {
        [TestMethod]
        public void Constructor_ShouldSetName()
        {
            // Arrange
            var name = "John Doe";

            // Act
            var subscriber = new Subscriber(name);

            // Assert
            Assert.AreEqual(name, subscriber.Name);
        }

        [TestMethod]
        public void Update_ShouldWriteExpectedMessage()
        {
            // Arrange
            var name = "John Doe";
            var subscriber = new Subscriber(name);

            var stockMock = new Mock<IStock>();
            stockMock.Setup(s => s.Symbol).Returns("AAPL");
            stockMock.Setup(s => s.Price).Returns(150.50);

            using (var consoleOutput = new ConsoleOutput())
            {
                // Act
                subscriber.Update(stockMock.Object);

                // Assert
                var expectedMessage = "John Doe received update: AAPL is now $150.50";
                Assert.IsTrue(consoleOutput.GetOutput().Trim().Contains(expectedMessage));
            }
        }
    }

    // Helper class to capture console output
    public class ConsoleOutput : IDisposable
    {
        private readonly System.IO.StringWriter _stringWriter;
        private readonly System.IO.TextWriter _originalOutput;

        public ConsoleOutput()
        {
            _stringWriter = new System.IO.StringWriter();
            _originalOutput = Console.Out;
            Console.SetOut(_stringWriter);
        }

        public string GetOutput()
        {
            return _stringWriter.ToString();
        }

        public void Dispose()
        {
            Console.SetOut(_originalOutput);
            _stringWriter.Dispose();
        }
    }
}