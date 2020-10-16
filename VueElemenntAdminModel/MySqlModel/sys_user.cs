using System;
using System.Collections.Generic;
using System.Text;

namespace vueElementAdminModel.MySqlModel
{
    public class sys_user
    {
        /// <summary>
        /// 用户ID guid 唯一
        /// </summary>
        public string userId { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string userName { get; set; }

        /// <summary>
        /// 密码 MD5加密
        /// </summary>
        public string passWord { get; set; }

        /// <summary>
        /// 用户token
        /// </summary>
        public string userToken { get; set; }

        /// <summary>
        /// token失效日期
        /// </summary>
        public DateTime? tokenExpirationDate { get; set; }

        /// <summary>
        /// 是否无效 0.有效 1.无效
        /// </summary>
        public int isValid { get; set; }

        /// <summary>
        /// 创建人ID
        /// </summary>
        public string createUserId { get; set; }

        /// <summary>
        /// 创建人姓名
        /// </summary>
        public string createUserName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime createTime { get; set; }

        /// <summary>
        /// 更新人ID
        /// </summary>
        public string updateUserId { get; set; }

        /// <summary>
        /// 更新人名称
        /// </summary>
        public string updateUserName { get; set; }

        /// <summary>
        ///更新时间
        /// </summary>
        public DateTime? updateTime { get; set; }
    }
}
