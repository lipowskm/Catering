using Catering.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Catering.Services
{
    public class CateringApiDataService : BaseHttpService, ICateringDataService
    {
        readonly Uri _baseUri;
        readonly IDictionary<string, string> _headers;

        public CateringApiDataService(Uri baseUri)
        {
            _baseUri = baseUri;
            _headers = new Dictionary<string, string>();

            // TODO: Add header with auth-based token in chapter 7
            _headers.Add("zumo-api-version", "2.0.0");
        }


        public async Task<IList<CateringEntry>> GetEntriesAsync()
        {
            var url = new Uri(_baseUri, "/tables/entry");
            var response = await SendRequestAsync<CateringEntry[]>(url,
                HttpMethod.Get, _headers);

            return response;
        }

        public async Task<CateringEntry> GetEntryAsync(string id)
        {
            var url = new Uri(_baseUri,
                string.Format("/tables/entry/{0}", id));
            var response = await SendRequestAsync<CateringEntry>(url,
                HttpMethod.Get, _headers);

            return response;
        }

        public async Task<CateringEntry> AddEntryAsync(CateringEntry entry)
        {
            var url = new Uri(_baseUri, "/tables/entry");
            var response = await SendRequestAsync<CateringEntry>(url,
                HttpMethod.Post, _headers, entry);

            return response;
        }

        public async Task<CateringEntry> UpdateEntryAsync(CateringEntry entry)
        {
            var url = new Uri(_baseUri,
                string.Format("/tables/entry/{0}", entry.Id));
            var response = await SendRequestAsync<CateringEntry>(url,
                new HttpMethod("PATCH"), _headers, entry);

            return response;
        }

        public async Task RemoveEntryAsync(CateringEntry entry)
        {
            var url = new Uri(_baseUri,
                string.Format("/tables/entry/{0}", entry.Id));
            var response = await SendRequestAsync<CateringEntry>(url,
                HttpMethod.Delete, _headers);
        }
    }
}
