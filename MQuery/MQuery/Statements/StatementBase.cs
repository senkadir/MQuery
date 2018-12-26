using System.Collections.Generic;

namespace MQuery
{
    public abstract class StatementBase
    {
        public List<WhereClauseParameter> Wheres { get; set; } = new List<WhereClauseParameter>();
    }
}
