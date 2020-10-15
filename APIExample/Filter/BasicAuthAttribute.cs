using APIExample.Engine;
using APIExample.Models;
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
                    //NLogHelper.Info(actionContext.ActionDescriptor.ActionName + requestBody);
                    var model = JsonHelper.Deserialize<Dictionary<string, object>>(requestBody);
                    if (model != null && model.ContainsKey("token"))
                        token = model["token"].ToString();
                }
                else
                {
                    var QueryString = HttpContext.Current.Request.QueryString;
                    string tokenname = "";
                    string ordernumname = "";
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
                    //订单编号
                    foreach (var item in QueryString)
                    {
                        if (item.ToString().IndexOf('.') == -1 && item.ToString().Length == 5)
                        {
                            if (item.ToString().Contains("ordernum"))
                            {
                                ordernumname = item.ToString();
                            }
                        }
                        else
                        {
                            var name = item.ToString().Substring(item.ToString().IndexOf('.') + 1);
                            //为了排除类似info.tokenid这样的参数误导，准确获取info.token
                            if (name.Contains("ordernum") && name.Length == 8)
                            {
                                ordernumname = item.ToString();
                            }
                        }
                    }
                    token = QueryString[tokenname];
                    string ordernum = QueryString[ordernumname];
                }
                //不用的验证Token方法
                List<string> actionNames = new List<string>() {
                    "UsersLogin", "UsersRegister","SendVerificationCode", "AlipayRequstPost", "WxpayRequstPost", "Version",
                     "thirdOpenID", "SendThirdVerificationCode","ThirdRegister","GetAndroidVersionInfo","GetSkillList","GetAllSkillCatas",
                     "CheckMsgByPhone","GetAllSkillCatasIos"
                };
                if (!actionNames.Contains(actionName))
                {
                    //using (cyEntities entities = new cyEntities())
                    //{
                    //    entities.Configuration.LazyLoadingEnabled = false;
                    //    entities.Configuration.ProxyCreationEnabled = false;
                    //    entities.Configuration.AutoDetectChangesEnabled = false;
                    //    var userid = "";
                    //    //token校验结果 
                    //    int check = 0;
                    //    try
                    //    {
                    //        check = CheckToken(token, entities, ref userid);
                    //    }
                    //    catch (Exception e)
                    //    {
                    //        Thread.Sleep(300); //停一秒
                    //        check = CheckToken(token, entities, ref userid);
                    //        NLogHelper.Error(null, e);
                    //    }
                    //    actionContext.Request.Properties["userid"] = userid;
                    //    //token不存在
                    //    if (check == 2)
                    //    {
                    //        CommonAPIResult<bool> response = new CommonAPIResult<bool>();
                    //        response.errCode = (short)MessageDict.TokenNon;
                    //        response.errMsg = "Token不存在";
                    //        string result = JsonConvert.SerializeObject(response);
                    //        actionContext.Response = actionContext.Request.CreateResponse(response);
                    //        actionContext.Request.Properties["userid"] = "";
                    //    }
                    //    //token已经失效
                    //    else if (check == 1)
                    //    {
                    //        CommonAPIResult<bool> response = new CommonAPIResult<bool>();
                    //        response.errCode = (short)MessageDict.TokenInvalid;
                    //        response.errMsg = "Token已过期";
                    //        string result = JsonConvert.SerializeObject(response);
                    //        actionContext.Response = actionContext.Request.CreateResponse(response);
                    //        actionContext.Request.Properties["userid"] = "";
                    //    }
                    //}
                }
            }
            catch (Exception ex)
            {
                var actionName = actionContext.ActionDescriptor.ActionName;
                if (actionName != "GetOrderList")
                {
                    CommonResult<bool> response = new CommonResult<bool>();
                    response.bSucceed = false;
                    response.errCode = (int)MessageDict.Failed;
                    response.errMsg = "接口访问统一入口请求异常" + ex.ToString();
                    string result = JsonConvert.SerializeObject(response);
                    actionContext.Response = actionContext.Request.CreateResponse(response);
                }
                //NLogHelper.Error(null, ex.ToString());
            }
        }

        /// <summary>
        /// 使用数据库和redis相互结合查询token减轻查询压力防止报错
        /// </summary>
        /// <param name="token1"></param>
        /// <param name="entities"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        //private int CheckToken(string token1, cyEntities entities, ref string userid)
        //{
        //    //首先去redis查询token是否存在
        //    string redisUserInfo = RedisServiceConfig.Instance.Get<string>(token1);
        //    if (!string.IsNullOrWhiteSpace(redisUserInfo))
        //    {
        //        userid = redisUserInfo;
        //        return 0;//表示通过
        //    }
        //    else//redis中不存在或者未注册
        //    {
        //        //查数据库验证
        //        int checkResult = 0;
        //        var token = entities.tokens.Where(t => t.token1 == token1).FirstOrDefault();
        //        if (token != null)
        //        {
        //            userid = token.userid;
        //            var time = DateTime.Now - token.createtime;
        //            if (time > new TimeSpan(30, 0, 0, 0))
        //            {
        //                checkResult = 1;
        //            }
        //            else//数据库存在并且未过期时 注册redis 方便以后重复查询
        //            {
        //                RedisServiceConfig.Instance.Set(token.token1, userid, 43200);//设置一个月的过期时间
        //            }
        //        }
        //        else
        //        {
        //            checkResult = 2;
        //        }
        //        return checkResult;
        //    }
        //}

        //private string GetUserId(string token1, cyEntities entities)
        //{
        //    string userid = "";
        //    var token = entities.tokens.Where(t => t.token1 == token1).FirstOrDefault();
        //    if (token != null)
        //    {
        //        userid = token.userid;
        //    }
        //    return userid;

        //}

        //private int CheckOrder(string ordernum, string userid, cyEntities entities)
        //{
        //    int checkResult = 0;
        //    if (string.IsNullOrWhiteSpace(ordernum))
        //    {
        //        return 1;
        //    }
        //    var order = entities.Orders.Where(t => t.ordernum == ordernum).FirstOrDefault();

        //    if (order != null)
        //    {
        //        if (order.buyer.Contains(userid) || order.sellers.Contains(userid))
        //        {
        //            checkResult = 1;
        //        }
        //        else
        //        {
        //            checkResult = 0;
        //        }

        //    }
        //    else
        //    {
        //        checkResult = 0;
        //    }
        //    return checkResult;
        //}
    }

}