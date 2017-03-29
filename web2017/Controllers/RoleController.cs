using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using yynet.model;
using yynet.web;

namespace yynet.Controllers
{
    public class RoleController : Controller
    {
        // GET: Role
        public ActionResult Index(int? page)
        {
            if (Session["last_user_id"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            string user_id = (string)Session["last_user_id"];
            if (!p_helper.hasPermission("0105", user_id))
            {
                return RedirectToAction("Index", "Login");
            }
            if (TempData["ErrMsg"] != null)
            {
                ViewBag.ErrMsg = TempData["ErrMsg"];
            }
            int pageIndex = page ?? 1;
            IROLE bll = Bll_Utilitity.GetRole();
            int total = 0;
            int pageSize = CommonConfig.admin_page_size;
            IEnumerable<TB_ROLE> list = bll.GetList(pageIndex, pageSize, out total);
            var pagedList = new StaticPagedList<TB_ROLE>(list, pageIndex, pageSize, total);
            Session["pageId"] = pageIndex;
            return View(pagedList);
        }

        PermissionHelper p_helper = new PermissionHelper();

        public ActionResult Create()
        {
            if (Session["last_user_id"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            string user_id = (string)Session["last_user_id"];
            if (!p_helper.hasPermission("0106", user_id))
            {
                return RedirectToAction("Index", "Login");
            }

            IPERMISSION bll = Bll_Utilitity.GetPermission();
            IEnumerable<TB_PERMISSION> list = bll.ListAll();
            Dictionary<string,bool> checkState = new Dictionary<string, bool>();
            foreach(TB_PERMISSION bean in list)
            {
                checkState.Add(bean.PERMISSION_ID,false);
            }
            TB_ROLE role = new TB_ROLE();
            role.PERMISSION_LIST = list;
            ViewData["role"] = role;
            ViewData["check_state"] = checkState;
            return View();
        }

        public ActionResult Edit(TB_ROLE role)
        {
            if (Session["last_user_id"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            string user_id = (string)Session["last_user_id"];
            if (!p_helper.hasPermission("0107", user_id))
            {
                return RedirectToAction("Index", "Login");
            }

            IPERMISSION bll = Bll_Utilitity.GetPermission();
            IEnumerable<TB_PERMISSION> list = bll.ListAll();
            Dictionary<string, bool> checkState = new Dictionary<string, bool>();
            IROLEPERMISSION rp_bll = Bll_Utilitity.GetRolePermission();
            IEnumerable<string> permission_ids = rp_bll.GetPermissionIds(role.ROLE_ID);
            IROLE r_bll = Bll_Utilitity.GetRole();
            role = r_bll.Get(role.ROLE_ID);
            role.PERMISSION_LIST = list;
            ViewData["role"] = role;
            IList<string> permission_id_list = permission_ids as IList<string>;
            foreach (TB_PERMISSION bean in list)
            {
                checkState.Add(bean.PERMISSION_ID, false);
            }
            foreach(string p_id in permission_id_list)
            {
                if (checkState.ContainsKey(p_id))
                {
                    checkState[p_id] = true;
                }
            }
            ViewData["check_state"] = checkState;
            return View();
        }

        public ActionResult Details(TB_ROLE role)
        {
            IPERMISSION bll = Bll_Utilitity.GetPermission();
            IEnumerable<TB_PERMISSION> list = bll.ListAll();
            Dictionary<string, bool> checkState = new Dictionary<string, bool>();
            IROLEPERMISSION rp_bll = Bll_Utilitity.GetRolePermission();
            IEnumerable<string> permission_ids = rp_bll.GetPermissionIds(role.ROLE_ID);
            IROLE r_bll = Bll_Utilitity.GetRole();
            role = r_bll.Get(role.ROLE_ID);
            role.PERMISSION_LIST = list;
            ViewData["role"] = role;
            IList<string> permission_id_list = permission_ids as IList<string>;
            foreach (TB_PERMISSION bean in list)
            {
                checkState.Add(bean.PERMISSION_ID, false);
            }
            foreach (string p_id in permission_id_list)
            {
                if (checkState.ContainsKey(p_id))
                {
                    checkState[p_id] = true;
                }
            }
            ViewData["check_state"] = checkState;
            return View();
        }

        public ActionResult EditSave()
        {
            if (Session["last_user_id"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            string user_id = (string)Session["last_user_id"];
            if (!p_helper.hasPermission("0107", user_id))
            {
                return RedirectToAction("Index", "Login");
            }

         

            IROLEPERMISSION rp_bll = Bll_Utilitity.GetRolePermission();
            IPERMISSION p_bll = Bll_Utilitity.GetPermission();
            IEnumerable<TB_PERMISSION> list_permission = p_bll.ListAll();
            Dictionary<string, bool> checkState = new Dictionary<string, bool>();
            foreach (TB_PERMISSION bean in list_permission)
            {
                checkState.Add(bean.PERMISSION_ID, false);
            }
            IROLE bll = Bll_Utilitity.GetRole();
            string role_id = Request["role.ROLE_ID"];
            string role_name = Request["role.ROLE_NAME"];

            if (CommonConfig.isForDemo && role_id.ToLower() == "admin")
            {
                TempData["ErrMsg"] = "演示版本，admin角色无法编辑";
                return RedirectToAction("Index", "ROLE");
            }

            TB_ROLE role = new TB_ROLE();
            role.ROLE_ID = role_id;
            role.ROLE_NAME = role_name;
            string permissions = Request["permission"];
            string[] ps = null;
            if (!String.IsNullOrEmpty(permissions))
            {
                ps = permissions.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                if (ps != null && ps.Length > 0)
                {
                    for (int ii = 0; ii < ps.Length; ii++)
                    {
                        if (checkState.ContainsKey(ps[ii]))
                        {
                            checkState[ps[ii]] = true;
                        }
                    }

                }
            }
            role.PERMISSION_LIST = list_permission;

            string change_content = "";
            TB_ROLE old_role = bll.Get(role.ROLE_ID);
            if (old_role.ROLE_NAME!=role.ROLE_NAME)
            {
                change_content += "角色名称，"+old_role.ROLE_NAME+"改为"+role.ROLE_NAME;
            }
            IEnumerable<string> permission_ids = rp_bll.GetPermissionIds(role.ROLE_ID);
            IList<string> pa = new List<string>();
            if (ps != null && ps.Length > 0)
            {
                pa = ps.ToList<string>();
            }
            IList<string> pb = permission_ids.ToList<string>();
           
            string result = CollectionUtilitity.compare("原角色权限", "现角色权限", pa, pb);
            if (result != "")
            {
                if (change_content != "")
                {
                    change_content += ","+result;
                }
            }

            bool isError = false;
            if (string.IsNullOrEmpty(role_id))
            {
                ModelState.AddModelError("role.ROLE_ID", "角色ID不能为空");
                isError = true;
            }
            if (string.IsNullOrEmpty(role_name))
            {
                ModelState.AddModelError("role.ROLE_NAME", "角色名称不能为空");
                isError = true;
            }
            if (isError)
            {
                ViewData["role"] = role;
                ViewData["check_state"] = checkState;
                return View("Edit");
            }
            bll.Update(role);

            IList<String> list = new List<String>();
            if (ps != null)
            {
                foreach (string permission_id in ps)
                {
                    list.Add(permission_id);
                }
            }
            rp_bll.Save(role.ROLE_ID, list);

            
            IOPLOG op_bll = Bll_Utilitity.GetOpLog();
            TB_OP_LOG log = new TB_OP_LOG();
            log.OP_USER_ID = (string)Session["last_user_id"];
            log.OPER_NAME = "角色编辑";
            log.OPER_IP = IpHelper.GetClientIP();
            log.OPER_TIME = DateTime.Now;
            log.OPER_DESC = "角色编辑（" + change_content + "）";
            op_bll.AddLog(log);

            return RedirectToAction("Index", "Role");
        }

        public ActionResult CreateSave()
        {
            if (Session["last_user_id"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            string user_id = (string)Session["last_user_id"];
            if (!p_helper.hasPermission("0106", user_id))
            {
                return RedirectToAction("Index", "Login");
            }

            IROLEPERMISSION rp_bll = Bll_Utilitity.GetRolePermission();
            IPERMISSION p_bll = Bll_Utilitity.GetPermission();
            IEnumerable<TB_PERMISSION> list_permission = p_bll.ListAll();
            Dictionary<string, bool> checkState = new Dictionary<string, bool>();
            foreach (TB_PERMISSION bean in list_permission)
            {
                checkState.Add(bean.PERMISSION_ID, false);
            }
            IROLE bll = Bll_Utilitity.GetRole();
            string role_id = Request["role.ROLE_ID"];
            string role_name = Request["role.ROLE_NAME"];
            TB_ROLE role = new TB_ROLE();
            role.ROLE_ID = role_id;
            role.ROLE_NAME = role_name;
            string permissions = Request["permission"];
            string[] ps = null;
            if (!String.IsNullOrEmpty(permissions))
            {
                ps = permissions.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                if (ps != null && ps.Length > 0)
                {
                    for (int ii = 0; ii < ps.Length; ii++)
                    {
                        if (checkState.ContainsKey(ps[ii]))
                        {
                            checkState[ps[ii]] = true;
                        }
                    }
                    
                }
            }            
            role.PERMISSION_LIST = list_permission;
            bool isError = false;
            if (string.IsNullOrEmpty(role_id))
            {
                ModelState.AddModelError("role.ROLE_ID", "角色ID不能为空");                
                isError = true;                
            }
            if (string.IsNullOrEmpty(role_name))
            {
                ModelState.AddModelError("role.ROLE_NAME", "角色名称不能为空");               
                isError = true;
            }
            if (isError)
            {
                ViewData["role"] = role;
                ViewData["check_state"] = checkState;
                return View("Create");
            }                 
            bll.Insert(role);

            string change_content = "角色ID：" + role.ROLE_ID+",角色名称："+role.ROLE_NAME+ ",角色权限："
                + permissions;
            IOPLOG op_bll = Bll_Utilitity.GetOpLog();
            TB_OP_LOG log = new TB_OP_LOG();
            log.OP_USER_ID = (string)Session["last_user_id"];
            log.OPER_NAME = "角色添加";
            log.OPER_IP = IpHelper.GetClientIP();
            log.OPER_TIME = DateTime.Now;
            log.OPER_DESC = "角色添加（" + change_content + "）";
            op_bll.AddLog(log);

            IList<String> list = new List<String>();
            if (ps != null)
            {
                foreach (string permission_id in ps)
                {
                    list.Add(permission_id);
                }
            }            
            rp_bll.Save(role.ROLE_ID, list);
            return RedirectToAction("Index", "Role");
        }


        public ActionResult Delete(TB_ROLE role)
        {
            if (Session["last_user_id"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            string user_id = (string)Session["last_user_id"];
            if (!p_helper.hasPermission("0108", user_id))
            {
                return RedirectToAction("Index", "Login");
            }

            if (role == null)
                return RedirectToAction("Index", "ROLE");
            if (role.ROLE_ID == null)
                return RedirectToAction("Index", "ROLE");

            IROLE bll = Bll_Utilitity.GetRole();
            IROLEPERMISSION rp_bll = Bll_Utilitity.GetRolePermission();

            role = bll.Get(role.ROLE_ID);
            if(role==null)
                return RedirectToAction("Index", "ROLE");
            if (role.ROLE_ID == null)
                return RedirectToAction("Index", "ROLE");

            if (CommonConfig.isForDemo && role.ROLE_ID.ToLower() == "admin")
            {
                TempData["ErrMsg"] = "演示版本，admin角色无法删除";
                return RedirectToAction("Index", "ROLE");
            }

            IEnumerable<string> permissions_list =  rp_bll.GetPermissionIds(role.ROLE_ID);
            string permissions = "";
            foreach(string t in permissions_list)
            {
                permissions = t + ",";
            }
            if (permissions.EndsWith(","))
            {
                permissions = permissions.Substring(0, permissions.Length - 1);
            }

            string change_content = "角色ID：" + role.ROLE_ID + ",角色名称：" + role.ROLE_NAME + ",角色权限："
         + permissions;

            IList<String> list = new List<String>();
            IEnumerable<String> p_list = list.AsEnumerable<String>();            
            rp_bll.Save(role.ROLE_ID, p_list);                 
            bll.Delete(role.ROLE_ID);
          
            IOPLOG op_bll = Bll_Utilitity.GetOpLog();
            TB_OP_LOG log = new TB_OP_LOG();
            log.OP_USER_ID = (string)Session["last_user_id"];
            log.OPER_NAME = "角色删除";
            log.OPER_IP = IpHelper.GetClientIP();
            log.OPER_TIME = DateTime.Now;
            log.OPER_DESC = "角色删除（" + change_content + "）";
            op_bll.AddLog(log);

            return RedirectToAction("Index", "ROLE");
        }

    }
}