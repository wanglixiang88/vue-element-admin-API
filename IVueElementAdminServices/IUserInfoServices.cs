﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VueElementAdminModel.APIModel;
using VueElementAdminModel.BaseModel;
using VueElementAdminModel.MySqlModel;

namespace IVueElementAdminServices
{
    public interface IUserInfoServices
    {
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="userLoginReq"></param>
        /// <returns></returns>
        CommonAPIResult<UserLoginRes> UserLogin(UserLoginReq userLoginReq);

        /// <summary>
        /// 获取用户详细信息
        /// </summary>
        /// <param name="userDetailReq"></param>
        /// <returns></returns>
        CommonAPIResult<UserDetailRes> GetUserDetail(long userId);

        /// <summary>
        /// 获取列表信息
        /// </summary>
        /// <returns></returns>
        List<sys_user> GetUserInfoList(ref TableParame tableParame);

        /// <summary>
        /// 保存用户信息
        /// </summary>
        /// <param name="saveUserInfoReq"></param>
        /// <returns></returns>
        CommonAPIResult<string> SaveUserInfo(SaveUserInfoReq saveUserInfoReq);

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="saveUserInfoReq"></param>
        /// <returns></returns>
        CommonAPIResult<string> DeleteUser(DeleteUserReq saveUserInfoReq);

        /// <summary>
        /// 改变用户状态
        /// </summary>
        /// <param name="changUserVaildReq"></param>
        /// <returns></returns>
        CommonAPIResult<string> ChangUserVaild(ChangUserVaildReq changUserVaildReq);
    }
}
