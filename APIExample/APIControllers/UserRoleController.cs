using APIExample.Filter;
using IVueElememtAdminRepository;
using IVueElementAdminServices;
using System.Collections.Generic;
using System.Web.Http;
using VueElememtAdminRepository;
using VueElemenntAdminModel.APIModel;
using VueElementAdminModel.APIModel;
using VueElementAdminModel.BaseModel;
using VueElementAdminModel.MySqlModel;
using VueElementAdminServices;

namespace APIExample.APIControllers
{
    /// <summary>
    /// UserRole 角色
    /// </summary>
    [AuthFilter]
    [RoutePrefix("API/UserRole")]
    public class UserRoleController : ApiController
    {
        private readonly static ISysUserRoleRepository _sysUserRoleRepository = new SysUserRoleRepository();
        private readonly static ISysMenuRepository _sysMenuRepository = new SysMenuRepository();
        private readonly static ISysPermissionRoleRepository _sysPermissionRoleRepository = new SysPermissionRoleRepository();

        private readonly IUserRoleServices _userRoleServices = new UserRoleServices(_sysUserRoleRepository, _sysMenuRepository, _sysPermissionRoleRepository);

        #region 角色基本信息的维护

        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <param name="tableParame"></param>
        /// <returns></returns>
        [Route("GetRoleList")]
        [HttpPost]
        public CommonAPIResult<BaseTable<List<sys_role>>> GetRoleList([FromBody]TableParame tableParame)
        {
            CommonAPIResult<BaseTable<List<sys_role>>> commonAPIResult = new CommonAPIResult<BaseTable<List<sys_role>>>();

            var data = _userRoleServices.GetRoleList(ref tableParame);
            BaseTable<List<sys_role>> baseTable = new BaseTable<List<sys_role>>();
            baseTable.item = data;
            baseTable.total = tableParame.recordsFiltered;

            commonAPIResult.UpdateStatus(baseTable, MessageDict.Ok, "获取成功");
            return commonAPIResult;
        }

        /// <summary>
        /// 保存角色信息
        /// </summary>
        /// <param name="saveRoleReq"></param>
        /// <returns></returns>
        [Route("SaveRoleInfo")]
        [HttpPost]
        public CommonAPIResult<string> SaveRoleInfo([FromBody] SaveRoleReq saveRoleReq)
        {
            saveRoleReq.name = Request.Properties["userName"].ToString();
            saveRoleReq.id = Request.Properties["userId"].ToString();
            return _userRoleServices.SaveRoleInfo(saveRoleReq);
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="roleReq"></param>
        /// <returns></returns>
        [Route("DeleteRole")]
        [HttpPost]
        public CommonAPIResult<string> DeleteRole([FromBody] RoleReq roleReq)
        {
            roleReq.name = Request.Properties["userName"].ToString();
            roleReq.id = Request.Properties["userId"].ToString();
            return _userRoleServices.DeleteRole(roleReq);
        }

        #endregion

        #region 角色的菜单信息的数据

        /// <summary>
        /// 获取角色的菜单树形列表
        /// </summary>
        /// <param name="roleReq"></param>
        /// <returns></returns>
        [Route("GetRoleMenu")]
        [HttpPost]
        public CommonAPIResult<BaseTable<List<GetPermissionRoleRes>>> GetRoleMenu([FromBody]RoleReq roleReq)
        {
            CommonAPIResult<BaseTable<List<GetPermissionRoleRes>>> commonAPIResult = new CommonAPIResult<BaseTable<List<GetPermissionRoleRes>>>();

            var dataList= _userRoleServices.GetRoleMenuList(roleReq);
            BaseTable<List<GetPermissionRoleRes>> baseTable = new BaseTable<List<GetPermissionRoleRes>>();
            baseTable.item = dataList;
            baseTable.total = dataList.Count;

            commonAPIResult.UpdateStatus(baseTable, MessageDict.Ok, "获取成功");
            return commonAPIResult;
        }

        /// <summary>
        /// 保存用户的角色权限
        /// </summary>
        /// <param name="savePermissionReq"></param>
        /// <returns></returns>
        [Route("SavePermissionRole")]
        [HttpPost]
        public CommonAPIResult<string> SavePermissionRole([FromBody] SavePermissionRoleReq savePermissionReq)
        {
            savePermissionReq.name = Request.Properties["userName"].ToString();
            savePermissionReq.id = Request.Properties["userId"].ToString();
            return _userRoleServices.SavePermissionRole(savePermissionReq);
        }

        #endregion
    }
}
