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
    }
}
