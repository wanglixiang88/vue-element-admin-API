using SqlSugar;
using System;

namespace VueElementAdminModel.MySqlModel
{
    public class sys_user
    {
        /// <summary>
        /// 用户ID 唯一
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public long userId { get; set; }

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
        /// 用户所使用的的角色ID
        /// </summary>
        public int roleId { get; set; }

        /// <summary>
        /// 头像路径
        /// </summary>
        public string avatar { get; set; }

        /// <summary>
        /// 自我介绍
        /// </summary>
        public string introduction { get; set; }

        /// <summary>
        /// 是否删除 0.未删除 1.已删除
        /// </summary>
        public int isDelete { get; set; }

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
