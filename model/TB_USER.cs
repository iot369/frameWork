using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace yynet.model
{
	public class TB_USER
    {        
        [DisplayName("用户ID")]
        /// <summary>
        /// USER_ID
        /// </summary>
        public string USER_ID{
            get;set;
		}


        [DisplayName("密码")]
        /// <summary>
        /// PASSWORD
        /// </summary>
        public string PASSWORD
        {
            get; set;
        }

        [DisplayName("原密码")]
        /// <summary>
        /// OLD_PASSWORD
        /// </summary>
        public string OLD_PASSWORD
        {
            get; set;
        }

        [DisplayName("新密码")]
        /// <summary>
        /// NEW_PASSWORD
        /// </summary>
        public string NEW_PASSWORD
        {
            get; set;
        }

        [DisplayName("确认密码")]
        /// <summary>
        /// RE_PASSWORD
        /// </summary>
        public string RE_PASSWORD
        {
            get; set;
        }

        [DisplayName("姓名")]
        /// <summary>
        /// REAL_NAME
        /// </summary>
        public string REAL_NAME{
            get; set;
        }


        [DisplayName("性别")]
        /// <summary>
        /// SEX
        /// </summary>
        public string SEX{
            get; set;
        }

        [DisplayName("称呼")]
        /// <summary>
        /// TITLE
        /// </summary>
        public string TITLE
        {
            get; set;
        }

        [DisplayName("头像")]
        /// <summary>
        /// USER_IMAGE
        /// </summary>
        public byte[] USER_IMAGE
        {
            get; set;
        }

        [DisplayName("头像路径")]
        /// <summary>
        /// USER_IMAGE_PATH
        /// </summary>
        public string USER_IMAGE_PATH
        {
            get; set;
        }
        

        [DisplayName("电子邮箱")]
        /// <summary>
        /// EMAIL
        /// </summary>
        public string EMAIL
        {
            get;set;
        }


        [DisplayName("账号状态")]
        /// <summary>
        /// ACCOUNT_STATUS
        /// </summary>
        public string ACCOUNT_STATUS{
            get; set;
        }

        public IEnumerable<TB_ROLE> ROLE_LIST
        {
            get; set;
        }

    }
}
