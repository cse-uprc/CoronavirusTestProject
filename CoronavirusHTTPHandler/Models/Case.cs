using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoronavirusWebHandler.Models
{
    public class Case
    {
        public string Country { get; set; }
        
        public string Province { get; set; }
        
        [JsonProperty(PropertyName = "Lat")]
        public decimal Latitude { get; set; }
        
        [JsonProperty(PropertyName = "Lon")]
        public decimal Longitude { get; set; }
        
        [JsonProperty(PropertyName = "Date")]
        public DateTime DateOfConfirmation { get; set; }
        
        [JsonProperty(PropertyName = "Cases")]
        public int NumberOfCases { get; set; }

        public string Status { get; set; }
    }
}
