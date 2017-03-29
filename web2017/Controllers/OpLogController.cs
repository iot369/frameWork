using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using yynet.model;
using PagedList;

namespace yynet.web.Controllers
{
    public class OpLogController : Controller
    {
        PermissionHelper p_helper = new PermissionHelper();

        // GET: OpLog        
        public ActionResult Index(int? page)
        {
            if (Session["last_user_id"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            string user_id = (string)Session["last_user_id"];
            if (!p_helper.hasPermission("0202", user_id))
            {
                return RedirectToAction("Index", "Login");
            }


            int pageIndex = page ?? 1;
            IOPLOG bll = Bll_Utilitity.GetOpLog();
            int total = 0;
            int pageSize = CommonConfig.admin_page_size;
            IEnumerable<TB_OP_LOG> list = bll.GetList(pageIndex, pageSize, out total);
            var pagedList = new StaticPagedList<TB_OP_LOG>(list, pageIndex, pageSize, total);
            Session["pageId"] = pageIndex;
            return View(pagedList);
        }
    }
    
}