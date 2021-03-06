﻿using IVueElememtAdminRepository;
using IVueElementAdminServices;
using VueElementAdminModel.APIModel;
using VueElementAdminModel.BaseModel;
using ToolLibrary.Helper.Helper;
using System;
using VueElementAdminModel.MySqlModel;
using System.Collections.Generic;
using System.Linq;

namespace VueElementAdminServices
{
    public class UserInfoServices: IUserInfoServices
    {

        private readonly ISysUserRepository _sysUserRepository;

        /// <summary>
        /// 构造函数
        /// </summary>
        public UserInfoServices(ISysUserRepository sysUserRepository)
        {
            _sysUserRepository = sysUserRepository;
        }

        string Md5Key = "VueElementAdmin"; //用于字符串MD5加密

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="userLoginReq"></param>
        /// <returns></returns>
        public CommonAPIResult<UserLoginRes> UserLogin(UserLoginReq userLoginReq)
        {
            CommonAPIResult<UserLoginRes> commonAPIResult = new CommonAPIResult<UserLoginRes>();

            if (string.IsNullOrEmpty(userLoginReq.userName))
            {
                commonAPIResult.UpdateStatus(null, MessageDict.PramError, "请输入用户名！");
                return commonAPIResult;
            }
            if (string.IsNullOrEmpty(userLoginReq.passWord))
            {
                commonAPIResult.UpdateStatus(null, MessageDict.PramError, "请输入密码！");
                return commonAPIResult;
            }

            try
            {
                var afterMD5 = EncryptHelper.MD5Encrypt(Md5Key + userLoginReq.passWord); //加上字符串加密密码
                var userInfo = _sysUserRepository.GetUserForLogin(userLoginReq.userName, afterMD5); //查询用户是否存在

                if (userInfo == null)
                {
                    commonAPIResult.UpdateStatus(null, MessageDict.Failed, "请检查用户名密码是否正确");
                    return commonAPIResult;
                }
                if (userInfo.isValid != 0)
                {
                    commonAPIResult.UpdateStatus(null, MessageDict.Failed, "请联系管理员设置账号登录的权限");
                    return commonAPIResult;
                }

                userInfo.tokenExpirationDate = DateTime.Now.AddDays(30);

                systemUser u = new systemUser();
                u = AutoMapper.To<sys_user, systemUser>(userInfo);//对象转换
                userInfo.userToken = JWTHelper.GetToken(u); //GWT更新生成token

                var i = _sysUserRepository.UpdateToken(userInfo); //更新token
                if (i > 0)
                {
                    UserLoginRes userLoginRes = new UserLoginRes();
                    userLoginRes.token = userInfo.userToken; //设置返回到页面的token
                    commonAPIResult.UpdateStatus(userLoginRes, MessageDict.Ok, "登录成功！");
                    return commonAPIResult;
                }

                commonAPIResult.UpdateStatus(null, MessageDict.SystemUnkonw, "未知错误，更新用户数据异常！");
                return commonAPIResult;
            }
            catch (Exception ex)
            {
                commonAPIResult.UpdateStatus(null, MessageDict.SystemUnkonw, "系统异常，请联系管理员！" + ex.Message);
                return commonAPIResult;
            }
        }

        /// <summary>
        /// 获取用户详细信息
        /// </summary>
        /// <param name="userDetailReq"></param>
        /// <returns></returns>
        public CommonAPIResult<UserDetailRes> GetUserDetail(long userId)
        {
            CommonAPIResult<UserDetailRes> commonAPIResult = new CommonAPIResult<UserDetailRes>();

            var userInfo = _sysUserRepository.GetUserInfo(userId); //根据用户ID查询用户信息
            if (userInfo == null)
            {
                commonAPIResult.UpdateStatus(null, MessageDict.NoDataExists, "用户不存在");
            }
            UserDetailRes userDetailRes = new UserDetailRes();
            userDetailRes = AutoMapper.To<sys_user, UserDetailRes>(userInfo); //对象转换

            var aa = new List<string>();
            aa.Add("admin");
            userDetailRes.roles = aa;

            commonAPIResult.UpdateStatus(userDetailRes, MessageDict.Ok, "获取成功");
            return commonAPIResult;
        }

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="parameterJson"></param>
        /// <param name="tableParame"></param>
        /// <returns></returns>
        public List<sys_user> GetUserInfoList(ref TableParame tableParame)
        {
            return _sysUserRepository.GetUserInfoList(ref tableParame).ToList();
        }

        /// <summary>
        /// 保存用户信息
        /// </summary>
        /// <param name="saveUserInfoReq"></param>
        /// <returns></returns>
        public CommonAPIResult<string> SaveUserInfo(SaveUserInfoReq saveUserInfoReq)
        {
            CommonAPIResult<string> commonAPIResult = new CommonAPIResult<string>();
            var userExit = _sysUserRepository.GetUserInfoByUserName(saveUserInfoReq.userName); //查询用户是否存在
            if (userExit!=null)
            {
                commonAPIResult.UpdateStatus("", MessageDict.Failed, "用户已存在！");
                return commonAPIResult;
            }

            saveUserInfoReq.passWord = EncryptHelper.MD5Encrypt(Md5Key + saveUserInfoReq.passWord); //密码加密
            var saveUser = _sysUserRepository.SaveUserInfo(saveUserInfoReq); //保存用户
            if (saveUser > 0)
            {
                commonAPIResult.UpdateStatus("", MessageDict.Ok, "用户新增成功！");
            }
            else
            {
                commonAPIResult.UpdateStatus("", MessageDict.Failed, "用户新增成功！");
            }
            return commonAPIResult;
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="saveUserInfoReq"></param>
        /// <returns></returns>
        public CommonAPIResult<string> DeleteUser(DeleteUserReq saveUserInfoReq)
        {
            CommonAPIResult<string> commonAPIResult = new CommonAPIResult<string>();
            var deleteUser = _sysUserRepository.DeleteUser(saveUserInfoReq); //保存用户
            if (deleteUser > 0)
            {
                commonAPIResult.UpdateStatus("", MessageDict.Ok, "删除成功");
            }
            else
            {
                commonAPIResult.UpdateStatus("", MessageDict.Ok, "删除失败");
            }
            return commonAPIResult;
        }

        /// <summary>
        /// 修改用户状态
        /// </summary>
        /// <param name="changUserVaildReq"></param>
        /// <returns></returns>
        public CommonAPIResult<string> ChangUserVaild(ChangUserVaildReq changUserVaildReq)
        {
            CommonAPIResult<string> commonAPIResult = new CommonAPIResult<string>();
            var deleteUser = _sysUserRepository.ChangUserVaild(changUserVaildReq); //修改状态
            if (deleteUser > 0)
            {
                commonAPIResult.UpdateStatus("", MessageDict.Ok, "修改成功");
            }
            else
            {
                commonAPIResult.UpdateStatus("", MessageDict.Ok, "修改失败");
            }
            return commonAPIResult;
        }
    }
}
