using APIExample.Filter;
using IVueElememtAdminRepository;
using IVueElementAdminServices;
using System.Collections.Generic;
using System.Web.Http;
using VueElememtAdminRepository;
using VueElemenntAdminModel.APIModel;
using VueElemenntAdminModel.BaseModel;
using vueElementAdminModel.MySqlModel;
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
        private readonly IUserRoleServices _userRoleServices = new UserRoleServices(_sysUserRoleRepository);

        /// <summary>
        /// 获取角色列表
        /// </summary>
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
            return _userRoleServices.DeleteUser(roleReq);
        }

    }
}
