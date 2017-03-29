using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace yynet.model
{
    public interface IOPLOG
    {
        void AddLog(TB_OP_LOG log);

        IEnumerable<TB_OP_LOG> GetList(int pageId, int pageSize, out int total);
    }
}
