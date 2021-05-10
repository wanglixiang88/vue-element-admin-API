using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        CommonAPIResult<string> DeleteUser(RoleReq roleReq);

        /// <summary>
        /// 改变用户状态
        /// </summary>
        /// <param name="changUserVaildReq"></param>
        /// <returns></returns>
        CommonAPIResult<string> ChangUserVaild(ChangUserVaildReq changUserVaildReq);
    }
}
