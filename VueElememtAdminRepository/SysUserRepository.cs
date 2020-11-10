using Dapper;
using IVueElememtAdminRepository;
using SqlSugar;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolLibrary.Helper.Helper;
using ToolLibrary.Helper.Json;
using VueElememtAdminRepository.DBConfig;
using VueElemenntAdminModel.APIModel;
using vueElementAdminModel.MySqlModel;

namespace VueElememtAdminRepository
{
    public class SysUserRepository : ISysUserRepository
    {

        public static SqlSugarClient mysqlConn = MySQLInfo.mySqlSugarClient();

        /// <summary>
        /// 根据Token获取用户信息
        /// </summary>
        /// <param name="token">token</param>
        /// <returns></returns>
        public sys_user GetUserInfoByToken(string token)
        {
            using (mysqlConn)
            {
                return mysqlConn.Queryable<sys_user>().Where(t => t.userToken.Equals(token)).First();
            }
        }

        /// <summary>
        /// 用户登录所用，根据用户密码查询用户信息
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="passWord">密码</param>
        /// <returns></returns>
        public sys_user GetUserForLogin(string userName, string passWord)
        {
            using (mysqlConn)
            {
                return mysqlConn.Queryable<sys_user>().Where(t => t.userName.Equals(userName) && t.passWord.Equals(passWord)).First();
            }
        }

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="sysUser"></param>
        /// <returns></returns>
        public int UpdateToken(sys_user sysUser)
        {
            using (mysqlConn)
            {
                return mysqlConn.Updateable(sysUser).ExecuteCommand();
            }
        }

        /// <summary>
        /// 根据用户ID获取用户的详细信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public sys_user GetUserInfo(long userId)
        {
            using (mysqlConn)
            {
                return mysqlConn.Queryable<sys_user>().Where(t => t.userId == userId).First();
            }
        }

        public IEnumerable<sys_user> GetUserInfoList(string parameterJson, ref TableParame tableParame)
        {

            StringBuilder sql = new StringBuilder();
            sql.Append(@"  select  * from ( select DATEDIFF(DAY,GETDATE(),a.ScheduleLockDeadlineDate) as TimeDifferenceBySchedule, (isnull(a.ChangeAfterTotalSum,0)+a.WeddingCarTotalDecimal- isnull(a.AmountCollected,0)) as UnpaidAmount, a.* from 
                                    (select WeddingCarCount=(select count(1) from WeddingCarContract where OrderNumber=o.OrderNumber and IsDelete=0),WeddingPhotoCount=(select count(1) from Order_ProductDetails where  OrderNumber=o.OrderNumber and ContractNumber=o.ContractNumber and  F_ID=o.SetmealID and IsDelete=0 and State=1 and ProductType='af406c96-31fe-4440-a76e-70041afda106' ),bs.Grade as RealGrade,c.StoreID as CustomerInfoStoreID,TaskCount=(select COUNT(1) from ChangeTask where (ChangeState=0 or ChangeState=3 ) and IsDelete=0 and OrderNumber=o.OrderNumber),
                                            AllTaskCount=(select COUNT(1) from ChangeTask where OrderNumber=o.OrderNumber),
                                            wc.Total as WeddingCarTotal,isnull(wc.Total,0) as WeddingCarTotalDecimal,ChangeAfterTotalSum=(isnull(o.TotalWedding,0)+isnull(o.ChangeMoney,0)),b.BanquetHallName, hq.PackageName as Weddings,cx.PackageName as Cuisine,cx.PackagePrice as CuisinePrice,
                                            (select isnull(sum(AmountCollected),0) from RegisterRecord where OrderNumber=o.OrderNumber  and IsDelete=0) as AmountCollected,
                                            (select isnull(sum(InvoiceAmount),0) from InvoiceInfo where IsDelete=0 and OrderNumber=o.OrderNumber ) as InvoicePrice
                                            ,o.*
                                            from OrderDetails o 
		                                    left join CustomerInfo c on c.CustomerID=o.OrderNumber
                                            left join BanquetHall b on o.RealBanquetHall=b.BanquetHallID
                                            left join (select * from Order_PackageDetails where TypeOfSetMeal='婚庆' and IsDelete=0 and (OrderChangeAuditStatus = 0 or OrderChangeAuditStatus=1)) hq on hq.F_ID=o.SetmealID
                                            left join (select * from Order_PackageDetails where TypeOfSetMeal='菜系' and IsDelete=0 and (OrderChangeAuditStatus = 0 or OrderChangeAuditStatus=1)) cx on cx.F_ID=o.SetmealID
                                            left join (select * from  WeddingCarContract where (State=0 or State is null)  and IsDelete=0) wc on wc.OrderNumber=o.OrderNumber
                                            left join BanquetHallSchedule bs on bs.BanquetHallScheduleID=o.RealBanquetHallScheduleID
                                            where o.IsDelete=0 and o.TypeOfConsumption=1   and c.IsDelete=0
                                    ) a ) tg where 1=1   ");

            if (!string.IsNullOrEmpty(parameterJson))
            {
                DynamicParameters ParamList = new DynamicParameters();
                IList conditions = RequestHelper.UrlDecode(parameterJson).JonsToList<Condition>();
                string WhereSql = ConditionBuilder.GetWhereSql(conditions, out ParamList);
                sql.Append(WhereSql);
                return GetPageList<sys_user>(sql.ToString(), ParamList, ref tableParame);
            }
            return GetPageList<sys_user>(sql.ToString(), null, ref tableParame);
        }
    }
}
