using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace yynet.model
{
    public interface IPERMISSION
    {
        TB_PERMISSION Get(string PERMISSION_ID);

        void Insert(TB_PERMISSION m_TB_PERMISSION);

        void Update(TB_PERMISSION m_TB_PERMISSION);

        void Delete(string PERMISSION_ID);

        IEnumerable<TB_PERMISSION> ListAll();

        IEnumerable<TB_PERMISSION> GetList(int pageId, int pageSize, out int total);

        bool existsChild(String permission_id);

    }
}
