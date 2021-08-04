using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VueElemenntAdminModel.APIModel;
using VueElementAdminModel.APIModel;
using VueElementAdminModel.BaseModel;
using VueElementAdminModel.MySqlModel;

namespace IVueElementAdminServices
{
    public interface IUserRoleServices
    {
        /// <summary>
        /// 获取角色列表信息
        /// </summary>
        /// <returns></returns>
        List<sys_role> GetRoleList(ref TableParame tableParame);

        /// <summary>
        /// 保存角色信息
        /// </summary>
        /// <param name="saveRoleReq"></param>
        /// <returns></returns>
        CommonAPIResult<string> SaveRoleInfo(SaveRoleReq saveRoleReq);

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="saveUserInfoReq"></param>
        /// <returns></returns>
        CommonAPIResult<string> DeleteRole(RoleReq roleReq);

        #region 角色权限菜单

        /// <summary>
        /// 获取角色菜单权限的列表
        /// </summary>
        /// <param name="roleReq"></param>
        /// <returns></returns>
        List<GetPermissionRoleRes> GetRoleMenuList(RoleReq roleReq);

        /// <summary>
        /// 保存角色的权限
        /// </summary>
        /// <param name="savePermissionReq"></param>
        /// <param name="id">操作人ID</param>
        /// <param name="name">操作人名称</param>
        /// <returns></returns>
        CommonAPIResult<string> SavePermissionRole(SavePermissionRoleReq savePermissionReq);

        #endregion
    }
}
