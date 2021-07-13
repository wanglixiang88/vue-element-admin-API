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
using VueElementAdminModel.APIModel;
using VueElementAdminModel.MySqlModel;

namespace VueElememtAdminRepository
{
    public class SysUserRepository : Repository<sys_user>, ISysUserRepository
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
                //mysqlConn.CodeFirst.SetStringDefaultLength(200).InitTables(typeof(sys_user));//创建表
                //select table_name from information_schema.tables where table_schema = 'vue-element-admin'; //查询数据库里有哪些表
                return mysqlConn.Queryable<sys_user>().Where(t => t.userName.Equals(userName) && t.passWord.Equals(passWord) && t.isDelete==0).First();
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
        /// 获取用户列表
        /// </summary>
        /// <param name="parameterJson"></param>
        /// <param name="tableParame"></param>
        /// <returns></returns>
        public IEnumerable<sys_user> GetUserInfoList(ref TableParame tableParame)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@" SELECT * FROM  sys_user where 1=1 and isDelete=0 ");
            DynamicParameters ParamList = new DynamicParameters();
            string WhereSql = ToolLibrary.Helper.Helper.ConditionBuilder.GetWhereSql(tableParame.parameterJson, out ParamList);
            sql.Append(WhereSql);
            return GetMySqlPageList<sys_user>(sql.ToString(), ParamList, ref tableParame);
        }


        /// <summary>
        /// 保存用户信息
        /// </summary>
        /// <param name="saveUserInfoReq"></param>
        /// <returns></returns>
        public int SaveUserInfo(SaveUserInfoReq saveUserInfoReq)
        {
            using (Conn)
            {
                return Conn.Execute(@"insert into sys_user(isValid,isDelete,userName,passWord,roleId,createTime,createUserId,createUserName) values(0,0,@userName,@passWord,@roleId,@createTime,@createUserId,@createUserName)", new
                {
                    userName = saveUserInfoReq.userName,
                    passWord = saveUserInfoReq.passWord,
                    roleId = saveUserInfoReq.roleId,
                    createTime = DateTime.Now,
                    createUserId = saveUserInfoReq.id,
                    createUserName = saveUserInfoReq.name
                });
            }
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="saveUserInfoReq"></param>
        /// <returns></returns>
        public int  DeleteUser(DeleteUserReq saveUserInfoReq)
        {
            using (Conn)
            {
                return Conn.Execute(@" update sys_user set isDelete=1,updateUserId=@updateUserId,updateUserName=@updateUserName,updateTime=@updateTime where userId=@userId ", new
                {
                    userId = saveUserInfoReq.userId,
                    updateTime = DateTime.Now,
                    updateUserId = saveUserInfoReq.id,
                    updateUserName = saveUserInfoReq.name
                });
            }
        }


        /// <summary>
        /// 修改用户状态
        /// </summary>
        /// <param name="changUserVaildReq"></param>
        /// <returns></returns>
        public int ChangUserVaild(ChangUserVaildReq changUserVaildReq)
        {
            using (Conn)
            {
                return Conn.Execute(@" update sys_user set isValid=@isValid,updateUserId=@updateUserId,updateUserName=@updateUserName,updateTime=@updateTime where userId=@userId ", new
                {
                    isValid = changUserVaildReq.isValid,
                    updateTime = DateTime.Now,
                    updateUserId = changUserVaildReq.id,
                    updateUserName = changUserVaildReq.name,
                    userId = changUserVaildReq.userId
                });
            }
        }

        /// <summary>
        /// 根据用户姓名获取用户信息
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public sys_user GetUserInfoByUserName(string userName)
        {
            using (Conn)
            {
                return Conn.QueryFirstOrDefault<sys_user>(@" select * from sys_user where userName=@userName ", new
                {
                    userName = userName
                });
            }
        }
    }
}
