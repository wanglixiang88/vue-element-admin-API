using APIExample.Filter;
using IVueElememtAdminRepository;
using IVueElementAdminServices;
using System;
using System.Web.Configuration;
using System.Web.Http;
using System.Web.Http.Controllers;
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

        /// <summary>
        /// 获取用户详细信息
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        [Route("GetUserDetail")]
        [HttpGet]
        public CommonAPIResult<UserDetailRes> GetUserDetail(string token)
        {
            return _userInfoServices.GetUserDetail(token);
        }


        /// <summary>
        /// 获取
        /// </summary>
        /// <returns></returns>
        [Route("GetUserInfoList")]
        [HttpGet]
        public CommonAPIResult<BaseTable<UserDetailRes>> GetUserInfoList()
        {
            return _userInfoServices.GetUserDetail(token);
        }
    }
}
