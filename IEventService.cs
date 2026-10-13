using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RaceDay.Core.Entities;
using RaceDay.Core.DTOs;

namespace RaceDay.Core.Interfaces
{
    public interface IEventService
    {
        Task<List<Event>> GetAllAsync();
        Task<Event> GetByIdAsync(int id);
        Task<Event> CreateAsync(CreateEventDto dto, int organiserId);
        Task<Event> UpdateAsync(int id, UpdateEventDto dto, int organiserId);
        Task<bool> DeleteAsync(int id, int organiserId);
    }
}
