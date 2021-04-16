using Dapper;
using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace ToolLibrary.Helper.Helper
{
    /// <summary>
    /// 多条件动态查询属性
    /// </summary>
    public class Condition
    {
        /// <summary>
        /// 左边括号
        /// </summary>
        public string LeftBrace { get; set; }
        /// <summary>
        /// 项目名称（字段名）
        /// </summary>
        public string ParamName { get; set; }
        /// <summary>
        /// 比较（操作符）
        /// </summary>
        public ConditionOperate Operation { get; set; }
        /// <summary>
        /// 比较值（字段值）
        /// </summary>
        public object ParamValue { get; set; }
        /// <summary>
        /// 右边括号
        /// </summary>
        public string RightBrace { get; set; }
        /// <summary>
        /// 逻辑符：AND/OR
        /// </summary>
        public string Logic { get; set; }
    }

    /// <summary>
    /// 查询所用到的运算符
    /// </summary>
    public enum ConditionOperate : byte
    {
        /// <summary>
        /// 
        /// </summary>
        In,
        /// <summary>
        /// 等于
        /// </summary>
        Equal,
        /// <summary>
        /// 不等于
        /// </summary>
        NotEqual,
        /// <summary>
        /// 大于
        /// </summary>
        Greater,
        /// <summary>
        /// 大于等于
        /// </summary>
        GreaterThan,
        /// <summary>
        /// 小于
        /// </summary>
        Less,
        /// <summary>
        /// 小于等于
        /// </summary>
        LessThan,
        /// <summary>
        /// 为空
        /// </summary>
        Null,
        /// <summary>
        /// 不为空
        /// </summary>
        NotNull,
        /// <summary>
        /// 包含
        /// </summary>
        Like,
        /// <summary>
        /// 不包含
        /// </summary>
        NotLike,
        /// <summary>
        /// 左包含
        /// </summary>
        LeftLike,
        /// <summary>
        /// 右包含
        /// </summary>
        RightLike,
        /// <summary>
        /// 昨天
        /// </summary>
        Yesterday,
        /// <summary>
        /// 今天
        /// </summary>
        Today,
        /// <summary>
        /// 明天
        /// </summary>
        Tomorrow,
        /// <summary>
        /// 上周
        /// </summary>
        LastWeek,
        /// <summary>
        /// 本周
        /// </summary>
        ThisWeek,
        /// <summary>
        /// 下周
        /// </summary>
        NextWeek,
        /// <summary>
        /// 上月
        /// </summary>
        LastMonth,
        /// <summary>
        /// 本月
        /// </summary>
        ThisMonth,
        /// <summary>
        /// 下月
        /// </summary>
        NextMonth,
        /// <summary>
        /// 今天之前（天）
        /// </summary>
        BeforeDay,
        /// <summary>
        /// 今天之后（天）
        /// </summary>
        AfterDay,
    }

    public class ConditionBuilder
    {
        /// <summary>
        /// 动态查询条件
        /// </summary>
        /// <param name="conditions">条件参数集合</param>
        /// <param name="parameter">要返回Sql参数</param>
        /// <param name="orderField">排序字段</param>
        /// <param name="orderType">排序分类：DESC、ASC</param>
        /// <returns></returns>
        public static string GetWhereSql(IList conditions, out DynamicParameters parameter, string orderField = "", string orderType = "")
        {
            DateTime startTime;
            DateTime endTime;
            string ParamKey = "@";
            DynamicParameters ParamList = new DynamicParameters();
            StringBuilder sbWhere = new StringBuilder();

            int indexrow = 0;
            foreach (Condition item in conditions)
            {
                if (item.ParamValue == null || item.ParamValue.ToString() == "")
                    continue;
                string Logic = "";
                if (string.IsNullOrEmpty(item.Logic)) Logic = ""; else Logic = item.Logic == "AND" ? "AND" : "OR";
                if (conditions.Count - 1 == indexrow) { Logic = ""; }

                if (string.IsNullOrEmpty(item.Logic) || item.Logic == "AND")
                {
                    sbWhere.Append(" AND ");
                }
                else
                {
                    sbWhere.Append(" OR ");
                }

                string fieldName = item.ParamName;

                string[] ParamNameArry = item.ParamName.Split('.');
                string paramName = "";
                if (ParamNameArry.Length == 2)
                {
                    paramName = ParamNameArry[1] + indexrow;
                }
                else
                {
                    paramName = item.ParamName + indexrow;
                }
                int index = (int)item.Operation;
                switch (item.Operation)
                {
                    case ConditionOperate.In:
                        sbWhere.Append(" " + item.LeftBrace + fieldName + " in " + item.LeftBrace + ParamKey + paramName + item.RightBrace + item.RightBrace + " ");
                        ParamList.Add(ParamKey + paramName, item.ParamValue.ToString().Split(',').ToArray());
                        break;
                    case ConditionOperate.Equal:
                        sbWhere.Append(" " + item.LeftBrace + fieldName + " = " + ParamKey + paramName + item.RightBrace + " ");
                        ParamList.Add(ParamKey + paramName, item.ParamValue);
                        break;
                    case ConditionOperate.NotEqual:
                        sbWhere.Append(" " + item.LeftBrace + fieldName + " <> " + ParamKey + paramName + item.RightBrace + " ");
                        ParamList.Add(ParamKey + paramName, item.ParamValue);
                        break;
                    case ConditionOperate.Greater:
                        sbWhere.Append(" " + item.LeftBrace + fieldName + " > " + ParamKey + paramName + item.RightBrace + " ");
                        ParamList.Add(ParamKey + paramName, item.ParamValue);
                        break;
                    case ConditionOperate.GreaterThan:
                        sbWhere.Append(" " + item.LeftBrace + fieldName + " >= " + ParamKey + paramName + item.RightBrace + " ");
                        ParamList.Add(ParamKey + paramName, item.ParamValue);
                        break;
                    case ConditionOperate.Less:
                        sbWhere.Append(" " + item.LeftBrace + fieldName + " < " + ParamKey + paramName + item.RightBrace + " ");
                        ParamList.Add(ParamKey + paramName, item.ParamValue);
                        break;
                    case ConditionOperate.LessThan:
                        sbWhere.Append(" " + item.LeftBrace + fieldName + " <= " + ParamKey + paramName + item.RightBrace + " ");
                        ParamList.Add(ParamKey + paramName, item.ParamValue);
                        break;
                    case ConditionOperate.Null:
                        sbWhere.Append(string.Format(" " + item.LeftBrace + "{0} is null ", fieldName) + item.RightBrace + " ");
                        break;
                    case ConditionOperate.NotNull:
                        sbWhere.Append(string.Format(" " + item.LeftBrace + "{0} is not null ", fieldName) + item.RightBrace + " ");
                        break;
                    case ConditionOperate.Like:
                        sbWhere.Append(" " + item.LeftBrace + fieldName + " like " + ParamKey + paramName + item.RightBrace + " ");
                        ParamList.Add(ParamKey + paramName, "%" + item.ParamValue + "%");
                        break;
                    case ConditionOperate.NotLike:
                        sbWhere.Append(" " + item.LeftBrace + fieldName + " not like " + ParamKey + paramName + item.RightBrace + " ");
                        ParamList.Add(ParamKey + paramName, "%" + item.ParamValue + "%");
                        break;
                    case ConditionOperate.LeftLike:
                        sbWhere.Append(" " + item.LeftBrace + fieldName + " like " + ParamKey + paramName + item.RightBrace + " ");
                        ParamList.Add(ParamKey + paramName, "%" + item.ParamValue);
                        break;
                    case ConditionOperate.RightLike:
                        sbWhere.Append(" " + item.LeftBrace + fieldName + " like " + ParamKey + paramName + item.RightBrace + " ");
                        ParamList.Add(ParamKey + paramName, item.ParamValue + "%");
                        break;
                    case ConditionOperate.Yesterday:
                        startTime = DateTimeHelper.GetDateTime(DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") + " 00:00");
                        endTime = DateTimeHelper.GetDateTime(DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") + " 23:59");
                        sbWhere.Append(string.Format(" " + item.LeftBrace + "{0} between " + ParamKey + "start{1}  AND " + ParamKey + "end_{2}" + item.RightBrace + "", fieldName, paramName, paramName) + " ");
                        ParamList.Add(ParamKey + string.Format("start{0}", paramName), startTime);
                        ParamList.Add(ParamKey + string.Format("end_{0}", paramName), endTime);
                        break;
                    case ConditionOperate.Today:
                        startTime = DateTimeHelper.GetDateTime(DateTimeHelper.GetToday() + " 00:00");
                        endTime = DateTimeHelper.GetDateTime(DateTimeHelper.GetToday() + " 23:59");
                        sbWhere.Append(string.Format(" " + item.LeftBrace + "{0} between " + ParamKey + "start{1}  AND " + ParamKey + "end_{2}" + item.RightBrace + "", fieldName, paramName, paramName) + " ");
                        ParamList.Add(ParamKey + string.Format("start{0}", paramName), startTime);
                        ParamList.Add(ParamKey + string.Format("end_{0}", paramName), endTime);
                        break;
                    case ConditionOperate.Tomorrow:
                        startTime = DateTimeHelper.GetDateTime(DateTime.Now.AddDays(1).ToString("yyyy-MM-dd") + " 00:00");
                        endTime = DateTimeHelper.GetDateTime(DateTime.Now.AddDays(1).ToString("yyyy-MM-dd") + " 23:59");
                        sbWhere.Append(string.Format(" " + item.LeftBrace + "{0} between " + ParamKey + "start{1}  AND " + ParamKey + "end_{2}" + item.RightBrace + "", fieldName, paramName, paramName) + " ");
                        ParamList.Add(ParamKey + string.Format("start{0}", paramName), startTime);
                        ParamList.Add(ParamKey + string.Format("end_{0}", paramName), endTime);
                        break;
                    case ConditionOperate.LastWeek:
                        startTime = DateTimeHelper.GetDateTime(DateTime.Now.AddDays(Convert.ToInt32(1 - Convert.ToInt32(DateTime.Now.DayOfWeek)) - 7).ToString("yyyy-MM-dd") + " 00:00");
                        endTime = DateTimeHelper.GetDateTime(startTime.AddDays(6).ToString("yyyy-MM-dd") + " 23:59");
                        sbWhere.Append(string.Format(" " + item.LeftBrace + "{0} between " + ParamKey + "start{1}  AND " + ParamKey + "end_{2}" + item.RightBrace + "", fieldName, paramName, paramName) + " ");
                        ParamList.Add(ParamKey + string.Format("start{0}", paramName), startTime);
                        ParamList.Add(ParamKey + string.Format("end_{0}", paramName), endTime);
                        break;
                    case ConditionOperate.ThisWeek:
                        startTime = DateTimeHelper.GetDateTime(DateTime.Now.AddDays(1 - Convert.ToInt32(DateTime.Now.DayOfWeek)).ToString("yyyy-MM-dd") + " 00:00");
                        endTime = DateTimeHelper.GetDateTime(startTime.AddDays(6).ToString("yyyy-MM-dd") + " 23:59");
                        sbWhere.Append(string.Format(" " + item.LeftBrace + "{0} between " + ParamKey + "start{1}  AND " + ParamKey + "end_{2}" + item.RightBrace + "", fieldName, paramName, paramName) + " ");
                        ParamList.Add(ParamKey + string.Format("start{0}", paramName), startTime);
                        ParamList.Add(ParamKey + string.Format("end_{0}", paramName), endTime);
                        break;
                    case ConditionOperate.NextWeek:
                        startTime = DateTimeHelper.GetDateTime(DateTime.Now.AddDays(Convert.ToInt32(1 - Convert.ToInt32(DateTime.Now.DayOfWeek)) + 7).ToString("yyyy-MM-dd") + " 00:00");
                        endTime = DateTimeHelper.GetDateTime(startTime.AddDays(6).ToString("yyyy-MM-dd") + " 23:59");
                        sbWhere.Append(string.Format(" " + item.LeftBrace + "{0} between " + ParamKey + "start{1}  AND " + ParamKey + "end_{2}" + item.RightBrace + "", fieldName, paramName, paramName) + " ");
                        ParamList.Add(ParamKey + string.Format("start{0}", paramName), startTime);
                        ParamList.Add(ParamKey + string.Format("end_{0}", paramName), endTime);
                        break;
                    case ConditionOperate.LastMonth:
                        startTime = DateTimeHelper.GetDateTime(DateTime.Now.AddMonths(-1).ToString("yyyy-MM-01") + " 00:00");
                        endTime = DateTimeHelper.GetDateTime(DateTime.Parse(DateTime.Now.ToString("yyyy-MM-01")).AddDays(-1).ToString("yyyy-MM-dd") + " 23:59");
                        sbWhere.Append(string.Format(" " + item.LeftBrace + "{0} between " + ParamKey + "start{1}  AND " + ParamKey + "end_{2}" + item.RightBrace + "", fieldName, paramName, paramName) + " ");
                        ParamList.Add(ParamKey + string.Format("start{0}", paramName), startTime);
                        ParamList.Add(ParamKey + string.Format("end_{0}", paramName), endTime);
                        break;
                    case ConditionOperate.ThisMonth:
                        startTime = DateTimeHelper.GetDateTime(DateTime.Now.ToString("yyyy-MM-01") + " 00:00");
                        endTime = DateTimeHelper.GetDateTime(DateTime.Now.AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd") + " 23:59");
                        sbWhere.Append(string.Format(" " + item.LeftBrace + "{0} between " + ParamKey + "start{1}  AND " + ParamKey + "end_{2}" + item.RightBrace + "", fieldName, paramName, paramName) + " ");
                        ParamList.Add(ParamKey + string.Format("start{0}", paramName), startTime);
                        ParamList.Add(ParamKey + string.Format("end_{0}", paramName), endTime);
                        break;
                    case ConditionOperate.NextMonth:
                        startTime = DateTimeHelper.GetDateTime(DateTime.Now.AddMonths(1).ToString("yyyy-MM-01") + " 00:00");
                        endTime = DateTimeHelper.GetDateTime(DateTime.Parse(DateTime.Now.ToString("yyyy-MM-01")).AddMonths(2).AddDays(-1).ToString("yyyy-MM-dd") + " 23:59");
                        sbWhere.Append(string.Format(" " + item.LeftBrace + "{0} between " + ParamKey + "start{1}  AND " + ParamKey + "end_{2}" + item.RightBrace + "", fieldName, paramName, paramName) + " ");
                        ParamList.Add(ParamKey + string.Format("start{0}", paramName), startTime);
                        ParamList.Add(ParamKey + string.Format("end_{0}", paramName), endTime);
                        break;
                    case ConditionOperate.BeforeDay:
                        startTime = DateTimeHelper.GetDateTime(DateTime.Now.AddDays(double.Parse("-" + item.ParamValue.ToString())) + " 00:00");
                        endTime = DateTimeHelper.GetDateTime(DateTimeHelper.GetToday() + " 23:59");
                        sbWhere.Append(string.Format(" " + item.LeftBrace + "{0} between " + ParamKey + "start{1}  AND " + ParamKey + "end_{2}" + item.RightBrace + "", fieldName, paramName, paramName) + " ");
                        ParamList.Add(ParamKey + string.Format("start{0}", paramName), startTime);
                        ParamList.Add(ParamKey + string.Format("end_{0}", paramName), endTime);
                        break;
                    case ConditionOperate.AfterDay:
                        startTime = DateTimeHelper.GetDateTime(DateTime.Now.AddDays(double.Parse(item.ParamValue.ToString())) + " 00:00");
                        endTime = DateTimeHelper.GetDateTime(DateTimeHelper.GetToday() + " 23:59");
                        sbWhere.Append(string.Format(" " + item.LeftBrace + "{0} between " + ParamKey + "start{1}  AND " + ParamKey + "end_{2}" + item.RightBrace + "", fieldName, paramName, paramName) + " ");
                        ParamList.Add(ParamKey + string.Format("start{0}", paramName), startTime);
                        ParamList.Add(ParamKey + string.Format("end_{0}", paramName), endTime);
                        break;
                    default:
                        break;
                }
                indexrow++;
            }
            if (!string.IsNullOrEmpty(orderField))//判断是否有排序功能
            {
                orderType = orderType.ToLower() == "desc" ? "desc" : "asc";
                sbWhere.Append(" Order By " + orderField + " " + orderType + "");
            }
            parameter = ParamList;
            return sbWhere.ToString();
        }
    }
}
