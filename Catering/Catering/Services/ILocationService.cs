using Catering.Models;
using System.Threading.Tasks;

namespace Catering.Services
{
    public interface ILocationService
    {
        Task<GeoCoords> GetGeoCoordinatesAsync();
    }
}
