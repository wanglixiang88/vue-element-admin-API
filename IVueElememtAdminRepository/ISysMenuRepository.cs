using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VueElemenntAdminModel.APIModel;
using vueElementAdminModel.MySqlModel;

namespace IVueElememtAdminRepository
{
    public interface ISysMenuRepository
    {
        /// <summary>
        /// 获取全部的菜单
        /// </summary>
        /// <param name="tableParame"></param>
        /// <returns></returns>
        IEnumerable<sys_menu> GetMenuList(ref TableParame tableParame);

        /// <summary>
        /// 根据menuId查询菜单信息
        /// </summary>
        /// <param name="menuId">菜单ID</param>
        /// <returns></returns>
        sys_menu GetMenuById(long menuId);

        /// <summary>
        /// 保存菜单记录
        /// </summary>
        /// <param name="sys_menu"></param>
        /// <returns></returns>
        int SaveSysMenuInfo(sys_menu sys_menu);

        /// <summary>
        /// 更新菜单记录
        /// </summary>
        /// <param name="sys_menu"></param>
        /// <returns></returns>
        int UpdateSysMenuInfo(sys_menu sys_menu);

        /// <summary>
        /// 软删除菜单
        /// </summary>
        /// <param name="deleteMenuReq"></param>
        /// <returns></returns>
        int DeleteSysMenu(DeleteMenuReq deleteMenuReq);

        /// <summary>
        /// 根据ID查询子类信息
        /// </summary>
        /// <param name="parentId">父类ID</param>
        /// <returns></returns>
        List<sys_menu> GetMenuByParent(long parentId);
    }
}
