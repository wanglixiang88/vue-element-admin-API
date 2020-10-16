using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vueElementAdminModel.MySqlModel;

namespace IVueElememtAdminRepository
{
    public interface ISysUserRepository
    {

        /// <summary>
        /// 根据Token获取用户信息
        /// </summary>
        /// <param name="token">token</param>
        /// <returns></returns>
        sys_user GetUserInfoByToken(string token);
    }
}
