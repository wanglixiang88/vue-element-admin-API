using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ToolLibrary.Helper.Helper
{
    public class AutoMapper
    {
        /// <summary>
        /// B对象相同属性赋值给A对象
        /// </summary>
        /// <typeparam name="T1">类型A</typeparam>
        /// <typeparam name="T2">类型B</typeparam>
        /// <param name="t">B对象</param>
        /// <returns>返回创建的新A对象</returns>
        public static T2 To<T1, T2>(T1 t)
        {
            T2 a = Activator.CreateInstance<T2>();
            try
            {
                Type Typeb = t.GetType();//获得类型  
                Type Typea = typeof(T2);

                foreach (PropertyInfo bp in Typeb.GetProperties())//获得类型的属性字段  
                {
                    foreach (PropertyInfo ap in Typea.GetProperties())
                    {
                        if (ap.Name == bp.Name)//判断属性名是否相同  
                        {
                            if (ap.GetSetMethod() != null)
                            {
                                if (bp.GetGetMethod() != null)
                                {
                                    ap.SetValue(a, bp.GetValue(t, null), null);//获得b对象属性的值复制给a对象的属性   }
                                }

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return a;
        }
    }
}
