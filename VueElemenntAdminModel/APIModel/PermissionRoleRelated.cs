using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VueElementAdminModel.APIModel;

namespace VueElemenntAdminModel.APIModel
{
    /// <summary>
    /// 角色权限
    /// </summary>
    public class PermissionRoleRelated
    {
    }

    /// <summary>
    /// 获取角色权限列表，所响应的参数
    /// </summary>
    public class GetPermissionRoleRes 
    { 
        /// <summary>
        /// 菜单ID
        /// </summary>
        public long menuId { get; set; }

        /// <summary>
        /// 菜单ID
        /// </summary>
        public string menuName { get; set; }

        /// <summary>
        /// 父类
        /// </summary>
        public long? parentId { get; set; }

        /// <summary>
        /// 子类
        /// </summary>
        public List<GetPermissionRoleRes> children { get; set; }

        /// <summary>
        /// 可操作的多选框集合
        /// </summary>
        public List<ArryDictionary> arryList { get; set; }
    }

    /// <summary>
    /// 可以操作的多选框
    /// </summary>
    public class ArryDictionary: OperationItems
    {
        /// <summary>
        /// 是否选中
        /// </summary>
        public bool arryChecked { get; set; }
    }


    /// <summary>
    /// 
    /// </summary>
    public class SavePermissionRoleReq:BaseInfo
    {
        public long roleId { get; set; }

        public List<GetPermissionRoleRes> list { get; set; }
    }

}
