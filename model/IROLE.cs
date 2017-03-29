using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace yynet.model
{
    public interface IROLE
    {
        TB_ROLE Get(string Role_ID);

        void Insert(TB_ROLE m_TB_ROLE);

        void Update(TB_ROLE m_TB_ROLE);

        void Delete(string ROLE_ID);

        IEnumerable<TB_ROLE> ListAll();

        IEnumerable<TB_ROLE> GetList(int pageId, int pageSize, out int total);        

    }
}
