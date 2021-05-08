using IVueElememtAdminRepository;
using IVueElementAdminServices;
using System;
using System.Collections.Generic;
using System.Linq;
using ToolLibrary.Helper;
using ToolLibrary.Helper.Helper;
using VueElemenntAdminModel.APIModel;
using VueElemenntAdminModel.BaseModel;
using VueElemenntAdminModel.MySqlModel;
using vueElementAdminModel.MySqlModel;

namespace VueElementAdminServices
{
    public class SysDictionaryServices : ISysDictionaryServices
    {

        private readonly ISysDictionaryRepository _sysDictionaryRepository;

        /// <summary>
        /// 构造函数
        /// </summary>
        public SysDictionaryServices(ISysDictionaryRepository sysDictionaryRepository)
        {
            _sysDictionaryRepository = sysDictionaryRepository;
        }

        List<OperationItems> operationList = new List<OperationItems>() {
            new OperationItems(){name="增加",value="insert"},
            new OperationItems(){name="删除",value="delete"},
            new OperationItems(){name="查询",value="select"},
            new OperationItems(){name="修改",value="update"},
            new OperationItems(){name="导出",value="export"},
        };


        /// <summary>
        /// 获取全部的菜单
        /// </summary>
        /// <param name="tableParame"></param>
        /// <returns></returns>
        public List<dictionaryList> GetMenuList(ref TableParame tableParame)
        {
            var menuList = new List<dictionaryList>();
            var data = _sysDictionaryRepository.GetMenuList(ref tableParame).ToList();
            foreach (var item in data)
            {
                if (!item.parentId.HasValue)
                {
                    var operationList = JsonHelper.DeserializeList<OperationItems>(item.operation);
                    var treeVo = AutoMapper.To<sys_menu, dictionaryList>(item);
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
        public List<dictionaryList> GetTreeVos(List<sys_menu> GetModelList, long parentId)
        {
            List<dictionaryList> treeVos = new List<dictionaryList>();
            foreach (var item in GetModelList)
            {
                var treeVo = AutoMapper.To<sys_menu, dictionaryList>(item);
                if (item.parentId.Equals(parentId))
                {
                    var operationList = JsonHelper.DeserializeList<OperationItems>(item.operation);
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

                var valueList = JsonHelper.DeserializeList<string>(saveMenuReq.operation);
                var newList = operationList.Where(t => valueList.Contains(t.value)).ToList();
                saveMenuReq.operation = JsonHelper.Serialize(newList);

                if (saveMenuReq.menuId.HasValue)
                {
                    var menuInfo = _sysDictionaryRepository.GetMenuById(saveMenuReq.menuId.Value);
                    if (menuInfo == null)
                    {
                        commonAPIResult.UpdateStatus("", MessageDict.NoDataExists, "查询不到角色信息！请联系管理员！");
                        return commonAPIResult;
                    }
                    var needUpdate = AutoMapper.To<SaveMenuReq, sys_menu>(saveMenuReq);
                    needUpdate.updateTime = DateTime.Now;
                    needUpdate.updateUserId = saveMenuReq.id;
                    needUpdate.updateUserName = saveMenuReq.name;
                    i = _sysDictionaryRepository.UpdateSysMenuInfo(needUpdate);//更新数据到数据库
                }
                else
                {
                    var needAdd = AutoMapper.To<SaveMenuReq, sys_menu>(saveMenuReq);
                    needAdd.createTime = DateTime.Now;
                    needAdd.createUserId = saveMenuReq.id;
                    needAdd.createUserName = saveMenuReq.name;
                    i = _sysDictionaryRepository.SaveSysMenuInfo(needAdd);//保存到数据库
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

                var parentList = _sysDictionaryRepository.GetMenuByParent(deleteMenuReq.menuId.Value); //查询当前菜单的子类
                if (parentList.Count > 0)
                {
                    commonAPIResult.UpdateStatus("", MessageDict.Failed, "删除失败！请先删除当前菜单下的菜单！");
                    return commonAPIResult;
                }

                var i = _sysDictionaryRepository.DeleteSysMenu(deleteMenuReq); //软删除菜单
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
