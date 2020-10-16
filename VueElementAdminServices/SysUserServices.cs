
using IVueElementAdminRepository;
using IVueElementAdminServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace VueElementAdminServices
{
    public class SysUserServices : ISysUserServices
    {

        ISysUserRepository _sysUserRepository;
        public SysUserServices(ISysUserRepository sysUserRepository)
        {
            _sysUserRepository = sysUserRepository;
        }
    }
}
