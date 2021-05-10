using APIExample.APIBusiness;
using APIExample.Filter;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VueElementAdminModel.BaseModel;

namespace APIExample.APIControllers
{
    /// <summary>
    /// user
    /// </summary>
    [AuthFilter]
    [RoutePrefix("API/users")]
    public class UsersController : ApiController
    {

        /// <summary>
        /// 获取粉丝/关注/浏览/访客列表
        /// </summary>
        /// <param name="req">请求的参数</param>
        /// <returns></returns>
        [Route("GetFansList")]
        [HttpPost]
        public CommonAPIResult<string> GetFansList([FromBody] user aa)
        {
            CommonAPIResult<string> commonAPI = new CommonAPIResult<string>();
            commonAPI.data = UsersBusiness.Instance.GetUserInfo("");
            return commonAPI;
        }

        /// <summary>
        /// 
        /// </summary>
        public class user
        {

            /// <summary>
            /// 用户ID
            /// </summary>
            public string userId { get; set; }

            /// <summary>
            /// 用户名
            /// </summary>
            public string userName { get; set; }

            /// <summary>
            /// 头像
            /// </summary>
            public string userHeadImg { get; set; }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="aa"></param>
        /// <returns></returns>
        [Route("login")]
        [HttpPost]
        public CommonAPIResult<userToken> login([FromBody] userInfo aa)
        {
            CommonAPIResult<userToken> commonAPI = new CommonAPIResult<userToken>();
            userToken userToken = new userToken();
            userToken.token = "admin-token";
            commonAPI.UpdateStatus(userToken, MessageDict.Ok, "1");
            return commonAPI;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("info")]
        [HttpGet]
        public CommonAPIResult<userInfo1> info(string token)
        {
            CommonAPIResult<userInfo1> commonAPI = new CommonAPIResult<userInfo1>();
            userInfo1 userInfo = new userInfo1();
            var aa = new List<string>();
            aa.Add("admin");
            userInfo.roles = aa;
            userInfo.introduction = "11111111111222222";
            userInfo.name = "Super Adminguanli";
            userInfo.avatar = "https://wpimg.wallstcn.com/f778738c-e4f8-4870-b634-56703b4acafe.gif";
            commonAPI.UpdateStatus(userInfo, MessageDict.Ok, "成功！");
            return commonAPI;

        }

        [Route("getMenu")]
        [HttpGet]
        public CommonAPIResult<menu> getMenu()
        {
            CommonAPIResult<menu> commonAPI = new CommonAPIResult<menu>();
            menu menu1 = new menu();
            List<getMenu1> menu1s = new List<getMenu1>();
            for (var i= 0 ;i< 5; i++)
            {
                getMenu1 menu11 = new getMenu1();
                menu11.parent_id = "0";
                menu11.menu_name = "用户管理";
                menu11.icon = "icon";
                menu11.order_num = "1";
                menu11.menu_id = "1";
                menu11.url = "/Icons";

                menu1s.Add(menu11);
            }
            menu1.menuList = menu1s;
            commonAPI.UpdateStatus(menu1, MessageDict.Ok, "成功！");
            return commonAPI;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("getUserList")]
        [HttpGet]
        public CommonAPIResult<RequstUser> getUserList (int limit, int page)
        {
            CommonAPIResult<RequstUser> commonAPI = new CommonAPIResult<RequstUser>();

            List<userInfo> userInfo = new List<userInfo>();
            userInfo.Add(new userInfo() { id = 1, userName = "用户111", passWord = "密码1111" });
            userInfo.Add(new userInfo() { id = 2, userName = "用户222", passWord = "密码2222" });
            userInfo.Add(new userInfo() { id = 3, userName = "用户333", passWord = "密码3333" });
            userInfo.Add(new userInfo() { id = 4, userName = "用户444", passWord = "密码4444" });
            userInfo.Add(new userInfo() { id = 5, userName = "用户111", passWord = "密码1111" });
            userInfo.Add(new userInfo() { id = 6, userName = "用户222", passWord = "密码2222" });
            userInfo.Add(new userInfo() { id = 7, userName = "用户333", passWord = "密码3333" });
            userInfo.Add(new userInfo() { id = 8, userName = "用户444", passWord = "密码4444" });

            RequstUser menu1 = new RequstUser();
            menu1.total = 8;
            menu1.item = userInfo;

            commonAPI.UpdateStatus(menu1, MessageDict.Ok, "成功！");
            return commonAPI;
        }

    }

    public class getMenu1
    {
        public string parent_id { get; set; }
        public string menu_name { get; set; }
        public string icon { get; set; }
        public string perms { get; set; }
        public string order_num { get; set; }
        public string menu_id { get; set; }
        public string url { get; set; }
        public string component { get; set; } //重定向
        public List<getMenu1> children { get; set; }
    }


    public class menu
    {
        public List<getMenu1> menuList { get; set; }


    }

    public class RequstUser
    {
        public int total { get; set; }

        public List<userInfo> item { get; set; }
    }

    public class userInfo
    {
        public int id { get; set; }
        public string userName { get; set; }
        public string passWord { get; set; }
    }

    public class userToken
    {
        public string token { get; set; }
    }

    public class userInfo1
    {
        public List<string> roles { get; set; }
        public string introduction { get; set; }
        public string name { get; set; }
        public string avatar { get; set; }
    }

}
