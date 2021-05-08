using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VueElemenntAdminModel.MySqlModel;
using vueElementAdminModel.MySqlModel;

namespace VueElemenntAdminModel.APIModel
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

}
