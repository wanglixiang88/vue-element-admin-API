using IVueElememtAdminRepository;
using IVueElementAdminServices;
using VueElemenntAdminModel.APIModel;
using VueElemenntAdminModel.BaseModel;
using ToolLibrary.Helper.Helper;
using System;
using vueElementAdminModel.MySqlModel;
using System.Collections.Generic;
using System.Linq;

namespace VueElementAdminServices
{
    public class UserRoleServices : IUserRoleServices
    {

        private readonly ISysUserRoleRepository _sysUserRoleRepository;

        /// <summary>
        /// 构造函数
        /// </summary>
        public UserRoleServices(ISysUserRoleRepository sysUserRepository)
        {
            _sysUserRoleRepository = sysUserRepository;
        }

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="parameterJson"></param>
        /// <param name="tableParame"></param>
        /// <returns></returns>
        public List<sys_role> GetRoleList(ref TableParame tableParame)
        {
            return _sysUserRoleRepository.GetRoleList(ref tableParame).ToList();
        }

        /// <summary>
        /// 保存用户信息
        /// </summary>
        /// <param name="saveUserInfoReq"></param>
        /// <returns></returns>
        public CommonAPIResult<string> SaveRoleInfo(SaveRoleReq saveRoleReq)
        {
            CommonAPIResult<string> commonAPIResult = new CommonAPIResult<string>();
            var role = new sys_role();
            if (saveRoleReq.roleId.HasValue)
            {
                role = _sysUserRoleRepository.GetUserInfo(saveRoleReq.roleId.Value); //查询角色信息
                if (role == null)
                {
                    commonAPIResult.UpdateStatus("", MessageDict.Failed, "查询不到角色信息！请联系管理员！");
                    return commonAPIResult;
                }
                role.roleName = saveRoleReq.roleName;
                role.updateUserId = saveRoleReq.id;
                role.updateUserName = saveRoleReq.name;
                role.updateTime = DateTime.Now;
            }
            else
            {
                var roleExit = _sysUserRoleRepository.GetRoleByName(saveRoleReq.roleName); //查询角色信息
                if (roleExit != null)
                {
                    commonAPIResult.UpdateStatus("", MessageDict.Failed, "角色名称已存在！");
                    return commonAPIResult;
                }
                role = AutoMapper.To<SaveRoleReq, sys_role>(saveRoleReq);
                role.createUserId = saveRoleReq.id;
                role.createUserName = saveRoleReq.name;
                role.createTime = DateTime.Now;
            }

            var saveUser = _sysUserRoleRepository.SaveRoleInfo(role); //更新|保存角色
            if (saveUser > 0)
            {
                commonAPIResult.UpdateStatus("", MessageDict.Ok, "操作成功！");
            }
            else
            {
                commonAPIResult.UpdateStatus("", MessageDict.Failed, "操作失败！请稍后再试！");
            }
            return commonAPIResult;
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="saveUserInfoReq"></param>
        /// <returns></returns>
        public CommonAPIResult<string> DeleteUser(RoleReq roleReq)
        {
            CommonAPIResult<string> commonAPIResult = new CommonAPIResult<string>();
            var role = _sysUserRoleRepository.GetUserInfo(roleReq.roleId.Value); //查询角色信息
            if (role == null)
            {
                commonAPIResult.UpdateStatus("", MessageDict.Failed, "查询不到角色信息！请联系管理员！");
                return commonAPIResult;
            }
            role.isDelete = 1;
            role.updateTime = DateTime.Now;
            role.updateUserId = roleReq.id;
            role.updateUserName = roleReq.name;
            var deleteRole = _sysUserRoleRepository.SaveRoleInfo(role);//删除角色
            if (deleteRole > 0)
            {
                commonAPIResult.UpdateStatus("", MessageDict.Ok, "删除成功");
            }
            else
            {
                commonAPIResult.UpdateStatus("", MessageDict.Ok, "删除失败");
            }
            return commonAPIResult;
        }

        /// <summary>
        /// 修改用户状态
        /// </summary>
        /// <param name="changUserVaildReq"></param>
        /// <returns></returns>
        public CommonAPIResult<string> ChangUserVaild(ChangUserVaildReq changUserVaildReq)
        {
            CommonAPIResult<string> commonAPIResult = new CommonAPIResult<string>();
            var deleteUser = _sysUserRoleRepository.ChangUserVaild(changUserVaildReq); //修改状态
            if (deleteUser > 0)
            {
                commonAPIResult.UpdateStatus("", MessageDict.Ok, "修改成功");
            }
            else
            {
                commonAPIResult.UpdateStatus("", MessageDict.Ok, "修改失败");
            }
            return commonAPIResult;
        }
    }
}
