using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace yynet.model
{
    public interface IUSER
    {
        TB_USER Get(string User_ID);

        void Insert(TB_USER m_TB_USER);

        void SetPassword(string User_ID, string Password);

        bool isPasswordCorrect(string User_ID, string Password);

        void Update(TB_USER m_TB_USER);

        void Delete(string User_ID);

        IEnumerable<TB_USER> GetList(int pageId, int pageSize, out int total);        

    }
}
