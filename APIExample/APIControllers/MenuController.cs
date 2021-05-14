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
    /// Menu 菜单
    /// </summary>
    [AuthFilter]
    [RoutePrefix("API/Menu")]
    public class MenuController : ApiController
    {
        private readonly static ISysMenuRepository _sysMenuRepository = new SysMenuRepository();
        private readonly static ISysDictionaryRepository _sysDictionaryRepository = new SysDictionaryRepository();
        private readonly IMenuServices _menuServices = new MenuServices(_sysMenuRepository, _sysDictionaryRepository);

        /// <summary>
        /// 获取菜单列表
        /// </summary>
        /// <returns></returns>
        [Route("GetAllMenu")]
        [HttpPost]
        public CommonAPIResult<BaseTable<List<menuList>>> GetAllMenu([FromBody]TableParame tableParame)
        {
            CommonAPIResult<BaseTable<List<menuList>>> commonAPIResult = new CommonAPIResult<BaseTable<List<menuList>>>();

            var data = _menuServices.GetMenuList(ref tableParame);
            BaseTable<List<menuList>> baseTable = new BaseTable<List<menuList>>();
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
            return _menuServices.SaveMenuInfo(saveMenuReq);
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
            return _menuServices.DeleteMenu(deleteMenuReq);
        }
    }
}
