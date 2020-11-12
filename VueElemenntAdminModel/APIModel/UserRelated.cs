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

    #region /UserInfo/UserLogin

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
        public string token { get; set; }
    }

    #endregion

    #region /UserInfo/GetUserDetail


    /// <summary>
    /// 获取用户详细信息请求的参数
    /// </summary>
    public class UserDetailReq: BaseInfo
    {

    }

    /// <summary>
    /// 获取用户详细信息返回的参数
    /// </summary>
    public class UserDetailRes
    {
        /// <summary>
        ///用户姓名
        /// </summary>
        public string userName { get; set; }

        /// <summary>
        /// 角色ID
        /// </summary>
        public int roleId { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        public List<string> roles { get; set; }

        /// <summary>
        /// 自我介绍
        /// </summary>
        public string introduction { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public string avatar { get; set; }

    }

    #endregion

    #region /UserInfo/SaveUserInfo

    /// <summary>
    /// 保存用户信息请求的参数
    /// </summary>
    public class SaveUserInfoReq: BaseInfo
    {
        /// <summary>
        /// 用户姓名
        /// </summary>
        public string userName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string passWord { get; set; }

        /// <summary>
        /// 角色ID
        /// </summary>
        public int roleId { get; set; }
    }

    #endregion
}
