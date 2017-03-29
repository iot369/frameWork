using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace yynet.model
{
    public interface ILOGINLOG
    {
        void AddLog(TB_LOGIN_LOG log);

        IEnumerable<TB_LOGIN_LOG> GetList(int pageId, int pageSize, out int total);
    }
}
