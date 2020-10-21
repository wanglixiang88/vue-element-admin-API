using IVueElememtAdminRepository;
using IVueElementAdminServices;
using VueElemenntAdminModel.APIModel;
using VueElemenntAdminModel.BaseModel;
using ToolLibrary.Helper.Helper;
using System;

namespace VueElementAdminServices
{
    public class UserInfoServices: IUserInfoServices
    {

        private readonly ISysUserRepository _sysUserRepository;

        /// <summary>
        /// 构造函数
        /// </summary>
        public UserInfoServices(ISysUserRepository sysUserRepository)
        {
            _sysUserRepository = sysUserRepository;
        }

        string Md5Key = "VueElementAdmin"; //用于字符串MD5加密

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="userLoginReq"></param>
        /// <returns></returns>
        public CommonAPIResult<UserLoginRes> UserLogin(UserLoginReq userLoginReq)
        {
            CommonAPIResult<UserLoginRes> commonAPIResult = new CommonAPIResult<UserLoginRes>();

            if (string.IsNullOrEmpty(userLoginReq.userName))
            {
                commonAPIResult.UpdateStatus(null, MessageDict.PramError, "请输入用户名！");
                return commonAPIResult;
            }
            if (string.IsNullOrEmpty(userLoginReq.passWord))
            {
                commonAPIResult.UpdateStatus(null, MessageDict.PramError, "请输入密码！");
                return commonAPIResult;
            }

            try
            {
                var afterMD5 = EncryptHelper.MD5Encrypt(Md5Key + userLoginReq.passWord); //加上字符串加密密码
                var userInfo = _sysUserRepository.GetUserForLogin(userLoginReq.userName, afterMD5); //查询用户是否存在

                if (userInfo == null)
                {
                    commonAPIResult.UpdateStatus(null, MessageDict.Failed, "请检查用户名密码是否正确");
                    return commonAPIResult;
                }
                else
                {
                    userInfo.tokenExpirationDate = DateTime.Now.AddDays(30);
                    userInfo.userToken = Guid.NewGuid().ToString("N");
                    var i = _sysUserRepository.UpdateToken(userInfo); //更新token
                    if (i > 0)
                    {
                        UserLoginRes userLoginRes = new UserLoginRes();
                        userLoginRes.userToken = userInfo.userToken; //设置返回到页面的token

                        commonAPIResult.UpdateStatus(userLoginRes, MessageDict.Ok, "登录成功！");
                        return commonAPIResult;
                    }
                    else
                    {
                        commonAPIResult.UpdateStatus(null, MessageDict.SystemUnkonw, "未知错误，更新用户数据异常！");
                        return commonAPIResult;
                    }

                }
            }
            catch (Exception ex)
            {
                commonAPIResult.UpdateStatus(null, MessageDict.SystemUnkonw, "系统异常，请联系管理员！" + ex.Message);
                return commonAPIResult;
            }

        }


        /// <summary>
        /// 获取用户详细信息
        /// </summary>
        /// <param name="userDetailReq"></param>
        /// <returns></returns>
        public CommonAPIResult<UserDetailRes> GetUserDetail(UserDetailReq userDetailReq)
        {
            return new CommonAPIResult<UserDetailRes>();
        }

    }
}
