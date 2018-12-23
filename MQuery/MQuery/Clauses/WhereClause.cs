using System.Collections.Generic;

namespace MQuery
{
    public class WhereClause : ClauseBase
    {
        public List<WhereClauseParameter> Parameters { get; set; }

        public WhereClause()
        {
            Parameters = new List<WhereClauseParameter>();
        }
    }
}
