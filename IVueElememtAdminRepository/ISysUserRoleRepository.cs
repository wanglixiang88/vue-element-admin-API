using System.Collections.Generic;
using VueElementAdminModel.APIModel;
using VueElementAdminModel.MySqlModel;

namespace IVueElememtAdminRepository
{
    public interface ISysUserRoleRepository
    {
        /// <summary>
        /// 根据角色ID获取角色的详细信息
        /// </summary>
        /// <param name="userId">角色ID</param>
        /// <returns></returns>
        sys_role GetUserInfo(long roleId);

        /// <summary>
        /// 根据角色名称获取角色的详细信息
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        sys_role GetRoleByName(string roleName);

        /// <summary>
        /// 查询用户列表
        /// </summary>
        /// <param name="parameterJson"></param>
        /// <param name="tableParame"></param>
        /// <returns></returns>
        IEnumerable<sys_role> GetRoleList(ref TableParame tableParame);

        /// <summary>
        /// 保存|修改角色信息
        /// </summary>
        /// <param name="saveRoleReq"></param>
        /// <returns></returns>
        int SaveRoleInfo(sys_role sys_Role);

        /// <summary>
        /// 修改用户状态
        /// </summary>
        /// <param name="changUserVaildReq"></param>
        /// <returns></returns>
        int ChangUserVaild(ChangUserVaildReq changUserVaildReq);
    }
}
