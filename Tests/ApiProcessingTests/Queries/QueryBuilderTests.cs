using System;
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
        private IQueryBuilder builder = new QueryBuilder(ApiKey, Host);

        [TestMethod]
        public void BuildItemQuery_WithoutParameters_ExpectSuccess()
        {
            // ARRANGE
            var expectedResult = "random_host/item?itemListData=all&api_key=random_api_key";

            // ACT
            IQuery queryResult = builder.BuildQuery(ObjectType.Item);
            string queryString = queryResult.GetQueryString();

            // ASSERT
            Assert.AreEqual(expectedResult, queryString);
        }
    }
}
