using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VueElemenntAdminModel.BaseModel
{
    /// <summary>
    /// ErrorCode
    /// </summary>
    public enum MessageDict
    {
        /***************************基础类型：约定1000开头*****************************/
        /// <summary>
        /// 调用接口成功
        /// </summary>
        Ok = 1000,

        /// <summary>
        /// 调用失败
        /// </summary>
        Failed = 1001,

        /// <summary>
        /// 数据不存在（包括登录用户不存在）
        /// </summary>
        NoDataExists = 1002,

        /// <summary>
        /// token不存在
        /// </summary>
        TokenNon = 1003,

        /// <summary>
        /// token已失效
        /// </summary>
        TokenInvalid = 1004,

        /// <summary>
        /// 数据重复提交
        /// </summary>
        SubmitDuplicate = 1005,

        /// <summary>
        /// 数据过期
        /// </summary>
        DataExpire = 1006,

        /// <summary>
        /// 用户金币不足
        /// </summary>
        CreditsCoins = 1011,

        /***************************参数类型：约定2000开头*****************************/
        /// <summary>
        /// 参数错误
        /// </summary>
        PramError = 2001,

        /***************************认证异常：约定3000开头*****************************/
        /// <summary>
        /// 接口访问时间限制
        /// </summary>
        TimeStampExpired = 3001,

        /***************************业务异常：约定4000开头*****************************/
        /// <summary>
        /// redis操作失败
        /// </summary>
        RedisError = 4000,

        /// <summary>
        /// 未知异常
        /// </summary>
        SystemUnkonw = 9999
    }
}
