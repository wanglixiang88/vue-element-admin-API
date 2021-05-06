using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VueElemenntAdminModel.MySqlModel
{
    public class sys_dictionary
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public string value { get; set; }

        /// <summary>
        /// 是否删除 0.未删除 1.已删除
        /// </summary>
        public int isDelete { get; set; }

        /// <summary>
        /// 创建人ID
        /// </summary>
        public string createUserId { get; set; }

        /// <summary>
        /// 创建人姓名
        /// </summary>
        public string createUserName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime createTime { get; set; }

        /// <summary>
        /// 更新人ID
        /// </summary>
        public string updateUserId { get; set; }

        /// <summary>
        /// 更新人名称
        /// </summary>
        public string updateUserName { get; set; }

        /// <summary>
        ///更新时间
        /// </summary>
        public DateTime? updateTime { get; set; }
    }
}
