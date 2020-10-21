using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VueElemenntAdminModel.APIModel;
using VueElemenntAdminModel.BaseModel;

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
        CommonAPIResult<UserDetailRes> GetUserDetail(UserDetailReq userDetailReq);
    }
}
