using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace yynet.model
{
	public class TB_OP_LOG{

	
		private int m_OP_ID;

        [DisplayName("操作ID")]
        /// <summary>
        /// OP_ID
        /// </summary>
        public int OP_ID{
			get {return m_OP_ID;}
			set {m_OP_ID = value;}
		}
	
		private string m_OP_USER_ID;

        [DisplayName("操作用户名")]
        /// <summary>
        /// OP_USER_ID
        /// </summary>
        public string OP_USER_ID{
			get {return m_OP_USER_ID;}
			set {m_OP_USER_ID = value;}
		}
	
		private string m_OPER_NAME;

        [DisplayName("操作")]
        /// <summary>
        /// OPER_NAME
        /// </summary>
        public string OPER_NAME{
			get {return m_OPER_NAME;}
			set {m_OPER_NAME = value;}
		}
	
		private string m_OPER_IP;

        [DisplayName("IP")]
        /// <summary>
        /// OPER_IP
        /// </summary>
        public string OPER_IP{
			get {return m_OPER_IP;}
			set {m_OPER_IP = value;}
		}
	
		private DateTime m_OPER_TIME;

        [DisplayName("时间")]
        /// <summary>
        /// OPER_TIME
        /// </summary>
        public DateTime OPER_TIME{
			get {return m_OPER_TIME;}
			set {m_OPER_TIME = value;}
		}
	
		private string m_OPER_DESC;

        [DisplayName("描述")]
        /// <summary>
        /// OPER_DESC
        /// </summary>
        public string OPER_DESC{
			get {return m_OPER_DESC;}
			set {m_OPER_DESC = value;}
		}
	
	}
}
