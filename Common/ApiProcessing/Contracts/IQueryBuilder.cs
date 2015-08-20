﻿using System.Collections.Generic;
using ApiProcessing.Enumerations;
using ApiProcessing.Queries;

namespace ApiProcessing.Contracts
{
    public interface IQueryBuilder
    {
        IQuery BuildQuery(ObjectType objectType);

        IQuery BuildQuery(ObjectType objectType, IEnumerable<QueryParameter> parameters);
    }
}