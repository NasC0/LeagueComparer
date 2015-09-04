using System;
using System.Collections.Generic;
using ApiProcessing.Contracts;
using ApiProcessing.Enumerations;
using ApiProcessing.Queries;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ApiProcessingTests.Queries
{
    [TestClass]
    public class QueryBuilderTests
    {
        private const string ApiKey = "random_api_key";
        private const string Host = "random_host";
        private readonly IQueryBuilder builder = new QueryBuilder(ApiKey, Host);

        [TestMethod]
        public void BuildItemQuery_WithoutParameters_ExpectSuccess()
        {
            // ARRANGE
            var expectedResult = "random_host/item?api_key=random_api_key";

            // ACT
            IQuery queryResult = builder.BuildQuery(ObjectType.Item);
            string queryString = queryResult.GetQueryString();

            // ASSERT
            Assert.AreEqual(expectedResult, queryString);
        }

        [TestMethod]
        public void BuildChampionQuery_WithoutParameters_ExpectSuccess()
        {
            // ARRANGE
            var expectedResult = "random_host/champion?api_key=random_api_key";

            // ACT
            IQuery queryResult = builder.BuildQuery(ObjectType.Champion);
            string queryString = queryResult.GetQueryString();

            // ASSERT
            Assert.AreEqual(expectedResult, queryString);
        }

        [TestMethod]
        public void BuildRuneQuery_WithoutParameters_ExpectSuccess()
        {
            // ARRANGE
            var expectedResult = "random_host/rune?api_key=random_api_key";

            // ACT
            IQuery queryResult = builder.BuildQuery(ObjectType.Rune);
            string queryString = queryResult.GetQueryString();

            // ASSERT
            Assert.AreEqual(expectedResult, queryString);
        }

        [TestMethod]
        public void BuildMasteryQuery_WithoutParameters_ExpectSuccess()
        {
            // ARRANGE
            var expectedResult = "random_host/mastery?api_key=random_api_key";

            // ACT
            IQuery queryResult = builder.BuildQuery(ObjectType.Mastery);
            string queryString = queryResult.GetQueryString();

            // ASSERT
            Assert.AreEqual(expectedResult, queryString);
        }

        [TestMethod]
        public void BuildItemQuery_WithParameters_ExpectSuccess()
        {
            // ARRANGE
            var parameters = new List<QueryParameter>
            {
                new QueryParameter("foo", "foo"),
                new QueryParameter("bar", "bar")
            };

            var expectedResult = "random_host/item?foo=foo&bar=bar&api_key=random_api_key";

            // ACT
            IQuery queryResult = builder.BuildQuery(ObjectType.Item, parameters);
            string queryString = queryResult.GetQueryString();

            // ASSERT
            Assert.AreEqual(expectedResult, queryString);
        }

        [TestMethod]
        public void BuildChampionQuery_WithParameters_ExpectSuccess()
        {
            // ARRANGE
            var parameters = new List<QueryParameter>
            {
                new QueryParameter("foo", "foo"),
                new QueryParameter("bar", "bar")
            };

            var expectedResult = "random_host/champion?foo=foo&bar=bar&api_key=random_api_key";

            // ACT
            IQuery queryResult = builder.BuildQuery(ObjectType.Champion, parameters);
            string queryString = queryResult.GetQueryString();

            // ASSERT
            Assert.AreEqual(expectedResult, queryString);
        }

        [TestMethod]
        public void BuildRuneQuery_WithParameters_ExpectSuccess()
        {
            // ARRANGE
            var parameters = new List<QueryParameter>
            {
                new QueryParameter("foo", "foo"),
                new QueryParameter("bar", "bar")
            };

            var expectedResult = "random_host/rune?foo=foo&bar=bar&api_key=random_api_key";

            // ACT
            IQuery queryResult = builder.BuildQuery(ObjectType.Rune, parameters);
            string queryString = queryResult.GetQueryString();

            // ASSERT
            Assert.AreEqual(expectedResult, queryString);
        }

        [TestMethod]
        public void BuildMasteryQuery_WithParameters_ExpectSuccess()
        {
            // ARRANGE
            var parameters = new List<QueryParameter>
            {
                new QueryParameter("foo", "foo"),
                new QueryParameter("bar", "bar")
            };

            var expectedResult = "random_host/mastery?foo=foo&bar=bar&api_key=random_api_key";

            // ACT
            IQuery queryResult = builder.BuildQuery(ObjectType.Mastery, parameters);
            string queryString = queryResult.GetQueryString();

            // ASSERT
            Assert.AreEqual(expectedResult, queryString);
        }
    }
}
