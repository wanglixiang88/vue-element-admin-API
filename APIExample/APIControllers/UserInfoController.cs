using APIExample.Filter;
using IVueElememtAdminRepository;
using IVueElementAdminServices;
using System.Collections.Generic;
using System.Web.Http;
using VueElememtAdminRepository;
using VueElemenntAdminModel.APIModel;
using VueElemenntAdminModel.BaseModel;
using vueElementAdminModel.MySqlModel;
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
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        [Route("GetUserDetail")]
        [HttpGet]
        public CommonAPIResult<UserDetailRes> GetUserDetail(long userId)
        {
            return _userInfoServices.GetUserDetail(userId);
        }

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <returns></returns>
        [Route("GetUserInfoList")]
        [HttpPost]
        public CommonAPIResult<BaseTable<List<sys_user>>> GetUserInfoList([FromBody]TableParame tableParame)
        {
            CommonAPIResult<BaseTable<List<sys_user>>> commonAPIResult = new CommonAPIResult<BaseTable<List<sys_user>>>();

            var data = _userInfoServices.GetUserInfoList(ref tableParame);
            BaseTable<List<sys_user>> baseTable = new BaseTable<List<sys_user>>();
            baseTable.item = data;
            baseTable.total = tableParame.recordsFiltered;

            commonAPIResult.UpdateStatus(baseTable, MessageDict.Ok, "获取成功");
            return commonAPIResult;
        }

        /// <summary>
        /// 保存用户详细信息
        /// </summary>
        /// <param name="saveUserInfoReq"></param>
        /// <returns></returns>
        [Route("SaveUserInfo")]
        [HttpPost]
        public CommonAPIResult<string> SaveUserInfo([FromBody] SaveUserInfoReq saveUserInfoReq)
        {
            saveUserInfoReq.name = Request.Properties["userName"].ToString();
            saveUserInfoReq.id = Request.Properties["userId"].ToString();
            saveUserInfoReq.token = Request.Properties["userToken"].ToString();
            return _userInfoServices.SaveUserInfo(saveUserInfoReq);
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="saveUserInfoReq"></param>
        /// <returns></returns>
        [Route("DeleteUser")]
        [HttpPost]
        public CommonAPIResult<string> DeleteUser([FromBody] DeleteUserReq saveUserInfoReq)
        {
            saveUserInfoReq.name = Request.Properties["userName"].ToString();
            saveUserInfoReq.id = Request.Properties["userId"].ToString();
            saveUserInfoReq.token = Request.Properties["userToken"].ToString();
            return _userInfoServices.DeleteUser(saveUserInfoReq);
        }

        /// <summary>
        /// 改变用户状态
        /// </summary>
        /// <param name="changUserVaildReq"></param>
        /// <returns></returns>
        [Route("ChangUserVaild")]
        [HttpPost]
        public CommonAPIResult<string> ChangUserVaild([FromBody] ChangUserVaildReq changUserVaildReq)
        {
            changUserVaildReq.name = Request.Properties["userName"].ToString();
            changUserVaildReq.id = Request.Properties["userId"].ToString();
            changUserVaildReq.token = Request.Properties["userToken"].ToString();
            return _userInfoServices.ChangUserVaild(changUserVaildReq);
        }

    }
}
