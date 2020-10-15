using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIExample.APIBusiness
{
    public class UsersBusiness
    {

        #region Instance
        private UsersBusiness()
        {
        }

        private static UsersBusiness _instance;
        private static object _syncObject = new object();

        /// <summary>
        /// 单例
        /// </summary>
        public static UsersBusiness Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_syncObject)
                    {
                        if (_instance == null)
                        {
                            _instance = new UsersBusiness();
                        }
                    }
                }
                return _instance;
            }
        }
        #endregion

        public string GetUserInfo(string UserId)
        {
            return "";
        }
    }
}