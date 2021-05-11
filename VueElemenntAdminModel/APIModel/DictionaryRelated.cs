using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VueElementAdminModel.MySqlModel;

namespace VueElementAdminModel.APIModel
{
    /// <summary>
    /// 字典相关类
    /// </summary>
    public class DictionaryRelated
    {
    }

    #region 查询字体列表相关的类

    public class dictionaryList : sys_dictionary
    {
        /// <summary>
        /// 子类
        /// </summary>
        public List<dictionaryList> children { get; set; }
    }

    #endregion

    #region 保存数据字典信息

    /// <summary>
    /// 保存数据字典接口所请求的参数
    /// </summary>
    public class SaveDictionaryReq : BaseInfo
    {

        /// <summary>
        /// 数据字典表的主键
        /// </summary>
        public long? arryId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string arryName { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public string arryValue { get; set; }

        /// <summary>
        /// 父类ID
        /// </summary>
        public long? parentId { get; set; }
    }

    #endregion

    /// <summary>
    /// 删除数据字典所请求的参数
    /// </summary>
    public class DeleteDictionaryReq : BaseInfo
    {
        /// <summary>
        /// 数据字典表的主键
        /// </summary>
        public long? arryId { get; set; }
    }
}
