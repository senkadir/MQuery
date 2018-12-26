using System.Linq;
using System.Text;

namespace MQuery
{
    public class Compiler
    {
        public bool Plurize { get; set; }

        public Compiler(bool pluarize = false)
        {
            Plurize = pluarize;
        }

        public string Table { get; private set; }

        public string Compile<T>(Query<T> query) where T : class
        {
            Table = Plurize ? $"{query.Table}s" : query.Table;

            if (query.CurrentMethod is InsertStatement insertStatement)
            {
                query.RawSql += CompileInsertStatement(insertStatement);
            }
            else if (query.CurrentMethod is SelectStatement selectStatement)
            {
                query.RawSql += CompileSelectStatement(selectStatement);
            }

            return string.Empty;
        }

        private string CompileInsertStatement(InsertStatement insert)
        {
            StringBuilder raw = new StringBuilder();
            raw.Append($"INSERT INTO [{Table}]");

            var cols = $" ({string.Join(", ", insert.Values.Keys.Select(x => $"[{x}]"))}) ";
            var vals = $" VALUES ({string.Join(", ", insert.Values.Values.Select(x => $"'{x}'"))})";

            foreach (var item in cols)
            {
                raw.Append($"{item}");
            }

            foreach (var item in vals)
            {
                raw.Append($"{item}");
            }

            return raw.ToString();
        }

        private string CompileSelectStatement(SelectStatement statement)
        {
            StringBuilder raw = new StringBuilder();

            if (statement.Columns == null || statement.Columns.Count() == 0)
            {
                raw.Append($"SELECT * ");
            }
            else
            {
                raw.Append($"SELECT ");

                var columns = $"{string.Join(", ", statement.Columns.Select(x => $"[{x}]"))}";

                raw.Append(columns);
            }

            raw.Append($" FROM [{Table}]");

            if (statement.Wheres.Count > 0)
            {
                raw.Append($" WHERE ");

                raw.Append(CompileWheres(statement));
            }

            return raw.ToString();
        }

        private string CompileWheres(StatementBase statement)
        {
            StringBuilder wheres = new StringBuilder();

            wheres.Append(string.Join(" AND ", statement.Wheres.Select(x => $"[{x.ColumnName}] {x.Operand} '{x.Value}'")));

            return wheres.ToString();
        }
    }
}
