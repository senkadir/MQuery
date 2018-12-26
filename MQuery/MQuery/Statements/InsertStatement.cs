using System.Collections.Generic;

namespace MQuery
{
    public class InsertStatement : StatementBase
    {
        public Dictionary<string, object> Values { get; set; } = new Dictionary<string, object>();
    }
}
