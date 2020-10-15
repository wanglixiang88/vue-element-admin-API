using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIExample.Models
{
    /// <summary>
    /// 通用实体
    /// </summary>
    public class MobileBase
    {
        /// <summary>
        /// 通用Token
        /// </summary>
        public string token { get; set; }

        /// <summary>
        /// userid(不暴露在接口中)
        /// </summary>
        internal string userid { get; set; }

        /// <summary>
        /// 设置userid
        /// </summary>
        /// <param name="id"></param>
        public void SetUserid(string id)
        {
            userid = id;
        }
    }
}