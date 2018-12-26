using MQuery.Tools;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Linq;
using MQuery.Tools.Extensions;
using System.Reflection;

namespace MQuery
{
    public class Query<T> where T : class
    {
        public string RawSql = "";

        public string Table { get { return typeof(T).Name; } }

        public StatementBase CurrentMethod;

        public Query()
        {
        }

        public Query<T> Select()
        {
            CurrentMethod = new SelectStatement();

            return this;
        }

        public Query<T> Select(params Expression<Func<T, object>>[] columns)
        {
            SelectStatement select = new SelectStatement
            {
                Columns = columns.GetMemberName()
            };

            CurrentMethod = select;

            return this;
        }

        public Query<T> From()
        {
            return this;
        }

        public Query<T> Where()
        {
            return this;
        }

        public Query<T> Where(string column, string operand, object value)
        {
            WhereClauseParameter where = new WhereClauseParameter()
            {
                ColumnName = column,
                Operand = operand,
                Value = value
            };

            CurrentMethod.Wheres.Add(where);

            return this;
        }

        public Query<T> Insert(object data)
        {
            var insert = new InsertStatement();

            var props = data.GetType().GetRuntimeProperties();

            foreach (var prop in props)
            {
                insert.Values.Add(prop.Name, prop.GetValue(data));
            }

            CurrentMethod = insert;

            return this;
        }

        public override string ToString()
        {
            return RawSql;
        }
    }
}
