using IVueElememtAdminRepository;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VueElememtAdminRepository.DBConfig;
using vueElementAdminModel.MySqlModel;

namespace VueElememtAdminRepository
{
    public class SysUserRepository: ISysUserRepository
    {

        public static SqlSugarClient mysqlConn = MySQLInfo.mySqlSugarClient();

        /// <summary>
        /// 根据Token获取用户信息
        /// </summary>
        /// <param name="token">token</param>
        /// <returns></returns>
        public sys_user GetUserInfoByToken(string token)
        {
            using (mysqlConn)
            {
                return mysqlConn.Queryable<sys_user>().Where(t => t.userToken.Equals(token)).First();
            }
        }

        /// <summary>
        /// 用户登录所用，根据用户密码查询用户信息
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="passWord">密码</param>
        /// <returns></returns>
        public sys_user GetUserForLogin(string userName, string passWord)
        {
            using (mysqlConn)
            {
                return mysqlConn.Queryable<sys_user>().Where(t => t.userName.Equals(userName) && t.passWord.Equals(passWord)).First();
            }
        }

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="sysUser"></param>
        /// <returns></returns>
        public int UpdateToken(sys_user sysUser)
        {
            using (mysqlConn)
            {
                return mysqlConn.Updateable(sysUser).ExecuteCommand();
            }
        }

        /// <summary>
        /// 根据用户ID获取用户的详细信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public sys_user GetUserInfo(long userId)
        {
            using (mysqlConn)
            {
                return mysqlConn.Queryable<sys_user>().Where(t => t.userId == userId).First();
            }
        }

    }
}
