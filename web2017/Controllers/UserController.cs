using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using yynet.model;

namespace yynet.web.Controllers
{
    public class UserController : Controller
    {

        PermissionHelper p_helper = new PermissionHelper();


        // GET: User
        public ActionResult Index(int? page)
        {
            if (Session["last_user_id"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            string user_id = (string)Session["last_user_id"];
            if (!p_helper.hasPermission("0109", user_id))
            {
                return RedirectToAction("Index", "Login");
            }
            if (TempData["ErrMsg"] != null)
            {
                ViewBag.ErrMsg = TempData["ErrMsg"];
            }
            int pageIndex = page ?? 1;
            IUSER bll = Bll_Utilitity.GetUser();
            int total = 0;
            int pageSize = CommonConfig.admin_page_size;
            IEnumerable<TB_USER> list = bll.GetList(pageIndex, pageSize, out total);
            var pagedList = new StaticPagedList<TB_USER>(list, pageIndex, pageSize, total);
            Session["pageId"] = pageIndex;
            return View(pagedList);
        }

        public ActionResult Create()
        {
            if (Session["last_user_id"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            string user_id = (string)Session["last_user_id"];
            if (!p_helper.hasPermission("0110", user_id))
            {
                return RedirectToAction("Index", "Login");
            }

            List<SelectListItem> sex_list = new List<SelectListItem>();
            sex_list.Add(new SelectListItem() { Text = "男", Value = "男" });
            sex_list.Add(new SelectListItem() { Text = "女", Value = "女" });

            List<SelectListItem> account_status_list = new List<SelectListItem>();
            account_status_list.Add(new SelectListItem() { Text = "启用", Value = "Y" });
            account_status_list.Add(new SelectListItem() { Text = "禁用", Value = "N" });

            IROLE bll = Bll_Utilitity.GetRole();
            IEnumerable<TB_ROLE> list = bll.ListAll();
            Dictionary<string, bool> checkState = new Dictionary<string, bool>();
            foreach (TB_ROLE bean in list)
            {
                checkState.Add(bean.ROLE_ID, false);
            }
            TB_USER user = new TB_USER();
            user.ROLE_LIST = list;
            ViewData["user"] = user;
            ViewData["check_state"] = checkState;
            ViewData["sex"] = sex_list.AsEnumerable();
            ViewData["account_status"] = account_status_list.AsEnumerable();
            return View();
        }

        public ActionResult Edit(TB_USER user)
        {
            if (Session["last_user_id"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            string user_id = (string)Session["last_user_id"];
            if (!p_helper.hasPermission("0111", user_id))
            {
                return RedirectToAction("Index", "Login");
            }

            if (user == null)
                return RedirectToAction("Index", "USER");
            if (string.IsNullOrEmpty(user.USER_ID))
                return RedirectToAction("Index", "USER");
            IUSER r_bll = Bll_Utilitity.GetUser();
            user = r_bll.Get(user.USER_ID);

            List<SelectListItem> sex_list = new List<SelectListItem>();
            bool selected_man = false;
            bool selected_woman = false;
            if(user.SEX== "男")
            {
                selected_man = true;
            }
            else if (user.SEX == "女")
            {
                selected_woman = true;
            }
            sex_list.Add(new SelectListItem() { Text = "男", Value = "男",Selected = selected_man });
            sex_list.Add(new SelectListItem() { Text = "女", Value = "女",Selected = selected_woman });

            List<SelectListItem> account_status_list = new List<SelectListItem>();
            bool selected_account_status_y = false;
            bool selected_account_status_n = false;
            if (user.ACCOUNT_STATUS == "Y")
            {
                selected_account_status_y = true;
            }
            else if (user.ACCOUNT_STATUS == "N")
            {
                selected_account_status_n = true;
            }
            account_status_list.Add(new SelectListItem() { Text = "启用", Value = "Y",Selected = selected_account_status_y });
            account_status_list.Add(new SelectListItem() { Text = "禁用", Value = "N" ,Selected = selected_account_status_n });

            IROLE bll = Bll_Utilitity.GetRole();
            IEnumerable<TB_ROLE> list = bll.ListAll();
            Dictionary<string, bool> checkState = new Dictionary<string, bool>();
            IUSERROLE rp_bll = Bll_Utilitity.GetUserRole();
            IEnumerable<string> role_ids = rp_bll.GetRoleIds(user.USER_ID);
           
            user.ROLE_LIST = list;
            ViewData["user"] = user;
            IList<string> role_id_list = role_ids as IList<string>;
            foreach (TB_ROLE bean in list)
            {
                checkState.Add(bean.ROLE_ID, false);
            }
            foreach (string p_id in role_id_list)
            {
                if (checkState.ContainsKey(p_id))
                {
                    checkState[p_id] = true;
                }
            }
            ViewData["check_state"] = checkState;
            ViewData["sex"] = sex_list.AsEnumerable();
            ViewData["account_status"] = account_status_list.AsEnumerable();
            if (user.USER_IMAGE != null && !string.IsNullOrEmpty(user.USER_IMAGE_PATH))
            {
                string file_path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, user.USER_IMAGE_PATH);
                if (!System.IO.File.Exists(file_path))
                {
                    System.IO.File.WriteAllBytes(file_path, user.USER_IMAGE);
                }
            }
            return View();
        }

        public ActionResult Details(TB_USER user)
        {
            if (user == null)
                return RedirectToAction("Index", "USER");
            if(string.IsNullOrEmpty(user.USER_ID))
                return RedirectToAction("Index", "USER");
            IUSER r_bll = Bll_Utilitity.GetUser();
            user = r_bll.Get(user.USER_ID);

            List<SelectListItem> sex_list = new List<SelectListItem>();
            bool selected_man = false;
            bool selected_woman = false;
            if (user.SEX == "男")
            {
                selected_man = true;
            }
            else if (user.SEX == "女")
            {
                selected_woman = true;
            }
            sex_list.Add(new SelectListItem() { Text = "男", Value = "男", Selected = selected_man });
            sex_list.Add(new SelectListItem() { Text = "女", Value = "女", Selected = selected_woman });

            List<SelectListItem> account_status_list = new List<SelectListItem>();
            bool selected_account_status_y = false;
            bool selected_account_status_n = false;
            if (user.ACCOUNT_STATUS == "Y")
            {
                selected_account_status_y = true;
            }
            else if (user.ACCOUNT_STATUS == "N")
            {
                selected_account_status_n = true;
            }
            account_status_list.Add(new SelectListItem() { Text = "启用", Value = "Y", Selected = selected_account_status_y });
            account_status_list.Add(new SelectListItem() { Text = "禁用", Value = "N", Selected = selected_account_status_n });

            IROLE bll = Bll_Utilitity.GetRole();
            IEnumerable<TB_ROLE> list = bll.ListAll();
            Dictionary<string, bool> checkState = new Dictionary<string, bool>();
            IUSERROLE rp_bll = Bll_Utilitity.GetUserRole();
            IEnumerable<string> role_ids = rp_bll.GetRoleIds(user.USER_ID);

            user.ROLE_LIST = list;
            ViewData["user"] = user;
            IList<string> role_id_list = role_ids as IList<string>;
            foreach (TB_ROLE bean in list)
            {
                checkState.Add(bean.ROLE_ID, false);
            }
            foreach (string p_id in role_id_list)
            {
                if (checkState.ContainsKey(p_id))
                {
                    checkState[p_id] = true;
                }
            }
            ViewData["check_state"] = checkState;
            ViewData["sex"] = sex_list.AsEnumerable();
            ViewData["account_status"] = account_status_list.AsEnumerable();
            return View();
        }

        public ActionResult EditSave()
        {
            if (Session["last_user_id"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            string user_id0 = (string)Session["last_user_id"];
            if (!p_helper.hasPermission("0111", user_id0))
            {
                return RedirectToAction("Index", "Login");
            }
            if (CommonConfig.isForDemo && user_id0.ToLower() == "admin")
            {
                TempData["ErrMsg"] = "演示版本，admin用户无法编辑";
                return RedirectToAction("Index", "USER");
            }
            IUSERROLE rp_bll = Bll_Utilitity.GetUserRole();
            IROLE p_bll = Bll_Utilitity.GetRole();
            IEnumerable<TB_ROLE> list_role = p_bll.ListAll();
            Dictionary<string, bool> checkState = new Dictionary<string, bool>();
            foreach (TB_ROLE bean in list_role)
            {
                checkState.Add(bean.ROLE_ID, false);
            }
            IUSER bll = Bll_Utilitity.GetUser();
            string user_id = Request["user.USER_ID"];
            string real_name = Request["user.REAL_NAME"];
            string sex = Request["sex"];
            string old_password = Request["user.OLD_PASSWORD"];
            string new_password = Request["user.NEW_PASSWORD"];
            string re_password = Request["user.RE_PASSWORD"];
            string email = Request["user.EMAIL"];
            string account_status = Request["account_status"];
            string image_file = Request["image_file"];
            string title = Request["user.TITLE"];
            TB_USER user = new TB_USER();
            user.USER_ID = user_id;
            user.REAL_NAME = real_name;
            user.SEX = sex;
            user.EMAIL = email;
            user.ACCOUNT_STATUS = account_status;
            user.TITLE = title;
            string file_path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                "Uploads/" + image_file);
            if (System.IO.File.Exists(file_path))
            {
                byte[] b = System.IO.File.ReadAllBytes(file_path);
                user.USER_IMAGE = b;
                user.USER_IMAGE_PATH = image_file;
            }
            string roles = Request["role"];
            string[] ps = null;
            if (!String.IsNullOrEmpty(roles))
            {
                ps = roles.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
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
            user.ROLE_LIST = list_role;
            bool isError = false;
            bool change_password = false;
            if (string.IsNullOrEmpty(user_id))
            {
                ModelState.AddModelError("user.USER_ID", "用户ID不能为空");
                isError = true;
            }
            if (string.IsNullOrEmpty(real_name))
            {
                ModelState.AddModelError("user.REAL_NAME", "姓名不能为空");
                isError = true;
            }
            if(new_password=="" && re_password == "")
            {

            } else if (new_password != re_password)
            {
                ModelState.AddModelError("user.RE_PASSWORD", "确认密码与输入密码不一致");
                isError = true;
            } else
            {
                bool passowrd_collect = bll.isPasswordCorrect(user_id, old_password);
                if (!passowrd_collect)
                {
                    ModelState.AddModelError("user.OLD_PASSWORD", "原密码错误");
                    isError = true;

                } else
                {
                    user.PASSWORD = new_password;
                    change_password = true;
                    
                }                
            }            
            if (string.IsNullOrEmpty(email))
            {
                ModelState.AddModelError("user.EMAIL", "电子邮箱不能为空");
                isError = true;
            }
            if (!Validator.IsEmail(email))
            {
                ModelState.AddModelError("user.EMAIL", "电子邮箱格式错误");
                isError = true;
            }
            if (isError)
            {
                ViewData["user"] = user;
                ViewData["check_state"] = checkState;

                List<SelectListItem> sex_list = new List<SelectListItem>();
                bool selected_man = false;
                bool selected_woman = false;
                if (user.SEX == "男")
                {
                    selected_man = true;
                }
                else if (user.SEX == "女")
                {
                    selected_woman = true;
                }
                sex_list.Add(new SelectListItem() { Text = "男", Value = "男", Selected = selected_man });
                sex_list.Add(new SelectListItem() { Text = "女", Value = "女", Selected = selected_woman });

                List<SelectListItem> account_status_list = new List<SelectListItem>();
                bool selected_account_status_y = false;
                bool selected_account_status_n = false;
                if (user.ACCOUNT_STATUS == "Y")
                {
                    selected_account_status_y = true;
                }
                else if (user.ACCOUNT_STATUS == "N")
                {
                    selected_account_status_n = true;
                }
                account_status_list.Add(new SelectListItem() { Text = "启用", Value = "Y", Selected = selected_account_status_y });
                account_status_list.Add(new SelectListItem() { Text = "禁用", Value = "N", Selected = selected_account_status_n });

                ViewData["sex"] = sex_list.AsEnumerable();
                ViewData["account_status"] = account_status_list.AsEnumerable();

                return View("Edit");
            }
            TB_USER old_user = bll.Get(user.USER_ID);
            bll.Update(user);
            if(change_password)
                bll.SetPassword(user_id, new_password);

            string change_content = "";
            if (old_user.REAL_NAME != user.REAL_NAME)
            {
                change_content += string.Format( "姓名（{0}->{1}）",old_user.REAL_NAME,user.REAL_NAME);
            }
            if (old_user.SEX != user.SEX)
            {
                if (change_content != "")
                {
                    change_content += ",";
                }
                change_content += string.Format("性别（{0}->{1}）", old_user.SEX, user.SEX);
            }
            if (old_user.TITLE != user.TITLE)
            {
                if (change_content != "")
                {
                    change_content += ",";
                }
                change_content += string.Format("称呼（{0}->{1}）", old_user.TITLE, user.TITLE);
            }
            if (old_user.USER_IMAGE_PATH != user.USER_IMAGE_PATH)
            {
                if (change_content != "")
                {
                    change_content += ",";
                }
                change_content += string.Format("图片（{0}->{1}）", old_user.USER_IMAGE_PATH, user.USER_IMAGE_PATH);
            }

            IEnumerable<string> old_role_ids = rp_bll.GetRoleIds(user.USER_ID);

            IList<String> list = new List<String>();
            if (ps != null)
            {
                foreach (string role_id in ps)
                {
                    list.Add(role_id);
                }
            }
            rp_bll.Save(user.USER_ID, list);


            IList<string> old_role_id_list = old_role_ids.ToList<string>();
            string result = CollectionUtilitity.compare("原包含角色", "现包含角色", old_role_id_list, list);
            if (result != "")
            {
                if (change_content != "")
                {
                    change_content += ",";
                }
                change_content += result;
            }

            IOPLOG op_bll = Bll_Utilitity.GetOpLog();
            TB_OP_LOG log = new TB_OP_LOG();
            log.OP_USER_ID = (string)Session["last_user_id"];
            log.OPER_NAME = "用户编辑";
            log.OPER_IP = IpHelper.GetClientIP();
            log.OPER_TIME = DateTime.Now;
            log.OPER_DESC = "用户编辑（" + change_content + "）";
            op_bll.AddLog(log);            
            
            return RedirectToAction("Index", "User");
        }

        public ActionResult UserProfile()
        {            
            if(Session["last_user_id"]==null)
                return RedirectToAction("Index", "Login");
            if (string.IsNullOrEmpty(Session["last_user_id"].ToString())){
                return RedirectToAction("Index", "Login");
            }
            String userid = (String)Session["last_user_id"];
            IUSER bll = Bll_Utilitity.GetUser();
            
            TB_USER user = bll.Get(userid);
            ViewData["user"] = user;
            List<SelectListItem> sex_list = new List<SelectListItem>();
            bool selected_man = false;
            bool selected_woman = false;
            if (user.SEX == "男")
            {
                selected_man = true;
            }
            else if (user.SEX == "女")
            {
                selected_woman = true;
            }
            sex_list.Add(new SelectListItem() { Text = "男", Value = "男", Selected = selected_man });
            sex_list.Add(new SelectListItem() { Text = "女", Value = "女", Selected = selected_woman });
         
            ViewData["sex"] = sex_list.AsEnumerable();           
            return View();
        }

        public ActionResult ProfileSave()
        {                                
            IUSER bll = Bll_Utilitity.GetUser();
            string user_id = (string)Session["last_user_id"];

            TB_USER user = bll.Get(user_id);            
            string real_name = Request["user.REAL_NAME"];
            string sex = Request["sex"];
            string old_password = Request["user.OLD_PASSWORD"];
            string new_password = Request["user.NEW_PASSWORD"];
            string re_password = Request["user.RE_PASSWORD"];
            string email = Request["user.EMAIL"];
            string image_file = Request["image_file"];
            string title = Request["user.TITLE"];            
            user.USER_ID = user_id;
            user.REAL_NAME = real_name;
            user.SEX = sex;
            user.EMAIL = email;
            user.TITLE = title;
            string file_path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                "Uploads/" + image_file);
            bool change_password = false;
            if (System.IO.File.Exists(file_path))
            {
                byte[] b = System.IO.File.ReadAllBytes(file_path);
                user.USER_IMAGE = b;
                user.USER_IMAGE_PATH = image_file;
            }     
            bool isError = false;
            if (string.IsNullOrEmpty(user_id))
            {
                ModelState.AddModelError("user.USER_ID", "用户ID不能为空");
                isError = true;
            }
            if (string.IsNullOrEmpty(real_name))
            {
                ModelState.AddModelError("user.REAL_NAME", "姓名不能为空");
                isError = true;
            }
            if (new_password == "" && re_password == "")
            {

            }
            else if (new_password != re_password)
            {
                ModelState.AddModelError("user.RE_PASSWORD", "确认密码与输入密码不一致");
                isError = true;
            }
            else
            {
                bool passowrd_collect = bll.isPasswordCorrect(user_id, old_password);
                if (!passowrd_collect)
                {
                    ModelState.AddModelError("user.OLD_PASSWORD", "原密码错误");
                    isError = true;

                }
                else
                {
                    user.PASSWORD = new_password;
                    change_password = true;
                }
            }
            if (string.IsNullOrEmpty(email))
            {
                ModelState.AddModelError("user.EMAIL", "电子邮箱不能为空");
                isError = true;
            }
            if (!Validator.IsEmail(email))
            {
                ModelState.AddModelError("user.EMAIL", "电子邮箱格式错误");
                isError = true;
            }
            if (isError)
            {
                List<SelectListItem> sex_list = new List<SelectListItem>();
                bool selected_man = false;
                bool selected_woman = false;
                if (user.SEX == "男")
                {
                    selected_man = true;
                }
                else if (user.SEX == "女")
                {
                    selected_woman = true;
                }
                sex_list.Add(new SelectListItem() { Text = "男", Value = "男", Selected = selected_man });
                sex_list.Add(new SelectListItem() { Text = "女", Value = "女", Selected = selected_woman });
           
                ViewData["sex"] = sex_list.AsEnumerable();

                return View("UserProfile");
            }
            bll.Update(user);
            if (change_password)
                bll.SetPassword(user_id, new_password);


            return RedirectToAction("Index", "Board");
        }

        public ActionResult CreateSave()
        {
            if (Session["last_user_id"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            string user_id0 = (string)Session["last_user_id"];
            if (!p_helper.hasPermission("0110", user_id0))
            {
                return RedirectToAction("Index", "Login");
            }

            IUSERROLE rp_bll = Bll_Utilitity.GetUserRole();
            IROLE p_bll = Bll_Utilitity.GetRole();
            IEnumerable<TB_ROLE> list_role = p_bll.ListAll();
            Dictionary<string, bool> checkState = new Dictionary<string, bool>();
            foreach (TB_ROLE bean in list_role)
            {
                checkState.Add(bean.ROLE_ID, false);
            }
            IUSER bll = Bll_Utilitity.GetUser();
            string user_id = Request["user.USER_ID"];
            string real_name = Request["user.REAL_NAME"];
            string sex = Request["sex"];
            string password = Request["user.PASSWORD"];
            string re_password = Request["user.RE_PASSWORD"];
            string email = Request["user.EMAIL"];
            string account_status = Request["account_status"];
            string image_file = Request["image_file"];
            string title = Request["user.TITLE"];
            TB_USER user = new TB_USER();
            user.USER_ID = user_id;
            user.REAL_NAME = real_name;
            user.SEX = sex;
            user.PASSWORD = password;
            user.EMAIL = email;
            user.ACCOUNT_STATUS = account_status;
            user.TITLE = title;     
            string roles = Request["role"];
            string[] ps = null;
            if (!String.IsNullOrEmpty(roles))
            {
                ps = roles.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
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
            user.ROLE_LIST = list_role;
            string file_path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
              "Uploads/" + image_file);
            if (System.IO.File.Exists(file_path))
            {
                byte[] b = System.IO.File.ReadAllBytes(file_path);
                user.USER_IMAGE = b;
                user.USER_IMAGE_PATH = image_file;
            }
            bool isError = false;
            if (string.IsNullOrEmpty(user_id))
            {
                ModelState.AddModelError("user.USER_ID", "用户ID不能为空");
                isError = true;
            }
            if (string.IsNullOrEmpty(real_name))
            {
                ModelState.AddModelError("user.REAL_NAME", "姓名不能为空");
                isError = true;
            }
            if (string.IsNullOrEmpty(password))
            {
                ModelState.AddModelError("user.PASSWORD", "密码不能为空");
                isError = true;
            }
            if (password != re_password)
            {
                ModelState.AddModelError("user.RE_PASSWORD", "确认密码与输入密码不一致");
                isError = true;
            }
            if (string.IsNullOrEmpty(email))
            {
                ModelState.AddModelError("user.EMAIL", "电子邮箱不能为空");
                isError = true;
            }
            if (isError)
            {

                ViewData["user"] = user;
                ViewData["check_state"] = checkState;

                List<SelectListItem> sex_list = new List<SelectListItem>();
                bool selected_man = false;
                bool selected_woman = false;
                if (user.SEX == "男")
                {
                    selected_man = true;
                }
                else if (user.SEX == "女")
                {
                    selected_woman = true;
                }
                sex_list.Add(new SelectListItem() { Text = "男", Value = "男", Selected = selected_man });
                sex_list.Add(new SelectListItem() { Text = "女", Value = "女", Selected = selected_woman });

                List<SelectListItem> account_status_list = new List<SelectListItem>();
                bool selected_account_status_y = false;
                bool selected_account_status_n = false;
                if (user.ACCOUNT_STATUS == "Y")
                {
                    selected_account_status_y = true;
                }
                else if (user.ACCOUNT_STATUS == "N")
                {
                    selected_account_status_n = true;
                }
                account_status_list.Add(new SelectListItem() { Text = "启用", Value = "Y", Selected = selected_account_status_y });
                account_status_list.Add(new SelectListItem() { Text = "禁用", Value = "N", Selected = selected_account_status_n });
                ViewData["sex"] = sex_list.AsEnumerable();
                ViewData["account_status"] = account_status_list.AsEnumerable();

                return View("Create");
            }
            bll.Insert(user);
            bll.SetPassword(user.USER_ID,user.PASSWORD);

            string change_content = "用户ID：" + user.USER_ID + ",用户姓名：" + user.REAL_NAME;
            IOPLOG op_bll = Bll_Utilitity.GetOpLog();
            TB_OP_LOG log = new TB_OP_LOG();
            log.OP_USER_ID = (string)Session["last_user_id"];
            log.OPER_NAME = "用户添加";
            log.OPER_IP = IpHelper.GetClientIP();
            log.OPER_TIME = DateTime.Now;
            log.OPER_DESC = "用户添加（" + change_content + "）";
            op_bll.AddLog(log);

            IList<String> list = new List<String>();
            if (ps != null)
            {
                foreach (string role_id in ps)
                {
                    list.Add(role_id);
                }
            }
            rp_bll.Save(user.USER_ID, list);
            return RedirectToAction("Index", "User");
        }

        public ActionResult Delete(TB_USER user)
        {
            if (Session["last_user_id"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            string user_id = (string)Session["last_user_id"];
            if (!p_helper.hasPermission("0112", user_id))
            {
                return RedirectToAction("Index", "Login");
            }

            if (user == null)
                return RedirectToAction("Index", "USER");
            if (string.IsNullOrEmpty(user.USER_ID))
                return RedirectToAction("Index", "USER");
            IUSER bll = Bll_Utilitity.GetUser();
            user = bll.Get(user.USER_ID);
            if (user == null)
                return RedirectToAction("Index", "USER");

            if(CommonConfig.isForDemo && user.USER_ID.ToLower() == "admin")
            {
                TempData["ErrMsg"] = "演示版本，admin用户无法删除";
                return RedirectToAction("Index", "USER");
            }

            IUSERROLE ur_bll = Bll_Utilitity.GetUserRole();
            IList<string> role_id_list = new List<string>();
            ur_bll.Save(user.USER_ID, role_id_list.AsEnumerable<string>());
            bll.Delete(user.USER_ID);

            string change_content = "用户ID：" + user.USER_ID + ",用户姓名：" + user.REAL_NAME ;
            IOPLOG op_bll = Bll_Utilitity.GetOpLog();
            TB_OP_LOG log = new TB_OP_LOG();
            log.OP_USER_ID = (string)Session["last_user_id"];
            log.OPER_NAME = "用户删除";
            log.OPER_IP = IpHelper.GetClientIP();
            log.OPER_TIME = DateTime.Now;
            log.OPER_DESC = "用户删除（" + change_content + "）";
            op_bll.AddLog(log);

            return RedirectToAction("Index", "USER");
        }
    }
}