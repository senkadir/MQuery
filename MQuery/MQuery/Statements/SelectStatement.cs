﻿using System.Collections.Generic;

namespace MQuery
{
    public class SelectStatement : StatementBase
    {
        public List<string> Columns { get; set; }
    }
}
