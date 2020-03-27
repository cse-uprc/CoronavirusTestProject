using Nest;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace CoronavirusWebHandler.Models
{
    public class Country
    {
        [JsonProperty(PropertyName = "Country")]
        public string CountryName { get; set; }

        [JsonProperty(PropertyName = "CountrySlug")]
        public string CountrySlug { get; set; }

        public int NewConfirmed { get; set; }

        public int TotalConfirmed { get; set; }

        public int NewDeaths { get; set; }

        public int TotalDeaths { get; set; }

        public int NewRecovered { get; set; }

        public int TotalRecovered { get; set; }
    }
}
