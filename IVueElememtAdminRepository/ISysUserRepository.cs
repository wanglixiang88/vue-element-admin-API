﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vueElementAdminModel.MySqlModel;

namespace IVueElememtAdminRepository
{
    public interface ISysUserRepository
    {

        /// <summary>
        /// 根据Token获取用户信息
        /// </summary>
        /// <param name="token">token</param>
        /// <returns></returns>
        sys_user GetUserInfoByToken(string token);

        /// <summary>
        /// 用户登录所用，根据用户密码查询用户信息
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="passWord">密码</param>
        /// <returns></returns>
        sys_user GetUserForLogin(string userName, string passWord);

        /// <summary>
        /// 更新用户Token
        /// </summary>
        /// <param name="sysUser"></param>
        /// <returns></returns>
        int UpdateToken(sys_user sysUser);
    }
}
