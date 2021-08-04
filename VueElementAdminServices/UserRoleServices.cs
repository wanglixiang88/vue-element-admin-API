using IVueElememtAdminRepository;
using IVueElementAdminServices;
using VueElementAdminModel.APIModel;
using VueElementAdminModel.BaseModel;
using ToolLibrary.Helper.Helper;
using System;
using VueElementAdminModel.MySqlModel;
using System.Collections.Generic;
using System.Linq;
using VueElemenntAdminModel.APIModel;
using ToolLibrary.Helper;

namespace VueElementAdminServices
{
    public class UserRoleServices : IUserRoleServices
    {

        private readonly ISysUserRoleRepository _sysUserRoleRepository;
        private readonly ISysMenuRepository _sysMenuRepository;
        private readonly ISysPermissionRoleRepository _sysPermissionRoleRepository;

        /// <summary>
        /// 构造函数
        /// </summary>
        public UserRoleServices(ISysUserRoleRepository sysUserRepository, ISysMenuRepository sysMenuRepository, ISysPermissionRoleRepository sysPermissionRoleRepository)
        {
            _sysUserRoleRepository = sysUserRepository;
            _sysMenuRepository = sysMenuRepository;
            _sysPermissionRoleRepository = sysPermissionRoleRepository;
        }

        #region 角色信息

        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <param name="parameterJson"></param>
        /// <param name="tableParame"></param>
        /// <returns></returns>
        public List<sys_role> GetRoleList(ref TableParame tableParame)
        {
            return _sysUserRoleRepository.GetRoleList(ref tableParame).ToList();
        }

        /// <summary>
        /// 保存角色信息
        /// </summary>
        /// <param name="saveUserInfoReq"></param>
        /// <returns></returns>
        public CommonAPIResult<string> SaveRoleInfo(SaveRoleReq saveRoleReq)
        {
            CommonAPIResult<string> commonAPIResult = new CommonAPIResult<string>();
            var role = new sys_role();
            if (saveRoleReq.roleId.HasValue)
            {
                role = _sysUserRoleRepository.GetRoleInfo(saveRoleReq.roleId.Value); //查询角色信息
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
        /// 删除角色
        /// </summary>
        /// <param name="saveUserInfoReq"></param>
        /// <returns></returns>
        public CommonAPIResult<string> DeleteRole(RoleReq roleReq)
        {
            CommonAPIResult<string> commonAPIResult = new CommonAPIResult<string>();
            var role = _sysUserRoleRepository.GetRoleInfo(roleReq.roleId.Value); //查询角色信息
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

        #endregion

        #region 角色菜单权限设置

        /// <summary>
        /// 获取角色菜单权限的列表
        /// </summary>
        /// <param name="roleReq"></param>
        /// <returns></returns>
        public List<GetPermissionRoleRes> GetRoleMenuList(RoleReq roleReq)
        {
            var roleMenuList = new List<GetPermissionRoleRes>();
            
            var menuList = _sysMenuRepository.GetMenuList().ToList();//获取菜单列表
            var roleList = _sysPermissionRoleRepository.GetPermissionRoleList(roleReq.roleId).ToList(); //角色列表

            foreach (var item in menuList)
            {
                if (!item.parentId.HasValue)
                {
                    var treeVo = AutoMapper.To<sys_menu, GetPermissionRoleRes>(item);
                    treeVo.arryList = JsonHelper.DeserializeList<ArryDictionary>(item.operation);
                    foreach (var arry in treeVo.arryList)
                    {
                        arry.arryChecked = roleList.Where(t => t.menuId.Equals(item.menuId) && t.operation.Equals(arry.arryValue)).FirstOrDefault() != null;
                    }
                    treeVo.children = GetTreeVos(menuList, roleList, item.menuId);
                    roleMenuList.Add(treeVo);
                }
            }

            return roleMenuList;
        }

        /// <summary>
        /// 无限递归
        /// </summary>
        /// <param name="treeVos"></param>
        /// <returns></returns>
        public List<GetPermissionRoleRes> GetTreeVos(List<sys_menu> GetModelList, List<sys_permissionrole> roleList, long parentId)
        {
            List<GetPermissionRoleRes> treeVos = new List<GetPermissionRoleRes>();
            foreach (var item in GetModelList)
            {
                var treeVo = AutoMapper.To<sys_menu, GetPermissionRoleRes>(item);
                if (item.parentId.Equals(parentId))
                {
                    treeVo.arryList = JsonHelper.DeserializeList<ArryDictionary>(item.operation);
                    foreach (var arry in treeVo.arryList)
                    {
                        arry.arryChecked = roleList.Where(t => t.menuId.Equals(item.menuId) && t.operation.Equals(arry.arryValue)).FirstOrDefault() != null;
                    }
                    treeVo.children = GetTreeVos(GetModelList, roleList, item.menuId);
                    treeVos.Add(treeVo);
                }
            }
            return treeVos;
        }

        /// <summary>
        /// 保存角色的权限信息
        /// </summary>
        /// <param name="savePermissionReq"></param>
        /// <param name="id">用户ID</param>
        /// <param name="name">用户名</param>
        /// <returns></returns>
        public CommonAPIResult<string> SavePermissionRole(SavePermissionRoleReq savePermissionReq)
        {
            CommonAPIResult<string> commonAPIResult = new CommonAPIResult<string>();

            if (savePermissionReq.list == null)
            {
                commonAPIResult.UpdateStatus("参数缺失", MessageDict.PramError, "保存失败，参数缺失");
                return commonAPIResult;
            }

            var str = _sysPermissionRoleRepository.DeleteAndSavePermissionRole(savePermissionReq);
            if (str == "1")
            {
                commonAPIResult.UpdateStatus("保存成功", MessageDict.Ok, "保存成功");
                return commonAPIResult;
            }
            commonAPIResult.UpdateStatus("保存失败", MessageDict.Failed, "保存失败，原因：" + str);
            return commonAPIResult;
        }

        #endregion
    }
}
