using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VueElementAdminModel.APIModel
{
    /// <summary>
    /// 角色相关类
    /// </summary>
    public class RoleRelated
    {
    }

    /// <summary>
    /// 基础类
    /// </summary>
    public class RoleReq : BaseInfo
    {
        /// <summary>
        /// 角色ID
        /// </summary>
        public long? roleId { get; set; }
    }

    /// <summary>
    /// 保存|修改角色接口请求的参数
    /// </summary>
    public class SaveRoleReq: RoleReq
    {

        /// <summary>
        /// 角色名称
        /// </summary>
        public string roleName { get; set; }
    }

}
