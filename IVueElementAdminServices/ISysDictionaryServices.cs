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
        /// 保存菜单信息
        /// </summary>
        /// <param name="saveMenuReq"></param>
        /// <returns></returns>
        CommonAPIResult<string> SaveMenuInfo(SaveMenuReq saveMenuReq);

        /// <summary>
        /// 删除菜单信息
        /// </summary>
        /// <param name="deleteMenuReq"></param>
        /// <returns></returns>
        CommonAPIResult<string> DeleteMenu(DeleteMenuReq deleteMenuReq);
    }
}
