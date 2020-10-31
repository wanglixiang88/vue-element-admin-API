using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VueElemenntAdminModel.APIModel
{
    /// <summary>
    /// 用户信息的基类
    /// </summary>
    public class BaseInfo
    {
        /// <summary>
        /// 用户token
        /// </summary>
        public string userToken { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string userName { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public string userId { get; set; }
    }


    /// <summary>
    /// 列表返回的数据，必须包上这一层
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseTable<T>
    {
        public int count { get; set; }

        public T item { get; set; }
    }

    public class TableParame
    {
        public int page { get; set; }

        public int limit { get; set; }
    }

}
