using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VueElementAdminModel.APIModel;
using VueElementAdminModel.MySqlModel;

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
        /// 根据用户姓名获取用户信息
        /// </summary>
        /// <param name="token">token</param>
        /// <returns></returns>
        sys_user GetUserInfoByUserName(string userName);

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

        /// <summary>
        /// 获取用户的详细信息
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        sys_user GetUserInfo(long userId);

        /// <summary>
        /// 查询用户列表
        /// </summary>
        /// <param name="parameterJson"></param>
        /// <param name="tableParame"></param>
        /// <returns></returns>
        IEnumerable<sys_user> GetUserInfoList(ref TableParame tableParame);

        /// <summary>
        /// 保存用户信息
        /// </summary>
        /// <param name="saveUserInfoReq"></param>
        /// <returns></returns>
        int SaveUserInfo(SaveUserInfoReq saveUserInfoReq);

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="saveUserInfoReq"></param>
        /// <returns></returns>
        int DeleteUser(DeleteUserReq saveUserInfoReq);

        /// <summary>
        /// 修改用户状态
        /// </summary>
        /// <param name="changUserVaildReq"></param>
        /// <returns></returns>
        int ChangUserVaild(ChangUserVaildReq changUserVaildReq);
    }
}
