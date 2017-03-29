using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace yynet.model
{
	public class TB_LOGIN_LOG{

	
		private int m_LOG_ID;

        [DisplayName("ID")]
        /// <summary>
        /// LOG_ID
        /// </summary>
        public int LOG_ID{
			get {return m_LOG_ID;}
			set {m_LOG_ID = value;}
		}
	
		private string m_LOG_USER_ID;

        [DisplayName("用户ID")]
        /// <summary>
        /// LOG_USER_ID
        /// </summary>
        public string LOG_USER_ID{
			get {return m_LOG_USER_ID;}
			set {m_LOG_USER_ID = value;}
		}
	
		private string m_LOG_IP;

        [DisplayName("IP")]
        /// <summary>
        /// LOG_IP
        /// </summary>
        public string LOG_IP{
			get {return m_LOG_IP;}
			set {m_LOG_IP = value;}
		}
	
		private DateTime m_LOG_TIME;

        [DisplayName("时间")]
        /// <summary>
        /// LOG_TIME
        /// </summary>
        public DateTime LOG_TIME{
			get {return m_LOG_TIME;}
			set {m_LOG_TIME = value;}
		}
	
		private string m_LOG_RESULT;

        [DisplayName("结果")]
        /// <summary>
        /// LOG_RESULT
        /// </summary>
        public string LOG_RESULT{
			get {return m_LOG_RESULT;}
			set {m_LOG_RESULT = value;}
		}
	
	}
}
