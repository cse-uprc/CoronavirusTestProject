
using System;
using System.Collections.Generic;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CoronavirusWebHandler.Models
{
    public partial class Case
    {
        [JsonProperty(PropertyName = "Country")]
        public string Country { get; set; }
        
        [JsonProperty(PropertyName = "Province")]
        public string Province { get; set; }
        
        [JsonProperty(PropertyName = "Lat")]
        public double Lat { get; set; }
        
        [JsonProperty(PropertyName = "Lon")]
        public double Lon { get; set; }
        
        [JsonProperty(PropertyName = "Date")]
        public DateTimeOffset Date { get; set; }
        
        [JsonProperty(PropertyName = "Cases")]
        public long Cases { get; set; }

        [JsonProperty(PropertyName = "Status")]
        public string Status { get; set; }
    }

    public partial class Case
    {
        public static Case FromJson(string json) => JsonConvert
            .DeserializeObject<Case>(json, CaseConverter.Settings);

        public static List<Case> FromJsonToList(string json) => JsonConvert
            .DeserializeObject<List<Case>>(json, CaseConverter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this Case self) => JsonConvert.SerializeObject(self, CaseConverter.Settings);
    }
}
