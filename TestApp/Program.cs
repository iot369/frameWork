using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using yynet.model;
using yynet.sql;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //TB_PERMISSION m = new TB_PERMISSION();
            //m.PARENT_PERMISSION_ID = null;
            //m.PERMISSION_ID = "2";
            //m.PERMISSION_NAME = "1";

            // Bll_TB_PERMISSION.Insert(m);
            IPERMISSION bll = GetPermission();
            IEnumerable<TB_PERMISSION>  m2 = bll.ListAll();
            foreach(TB_PERMISSION T in m2)
            {
                Console.WriteLine(T.PERMISSION_ID);
            }
            Console.Read();
        }

        public static IPERMISSION GetPermission()
        {
            UnityContainer container = new UnityContainer();
            container.RegisterType<IPERMISSION, Bll_TB_PERMISSION>();
            IPERMISSION bll = container.Resolve<IPERMISSION>();
            return bll;
        }
    }
}
