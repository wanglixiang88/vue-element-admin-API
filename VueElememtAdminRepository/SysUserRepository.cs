using Dapper;
using IVueElememtAdminRepository;
using SqlSugar;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolLibrary.Helper.Helper;
using ToolLibrary.Helper.Json;
using VueElememtAdminRepository.Base;
using VueElememtAdminRepository.DBConfig;
using VueElemenntAdminModel.APIModel;
using vueElementAdminModel.MySqlModel;

namespace VueElememtAdminRepository
{
    public class SysUserRepository : Repository<sys_user> ,ISysUserRepository
    {
        public SysUserRepository()
        {
            Conn = DBUtilities.GetMySqlConnectionString();
        }


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



        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameterJson"></param>
        /// <param name="tableParame"></param>
        /// <returns></returns>
        public IEnumerable<sys_user> GetUserInfoList(string parameterJson, ref TableParame tableParame)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"  SELECT * FROM  sys_user ");

            if (!string.IsNullOrEmpty(parameterJson))
            {
                DynamicParameters ParamList = new DynamicParameters();
                IList conditions = RequestHelper.UrlDecode(parameterJson).JonsToList<Condition>();
                string WhereSql = ConditionBuilder.GetWhereSql(conditions, out ParamList);
                sql.Append(WhereSql);
                return GetMySqlPageList<sys_user>(sql.ToString(), ParamList, ref tableParame);
            }
            return GetMySqlPageList<sys_user>(sql.ToString(), null, ref tableParame);
        }
    }
}
