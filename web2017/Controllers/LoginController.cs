using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using yynet.model;
using yynet.web;

namespace yynet.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            ViewData["last_user_id"] = "";
            if (Request.Cookies["remember_me"]!=null && Request.Cookies["remember_me"].Value == "remember_me")
            {
                if (Request.Cookies["uid"] != null && Request.Cookies["uid"].Value != "")
                {
                    ViewData["last_user_id"] = Request.Cookies["uid"].Value;
                    ViewData["remember_me_checked"] = "checked=\"checked\"";
                }
                else
                {
                    ViewData["remember_me_checked"] = "";
                }
            } else
            {
                ViewData["remember_me_checked"] = "";
            }
            return View();
        }

        public ActionResult Logout()
        {
            string userid = "";
            if (Session["last_user_id"] != null)
            {
                userid = Session["last_user_id"].ToString();
            }
            Session["login_in_success"] = null;
            Session["last_user_id"] = null;
            Session["real_name"] = null;
            Session["title"] = null;
            Session["image_url"] = null;
            if (userid !=""){
                Session[userid + "||permission_ids"] = null;
            }            
            return RedirectToAction("Index", "Login");
        }

        public ActionResult Login()
        {
            ViewData["errMsg_user"] = "";
            Session["login_in_success"]= "";
            string userid = Request["userid"];
            string password = Request["password"];
            string remember_me = Request["hid_remember_me"];
            if ("remember_me" == remember_me)
            {
                ViewData["remember_me_checked"] = "checked=\"checked\"";
                ViewData["remember_me"] = "remember_me";
            } else
            {
                ViewData["remember_me_checked"] = "";
                ViewData["remember_me"] = "";
            }
            if (string.IsNullOrEmpty(userid))
            {
                ViewData["errMsg_user"] = "用户名不能为空";
                return View("Index");
            }
            if (string.IsNullOrEmpty(password))
            {
                ViewData["errMsg_user"] = "密码不能为空";
                ViewData["last_user_id"] = userid;
                return View("Index");
            }
            IUSER bll = Bll_Utilitity.GetUser();
            ILOGINLOG login_bll = Bll_Utilitity.GetLoginLog();
            TB_USER user = bll.Get(userid);
            TB_LOGIN_LOG login_log = new TB_LOGIN_LOG();
            login_log.LOG_USER_ID = userid;
            login_log.LOG_TIME = DateTime.Now;            
            login_log.LOG_IP = IpHelper.GetClientIP();
            if (user == null)
            {
                ViewData["errMsg_user"] = "用户名或密码错误";
                ViewData["last_user_id"] = userid;
                login_log.LOG_RESULT = "N";
                login_bll.AddLog(login_log);
                return View("Index");
            }
            bool is_password_correct = bll.isPasswordCorrect(userid, password);
            if (!is_password_correct)
            {
                ViewData["errMsg_user"] = "用户名或密码错误";
                ViewData["last_user_id"] = userid;
                login_log.LOG_RESULT = "N";
                login_bll.AddLog(login_log);
                return View("Index");
            }

            if ("remember_me" == remember_me)
            {
                HttpCookie mycookie = new HttpCookie("remember_me");
                mycookie.Value = "remember_me";
                mycookie.Path = "/";
                mycookie.Expires = DateTime.Now.AddDays(7);
                Response.Cookies.Add(mycookie);

                HttpCookie mycookie2 = new HttpCookie("uid");
                mycookie2.Value = userid;
                mycookie2.Path = "/";
                mycookie2.Expires = DateTime.Now.AddDays(7);
                Response.Cookies.Add(mycookie2);

            } else
            {
                HttpCookie mycookie = new HttpCookie("remember_me");
                mycookie.Value = "";
                mycookie.Path = "/";
                mycookie.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(mycookie);

                HttpCookie mycookie2 = new HttpCookie("uid");
                mycookie2.Value = userid;
                mycookie2.Path = "/";
                mycookie2.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(mycookie2);
            }
            PermissionHelper p_helper = new PermissionHelper();
            Session["login_in_success"] = "true";
            Session["last_user_id"] = userid;
            Session["real_name"] = user.REAL_NAME;
            Session["title"] = user.TITLE;
            Session["image_url"] = user.USER_IMAGE_PATH;
            Session[userid + "||permission_ids"] = p_helper.getAllPermissionIds(userid);
            login_log.LOG_RESULT = "Y";
            login_bll.AddLog(login_log);
            return RedirectToAction("Index", "Board");

        }
    }
}