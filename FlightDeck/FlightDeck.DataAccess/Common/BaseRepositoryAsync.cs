using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightDeck.DataAccess.Common
{
    public class BaseRepositoryAsync<T> where T : class
    {
        #region Private properties
        private IDbTransaction _transaction;
        //private IDbConnection _connection { get { return _transaction.Connection; } }
        private IDbConnection _connection;
        #endregion

        public BaseRepositoryAsync(IDbTransaction transaction)
        {
            _transaction = transaction;
            _connection = _transaction.Connection;
        }

        #region Add,update,Delete other approach (Do not uncomment)
        //public async Task<T> AddAsync(string sqlQuery, T entity)
        //{
        //    return await _connection.ExecuteScalarAsync<T>(sqlQuery, entity, _transaction);
        //    //  var addedEntity= _connection.ExecuteScalar<T>(sqlQuery, entity, _transaction);

        //}
        //public async Task<int> UpdateAsync(string sqlQuery, T entity)
        //{
        //    return await _connection.ExecuteAsync(sqlQuery, entity, _transaction);
        //}
        //public async Task<int> DeleteAsync(string sqlQuery, DynamicParameters parameters)
        //{
        //    return await _connection.ExecuteAsync(sqlQuery, parameters, _transaction);
        //}
        #endregion

        public async Task<T> AddAsync(string sqlQuery, T entity)
        {
            return await _connection.QuerySingleAsync<T>(sqlQuery, entity, _transaction);
            //  var addedEntity= _connection.ExecuteScalar<T>(sqlQuery, entity, _transaction);

        }
        public async Task<T> UpdateAsync(string sqlQuery, T entity)
        {
            return await _connection.QuerySingleAsync<T>(sqlQuery, entity, _transaction);
        }
        public async Task<T> DeleteAsync(string sqlQuery, DynamicParameters parameters)
        {
            return await _connection.QuerySingleAsync<T>(sqlQuery, parameters, _transaction);
        }
        public async Task<T> GetSingleAsync(string sqlQuery, DynamicParameters parameters)
        {
            return (await  _connection.QueryAsync<T>(sqlQuery, parameters, _transaction)).FirstOrDefault();
        }
        public async Task<List<T>> GetAllAsync(string sqlQuery, DynamicParameters parameters)
        {
            return (await _connection.QueryAsync<T>(sqlQuery, parameters, _transaction)).ToList();
        }
        public async Task<T> GetSingleByStoredProcedureAsync(string storedProcedureName, DynamicParameters parameters)
        {
            return (await _connection.QueryAsync<T>(storedProcedureName, parameters, _transaction, commandType: CommandType.StoredProcedure)).FirstOrDefault();
        }
        public async Task<List<T>> GetAllByStoredProcedureAsync(string storedProcedureName, DynamicParameters parameters)
        {
            return (await _connection.QueryAsync<T>(storedProcedureName, parameters, _transaction, commandType: CommandType.StoredProcedure)).ToList();
        }
    }
}
