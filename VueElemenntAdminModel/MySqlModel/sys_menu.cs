using SqlSugar;
using System;

namespace vueElementAdminModel.MySqlModel
{
    public class sys_menu
    {
        /// <summary>
        /// 菜单ID guid 唯一
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public long menuId { get; set; }

        /// <summary>
        /// 菜单名称
        /// </summary>
        public string menuName { get; set; }

        /// <summary>
        /// 菜单父类ID
        /// </summary>
        public long? parentId { get; set; }

        /// <summary>
        /// 排序字段，越小的排在越前面
        /// </summary>
        public int sequence { get; set; }

        /// <summary>
        /// 菜单路径
        /// </summary>
        public string route { get; set; }

        /// <summary>
        /// 图标样式
        /// </summary>
        public string iconClass { get; set; }

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
