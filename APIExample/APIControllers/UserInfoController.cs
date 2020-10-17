﻿using APIExample.Filter;
using IVueElememtAdminRepository;
using IVueElementAdminServices;
using System.Web.Http;
using VueElememtAdminRepository;
using VueElemenntAdminModel.APIModel;
using VueElemenntAdminModel.BaseModel;
using VueElementAdminServices;

namespace APIExample.APIControllers
{
    /// <summary>
    /// UserInfo
    /// </summary>
    [AuthFilter]
    [RoutePrefix("API/UserInfo")]
    public class UserInfoController : ApiController
    {
        private readonly static ISysUserRepository _sysUserRepository = new SysUserRepository();
        private readonly IUserInfoServices _userInfoServices = new UserInfoServices(_sysUserRepository);

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <returns></returns>
        [Route("UserLogin")]
        [HttpPost]
        public CommonAPIResult<UserLoginRes> UserLogin([FromBody]UserLoginReq userLoginReq)
        {
            return _userInfoServices.UserLogin(userLoginReq);
        }

    }
}