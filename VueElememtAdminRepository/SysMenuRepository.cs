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
    public class SysMenuRepository : Repository<sys_menu>, ISysMenuRepository
    {
        public SysMenuRepository()
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
        public IEnumerable<sys_menu> GetMenuList(ref TableParame tableParame)
        {
            using (Conn)
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(@" SELECT * FROM  sys_menu where 1=1 and isDelete=0 ");
                DynamicParameters ParamList = new DynamicParameters();
                string WhereSql = ConditionBuilder.GetWhereSql(tableParame.parameterJson, out ParamList);
                sql.Append(WhereSql);
                return Conn.Query<sys_menu>(sql.ToString());
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
                return Conn.Execute(@"insert into sys_menu(menuName,parentId,sequence,route,iconClass,isDelete,createTime,createUserId,createUserName) values(@menuName,@parentId,@sequence,@route,@iconClass,0,@createTime,@createUserId,@createUserName)", new
                {
                    menuName= sys_menu.menuName,
                    parentId= sys_menu.parentId,
                    sequence= sys_menu.sequence,
                    route= sys_menu.route,
                    iconClass= sys_menu.iconClass,
                    isDelete=0,
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
                return Conn.Execute(@" update sys_menu set menuName=@menuName,parentId=@parentId,sequence=@sequence,route=@route,iconClass=@iconClass,updateUserId=@updateUserId,updateUserName=@updateUserName,updateTime=@updateTime where menuId=@menuId ", new
                {
                    menuName = sys_menu.menuName,
                    parentId = sys_menu.parentId,
                    sequence = sys_menu.sequence,
                    route = sys_menu.route,
                    iconClass = sys_menu.iconClass,
                    updateUserId = sys_menu.updateUserId,
                    updateUserName = sys_menu.updateUserName,
                    updateTime = sys_menu.updateTime,
                    menuId = sys_menu.menuId
                });
            }
        }
    }
}
