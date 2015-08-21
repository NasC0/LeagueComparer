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
            Query testQuery = new Query(ObjectType.Champion, "randomHost");
        }

        [TestMethod]
        public void CreateQueryWithParameters_ValidParameters_ExpectSuccess()
        {
            //Arrange, ACT
            QueryParameter parameterOne = new QueryParameter("foo", "foo");
            QueryParameter parameterTwo = new QueryParameter("bar", "bar");
            Query testQuery = new Query(ObjectType.Champion, "randomHost", parameterOne, parameterTwo);
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
            ICollection<QueryParameter> parameters = new QueryParameter[]
            {
                parameterOne,
                parameterTwo
            };

            Query testQuery = new Query(ObjectType.Champion, "randomHost", parameters);

            CollectionAssert.AreEqual((ICollection)parameters, (ICollection)testQuery.Parameters);
        }
    }
}
