using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using VueElemenntAdminModel.APIModel;
using IVueElememtAdminRepository.Base;
using Dapper;
using MySql.Data.MySqlClient;

namespace VueElememtAdminRepository.Base
{
    public class Repository<T> : IDisposable, IRepository<T> where T : new()
    {
        private MySqlConnection _connection;
        public string ConnectionString { get; set; }
        public MySqlConnection Conn
        {
            get { return _connection ?? (_connection = new MySqlConnection(ConnectionString)); }
            set { _connection = value; }
        }

        #region 同步

        #region 执行SQL语句
        public int Execute(string sql, object param = null, IDbTransaction transaction = null)
        {
            return Conn.Execute(sql, param, transaction);
        }
        public object ExecuteScalar(string sql, object param = null, IDbTransaction transaction = null)
        {
            return Conn.ExecuteScalar(sql, param, transaction);
        }
        public TEntity ExecuteScalar<TEntity>(string sql, object param = null, IDbTransaction transaction = null)
        {
            return Conn.ExecuteScalar<TEntity>(sql, param, transaction);
        }
        public T Get(object id, IDbTransaction transaction = null)
        {
            return Conn.Get<T>(id, transaction);
        }
        public T GetBySql(string sql, object parameters = null, IDbTransaction transaction = null)
        {
            return Conn.QueryFirstOrDefault<T>(sql, parameters, transaction);
        }
        public TEntity GetBySql<TEntity>(string sql, object parameters = null, IDbTransaction transaction = null)
        {
            return Conn.QueryFirstOrDefault<TEntity>(sql, parameters, transaction);
        }
        public IEnumerable<T> GetListBySql(string sql, object parameters = null, IDbTransaction transaction = null)
        {
            return Conn.Query<T>(sql, parameters, transaction, true, null, CommandType.Text);
        }
        #endregion

        #region 执行存储过程
        public int ExecuteByProc(string procName, object param = null, IDbTransaction transaction = null)
        {
            return Conn.Execute(procName, param, transaction, null, CommandType.StoredProcedure);
        }
        public object ExecuteScalarByProc(string procName, object param = null, IDbTransaction transaction = null)
        {
            return Conn.ExecuteScalar(procName, param, transaction, null, CommandType.StoredProcedure);
        }
        public TEntity ExecuteScalarByProc<TEntity>(string procName, object param = null, IDbTransaction transaction = null)
        {
            return Conn.ExecuteScalar<TEntity>(procName, param, transaction, null, CommandType.StoredProcedure);
        }
        public T GetByProc(string procName, object parameters = null, IDbTransaction transaction = null)
        {
            return Conn.QueryFirstOrDefault<T>(procName, parameters, transaction, null, CommandType.StoredProcedure);
        }
        public TEntity GetByProc<TEntity>(string procName, object parameters = null, IDbTransaction transaction = null)
        {
            return Conn.QueryFirstOrDefault<TEntity>(procName, parameters, transaction, null, CommandType.StoredProcedure);
        }
        public IEnumerable<T> GetListByProc(string procName, object parameters = null, IDbTransaction transaction = null)
        {
            return Conn.Query<T>(procName, parameters, transaction, true, null, CommandType.StoredProcedure);
        }

        #endregion

        #region 增删改查

        public int? Insert(T entity, IDbTransaction transaction = null)
        {
            return Conn.Insert(entity, transaction);
        }

        public TKey Insert<TKey>(T entity, IDbTransaction transaction = null)
        {
            return Conn.Insert<TKey, T>(entity, transaction);
        }

        public int Delete(object id, IDbTransaction transaction = null)
        {
            return Conn.Delete<T>(id, transaction);
        }
        public int DeleteList(string whereConditions, object parameters = null, IDbTransaction transaction = null)
        {
            return Conn.DeleteList<T>(whereConditions, parameters, transaction);
        }
        public int Update(object entity, IDbTransaction transaction = null)
        {
            return Conn.Update(entity, transaction);
        }
        public IEnumerable<T> GetList(string whereConditions, object parameters = null, IDbTransaction transaction = null)
        {
            return Conn.GetList<T>(whereConditions, parameters, transaction);
        }
        public IEnumerable<T> GetListPaged(int pageNumber, int rowsPerPage, string whereConditions, string orderby, object parameters = null, IDbTransaction transaction = null)
        {
            return Conn.GetListPaged<T>(pageNumber, rowsPerPage, whereConditions, orderby, parameters, transaction);
        }
        public int RecordCount(string whereConditions = "", object parameters = null, IDbTransaction transaction = null)
        {
            return Conn.RecordCount<T>(whereConditions, parameters, transaction);
        }
        #endregion

        #endregion

        #region 异步

        #region 执行SQL语句
        public async Task<int> ExecuteAsync(string sql, object param = null, IDbTransaction transaction = null)
        {
            return await Conn.ExecuteAsync(sql, param, transaction);
        }
        public async Task<object> ExecuteScalarAsync(string sql, object param = null, IDbTransaction transaction = null)
        {
            return await Conn.ExecuteScalarAsync(sql, param, transaction);
        }
        public async Task<TEntity> ExecuteScalarAsync<TEntity>(string sql, object param = null, IDbTransaction transaction = null)
        {
            return await Conn.ExecuteScalarAsync<TEntity>(sql, param, transaction);
        }
        public async Task<T> GetAsync(object id, IDbTransaction transaction = null)
        {
            return await Conn.GetAsync<T>(id, transaction);
        }
        public async Task<T> GetBySqlAsync(string sql, object parameters = null, IDbTransaction transaction = null)
        {
            return await Conn.QueryFirstOrDefaultAsync<T>(sql, parameters, transaction);
        }
        public async Task<TEntity> GetBySqlAsync<TEntity>(string sql, object parameters = null, IDbTransaction transaction = null)
        {
            return await Conn.QueryFirstAsync<TEntity>(sql, parameters, transaction);
        }
        public async Task<IEnumerable<T>> GetListBySqlAsync(string sql, object parameters = null, IDbTransaction transaction = null)
        {
            return await Conn.QueryAsync<T>(sql, parameters, transaction, null, CommandType.Text);
        }
        #endregion

        #region 执行存储过程
        public async Task<int> ExecuteByProcAsync(string procName, object param = null, IDbTransaction transaction = null)
        {
            return await Conn.ExecuteAsync(procName, param, transaction, null, CommandType.StoredProcedure);
        }
        public async Task<object> ExecuteScalarByProcAsync(string procName, object param = null, IDbTransaction transaction = null)
        {
            return await Conn.ExecuteScalarAsync(procName, param, transaction, null, CommandType.StoredProcedure);
        }
        public async Task<TEntity> ExecuteScalarByProcAsync<TEntity>(string procName, object param = null, IDbTransaction transaction = null)
        {
            return await Conn.ExecuteScalarAsync<TEntity>(procName, param, transaction, null, CommandType.StoredProcedure);
        }
        public async Task<T> GetByProcAsync(string procName, object parameters = null, IDbTransaction transaction = null)
        {
            return await Conn.QueryFirstOrDefaultAsync<T>(procName, parameters, transaction, null, CommandType.StoredProcedure);
        }
        public async Task<TEntity> GetByProcAsync<TEntity>(string procName, object parameters = null, IDbTransaction transaction = null)
        {
            return await Conn.QueryFirstOrDefaultAsync<TEntity>(procName, parameters, transaction, null, CommandType.StoredProcedure);
        }
        public async Task<IEnumerable<T>> GetListByProcAsync(string procName, object parameters = null, IDbTransaction transaction = null)
        {
            return await Conn.QueryAsync<T>(procName, parameters, transaction, null, CommandType.StoredProcedure);
        }
        #endregion

        #region 增删改查

        public async Task<int?> InsertAsync(T entity, IDbTransaction transaction = null)
        {
            return await Conn.InsertAsync(entity, transaction);
        }

        public async Task<TKey> InsertAsync<TKey>(T entity, IDbTransaction transaction = null)
        {
            return await Conn.InsertAsync<TKey, T>(entity, transaction);
        }
        public async Task<int> DeleteAsync(object id, IDbTransaction transaction = null)
        {
            return await Conn.DeleteAsync<T>(id, transaction);
        }
        public async Task<int> DeleteListAsync(string whereConditions, object parameters = null, IDbTransaction transaction = null)
        {
            return await Conn.DeleteListAsync<T>(whereConditions, parameters, transaction);
        }
        public async Task<int> UpdateAsync(T entity, IDbTransaction transaction = null)
        {
            return await Conn.UpdateAsync<T>(entity, transaction);
        }
        public async Task<IEnumerable<T>> GetListAsync(string whereConditions, object parameters = null, IDbTransaction transaction = null)
        {
            return await Conn.GetListAsync<T>(whereConditions, parameters, transaction);
        }
        public async Task<IEnumerable<T>> GetListPagedAsync(int pageNumber, int rowsPerPage, string whereConditions, string orderby, object parameters = null, IDbTransaction transaction = null)
        {
            return await Conn.GetListPagedAsync<T>(pageNumber, rowsPerPage, whereConditions, orderby, parameters, transaction);
        }
        public async Task<int> RecordCountAsync(string whereConditions = "", object parameters = null, IDbTransaction transaction = null)
        {
            return await Conn.RecordCountAsync<T>(whereConditions, parameters, transaction);
        }
        #endregion

        #endregion

        #region SQL分页

        /// <summary>
        /// 分页
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <param name="gridparam"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public IEnumerable<TEntity> GetPageList<TEntity>(string sql, object parameters, ref TableParame tableParame, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return Conn.GetPageList<TEntity>(sql, ref tableParame, parameters, transaction, commandTimeout);
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <param name="gridparam"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public Task<IEnumerable<TEntity>> GetPageListAsync<TEntity>(string sql, object parameters, ref TableParame tableParame, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return Conn.GetPageListAsync<TEntity>(sql, ref tableParame, parameters, transaction, commandTimeout);
        }

        #endregion


        #region MySql分页


        /// <summary>
        /// 分页
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <param name="gridparam"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public IEnumerable<TEntity> GetMySqlPageList<TEntity>(string sql, object parameters, ref TableParame tableParame, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return Conn.GetMySqlPageList<TEntity>(sql, ref tableParame, parameters, transaction, commandTimeout);
        }

        #endregion

        public void Dispose()
        {
            _connection?.Dispose();
        }
    }
}
