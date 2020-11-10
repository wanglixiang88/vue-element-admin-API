using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IVueElememtAdminRepository.Base
{

    /// <summary>
    /// 通用的Repository接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T> where T : new()
    {
        #region 同步
        int? Insert(T entity, IDbTransaction transaction = null);
        TKey Insert<TKey>(T entity, IDbTransaction transaction = null);
        int Delete(object id, IDbTransaction transaction = null);
        int DeleteList(string conditions, object parameters = null, IDbTransaction transaction = null);
        int Update(object entity, IDbTransaction transaction = null);
        T Get(object id, IDbTransaction transaction = null);
        #endregion

        #region 异步
        Task<int?> InsertAsync(T entityToInsert, IDbTransaction transaction = null);
        Task<TKey> InsertAsync<TKey>(T entityToInsert, IDbTransaction transaction = null);
        Task<int> DeleteListAsync(string conditions, object parameters = null, IDbTransaction transaction = null);
        Task<int> DeleteAsync(object id, IDbTransaction transaction = null);
        Task<int> UpdateAsync(T entity, IDbTransaction transaction = null);
        Task<T> GetAsync(object id, IDbTransaction transaction = null);
        #endregion
    }
}
