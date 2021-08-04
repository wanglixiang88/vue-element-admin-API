using IVueElememtAdminRepository;
using SqlSugar;
using System.Collections.Generic;
using VueElememtAdminRepository.Base;
using VueElememtAdminRepository.DBConfig;
using VueElementAdminModel.MySqlModel;
using System.Data;
using System.Data.SqlClient;
using VueElemenntAdminModel.APIModel;
using Dapper;
using System;

namespace VueElememtAdminRepository
{
    public class SysPermissionRoleRepository : Repository<sys_permissionrole>, ISysPermissionRoleRepository
    {
        public SysPermissionRoleRepository()
        {
            Conn = DBUtilities.GetMySqlConnectionString();
        }


        public static SqlSugarClient mysqlConn = MySQLInfo.mySqlSugarClient();

        /// <summary>
        /// 查询用户所有的权限
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <returns></returns>
        public IEnumerable<sys_permissionrole> GetPermissionRoleList(long? roleId)
        {
            using (mysqlConn)
            {
                return mysqlConn.Queryable<sys_permissionrole>().Where(t => t.roleId.Equals(roleId) && t.isDelete.Equals(0)).ToList();
            }
        }

        /// <summary>
        /// 删除 角色权限配置
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public string DeleteAndSavePermissionRole(SavePermissionRoleReq savePermissionReq)
        {
            using (Conn)
            {
                Conn.Open();

                var nowTime = DateTime.Now;

                //开始初始化事务
                IDbTransaction dbTransaction = Conn.BeginTransaction();

                try
                {
                    //将目前所有的角色权限设为已删除
                    Conn.Execute(@"update sys_permissionrole set isDelete=1,updateUserId=@updateUserId,updateUserName=@updateUserName,updateTime=@updateTime where roleId=@roleId",
                        new { roleId = savePermissionReq.roleId, updateUserId = savePermissionReq.id, updateUserName = savePermissionReq.name, updateTime = nowTime }, dbTransaction);

                    //将父子级数据结构转换为普通list
                    var allList = new List<GetPermissionRoleRes>();
                    foreach (var item in savePermissionReq.list)
                    {
                        OperationChildData(allList, item);
                        allList.Add(item);
                    }

                    //循环保存到数据库
                    foreach (var item in allList)
                    {
                        foreach (var checkbox in item.arryList)
                        {
                            if (checkbox.arryChecked)
                            {
                                //保存到数据库
                                Conn.Execute("insert into sys_permissionrole(roleId,menuId,operation,isDelete,createUserId,createUserName,createTime) values(@roleId,@menuId,@operation,@isDelete,@createUserId,@createUserName,@createTime)",
                                    new { roleId = savePermissionReq.roleId, menuId = item.menuId, operation = checkbox.arryValue, isDelete = 0, createUserId = savePermissionReq.id, createUserName = savePermissionReq.name, createTime = nowTime },
                                    dbTransaction);
                            }
                        }
                    }
                    Conn.Close();
                    dbTransaction.Commit();
                    return "1";
                }
                catch (Exception ex)
                {
                    dbTransaction.Rollback();
                    return ex.Message;
                }
            }
        }

        /// <summary>
        /// 递归子级数据
        /// </summary>
        /// <param name="AllList">树形列表数据</param>
        /// <param name="item">父级model</param>
        public static void OperationChildData(List<GetPermissionRoleRes> AllList, GetPermissionRoleRes item)
        {
            if (item.children != null)
            {
                if (item.children.Count > 0)
                {
                    AllList.AddRange(item.children);
                    foreach (var subItem in item.children)
                    {
                        OperationChildData(AllList, subItem);
                    }
                }
            }
        }

    }
}
