using System;
using System.Collections.Generic;
using System.Text;

namespace MQuery
{
    public class WhereClauseParameter : ClauseParameterBase
    {
        public string ColumnName { get; set; }

        public string Operand { get; set; }

        public object Value { get; set; }

        public override string ToString()
        {
            return $" {ColumnName} {Operand} {Value} ";
        }
    }
}
