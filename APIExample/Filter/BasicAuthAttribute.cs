using IVueElememtAdminRepository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using ToolLibrary.Helper;
using VueElememtAdminRepository;
using VueElemenntAdminModel.BaseModel;

namespace APIExample.Filter
{
    /// <summary>
    /// 接口统一入口
    /// </summary>
    public class BasicAuthAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="actionContext"></param>
        public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            var data = actionContext.ActionArguments.FirstOrDefault().Value;
            if (data == null)
            {
                HttpContext.Current.Response.Redirect("~//API/Tool/Error");
            }
            var actionName = actionContext.ActionDescriptor.ActionName;
            base.OnActionExecuting(actionContext);
            //获取请求消息中的token
            var QueryString = HttpContext.Current.Request.QueryString;
            string tokenname = "";
            foreach (var item in QueryString)
            {
                if (item.ToString().Contains("token"))
                {
                    tokenname = item.ToString();
                }
            }
            string token = QueryString[tokenname];
            //非登录验证码场景调用借口都需要验证Token
            if (actionName == "Login" || actionName == "SendVerificationCode")
            {

            }
            else
            {
                if (!CheckToken(token))
                {
                    //throw new xlException() { errCode = 400, errMessage = "请求参数错误" };
                    HttpContext.Current.Response.Redirect("~/API/Tool/Error");
                    HttpRequestMessage request = new HttpRequestMessage();
                    request.RequestUri = new Uri("https://www.baidu.com/");
                    HttpResponseMessage response = new HttpResponseMessage();
                    response.RequestMessage = request;
                    actionContext.Response = response;
                    actionContext.Request.Properties["userid"] = "";
                }
                else
                {
                    actionContext.Request.Properties["userid"] = "134";

                }
            }
        }

        //Token存在且未过期（30天）
        private bool CheckToken(string token1)
        {
            bool checkResult = false;
            //using (cyEntities entities = new cyEntities())
            //{
            //    var token = entities.tokens.Where(t => t.token1 == token1).FirstOrDefault();
            //    if (token != null)
            //    {
            //        var time = DateTime.Now - token.createtime;
            //        if (time < new TimeSpan(30, 0, 0, 0))
            //        {
            //            checkResult = true;
            //        }
            //    }
            //}
            return checkResult;
        }
    }

    /// <summary>
    /// 接口访问统一入口
    /// </summary>
    public class AuthFilterAttribute : AuthorizationFilterAttribute
    {
        /// <summary>
        /// 接口统一授权
        /// </summary>
        /// <param name="actionContext"></param>
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            try
            {
                var actionName = actionContext.ActionDescriptor.ActionName;
                //如果用户方位的Action带有AllowAnonymousAttribute，则不进行授权验证
                if (actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any())
                {
                    return;
                }
                string token = string.Empty;
                //Post请求获取参数
                if (actionContext.Request.Method == HttpMethod.Post && actionContext.Request.Content.Headers.ContentType != null && actionContext.Request.Content.Headers.ContentType.MediaType.Contains("application/json"))
                {
                    var stream = HttpContext.Current.Request.InputStream;
                    stream.Position = 0;
                    StreamReader reader = new StreamReader(stream);
                    var requestBody = reader.ReadToEnd();
                    //反序列化
                    var model = JsonHelper.Deserialize<Dictionary<string, object>>(requestBody);
                    if (model != null && model.ContainsKey("token"))
                        token = model["token"].ToString();
                }
                else
                {
                    var QueryString = HttpContext.Current.Request.Headers;
                    string tokenname = "";
                    foreach (var item in QueryString)
                    {
                        if (item.ToString().IndexOf('.') == -1 && item.ToString().Length == 5)
                        {
                            if (item.ToString().Contains("token"))
                            {
                                tokenname = item.ToString();
                            }
                        }
                        else
                        {
                            var name = item.ToString().Substring(item.ToString().IndexOf('.') + 1);
                            //为了排除类似info.tokenid这样的参数误导，准确获取info.token
                            if (name.Contains("token") && name.Length == 5)
                            {
                                tokenname = item.ToString();
                            }
                        }
                    }
                    token = QueryString[tokenname];
                }
                //不用的验证Token方法
                List<string> actionNames = new List<string>() {
                    "login","UserLogin","GetUserDetail","GetUserInfoList"
                };
                if (!actionNames.Contains(actionName))
                {
                    ISysUserRepository _sysUserRepository = new SysUserRepository();
                    var userInfo = _sysUserRepository.GetUserInfoByToken(token); //验证token是否存在

                    if (userInfo == null)
                    {
                        CommonAPIResult<bool> response = new CommonAPIResult<bool>();
                        response.code = (short)MessageDict.TokenNon;
                        response.errMsg = "Token不存在";
                        string result = JsonConvert.SerializeObject(response);
                        actionContext.Response = actionContext.Request.CreateResponse(response);
                        actionContext.Request.Properties["userId"] = "";
                        actionContext.Request.Properties["userName"] = "";
                    }
                    else
                    {
                        var tokenTime = userInfo.tokenExpirationDate; //token失效日期
                        if (tokenTime.HasValue)
                        {
                            if (tokenTime.Value.CompareTo(DateTime.Now) < 0) 
                            {
                                CommonAPIResult<bool> response = new CommonAPIResult<bool>();
                                response.code = (short)MessageDict.TokenInvalid;
                                response.errMsg = "Token已过期";
                                string result = JsonConvert.SerializeObject(response);
                                actionContext.Response = actionContext.Request.CreateResponse(response);
                                actionContext.Request.Properties["userId"] = "";
                                actionContext.Request.Properties["userName"] = "";
                            }
                            else
                            {
                                actionContext.Request.Properties["userId"] = userInfo.userId;
                                actionContext.Request.Properties["userName"] = userInfo.userName;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                CommonResult<bool> response = new CommonResult<bool>();
                response.bSucceed = false;
                response.errCode = (int)MessageDict.Failed;
                response.errMsg = "接口访问统一入口请求异常" + ex.ToString();
                string result = JsonConvert.SerializeObject(response);
                actionContext.Response = actionContext.Request.CreateResponse(response);
            }
        }

    }

}