using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using yynet.model;
using PagedList;

namespace yynet.web.Controllers
{
    public class LoginLogController : Controller
    {
        PermissionHelper p_helper = new PermissionHelper();

        // GET: LoginLog
        public ActionResult Index(int? page)
        {
            if (Session["last_user_id"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            string user_id = (string)Session["last_user_id"];
            if (!p_helper.hasPermission("0201", user_id))
            {
                return RedirectToAction("Index", "Login");
            }

            int pageIndex = page ?? 1;
            ILOGINLOG bll = Bll_Utilitity.GetLoginLog();
            int total = 0;
            int pageSize = CommonConfig.admin_page_size;
            IEnumerable<TB_LOGIN_LOG> list = bll.GetList(pageIndex, pageSize, out total);
            var pagedList = new StaticPagedList<TB_LOGIN_LOG>(list, pageIndex, pageSize, total);
            Session["pageId"] = pageIndex;
            return View(pagedList);
        }
    }
}