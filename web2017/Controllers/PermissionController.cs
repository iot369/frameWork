using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using yynet.model;
using yynet.sql;

namespace yynet.web.Controllers
{
    public class PermissionController : Controller
    {
        // GET: Permission
        public ActionResult Index(int? page)
        {
            if (Session["last_user_id"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            string user_id = (string)Session["last_user_id"];
            if (!p_helper.hasPermission("0101", user_id))
            {
                return RedirectToAction("Index", "Login");
            }

            int pageIndex = page ?? 1;
            IPERMISSION bll = Bll_Utilitity.GetPermission();
            int total = 0;
            int pageSize = CommonConfig.admin_page_size;
            IEnumerable<TB_PERMISSION> list = bll.GetList(pageIndex, pageSize, out total);
            var pagedList = new StaticPagedList<TB_PERMISSION>(list, pageIndex, pageSize, total);
            Session["pageId"] = pageIndex;
            return View(pagedList);
        }

        private IEnumerable<TB_PERMISSION> addEmptyItemAndRemoveOne(IEnumerable<TB_PERMISSION> ienum,TB_PERMISSION one)
        {
            IList<TB_PERMISSION> list = ienum as IList<TB_PERMISSION>;
            if (one != null)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].PERMISSION_ID == one.PERMISSION_ID)
                    {
                        list.RemoveAt(i);
                        i--;
                    }
                }
            }
            list.Add(new TB_PERMISSION() {  PERMISSION_ID="",PERMISSION_NAME="",PARENT_PERMISSION_ID=""});
            return list as IEnumerable<TB_PERMISSION>;
        }

        PermissionHelper p_helper = new PermissionHelper();

        public ActionResult Create()
        {
            if (Session["last_user_id"] == null)
            {
                return RedirectToAction("Index", "Login");
            }                
            string user_id = (string)Session["last_user_id"];
            if(!p_helper.hasPermission("0102", user_id))
            {
                return RedirectToAction("Index", "Login");
            }

            model.TB_PERMISSION m = new model.TB_PERMISSION();
            IPERMISSION bll = Bll_Utilitity.GetPermission();
            IEnumerable<TB_PERMISSION> list = bll.ListAll();
            list = addEmptyItemAndRemoveOne(list,null);
            IEnumerable<SelectListItem> items =
            from value in list
            select new SelectListItem
            {
                Text = value.PERMISSION_NAME,
                Value = value.PERMISSION_ID
            };

            ViewBag.PARENT_PERMISSION_ID = items;

            return View(m);
        }

        public ActionResult Details(TB_PERMISSION permission)
        {
            IPERMISSION bll = Bll_Utilitity.GetPermission();
            permission = bll.Get(permission.PERMISSION_ID);           
            return View(permission);
        }

        public ActionResult Edit(TB_PERMISSION permission)
        {
            if (Session["last_user_id"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            string user_id = (string)Session["last_user_id"];
            if (!p_helper.hasPermission("0103", user_id))
            {
                return RedirectToAction("Index", "Login");
            }


            IPERMISSION bll = Bll_Utilitity.GetPermission();
            permission = bll.Get(permission.PERMISSION_ID);
            if (permission == null)
            {
                return RedirectToAction("Index", "Permission");
            }
            IEnumerable<TB_PERMISSION> list = bll.ListAll();
            list = addEmptyItemAndRemoveOne(list,permission);
            IEnumerable<SelectListItem> items =
            from value in list
            select new SelectListItem
            {
                Text = value.PERMISSION_NAME,
                Value = value.PERMISSION_ID,
                Selected = (value.PERMISSION_ID == permission.PARENT_PERMISSION_ID)
            };
            

            ViewBag.PARENT_PERMISSION_ID = items;
            return View(permission);
        }

        public ActionResult EditSave(TB_PERMISSION permission)
        {
            if (Session["last_user_id"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            string user_id = (string)Session["last_user_id"];
            if (!p_helper.hasPermission("0103", user_id))
            {
                return RedirectToAction("Index", "Login");
            }

            IPERMISSION bll = Bll_Utilitity.GetPermission();
            TB_PERMISSION old_permission = bll.Get(permission.PERMISSION_ID);
            string change_content = "";
            if (old_permission.PERMISSION_NAME != permission.PERMISSION_NAME)
            {
                change_content += "权限名称，"+old_permission.PERMISSION_NAME
                    +"改为"+permission.PERMISSION_NAME+"";
            }
            if (old_permission.PARENT_PERMISSION_ID != permission.PARENT_PERMISSION_ID)
            {
                if (change_content != "")
                {
                    change_content += ",";
                }
                change_content += "父权限ID，"+old_permission.PARENT_PERMISSION_ID
                    + "改为"+permission.PARENT_PERMISSION_ID+"";
            }

            if (change_content != "")
            {
                bll.Update(permission);

                IOPLOG op_bll = Bll_Utilitity.GetOpLog();
                TB_OP_LOG log = new TB_OP_LOG();
                log.OP_USER_ID = (string)Session["last_user_id"];
                log.OPER_NAME = "权限编辑";
                log.OPER_IP = IpHelper.GetClientIP();
                log.OPER_TIME = DateTime.Now;
                log.OPER_DESC = "权限编辑（" + change_content+ "）";
                op_bll.AddLog(log);                
            }                        
            return RedirectToAction("Index", "Permission");
        }

        public ActionResult CreateSave(TB_PERMISSION permission)
        {
            if (Session["last_user_id"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            string user_id = (string)Session["last_user_id"];
            if (!p_helper.hasPermission("0102", user_id))
            {
                return RedirectToAction("Index", "Login");
            }

            IPERMISSION bll = Bll_Utilitity.GetPermission();
            bll.Insert(permission);

            IOPLOG op_bll = Bll_Utilitity.GetOpLog();
            TB_OP_LOG log = new TB_OP_LOG();
            log.OP_USER_ID = (string)Session["last_user_id"];
            log.OPER_NAME = "权限添加";
            log.OPER_IP = IpHelper.GetClientIP();
            log.OPER_TIME = DateTime.Now;
            log.OPER_DESC = string.Format("权限添加（权限编号：{0},权限名称：{1}）",
                permission.PERMISSION_ID,permission.PERMISSION_NAME);
            op_bll.AddLog(log);

            return RedirectToAction("Index", "Permission");
        }

        public ActionResult Delete(TB_PERMISSION permission)
        {
            if (Session["last_user_id"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            string user_id = (string)Session["last_user_id"];
            if (!p_helper.hasPermission("0104", user_id))
            {
                return RedirectToAction("Index", "Login");
            }

            if (permission==null)
                return RedirectToAction("Index", "Permission");
            IPERMISSION bll = Bll_Utilitity.GetPermission();
            permission = bll.Get(permission.PERMISSION_ID);
            if (permission == null)
            {
                ViewBag.ErrMsg = "未找到指定的权限";
                int pageIndex = 1;
                if (Session["pageId"] != null)
                {
                    pageIndex = (int)Session["pageId"];
                }
                int total = 0;
                int pageSize = CommonConfig.admin_page_size;
                IEnumerable<TB_PERMISSION> list = bll.GetList(pageIndex, pageSize, out total);
                var pagedList = new StaticPagedList<TB_PERMISSION>(list, pageIndex, pageSize, total);
                Session["pageId"] = pageIndex;
                return View("Index", pagedList);
            }
            bool child_exist = bll.existsChild(permission.PERMISSION_ID);
            if(child_exist)
            {
                ViewBag.ErrMsg = "该项存在子权限，请先删除子权限再删除该项";
                int pageIndex = 1;
                if (Session["pageId"] != null)
                {
                    pageIndex = (int)Session["pageId"];
                }
                int total = 0;
                int pageSize = CommonConfig.admin_page_size;
                IEnumerable<TB_PERMISSION> list = bll.GetList(pageIndex, pageSize, out total);
                var pagedList = new StaticPagedList<TB_PERMISSION>(list, pageIndex, pageSize, total);
                Session["pageId"] = pageIndex;
                return View("Index",pagedList);
            }
            bll.Delete(permission.PERMISSION_ID);

            IOPLOG op_bll = Bll_Utilitity.GetOpLog();
            TB_OP_LOG log = new TB_OP_LOG();
            log.OP_USER_ID = (string)Session["last_user_id"];
            log.OPER_NAME = "权限删除";
            log.OPER_IP = IpHelper.GetClientIP();
            log.OPER_TIME = DateTime.Now;
            log.OPER_DESC = string.Format("权限删除（权限编号：{0},权限名称：{1}）",
                permission.PERMISSION_ID, permission.PERMISSION_NAME);
            op_bll.AddLog(log);
                        
            return RedirectToAction("Index", "Permission");
        }
    }
}