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
    public class SysDictionaryRepository : Repository<sys_dictionary>, ISysDictionaryRepository
    {
        public SysDictionaryRepository()
        {
            Conn = DBUtilities.GetMySqlConnectionString();
        }

        public static SqlSugarClient mysqlConn = MySQLInfo.mySqlSugarClient();

        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <param name="parameterJson"></param>
        /// <param name="tableParame"></param>
        /// <returns></returns>
        public IEnumerable<sys_dictionary> GetMenuList(ref TableParame tableParame)
        {
            using (Conn)
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(@" SELECT * FROM sys_dictionary where 1=1 and isDelete=0 ");
                DynamicParameters ParamList = new DynamicParameters();
                string WhereSql = ConditionBuilder.GetWhereSql(tableParame.parameterJson, out ParamList);
                sql.Append(WhereSql);
                if (!string.IsNullOrEmpty(tableParame.sidx))
                {
                    if (tableParame.sidx.ToUpper().IndexOf("ASC") + tableParame.sidx.ToUpper().IndexOf("DESC") > 0)
                    {
                        sql.Append(" Order By " + tableParame.sidx);
                    }
                    else
                    {
                        sql.Append(" Order By " + tableParame.sidx + " " + (tableParame.sort == "ASC" ? "" : "DESC"));
                    }
                }
                return Conn.Query<sys_dictionary>(sql.ToString());
            }
        }

        /// <summary>
        /// 根据menuId查询菜单信息
        /// </summary>
        /// <param name="menuId"></param>
        /// <returns></returns>
        public sys_menu GetMenuById(long menuId)
        {
            using (Conn)
            {
                return Conn.QueryFirstOrDefault<sys_menu>("select * from sys_menu where isDelete=0 and menuId=@menuId", new { menuId = menuId });
            }
        }

        /// <summary>
        /// 保存菜单记录
        /// </summary>
        /// <param name="sys_menu"></param>
        /// <returns></returns>
        public int SaveSysMenuInfo(sys_menu sys_menu)
        {
            using (Conn)
            {
                return Conn.Execute(@"insert into sys_menu(menuName,parentId,sequence,route,iconClass,isDelete,createTime,createUserId,createUserName,operation) values(@menuName,@parentId,@sequence,@route,@iconClass,0,@createTime,@createUserId,@createUserName,@operation)", new
                {
                    menuName= sys_menu.menuName,
                    parentId= sys_menu.parentId,
                    sequence= sys_menu.sequence,
                    route= sys_menu.route,
                    iconClass= sys_menu.iconClass,
                    operation= sys_menu.operation,
                    isDelete =0,
                    createTime= sys_menu.createTime,
                    createUserId= sys_menu.createUserId,
                    createUserName= sys_menu.createUserName
                });
            }
        }

        /// <summary>
        /// 更新菜单记录
        /// </summary>
        /// <param name="sys_menu"></param>
        /// <returns></returns>
        public int UpdateSysMenuInfo(sys_menu sys_menu)
        {
            using (Conn)
            {
                return Conn.Execute(@" update sys_menu set menuName=@menuName,parentId=@parentId,sequence=@sequence,route=@route,iconClass=@iconClass,updateUserId=@updateUserId,updateUserName=@updateUserName,updateTime=@updateTime,operation=@operation where menuId=@menuId and isDelete=0", new
                {
                    menuName = sys_menu.menuName,
                    parentId = sys_menu.parentId,
                    sequence = sys_menu.sequence,
                    route = sys_menu.route,
                    iconClass = sys_menu.iconClass,
                    updateUserId = sys_menu.updateUserId,
                    updateUserName = sys_menu.updateUserName,
                    updateTime = sys_menu.updateTime,
                    operation=sys_menu.operation,
                    menuId = sys_menu.menuId
                });
            }
        }

        /// <summary>
        /// 软删除菜单
        /// </summary>
        /// <param name="deleteMenuReq"></param>
        /// <returns></returns>
        public int DeleteSysMenu(DeleteMenuReq deleteMenuReq)
        {
            using (Conn)
            {
                return Conn.Execute(@" update sys_menu set isDelete=@isDelete, updateUserId=@updateUserId,updateUserName=@updateUserName,updateTime=@updateTime where menuId=@menuId and isDelete=0 ", new
                {
                    isDelete = 1,
                    updateUserId = deleteMenuReq.id,
                    updateUserName = deleteMenuReq.name,
                    updateTime = DateTime.Now,
                    menuId = deleteMenuReq.menuId
                });
            }
        }

        /// <summary>
        /// 根据ID查询子类信息
        /// </summary>
        /// <param name="parentId">父类ID</param>
        /// <returns></returns>
        public List<sys_menu> GetMenuByParent(long parentId)
        {
            using (Conn)
            {
                return Conn.Query<sys_menu>("select * from sys_menu where isDelete=0 and parentId=@parentId", new { parentId = parentId }).ToList();
            }
        }
    }
}
