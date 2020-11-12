using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace VueElemenntAdminModel.BaseModel
{
    /// <summary>
    /// 响应
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CommonAPIResult<T>
    {
        /// <summary>
        /// 错误代码  
        /// 1000--正常  1001--调用失败  1002--数据不存在（包括登录用户不存在） 1003-token不存在  1004--token已失效  1005--数据重复提交  1011--用户金币不足
        /// 2001--参数错误
        /// 3001--接口访问时间限制
        /// 4000--redis操作失败
        /// 9999--未知异常
        /// </summary>
        public int code { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string message { get; set; }

        /// <summary>
        /// 返回值实体
        /// </summary>
        public T data { get { return Result; } set { if (value != null) { Result = CopyTo(value); } } }

        private T Result;

        /// <summary>
        /// 接口统一返回消息
        /// </summary>
        /// <param name="result"></param>
        /// <param name="messageDict"></param>
        /// <param name="errMsg"></param>
        public void UpdateStatus(T result, MessageDict messageDict, string message)
        {
            this.data = result;
            code = (int)messageDict;
            this.message = message;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="target"></param>
        /// <returns></returns>
        public static T CopyTo<T>(T target)
        {

            foreach (var property in target.GetType().GetProperties())
            {
                var name = target.GetType().GetProperty(property.Name).PropertyType.Name;
                //如果是string类型的则进行null转空字符“”操作
                if (name == "String")
                {
                    var propertyValue = target.GetType().GetProperty(property.Name).GetValue(target, null);
                    if (propertyValue == null)
                    {

                        target.GetType().InvokeMember(property.Name, BindingFlags.SetProperty, null, target, new object[] { "" });
                    }
                }
            }
            return target;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class OrderResult<T> : CommonAPIResult<T>
    {
        /// <summary>
        /// 操作完成后订单状态
        /// </summary>
        public short afterStatus { get; set; }
        /// <summary>
        /// 订单状态(中文显示)
        /// </summary>
        public string ordestatus { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public static class Common
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="target"></param>
        public static void CopyTo<T>(this object source, T target)
            where T : class, new()
        {
            if (source == null)
                return;

            if (target == null)
            {
                target = new T();
            }

            foreach (var property in target.GetType().GetProperties())
            {
                if (property.Name == "System.String")
                {
                    var propertyValue = source.GetType().GetProperty(property.Name).GetValue(source, null);
                    if (propertyValue == null)
                    {

                        target.GetType().InvokeMember(property.Name, BindingFlags.SetProperty, null, "", new object[] { propertyValue });
                    }
                }
            }

            foreach (var field in target.GetType().GetFields())
            {
                var fieldValue = source.GetType().GetField(field.Name).GetValue(source);
                if (fieldValue != null)
                {
                    target.GetType().InvokeMember(field.Name, BindingFlags.SetField, null, target, new object[] { fieldValue });
                }
            }
        }
    }
}
