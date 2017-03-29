using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace yynet.model
{
	public class TB_USER_ROLE{



        [DisplayName("用户ID")]
        /// <summary>
        /// USER_ID
        /// </summary>
        public string USER_ID{
            get; set;
        }


        [DisplayName("角色ID")]
        /// <summary>
        /// ROLE_ID
        /// </summary>
        public string ROLE_ID{
            get; set;
        }
	
	}
}
