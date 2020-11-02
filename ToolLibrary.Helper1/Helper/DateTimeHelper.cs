using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolLibrary.Helper.Helper
{
    public class DateTimeHelper
    {

        /// <summary>
        /// 取得DateTime值
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static DateTime GetDateTime(object obj)
        {
            if (obj != null && obj.ToString() != "")
                return DateTime.Parse(obj.ToString());
            else
                return DateTime.Now;
            //return DateTime.MinValue;
        }


        /// <summary>
        /// 当前日期
        /// </summary>
        /// <returns></returns>
        public static string GetToday()
        {
            return DateTime.Now.ToString("yyyy-MM-dd");
        }


    }
}
