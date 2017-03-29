using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace yynet.model
{
    public interface IROLEPERMISSION
    {       
        void Save(String Role_ID,IEnumerable<String> PermissionIDs);

        IEnumerable<String> GetPermissionIds(String Role_ID);
    }
}
