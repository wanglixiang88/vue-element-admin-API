using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vueElementAdminModel.MySqlModel;

namespace VueElemenntAdminModel.APIModel
{
    /// <summary>
    /// 菜单相关类
    /// </summary>
    public class MenuRelated
    {
    }

    #region 查询菜单列表相关的类

    public class menuList : sys_menu
    {
        /// <summary>
        /// 子类
        /// </summary>
        public List<menuList> children { get; set; }
    }

    #endregion

    #region 保存菜单接口所请求的参数

    /// <summary>
    /// 保存菜单接口所请求的参数
    /// </summary>
    public class SaveMenuReq: BaseInfo
    {
        public long? menuId { get; set; }
        public string menuName { get; set; }
        public long? parentId { get; set; }
        public int sequence { get; set; }
        public string route { get; set; }
        public string iconClass { get; set; }
    }

    #endregion

    #region 删除菜单接口所请求的参数

    /// <summary>
    /// 删除菜单接口所请求的参数
    /// </summary>
    public class DeleteMenuReq : BaseInfo
    {
        /// <summary>
        /// 菜单ID
        /// </summary>
        public long? menuId { get; set; }
    }

    #endregion
}
