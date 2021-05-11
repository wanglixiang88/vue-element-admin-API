using System.Collections.Generic;
using VueElementAdminModel.APIModel;
using VueElementAdminModel.MySqlModel;

namespace IVueElememtAdminRepository
{
    public interface ISysDictionaryRepository
    {
        /// <summary>
        /// 获取全部的数据字典
        /// </summary>
        /// <param name="tableParame"></param>
        /// <returns></returns>
        IEnumerable<sys_dictionary> GetDictionaryList(ref TableParame tableParame);

        /// <summary>
        /// 根据arryId查询数据字典
        /// </summary>
        /// <param name="arryId">数据字典ID</param>
        /// <returns></returns>
        sys_dictionary GetDictionaryById(long arryId);

        /// <summary>
        /// 保存数据字典
        /// </summary>
        /// <param name="sys_menu"></param>
        /// <returns></returns>
        int SaveDictionaryInfo(sys_dictionary sys_Dictionary);

        /// <summary>
        /// 更新数据字典
        /// </summary>
        /// <param name="sys_menu"></param>
        /// <returns></returns>
        int UpdateDictionaryInfo(sys_dictionary sys_Dictionary);

        /// <summary>
        /// 软删除菜单
        /// </summary>
        /// <param name="deleteDictionaryReq"></param>
        /// <returns></returns>
        int DeleteDictionary(DeleteDictionaryReq deleteDictionaryReq);

        /// <summary>
        /// 根据ID查询子类信息
        /// </summary>
        /// <param name="parentId">父类ID</param>
        /// <returns></returns>
        List<sys_dictionary> GetDictionaryByParent(long parentId);
    }
}
