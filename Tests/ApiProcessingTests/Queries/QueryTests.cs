using System;
using System.Collections;
using System.Collections.Generic;
using ApiProcessing.Enumerations;
using ApiProcessing.Queries;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ApiProcessingTests.Queries
{
    [TestClass]
    public class QueryTests
    {
        [TestMethod]
        public void CreateQueryWithoutParameters_ExpectSuccess()
        {
            // ARRANGE, ACT, ASSERT
            Query testQuery = new Query(ObjectType.Champion, "dwasdawd", "randomHost");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateQueryWithNullApiKey_ExpectArgumentException()
        {
            //ARRANGE, ACT, ASSERT
            Query testQuery = new Query(ObjectType.Champion, null, "randomHost");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateQueryWithEmptyApiKey_ExpectArgumentException()
        {
            //ARRANGE, ACT, ASSERT
            Query testQuery = new Query(ObjectType.Champion, "", "randomHost");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateQueryWithWhitespaceApiKey_ExpectArgumentException()
        {
            //ARRANGE, ACT, ASSERT
            Query testQuery = new Query(ObjectType.Champion, "     ", "randomHost");
        }

        [TestMethod]
        public void CreateQueryWithParameters_ValidParameters_ExpectSuccess()
        {
            //Arrange, ACT
            QueryParameter parameterOne = new QueryParameter("foo", "foo");
            QueryParameter parameterTwo = new QueryParameter("bar", "bar");
            Query testQuery = new Query(ObjectType.Champion, "randomApiKey", "randomHost", parameterOne, parameterTwo);
            IEnumerable<QueryParameter> parameters = new QueryParameter[]
            {
                parameterOne,
                parameterTwo
            };

            // ASSERT
            CollectionAssert.AreEqual((ICollection)parameters, (ICollection)testQuery.Parameters);
        }

        [TestMethod]
        public void CreateQueryWithParametersInCollection_ValidParameters_ExpectSuccess()
        {
            //ARRANGE, ACT

            QueryParameter parameterOne = new QueryParameter("foo", "foo");
            QueryParameter parameterTwo = new QueryParameter("bar", "bar");
            IEnumerable<QueryParameter> parameters = new QueryParameter[]
            {
                parameterOne,
                parameterTwo
            };

            Query testQuery = new Query(ObjectType.Champion, "randomApiKey", "randomHost", parameters);

            CollectionAssert.AreEqual((ICollection)parameters, (ICollection)testQuery.Parameters);
        }

        [TestMethod]
        public void CreateQueryWithoutParameters_ValidConstructor_ExpectValidQueryString()
        {
            // ARRANGE
            string expectedResult = "random_host/item?api_key=random_api_key";
            Query testQuery = new Query(ObjectType.Item, "random_api_key", "random_host");

            // ACT
            var queryString = testQuery.GetQueryString();

            // ASSERT
            Assert.AreEqual(expectedResult, queryString);
        }

        [TestMethod]
        public void CreateQueryWithParameters_ValidConstructor_ExpectValidQueryString()
        {
            // Arrange
            string expectedResult = "random_host/item?foo=foo&bar=bar&api_key=random_api_key";

            QueryParameter parameterOne = new QueryParameter("foo", "foo");
            QueryParameter parameterTwo = new QueryParameter("bar", "bar");
            IEnumerable<QueryParameter> parameters = new QueryParameter[]
            {
                parameterOne,
                parameterTwo
            };

            Query testQuery = new Query(ObjectType.Item, "random_api_key", "random_host", parameters);

            // ACT
            var queryString = testQuery.GetQueryString();

            // ASSERT
            Assert.AreEqual(expectedResult, queryString);
        }
    }
}
