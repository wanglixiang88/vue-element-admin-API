﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VueElementAdminModel.APIModel;
using VueElementAdminModel.BaseModel;
using VueElementAdminModel.MySqlModel;

namespace IVueElementAdminServices
{
    public interface IMenuServices
    {
        /// <summary>
        /// 获取全部的菜单
        /// </summary>
        /// <param name="tableParame"></param>
        /// <returns></returns>
        List<menuList> GetMenuList(ref TableParame tableParame);

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
