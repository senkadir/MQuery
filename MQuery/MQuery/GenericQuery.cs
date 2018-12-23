using MQuery.Tools;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Linq;
using MQuery.Tools.Extensions;

namespace MQuery
{
    public class Query<T> where T : class
    {
        StringBuilder query;

        Dictionary<string, ClauseBase> Clauses;

        public Query()
        {
            query = new StringBuilder();
            Clauses = new Dictionary<string, ClauseBase>();
        }

        public Query<T> Select()
        {
            query.Append("SELECT * ");

            return this;
        }

        public Query<T> Select(params Expression<Func<T, object>>[] columns)
        {
            query.Append("SELECT ");

            var cols = columns.GetMemberName();

            query.Append(string.Join(", ", cols.Select(x => $"[{x}]")));

            return this;
        }

        public Query<T> From()
        {
            query.Append($"FROM {typeof(T).Name}s ");

            return this;
        }

        public Query<T> Where()
        {
            Clauses.Add("WHERE", new WhereClause());

            return this;
        }

        public Query<T> Where(string column, string operand, object value)
        {
            var where = (WhereClause)Clauses["WHERE"];
            where.Parameters.Add(new WhereClauseParameter() { ColumnName = column, Operand = operand, Value = value });

            return this;
        }

        public override string ToString()
        {
            foreach (var clause in Clauses.Values)
            {
                if (clause is WhereClause whereClause)
                {
                    query.Append(" WHERE ");

                    if (whereClause.Parameters.Any())
                    {
                        query.Append(string.Join("AND", whereClause.Parameters));
                    }
                }

            }

            return query.ToString();
        }
    }
}
