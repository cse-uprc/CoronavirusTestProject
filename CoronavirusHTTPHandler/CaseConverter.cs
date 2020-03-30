using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Globalization;

namespace CoronavirusWebHandler
{
    internal static class CaseConverter
    {
        // Explicitly define rules for how to deserialize from/to JSONs for future reference.
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}
