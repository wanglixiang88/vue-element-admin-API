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
}
