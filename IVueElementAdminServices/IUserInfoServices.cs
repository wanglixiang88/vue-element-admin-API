using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VueElemenntAdminModel.APIModel;
using VueElemenntAdminModel.BaseModel;
using vueElementAdminModel.MySqlModel;

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
        CommonAPIResult<UserDetailRes> GetUserDetail(string token);

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
    }
}
