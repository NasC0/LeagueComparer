using System;
using ApiProcessing.Processing;
using Data;
using Data.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using MongoDB.Bson;
using Moq;

namespace ApiProcessingTests.Processing
{
    [TestClass]
    public class ProcessingStrategyFactoryTests
    {
        [TestMethod]
        public void GetItemProcessingStrategy_WithValidRepository_ExpectSuccess()
        {
            // ARRANGE
            var mockedRepositoryFactory = new Mock<IRepositoryFactory>();
            mockedRepositoryFactory.Setup(r => r.GetRepository<Item>())
                                   .Returns(new MongoPocoRepository<Item>(null));
            var repositoryFactory = mockedRepositoryFactory.Object;
            var strategyFactory = new ProcessingStrategyFactory(repositoryFactory);

            // ACT
            var resultingStrategy = strategyFactory.GetProcessingStrategy(ApiProcessing.Enumerations.ObjectType.Item);

            // ASSERT
            var expectedResultType = typeof(ItemProcessingStrategy);

            Assert.IsInstanceOfType(resultingStrategy, expectedResultType);
        }

        [TestMethod]
        public void GetChampionProcessingStrategy_WithValidRepository_ExpectSuccess()
        {
            // ARRANGE
            var mockedRepositoryFactory = new Mock<IRepositoryFactory>();
            mockedRepositoryFactory.Setup(r => r.GetRepository<Champion>())
                                   .Returns(new MongoPocoRepository<Champion>(null));
            mockedRepositoryFactory.Setup(r => r.GetRepository("champions"))
                                   .Returns(new MongoRepository<BsonDocument>(null));

            var repositoryFactory = mockedRepositoryFactory.Object;
            var strategyFactory = new ProcessingStrategyFactory(repositoryFactory);

            // ACT
            var resultingStrategy = strategyFactory.GetProcessingStrategy(ApiProcessing.Enumerations.ObjectType.Champion);

            // ASSERT
            var expectedResultType = typeof(ChampionProcessingStrategy);

            Assert.IsInstanceOfType(resultingStrategy, expectedResultType);
        }

        [TestMethod]
        public void GetMasteryProcessingStrategy_WithValidRepository_ExpectSuccess()
        {
            // ARRANGE
            var mockedRepositoryFactory = new Mock<IRepositoryFactory>();
            mockedRepositoryFactory.Setup(r => r.GetRepository<Mastery>())
                                   .Returns(new MongoPocoRepository<Mastery>(null));

            var repositoryFactory = mockedRepositoryFactory.Object;
            var strategyFactory = new ProcessingStrategyFactory(repositoryFactory);

            // ACT
            var resultingStrategy = strategyFactory.GetProcessingStrategy(ApiProcessing.Enumerations.ObjectType.Mastery);

            // ASSERT
            var expectedResultType = typeof(MasteryProcessingStrategy);

            Assert.IsInstanceOfType(resultingStrategy, expectedResultType);
        }

        [TestMethod]
        public void GetRuneProcessingStrategy_WithValidRepository_ExpectSuccess()
        {
            // ARRANGE
            var mockedRepositoryFactory = new Mock<IRepositoryFactory>();
            mockedRepositoryFactory.Setup(r => r.GetRepository<Rune>())
                                   .Returns(new MongoPocoRepository<Rune>(null));

            var repositoryFactory = mockedRepositoryFactory.Object;
            var strategyFactory = new ProcessingStrategyFactory(repositoryFactory);

            // ACT
            var resultingStrategy = strategyFactory.GetProcessingStrategy(ApiProcessing.Enumerations.ObjectType.Rune);

            // ASSERT
            var expectedResultType = typeof(RuneProcessingStrategy);

            Assert.IsInstanceOfType(resultingStrategy, expectedResultType);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetItemProcessingStrategy_WithInvalidRepository_ExpectException()
        {
            // ARRANGE
            var mockedRepositoryFactory = new Mock<IRepositoryFactory>();
            var repositoryFactory = mockedRepositoryFactory.Object;
            var strategyFactory = new ProcessingStrategyFactory(repositoryFactory);

            // ACT
            var resultingStrategy = strategyFactory.GetProcessingStrategy(ApiProcessing.Enumerations.ObjectType.Item);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetMasteryProcessingStrategy_WithInvalidRepository_ExpectException()
        {
            // ARRANGE
            var mockedRepositoryFactory = new Mock<IRepositoryFactory>();
            var repositoryFactory = mockedRepositoryFactory.Object;
            var strategyFactory = new ProcessingStrategyFactory(repositoryFactory);

            // ACT
            var resultingStrategy = strategyFactory.GetProcessingStrategy(ApiProcessing.Enumerations.ObjectType.Mastery);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetRuneProcessingStrategy_WithInvalidRepository_ExpectException()
        {
            // ARRANGE
            var mockedRepositoryFactory = new Mock<IRepositoryFactory>();
            var repositoryFactory = mockedRepositoryFactory.Object;
            var strategyFactory = new ProcessingStrategyFactory(repositoryFactory);

            // ACT
            var resultingStrategy = strategyFactory.GetProcessingStrategy(ApiProcessing.Enumerations.ObjectType.Rune);
        }
    }
}
