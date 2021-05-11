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
using VueElememtAdminRepository.Base;
using VueElememtAdminRepository.DBConfig;
using VueElementAdminModel.APIModel;
using VueElementAdminModel.MySqlModel;

namespace VueElememtAdminRepository
{
    public class SysDictionaryRepository : Repository<sys_dictionary>, ISysDictionaryRepository
    {
        public SysDictionaryRepository()
        {
            Conn = DBUtilities.GetMySqlConnectionString();
        }

        public static SqlSugarClient mysqlConn = MySQLInfo.mySqlSugarClient();

        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <param name="parameterJson"></param>
        /// <param name="tableParame"></param>
        /// <returns></returns>
        public IEnumerable<sys_dictionary> GetDictionaryList(ref TableParame tableParame)
        {
            using (Conn)
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(@" SELECT * FROM sys_dictionary where 1=1 and isDelete=0 ");
                DynamicParameters ParamList = new DynamicParameters();
                string WhereSql = ConditionBuilder.GetWhereSql(tableParame.parameterJson, out ParamList);
                sql.Append(WhereSql);
                if (!string.IsNullOrEmpty(tableParame.sidx))
                {
                    if (tableParame.sidx.ToUpper().IndexOf("ASC") + tableParame.sidx.ToUpper().IndexOf("DESC") > 0)
                    {
                        sql.Append(" Order By " + tableParame.sidx);
                    }
                    else
                    {
                        sql.Append(" Order By " + tableParame.sidx + " " + (tableParame.sort == "ASC" ? "" : "DESC"));
                    }
                }
                return Conn.Query<sys_dictionary>(sql.ToString());
            }
        }

        /// <summary>
        /// 根据arryId查询数据字典
        /// </summary>
        /// <param name="arryId"></param>
        /// <returns></returns>
        public sys_dictionary GetDictionaryById(long arryId)
        {
            using (Conn)
            {
                return Conn.QueryFirstOrDefault<sys_dictionary>("select * from sys_dictionary where isDelete=0 and arryId=@arryId", new { arryId = arryId });
            }
        }

        /// <summary>
        /// 保存数据字典
        /// </summary>
        /// <param name="sys_Dictionary"></param>
        /// <returns></returns>
        public int SaveDictionaryInfo(sys_dictionary sys_Dictionary)
        {
            using (Conn)
            {
                return Conn.Execute(@"insert into sys_dictionary(arryName,parentId,arryValue,isDelete,createTime,createUserId,createUserName) values(@arryName,@parentId,@arryValue,0,@createTime,@createUserId,@createUserName)", new
                {
                    parentId= sys_Dictionary.parentId,
                    arryName = sys_Dictionary.arryName, 
                    arryValue = sys_Dictionary.arryValue,
                    isDelete =0,
                    createTime= sys_Dictionary.createTime,
                    createUserId= sys_Dictionary.createUserId,
                    createUserName= sys_Dictionary.createUserName
                });
            }
        }

        /// <summary>
        /// 更新菜单记录
        /// </summary>
        /// <param name="sys_Dictionary"></param>
        /// <returns></returns>
        public int UpdateDictionaryInfo(sys_dictionary sys_Dictionary)
        {
            using (Conn)
            {
                return Conn.Execute(@" update sys_dictionary set arryName=@arryName,parentId=@parentId,arryValue=@arryValue,updateUserId=@updateUserId,updateUserName=@updateUserName,updateTime=@updateTime where arryId=@arryId and isDelete=0", new
                {
                    arryName = sys_Dictionary.arryName,
                    parentId = sys_Dictionary.parentId,
                    arryValue = sys_Dictionary.arryValue,
                    updateUserId = sys_Dictionary.updateUserId,
                    updateUserName = sys_Dictionary.updateUserName,
                    updateTime = sys_Dictionary.updateTime,
                    arryId = sys_Dictionary.arryId
                });
            }
        }

        /// <summary>
        /// 软删除菜单
        /// </summary>
        /// <param name="deleteDictionaryReq"></param>
        /// <returns></returns>
        public int DeleteDictionary(DeleteDictionaryReq deleteDictionaryReq)
        {
            using (Conn)
            {
                return Conn.Execute(@" update sys_dictionary set isDelete=@isDelete, updateUserId=@updateUserId,updateUserName=@updateUserName,updateTime=@updateTime where arryId=@arryId and isDelete=0 ", new
                {
                    isDelete = 1,
                    updateUserId = deleteDictionaryReq.id,
                    updateUserName = deleteDictionaryReq.name,
                    updateTime = DateTime.Now,
                    arryId = deleteDictionaryReq.arryId
                });
            }
        }

        /// <summary>
        /// 根据ID查询子类信息
        /// </summary>
        /// <param name="parentId">父类ID</param>
        /// <returns></returns>
        public List<sys_dictionary> GetDictionaryByParent(long parentId)
        {
            using (Conn)
            {
                return Conn.Query<sys_dictionary>("select * from sys_dictionary where isDelete=0 and parentId=@parentId", new { parentId = parentId }).ToList();
            }
        }
    }
}
