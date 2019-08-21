using Newtonsoft.Json;
using System;

namespace Catering.Models
{
    public class CateringEntry
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        public string Title { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime Date { get; set; }
        public int Rating { get; set; }
        public string Notes { get; set; }
    }
}
