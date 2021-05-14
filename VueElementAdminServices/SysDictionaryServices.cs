using IVueElememtAdminRepository;
using IVueElementAdminServices;
using System;
using System.Collections.Generic;
using System.Linq;
using ToolLibrary.Helper;
using ToolLibrary.Helper.Helper;
using VueElementAdminModel.APIModel;
using VueElementAdminModel.BaseModel;
using VueElementAdminModel.MySqlModel;

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

        /// <summary>
        /// 获取全部的数据字典
        /// </summary>
        /// <param name="tableParame"></param>
        /// <returns></returns>
        public List<dictionaryList> GetDictionary(ref TableParame tableParame)
        {
            var menuList = new List<dictionaryList>();
            var data = _sysDictionaryRepository.GetDictionaryList(ref tableParame).ToList();
            foreach (var item in data)
            {
                if (!item.parentId.HasValue)
                {
                    var treeVo = AutoMapper.To<sys_dictionary, dictionaryList>(item);
                    treeVo.children = GetTreeVos(data, item.arryId);
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
        public List<dictionaryList> GetTreeVos(List<sys_dictionary> GetModelList, long parentId)
        {
            List<dictionaryList> treeVos = new List<dictionaryList>();
            foreach (var item in GetModelList)
            {
                var treeVo = AutoMapper.To<sys_dictionary, dictionaryList>(item);
                if (item.parentId.Equals(parentId))
                {
                    treeVo.children = GetTreeVos(GetModelList, item.arryId);
                    treeVos.Add(treeVo);
                }
            }
            return treeVos;
        }

        /// <summary>
        /// 保存数据字典
        /// </summary>
        /// <param name="saveDictionaryReq"></param>
        /// <returns></returns>
        public CommonAPIResult<string> SaveDictionaryInfo(SaveDictionaryReq saveDictionaryReq)
        {
            CommonAPIResult<string> commonAPIResult = new CommonAPIResult<string>();
            try
            {
                var i = 0;
                if (string.IsNullOrEmpty(saveDictionaryReq.arryName))
                {
                    commonAPIResult.UpdateStatus("", MessageDict.Failed, "请输入名称");
                    return commonAPIResult;
                }

                if (saveDictionaryReq.arryId.HasValue)
                {
                    var menuInfo = _sysDictionaryRepository.GetDictionaryById(saveDictionaryReq.arryId.Value);
                    if (menuInfo == null)
                    {
                        commonAPIResult.UpdateStatus("", MessageDict.NoDataExists, "查询不到数据字典信息！请稍后再试或联系管理员处理！");
                        return commonAPIResult;
                    }
                    var needUpdate = AutoMapper.To<SaveDictionaryReq, sys_dictionary>(saveDictionaryReq);
                    needUpdate.updateTime = DateTime.Now;
                    needUpdate.updateUserId = saveDictionaryReq.id;
                    needUpdate.updateUserName = saveDictionaryReq.name;
                    i = _sysDictionaryRepository.UpdateDictionaryInfo(needUpdate);//更新数据到数据库
                }
                else
                {
                    var needAdd = AutoMapper.To<SaveDictionaryReq, sys_dictionary>(saveDictionaryReq);
                    needAdd.createTime = DateTime.Now;
                    needAdd.createUserId = saveDictionaryReq.id;
                    needAdd.createUserName = saveDictionaryReq.name;
                    i = _sysDictionaryRepository.SaveDictionaryInfo(needAdd);//保存到数据库
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
        public CommonAPIResult<string> DeleteDictionary(DeleteDictionaryReq deleteMenuReq)
        {
            CommonAPIResult<string> commonAPIResult = new CommonAPIResult<string>();
            try
            {
                if (!deleteMenuReq.arryId.HasValue)
                {
                    commonAPIResult.UpdateStatus("", MessageDict.Ok, "菜单ID不能为空！");
                    return commonAPIResult;
                }

                var parentList = _sysDictionaryRepository.GetDictionaryByParent(deleteMenuReq.arryId.Value); //查询当前菜单的子类
                if (parentList.Count > 0)
                {
                    commonAPIResult.UpdateStatus("", MessageDict.Failed, "删除失败！请先删除当前菜单下的菜单！");
                    return commonAPIResult;
                }

                var i = _sysDictionaryRepository.DeleteDictionary(deleteMenuReq); //软删除菜单
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
