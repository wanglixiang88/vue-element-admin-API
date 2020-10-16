using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vueElementAdmin.Model;

namespace IVueElememtAdminRepository
{
    public interface ISysUserRepository
    {
        sys_user GetUserInfoByToken(string token);
    }
}
