using Dapper;
using IVueElememtAdminRepository;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using VueElememtAdminRepository.Base;
using VueElememtAdminRepository.DBConfig;
using VueElementAdminModel.APIModel;
using VueElementAdminModel.MySqlModel;

namespace VueElememtAdminRepository
{
    public class SysUserRoleRepository : Repository<sys_role>, ISysUserRoleRepository
    {
        public SysUserRoleRepository()
        {
            Conn = DBUtilities.GetMySqlConnectionString();
        }

        public static SqlSugarClient mysqlConn = MySQLInfo.mySqlSugarClient();

        /// <summary>
        /// 根据角色名称获取角色信息
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public sys_role GetRoleByName(string roleName)
        {
            using (mysqlConn)
            {
                return mysqlConn.Queryable<sys_role>().Where(t => t.roleName == roleName && t.isDelete==0).First();
            }
        }

        /// <summary>
        /// 根据角色ID获取角色的详细信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public sys_role GetRoleInfo(long roleId)
        {
            using (mysqlConn)
            {
                return mysqlConn.Queryable<sys_role>().Where(t => t.roleId == roleId && t.isDelete == 0).First();
            }
        }



        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <param name="parameterJson"></param>
        /// <param name="tableParame"></param>
        /// <returns></returns>
        public IEnumerable<sys_role> GetRoleList(ref TableParame tableParame)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@" SELECT * FROM  sys_role where 1=1 and isDelete=0 ");
            DynamicParameters ParamList = new DynamicParameters();
            string WhereSql = ToolLibrary.Helper.Helper.ConditionBuilder.GetWhereSql(tableParame.parameterJson, out ParamList);
            sql.Append(WhereSql);
            return GetMySqlPageList<sys_role>(sql.ToString(), ParamList, ref tableParame);
        }


        /// <summary>
        /// 保存用户信息
        /// </summary>
        /// <param name="saveUserInfoReq"></param>
        /// <returns></returns>
        public int SaveRoleInfo(sys_role sys_Role)
        {
            using (mysqlConn)
            {
                if (sys_Role.roleId.HasValue)
                {
                    return mysqlConn.Updateable(sys_Role).ExecuteCommand();
                }
                else
                {
                    return mysqlConn.Insertable(sys_Role).ExecuteCommand();
                }
            }
        }

    }
}
