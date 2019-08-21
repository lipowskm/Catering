using Catering.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catering.Services
{
    public interface ICateringDataService
    {
        Task<IList<CateringEntry>> GetEntriesAsync();
        Task<CateringEntry> GetEntryAsync(string id);
        Task<CateringEntry> AddEntryAsync(CateringEntry entry);
        Task<CateringEntry> UpdateEntryAsync(CateringEntry entry);
        Task RemoveEntryAsync(CateringEntry entry);
    }
}
