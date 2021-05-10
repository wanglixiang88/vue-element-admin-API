using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VueElementAdminModel.APIModel;

namespace VueElememtAdminRepository.Base
{

    /// <summary>
    /// 分页扩展
    /// </summary>
    public static class DapperExtension
    {
        #region SQL分页

        /// <summary>
        /// 分页
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connection"></param>
        /// <param name="sql"></param>
        /// <param name="gridparam"></param>
        /// <param name="parameters"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public static IEnumerable<T> GetPageList<T>(this IDbConnection connection, string sql, ref TableParame tableParame, object parameters = null, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            StringBuilder strSql = new StringBuilder();
            int num = tableParame.page;
            int num1 = tableParame.page + tableParame.limit;
            string OrderBy = "";
            if (!string.IsNullOrEmpty(tableParame.sidx))
                OrderBy = "Order By " + tableParame.sidx + " " + tableParame.sort + "";
            else
                OrderBy = "Order By (select 0)";
            strSql.Append("Select * From (Select ROW_NUMBER() Over (" + OrderBy + ")");
            strSql.Append(" As rowNum, * From (" + sql + ") As T ) As N Where rowNum > " + num + " And rowNum <= " + num1 + "");
            tableParame.recordsFiltered = connection.ExecuteScalar<int>("Select Count(1) From (" + sql + ") As t", parameters, transaction, commandTimeout);
            return connection.Query<T>(strSql.ToString(), parameters, transaction, true, commandTimeout);
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connection"></param>
        /// <param name="sql"></param>
        /// <param name="gridparam"></param>
        /// <param name="parameters"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public static Task<IEnumerable<T>> GetPageListAsync<T>(this IDbConnection connection, string sql, ref TableParame tableParame, object parameters = null, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            StringBuilder strSql = new StringBuilder();
            int num = tableParame.page * tableParame.limit;
            int num1 = num + tableParame.limit;
            string OrderBy = "";
            if (!string.IsNullOrEmpty(tableParame.sidx))
                OrderBy = "Order By " + tableParame.sidx + " " + tableParame.sort + "";
            else
                OrderBy = "Order By (select 0)";
            strSql.Append("Select * From (Select ROW_NUMBER() Over (" + OrderBy + ")");
            strSql.Append(" As rowNum, * From (" + sql + ") As T ) As N Where rowNum > " + num + " And rowNum <= " + num1 + "");
            Task<int> c = connection.ExecuteScalarAsync<int>("Select Count(1) From (" + sql + ") As t", parameters, transaction, commandTimeout);
            tableParame.recordsFiltered = c.Result;
            return connection.QueryAsync<T>(strSql.ToString(), parameters, transaction, commandTimeout);
        }
        #endregion



        /// <summary>
        /// mysql分页语句
        /// </summary>
        /// <param name="strSql">sql语句</param>
        /// <param name="orderField">排序字段</param>
        /// <param name="isAsc">排序类型</param>
        /// <param name="pageSize">每页数据条数</param>
        /// <param name="pageIndex">页码</param>
        /// <returns></returns>
        public static StringBuilder MySqlPageSql(string strSql, string orderField, string isAsc, int pageSize, int pageIndex)
        {
            StringBuilder sb = new StringBuilder();
            if (pageIndex == 0)
            {
                pageIndex = 1;
            }
            int num = (pageIndex - 1) * pageSize;
            string OrderBy = "";

            if (!string.IsNullOrEmpty(orderField))
            {
                if (orderField.ToUpper().IndexOf("ASC") + orderField.ToUpper().IndexOf("DESC") > 0)
                {
                    OrderBy = " Order By " + orderField;
                }
                else
                {
                    OrderBy = " Order By " + orderField + " " + (isAsc == "ASC" ? "" : "DESC");
                }
            }
            sb.Append(strSql + OrderBy);
            sb.Append(" limit " + num + "," + pageSize + "");
            return sb;
        }


        #region Mysql分页

        /// <summary>
        /// 分页
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connection"></param>
        /// <param name="sql"></param>
        /// <param name="gridparam"></param>
        /// <param name="parameters"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public static IEnumerable<T> GetMySqlPageList<T>(this IDbConnection connection, string sql, ref TableParame gridparam, object parameters = null, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            StringBuilder strSql = new StringBuilder();
            int num = gridparam.page;
            int num1 = gridparam.page + gridparam.limit;
            //string OrderBy = "";
            //if (!string.IsNullOrEmpty(gridparam.sidx))
            //    OrderBy = "Order By " + gridparam.sidx + " " + gridparam.sort + "";
            //else
            //    OrderBy = "Order By (select 0)";
            //strSql.Append("Select * From (Select ROW_NUMBER() Over (" + OrderBy + ")");
            //strSql.Append(" As rowNum, * From (" + sql + ") As T ) As N Where rowNum > " + num + " And rowNum <= " + num1 + "");
            strSql = MySqlPageSql(sql, gridparam.sidx, gridparam.sort, gridparam.limit, gridparam.page);
            gridparam.recordsFiltered = connection.ExecuteScalar<int>("Select Count(1) From (" + sql + ") As t", parameters, transaction, commandTimeout);
            return connection.Query<T>(strSql.ToString(), parameters, transaction, true, commandTimeout);
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connection"></param>
        /// <param name="sql"></param>
        /// <param name="gridparam"></param>
        /// <param name="parameters"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public static Task<IEnumerable<T>> GetMySqlPageListAsync<T>(this IDbConnection connection, string sql, ref TableParame gridparam, object parameters = null, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            StringBuilder strSql = new StringBuilder();
            int num = gridparam.page;
            int num1 = gridparam.page + gridparam.limit;
            //string OrderBy = "";
            //if (!string.IsNullOrEmpty(gridparam.sidx))
            //    OrderBy = "Order By " + gridparam.sidx + " " + gridparam.sort + "";
            //else
            //    OrderBy = "Order By (select 0)";
            //strSql.Append("Select * From (Select ROW_NUMBER() Over (" + OrderBy + ")");
            //strSql.Append(" As rowNum, * From (" + sql + ") As T ) As N Where rowNum > " + num + " And rowNum <= " + num1 + "");
            strSql = MySqlPageSql(sql, gridparam.sidx, gridparam.sort, gridparam.limit, gridparam.page);
            Task<int> c = connection.ExecuteScalarAsync<int>("Select Count(1) From (" + sql + ") As t", parameters, transaction, commandTimeout);
            gridparam.recordsFiltered = c.Result;
            return connection.QueryAsync<T>(strSql.ToString(), parameters, transaction, commandTimeout);
        }
        #endregion
    }
}
