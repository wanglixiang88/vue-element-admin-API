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
        /// 保存数据字典
        /// </summary>
        /// <param name="saveDictionaryReq"></param>
        /// <returns></returns>
        [Route("SaveDictionary")]
        [HttpPost]
        public CommonAPIResult<string> SaveDictionary([FromBody] SaveDictionaryReq saveDictionaryReq)
        {
            saveDictionaryReq.name = Request.Properties["userName"].ToString();
            saveDictionaryReq.id = Request.Properties["userId"].ToString();
            return _DictionaryServices.SaveDictionaryInfo(saveDictionaryReq);
        }

        /// <summary>
        /// 软删除数据字典
        /// </summary>
        /// <param name="deleteDictionaryReq">请求的参数</param>
        /// <returns></returns>
        [Route("DeleteDictionary")]
        [HttpPost]
        public CommonAPIResult<string> DeleteMenu([FromBody] DeleteDictionaryReq deleteDictionaryReq)
        {
            deleteDictionaryReq.name = Request.Properties["userName"].ToString();
            deleteDictionaryReq.id = Request.Properties["userId"].ToString();
            return _DictionaryServices.DeleteDictionary(deleteDictionaryReq);
        }
    }
}
