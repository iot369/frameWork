using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using yynet.model;

namespace yynet.web
{
    public class PermissionHelper
    {
        public Hashtable getAllPermissionIds(string user_id)
        {
            Hashtable permission_id_hash = new Hashtable();
            IUSERROLE i_user_role = Bll_Utilitity.GetUserRole();
            IROLE i_role = Bll_Utilitity.GetRole();
            IROLEPERMISSION i_role_permission = Bll_Utilitity.GetRolePermission();
            IEnumerable<string> role_ids = i_user_role.GetRoleIds(user_id);
            foreach (string role_id in role_ids)
            {
                IEnumerable<string> permission_ids = i_role_permission.GetPermissionIds(role_id);
                foreach (string s_permission_id in permission_ids)
                {
                    if (!permission_id_hash.ContainsKey(s_permission_id))
                    {
                        permission_id_hash.Add(s_permission_id, s_permission_id);
                    }
                }
            }
            return permission_id_hash;
        }

        public bool hasPermission(string permission_id, string user_id)
        {
            if (HttpContext.Current.Session[user_id + "||permission_ids"] != null)
            {
                Hashtable permission_id_hash = (Hashtable)HttpContext.Current.Session[user_id + "||permission_ids"];
                if (permission_id_hash.ContainsKey(permission_id))
                {
                    return true;
                }
            }
            return false;
        }

        public bool hasPermission_3(string permission_id,string user_id)
        {
            IUSERROLE i_user_role = Bll_Utilitity.GetUserRole();
            IROLE i_role = Bll_Utilitity.GetRole();
            IROLEPERMISSION i_role_permission = Bll_Utilitity.GetRolePermission();
            IEnumerable<string> role_ids =  i_user_role.GetRoleIds(user_id);
            foreach(string role_id in role_ids)
            {
                IEnumerable<string> permission_ids = i_role_permission.GetPermissionIds(role_id);
                foreach(string s_permission_id in permission_ids)
                {
                    if (s_permission_id == permission_id)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}