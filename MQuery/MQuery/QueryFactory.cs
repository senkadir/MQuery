using System;
using System.Collections.Generic;
using System.Text;

namespace MQuery
{
    internal class QueryFactory
    {
        private StringBuilder query;
        public QueryFactory()
        {
            query = new StringBuilder();
        }

        public void AddParameter(params string[] parameter)
        {
            var joined = string.Join(" , ", parameter);
            query.Append(joined);
        }

        public void AddStatement(string statement)
        {
            query.Append(statement);
        }

        public void AddStatement(string statement, params string[] parameters)
        {
            query.Append(statement);

            var joined = string.Join(" , ", parameters);

            query.Append(joined);
        }

        public string CreateQuery()
        {
            return query.ToString();
        }
    }
}