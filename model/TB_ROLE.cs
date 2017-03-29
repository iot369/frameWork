using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace yynet.model
{
	public class TB_ROLE{


        [Required(AllowEmptyStrings = false, ErrorMessage = "角色ID不能为空")]
        [DisplayName("角色ID")]
        /// <summary>
        /// ROLE_ID
        /// </summary>
        public string ROLE_ID{
            get; set;
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "角色名称不能为空")]
        [DisplayName("角色名称")]
        /// <summary>
        /// ROLE_NAME
        /// </summary>
        public string ROLE_NAME{
            get;set;
		}

        public IEnumerable<TB_PERMISSION> PERMISSION_LIST
        {
            get;set;
        }
	
	}
}
