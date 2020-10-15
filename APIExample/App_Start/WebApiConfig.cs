using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web.Http;

namespace APIExample
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API 配置和服务

            // Web API 路由
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            //设置返回json
            config.Formatters.Clear();
            config.Formatters.Add(new JsonMediaTypeFormatter());
        }



        /// <summary>
        /// 
        /// </summary>
        public class JsonContentNegotiator : IContentNegotiator
        {
            private readonly JsonMediaTypeFormatter _jsonFormatter;

            /// <summary>
            /// 
            /// </summary>
            /// <param name="formatter"></param>
            public JsonContentNegotiator(JsonMediaTypeFormatter formatter)
            {
                _jsonFormatter = formatter;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="type"></param>
            /// <param name="request"></param>
            /// <param name="formatters"></param>
            /// <returns></returns>
            public ContentNegotiationResult Negotiate(Type type, HttpRequestMessage request, IEnumerable<MediaTypeFormatter> formatters)
            {
                var result = new ContentNegotiationResult(_jsonFormatter, new MediaTypeHeaderValue("application/json"));
                return result;
            }
        }
    }
}
