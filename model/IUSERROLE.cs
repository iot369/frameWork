using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace yynet.model
{
    public interface IUSERROLE
    {

        void Save(String User_ID, IEnumerable<String> RoleIDs);

        IEnumerable<String> GetRoleIds(String User_ID);

    }
}
