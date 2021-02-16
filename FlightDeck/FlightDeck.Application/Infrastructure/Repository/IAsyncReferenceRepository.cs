using FlightDeck.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FlightDeck.Application.Infrastructure.Repository
{
    public interface IAsyncReferenceRepository 
    {
        Task<List<Reference>> GetAllAsync();
        Task<Reference> GetByIdAsync(int id);
        Task AddAsync(Reference model);
        Task UpdateAsync(Reference model);
        Task DeleteAsync(int Id);
    }
}
