using APIExample.Filter;
using IVueElementAdminServices;
using System.Web.Http;
using VueElemenntAdminModel.APIModel;
using VueElemenntAdminModel.BaseModel;

namespace APIExample.APIControllers
{

    /// <summary>
    /// UserInfo
    /// </summary>
    [AuthFilter]
    [RoutePrefix("API/UserInfo")]
    public class UserInfoController : ApiController
    {

        IUserInfoServices _userInfoServices;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userInfoServices"></param>
        public UserInfoController(IUserInfoServices userInfoServices)
        {
            _userInfoServices = userInfoServices;
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <returns></returns>
        [Route("UserLogin")]
        [HttpPost]
        public CommonAPIResult<UserLoginRes> UserLogin([FromBody]UserLoginReq userLoginReq)
        {
            return new CommonAPIResult<UserLoginRes>();
            //return _userInfoServices.UserLogin(userLoginReq);
        }

    }
}
