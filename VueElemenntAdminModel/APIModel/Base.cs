using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VueElemenntAdminModel.APIModel
{
    /// <summary>
    /// 用户信息的基类
    /// </summary>
    public class BaseInfo
    {
        /// <summary>
        /// 用户token
        /// </summary>
        public string userToken { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string userName { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public long userId { get; set; }
    }
}
