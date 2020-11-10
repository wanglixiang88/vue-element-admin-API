using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolLibrary.Helper.Helper;

namespace VueElememtAdminRepository.DBConfig
{
    public class DBUtilities
    {

        #region vue-element-admin

        private static readonly string mySqlConnectionString = ConfigHelper.AppSettings("mySql_VueElementAdmin");
        public static MySqlConnection GetMySqlConnectionString()
        {
            return new MySqlConnection(mySqlConnectionString);
        }

        #endregion

    }
}
