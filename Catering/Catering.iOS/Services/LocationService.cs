using System.Threading.Tasks;
using Catering.Models;
using Catering.Services;
using CoreLocation;
using UIKit;

namespace Catering.iOS.Services
{
    public class LocationService : ILocationService
    {
        CLLocationManager _manager;
        TaskCompletionSource<CLLocation> _tcs;

        public async Task<GeoCoords> GetGeoCoordinatesAsync()
        {
            _manager = new CLLocationManager();
            _tcs = new TaskCompletionSource<CLLocation>();

            if (UIDevice.CurrentDevice.CheckSystemVersion(8, 0))
            {
                _manager.RequestWhenInUseAuthorization();
            }

            _manager.LocationsUpdated += (s, e) =>
            {
                _tcs.TrySetResult(e.Locations[0]);
            };

            _manager.StartUpdatingLocation();

            var location = await _tcs.Task;

            return new GeoCoords
            {
                Latitude = location.Coordinate.Latitude,
                Longitude = location.Coordinate.Longitude
            };
        }
    }
}