using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace visitsvc.Models
{
    public class LocationJsonList
    {
        [JsonProperty("Locations")]
        public List<LocationJson> LocationList { get; set; }
    }
    
    public class LocationJson
    {
        public string Name { get; set; }
        public string Id { get; set; }
        public string? Type { get; set; }
        public string? TypeEng { get; set; }
        public string? Cntry { get; set; }
    }
}