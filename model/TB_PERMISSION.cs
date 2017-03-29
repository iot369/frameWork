using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace yynet.model
{
	public class TB_PERMISSION{

	
		private string m_PERMISSION_ID;


        [DisplayName("权限ID")]
        /// <summary>
        /// PERMISSION_ID
        /// </summary>
        public string PERMISSION_ID{
			get {return m_PERMISSION_ID;}
			set {m_PERMISSION_ID = value;}
		}
	
		private string m_PERMISSION_NAME;


        [DisplayName("权限名称")]
        /// <summary>
        /// PERMISSION_NAME
        /// </summary>
        public string PERMISSION_NAME{
			get {return m_PERMISSION_NAME;}
			set {m_PERMISSION_NAME = value;}
		}
	
		private string m_PARENT_PERMISSION_ID;


        [DisplayName("父权限")]
        /// <summary>
        /// PARENT_PERMISSION_ID
        /// </summary>
        public string PARENT_PERMISSION_ID{
			get {return m_PARENT_PERMISSION_ID;}
			set {m_PARENT_PERMISSION_ID = value;}
		}
      
    }

}
