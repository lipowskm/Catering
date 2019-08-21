using System.Threading.Tasks;
using Android.Content;
using Android.Locations;
using Android.OS;
using Android.Runtime;
using Catering.Models;
using Catering.Services;
using Xamarin.Forms;

namespace Catering.Droid.Services
{
    public class LocationService
          : Java.Lang.Object, ILocationService, ILocationListener
    {
        TaskCompletionSource<Location> _tcs;

        public async Task<GeoCoords> GetGeoCoordinatesAsync()
        {
            var manager = (LocationManager)Forms.Context
                .GetSystemService(Context.LocationService);

            _tcs = new TaskCompletionSource<Location>();

            manager.RequestSingleUpdate("gps", this, null);

            var location = await _tcs.Task;

            return new GeoCoords
            {
                Latitude = location.Latitude,
                Longitude = location.Longitude
            };
        }

        public void OnLocationChanged(Location location)
        {
            _tcs.TrySetResult(location);
        }

        public void OnProviderDisabled(string provider)
        {
        }

        public void OnProviderEnabled(string provider)
        {
        }

        public void OnStatusChanged(string provider,
            [GeneratedEnum] Availability status, Bundle extras)
        {
        }
    }
}