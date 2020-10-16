using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolLibrary.Helper.Helper;

namespace VueElememtAdminRepository.DBConfig
{
    public class MySQLInfo
    {
        /// <summary>
        /// 连接对象
        /// </summary>
        /// <returns></returns>
        public static SqlSugarClient mySqlSugarClient()
        {
            SqlSugarClient Db = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = ConfigHelper.AppSettings("mySql_VueElementAdmin"),//数据库连接串
                DbType = DbType.MySql,
                InitKeyType = InitKeyType.Attribute,//从特性读取主键和自增列信息
                IsAutoCloseConnection = true,//开启自动释放模式和EF原理一样我就不多解释了
            });
            return Db;
        }
    }
}
