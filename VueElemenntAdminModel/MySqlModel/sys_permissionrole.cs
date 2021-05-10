using SqlSugar;
using System;

namespace VueElementAdminModel.MySqlModel
{
    public class sys_permissionrole
    {
        /// <summary>
        /// 角色权限配置表主键
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public long pId { get; set; }

        /// <summary>
        /// 角色ID
        /// </summary>
        public long roleId { get; set; }

        /// <summary>
        /// 菜单ID
        /// </summary>
        public long menuId { get; set; }

        /// <summary>
        /// 操作项 {"insert":"1","delete":"1","update":"1","select":"1","export":"1"}对应增改删查导出
        /// </summary>
        public string operation { get; set; }

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
