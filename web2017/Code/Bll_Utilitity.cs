using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using yynet.model;
using yynet.sql;

namespace yynet.web
{
    public class Bll_Utilitity
    {


        public static UnityContainer container = new UnityContainer();

        public static bool isInited = false;

        public static void init()
        {
            container.RegisterType<IPERMISSION, Bll_TB_PERMISSION>();
            container.RegisterType<IOPLOG, Bll_TB_OP_LOG>();
            container.RegisterType<ILOGINLOG, Bll_TB_LOGIN_LOG>();
            container.RegisterType<IROLE, Bll_TB_ROLE>();
            container.RegisterType<IUSER, Bll_TB_USER>();
            container.RegisterType<IUSERROLE, Bll_TB_USER_ROLE>();
            container.RegisterType<IROLEPERMISSION, Bll_TB_ROLE_PERMISSION>();
        }

        public static IPERMISSION GetPermission()
        {
            if (isInited == false)
            {
                init();
                isInited = true;
            }            
            IPERMISSION bll = container.Resolve<IPERMISSION>();
            return bll;
        }

        public static IOPLOG GetOpLog()
        {
            if (isInited == false)
            {
                init();
                isInited = true;
            }
            IOPLOG bll = container.Resolve<IOPLOG>();
            return bll;
        }

        public static ILOGINLOG GetLoginLog()
        {
            if (isInited == false)
            {
                init();
                isInited = true;
            }
            ILOGINLOG bll = container.Resolve<ILOGINLOG>();
            return bll;
        }

        public static IROLE GetRole()
        {
            if (isInited == false)
            {
                init();
                isInited = true;
            }
            IROLE bll = container.Resolve<IROLE>();
            return bll;
        }

        public static IUSER GetUser()
        {
            if (isInited == false)
            {
                init();
                isInited = true;
            }
            IUSER bll = container.Resolve<IUSER>();
            return bll;
        }

        public static IUSERROLE GetUserRole()
        {
            if (isInited == false)
            {
                init();
                isInited = true;
            }

            IUSERROLE bll = container.Resolve<IUSERROLE>();
            return bll;
        }

        public static IROLEPERMISSION GetRolePermission()
        {
            if (isInited == false)
            {
                init();
                isInited = true;
            }            
            IROLEPERMISSION bll = container.Resolve<IROLEPERMISSION>();
            return bll;
        }
    }
}