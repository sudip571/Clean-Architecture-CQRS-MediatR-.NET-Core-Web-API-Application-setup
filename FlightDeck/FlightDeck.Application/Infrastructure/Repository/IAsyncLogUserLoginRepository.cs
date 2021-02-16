using FlightDeck.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FlightDeck.Application.Infrastructure.Repository
{
   public interface IAsyncLogUserLoginRepository
    {
        Task AddAsync(LogUserLogin model);
    }
}
