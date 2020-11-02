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
        /// <summary>
        /// 总条数
        /// </summary>
        public int count { get; set; }

        /// <summary>
        /// 数据集合
        /// </summary>
        public T item { get; set; }
    }

    /// <summary>
    /// 列表返回请求的参数
    /// </summary>
    public class TableParame
    {
        /// <summary>
        /// 当前页
        /// </summary>
        public int page { get; set; }

        /// <summary>
        /// 每页数量
        /// </summary>
        public int limit { get; set; }

        /// <summary>
        /// 查询参数
        /// </summary>
        public string parameterJson { get; set; }
    }

}
