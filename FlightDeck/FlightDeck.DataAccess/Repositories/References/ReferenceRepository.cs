using Dapper;
using FlightDeck.Application.Infrastructure.Repository;
using FlightDeck.Application.JsonModels;
using FlightDeck.DataAccess.Unitofwork;
using FlightDeck.Domain.Entities;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FlightDeck.DataAccess.Repositories.References
{
    public class ReferenceRepository : IAsyncReferenceRepository
    {
        private readonly ConnectionStrings _connectionStrings;
        public ReferenceRepository(IOptions<ConnectionStrings> connectionStrings)
        {
            _connectionStrings = connectionStrings.Value;
        }
        public async Task AddAsync(Reference model)
        {
            using (var unitOfWork = new DapUnitOfWork(_connectionStrings.FlightDeckAPI))
            {
                var sqlQuery = ReferenceQuery.INSERT;
                var result = await unitOfWork.ReferenceRepository.AddAsync(sqlQuery, model);
                unitOfWork.Commit();
            }
        }

        public async Task UpdateAsync(Reference model)
        {
            using (var unitOfWork = new DapUnitOfWork(_connectionStrings.FlightDeckAPI))
            {
                var sqlQuery = ReferenceQuery.UPDATE;
                var result = await  unitOfWork.ReferenceRepository.UpdateAsync(sqlQuery, model);
                unitOfWork.Commit();
            }
        }

        public async Task DeleteAsync(int Id)
        {
            using (var unitOfWork = new DapUnitOfWork(_connectionStrings.FlightDeckAPI))
            {
                var sqlQuery = ReferenceQuery.DELETE;
                var parameter = new DynamicParameters();
                parameter.Add("@Id", Id);
                var result = await unitOfWork.ReferenceRepository.DeleteAsync(sqlQuery, parameter);
                unitOfWork.Commit();
            }
        }

        public async Task<List<Reference>> GetAllAsync()
        {
            using (var unitOfWork = new DapUnitOfWork(_connectionStrings.FlightDeckAPI))
            {
                var sqlQuery = ReferenceQuery.GET_ALL;
                var result = await unitOfWork.ReferenceRepository.GetAllAsync(sqlQuery, null);
                return result;
            }
        }

        public async Task<Reference> GetByIdAsync(int id)
        {
            using (var unitOfWork = new DapUnitOfWork(_connectionStrings.FlightDeckAPI))
            {
                var sqlQuery = ReferenceQuery.GET_BY_ID;
                var parameter = new DynamicParameters();
                parameter.Add("@Id", id);
                var result = await unitOfWork.ReferenceRepository.GetSingleAsync(sqlQuery, parameter);
                return result;
            }
        }

      
    }
}
