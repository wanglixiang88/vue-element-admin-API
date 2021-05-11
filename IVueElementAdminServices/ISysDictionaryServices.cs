using System.Collections.Generic;
using VueElementAdminModel.APIModel;
using VueElementAdminModel.BaseModel;

namespace IVueElementAdminServices
{
    public interface ISysDictionaryServices
    {
        /// <summary>
        /// 获取全部的菜单
        /// </summary>
        /// <param name="tableParame"></param>
        /// <returns></returns>
        List<dictionaryList> GetDictionary(ref TableParame tableParame);

        /// <summary>
        /// 保存数据字典
        /// </summary>
        /// <param name="saveDictionaryReq"></param>
        /// <returns></returns>
        CommonAPIResult<string> SaveDictionaryInfo(SaveDictionaryReq saveDictionaryReq);

        /// <summary>
        /// 删除菜单信息
        /// </summary>
        /// <param name="deleteMenuReq"></param>
        /// <returns></returns>
        CommonAPIResult<string> DeleteDictionary(DeleteDictionaryReq deleteMenuReq);
    }
}
