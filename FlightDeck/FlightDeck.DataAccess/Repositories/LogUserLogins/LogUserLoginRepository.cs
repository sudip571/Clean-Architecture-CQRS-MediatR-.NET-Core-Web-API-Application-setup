using FlightDeck.Application.Infrastructure.Repository;
using FlightDeck.Application.JsonModels;
using FlightDeck.DataAccess.Unitofwork;
using FlightDeck.Domain.Entities;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FlightDeck.DataAccess.Repositories.LogUserLogins
{
    public class LogUserLoginRepository : IAsyncLogUserLoginRepository
    {
        private readonly ConnectionStrings _connectionStrings;
        public LogUserLoginRepository(IOptions<ConnectionStrings> connectionStrings)
        {
            _connectionStrings = connectionStrings.Value;
        }
       
        public async Task AddAsync(LogUserLogin model)
        {
            using (var unitOfWork = new DapUnitOfWork(_connectionStrings.FlightDeckAPI))
            {
                var sqlQuery = LogUserLoginQuery.INSERT;
                var result = await unitOfWork.LogUserLoginRepository.AddAsync(sqlQuery, model);
                unitOfWork.Commit();
            }
        }
    }
}
