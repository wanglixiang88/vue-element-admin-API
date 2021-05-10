using APIExample.Filter;
using IVueElememtAdminRepository;
using IVueElementAdminServices;
using System.Collections.Generic;
using System.Web.Http;
using VueElememtAdminRepository;
using VueElementAdminModel.APIModel;
using VueElementAdminModel.BaseModel;
using VueElementAdminServices;

namespace APIExample.APIControllers
{
    /// <summary>
    /// 字典列表
    /// </summary>
    [AuthFilter]
    [RoutePrefix("API/Dictionary")]
    public class DictionaryController : ApiController
    {
        private readonly static ISysDictionaryRepository _sysDictionaryRepository = new SysDictionaryRepository();
        private readonly ISysDictionaryServices _DictionaryServices = new SysDictionaryServices(_sysDictionaryRepository);

        /// <summary>
        /// 获取字典列表
        /// </summary>
        /// <returns></returns>
        [Route("GetDictionary")]
        [HttpPost]
        public CommonAPIResult<BaseTable<List<dictionaryList>>> GetDictionary([FromBody]TableParame tableParame)
        {
            CommonAPIResult<BaseTable<List<dictionaryList>>> commonAPIResult = new CommonAPIResult<BaseTable<List<dictionaryList>>>();

            var data = _DictionaryServices.GetDictionary(ref tableParame);
            BaseTable<List<dictionaryList>> baseTable = new BaseTable<List<dictionaryList>>();
            baseTable.item = data;
            baseTable.total = tableParame.recordsFiltered;

            commonAPIResult.UpdateStatus(baseTable, MessageDict.Ok, "获取成功");
            return commonAPIResult;
        }

        /// <summary>
        /// 保存菜单信息
        /// </summary>
        /// <param name="saveMenuReq"></param>
        /// <returns></returns>
        [Route("SaveMenu")]
        [HttpPost]
        public CommonAPIResult<string> SaveMenu([FromBody] SaveMenuReq saveMenuReq)
        {
            saveMenuReq.name = Request.Properties["userName"].ToString();
            saveMenuReq.id = Request.Properties["userId"].ToString();
            return _DictionaryServices.SaveMenuInfo(saveMenuReq);
        }

        /// <summary>
        /// 软删除菜单
        /// </summary>
        /// <param name="deleteMenuReq">请求的参数</param>
        /// <returns></returns>
        [Route("DeleteMenu")]
        [HttpPost]
        public CommonAPIResult<string> DeleteMenu([FromBody] DeleteMenuReq deleteMenuReq)
        {
            deleteMenuReq.name = Request.Properties["userName"].ToString();
            deleteMenuReq.id = Request.Properties["userId"].ToString();
            return _DictionaryServices.DeleteMenu(deleteMenuReq);
        }
    }
}
