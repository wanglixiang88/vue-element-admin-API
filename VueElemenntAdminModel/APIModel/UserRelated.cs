using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VueElemenntAdminModel.APIModel
{
    /// <summary>
    /// 用户相关的请求
    /// </summary>
    public class UserRelated
    {
    }

    #region

    /// <summary>
    /// 用户登录请求的参数
    /// </summary>
    public class UserLoginReq
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string userName { get; set; }


        /// <summary>
        /// 密码
        /// </summary>
        public string passWord { get; set; }
    }

    /// <summary>
    /// 用户登录返回的参数
    /// </summary>
    public class UserLoginRes
    {
        public string userToken { get; set; }
    }

    #endregion

}
