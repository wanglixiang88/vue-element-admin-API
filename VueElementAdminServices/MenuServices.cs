using IVueElememtAdminRepository;
using IVueElementAdminServices;
using System;
using System.Collections.Generic;
using System.Linq;
using ToolLibrary.Helper.Helper;
using VueElemenntAdminModel.APIModel;
using VueElemenntAdminModel.BaseModel;
using vueElementAdminModel.MySqlModel;

namespace VueElementAdminServices
{
    public class MenuServices: IMenuServices
    {

        private readonly ISysMenuRepository _sysMenuRepository;

        /// <summary>
        /// 构造函数
        /// </summary>
        public MenuServices(ISysMenuRepository sysMenuRepository)
        {
            _sysMenuRepository = sysMenuRepository;
        }

        /// <summary>
        /// 获取全部的菜单
        /// </summary>
        /// <param name="tableParame"></param>
        /// <returns></returns>
        public List<menuList> GetMenuList(ref TableParame tableParame)
        {
            var menuList = new List<menuList>();
            var data = _sysMenuRepository.GetMenuList(ref tableParame).ToList();
            foreach(var item in data)
            {
                if (!item.parentId.HasValue)
                {
                    var treeVo = AutoMapper.To<sys_menu, menuList>(item);
                    treeVo.children = GetTreeVos(data, item.menuId);
                    menuList.Add(treeVo);
                }
            }
            return menuList;
        }

        /// <summary>
        /// 无限递归
        /// </summary>
        /// <param name="treeVos"></param>
        /// <returns></returns>
        public List<menuList> GetTreeVos(List<sys_menu> GetModelList, long parentId)
        {
            List<menuList> treeVos = new List<menuList>();
            foreach (var item in GetModelList)
            {
                var treeVo = AutoMapper.To<sys_menu, menuList>(item);
                if (item.parentId.Equals(parentId))
                {
                    treeVo.children = GetTreeVos(GetModelList, item.menuId);
                    treeVos.Add(treeVo);
                }
            }
            return treeVos;
        }

        /// <summary>
        /// 保存菜单信息
        /// </summary>
        /// <param name="saveMenuReq"></param>
        /// <returns></returns>
        public CommonAPIResult<string> SaveMenuInfo(SaveMenuReq saveMenuReq)
        {
            CommonAPIResult<string> commonAPIResult = new CommonAPIResult<string>();
            try
            {
                var i = 0;
                if (string.IsNullOrEmpty(saveMenuReq.menuName))
                {
                    commonAPIResult.UpdateStatus("", MessageDict.Failed, "请输入菜单名称");
                    return commonAPIResult;
                }

                if (saveMenuReq.menuId.HasValue)
                {
                    var menuInfo = _sysMenuRepository.GetMenuById(saveMenuReq.menuId.Value);
                    if (menuInfo == null)
                    {
                        commonAPIResult.UpdateStatus("", MessageDict.NoDataExists, "查询不到角色信息！请联系管理员！");
                        return commonAPIResult;
                    }
                    var needUpdate = AutoMapper.To<SaveMenuReq, sys_menu>(saveMenuReq);
                    needUpdate.updateTime = DateTime.Now;
                    needUpdate.updateUserId = saveMenuReq.id;
                    needUpdate.updateUserName = saveMenuReq.name;
                    i = _sysMenuRepository.UpdateSysMenuInfo(needUpdate);//更新数据到数据库
                }
                else
                {
                    var needAdd = AutoMapper.To<SaveMenuReq, sys_menu>(saveMenuReq);
                    needAdd.createTime = DateTime.Now;
                    needAdd.createUserId = saveMenuReq.id;
                    needAdd.createUserName = saveMenuReq.name;
                    i = _sysMenuRepository.SaveSysMenuInfo(needAdd);//保存到数据库
                }

                if (i > 0)
                {
                    commonAPIResult.UpdateStatus("", MessageDict.Ok, "操作成功！");
                    return commonAPIResult;
                }
                commonAPIResult.UpdateStatus("", MessageDict.Failed, "操作失败，请稍后再试！");
                return commonAPIResult;
            }
            catch(Exception ex)
            {
                commonAPIResult.UpdateStatus("", MessageDict.Failed, "操作失败！失败原因：" + ex.Message);
                return commonAPIResult;
            }
        }

        /// <summary>
        /// 删除菜单信息
        /// </summary>
        /// <param name="deleteMenuReq"></param>
        /// <returns></returns>
        public CommonAPIResult<string> DeleteMenu(DeleteMenuReq deleteMenuReq)
        {
            CommonAPIResult<string> commonAPIResult = new CommonAPIResult<string>();
            try
            {
                if (!deleteMenuReq.menuId.HasValue)
                {
                    commonAPIResult.UpdateStatus("", MessageDict.Ok, "菜单ID不能为空！");
                    return commonAPIResult;
                }

                var parentList = _sysMenuRepository.GetMenuByParent(deleteMenuReq.menuId.Value); //查询当前菜单的子类
                if (parentList.Count > 0)
                {
                    commonAPIResult.UpdateStatus("", MessageDict.Failed, "删除失败！请先删除当前菜单下的菜单！");
                    return commonAPIResult;
                }

                var i = _sysMenuRepository.DeleteSysMenu(deleteMenuReq); //软删除菜单
                if (i > 0)
                {
                    commonAPIResult.UpdateStatus("", MessageDict.Ok, "删除成功！");
                    return commonAPIResult;
                }

                commonAPIResult.UpdateStatus("", MessageDict.Failed, "删除失败，请稍后再试！");
                return commonAPIResult;
            }
            catch (Exception ex)
            {
                commonAPIResult.UpdateStatus("", MessageDict.Failed, "系统出现异常！异常信息：" + ex.Message);
                return commonAPIResult;
            }
        }
    }
}
