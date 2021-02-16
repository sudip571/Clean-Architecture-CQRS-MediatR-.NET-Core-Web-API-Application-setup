using FlightDeck.DataAccess.Common;
using FlightDeck.Domain.Entities;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace FlightDeck.DataAccess.Unitofwork
{
    public class DapUnitOfWork : IDisposable
    {
        private IDbConnection _connection;
        private IDbTransaction _transaction;
        /// <summary>
        /// NpgsqlConnection is for PostgreSQL
        /// SqlConnection is for MSSQL
        /// </summary>
        /// <param name="connectionString"></param>
        public DapUnitOfWork(string connectionString)
        {
            //_connection = new NpgsqlConnection(connectionString);
            _connection = new SqlConnection(connectionString);
            _connection.Open();
            _transaction = _connection.BeginTransaction();
        }

        #region Setting repository forEntities

        #region Declaration       
        private BaseRepositoryAsync<Reference> _referenceRepository = null;
        private BaseRepositoryAsync<LogUserLogin> _logUserLoginRepository = null;
        #endregion


        #region Implementation      
        public BaseRepositoryAsync<Reference> ReferenceRepository
        {
            get
            {
                return _referenceRepository ?? (_referenceRepository = new BaseRepositoryAsync<Reference>(_transaction));
            }
        }
        public BaseRepositoryAsync<LogUserLogin> LogUserLoginRepository
        {
            get
            {
                return _logUserLoginRepository ?? (_logUserLoginRepository = new BaseRepositoryAsync<LogUserLogin>(_transaction));
            }
        }
        #endregion

        #region Re-setting
        /// <summary>
        /// When we commit changes, we will be on a new transaction, so  to re-instantiate our repositories we should reset them.        
        /// </summary>
        private void ResetRepositories()
        {
            _referenceRepository = null;
            _logUserLoginRepository = null;
        }
        #endregion


        #endregion



        public void Commit()
        {
            try
            {
                _transaction.Commit();
            }
            catch
            {
                _transaction.Rollback();
                throw;
            }
            finally
            {
                _transaction.Dispose();
                _transaction = _connection.BeginTransaction();
                ResetRepositories();
            }
        }

       
        public void Dispose()
        {
            if (_transaction != null)
            {
                _transaction.Dispose();
                _transaction = null;
            }

            if (_connection != null)
            {
                _connection.Dispose();
                _connection = null;
            }
        }

    }
}
