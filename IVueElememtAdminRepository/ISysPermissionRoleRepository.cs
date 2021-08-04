using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VueElemenntAdminModel.APIModel;
using VueElementAdminModel.APIModel;
using VueElementAdminModel.MySqlModel;

namespace IVueElememtAdminRepository
{
    public interface ISysPermissionRoleRepository
    {
        /// <summary>
        /// 获取全部的权限列表
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        IEnumerable<sys_permissionrole> GetPermissionRoleList(long? roleId);

        /// <summary>
        /// 删除 角色权限配置
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        string DeleteAndSavePermissionRole(SavePermissionRoleReq savePermissionReq);


    }
}
