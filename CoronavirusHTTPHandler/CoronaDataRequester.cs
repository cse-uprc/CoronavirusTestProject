using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using CoronavirusWebHandler.Models;
using CoronavirusWebHandler.Models.Responses;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace CoronavirusWebHandler
{
    public static class CoronaDataRequester
    {
        private static string COVID19_API_URL = "https://api.covid19api.com/";
        private static string GetURL(string EndpointName) => COVID19_API_URL + EndpointName; 

        public static List<Case> GetListOfCasesSinceDayOne(List<string> CountrySlugs)
        // Returns all cases since Day one of COVID-19 for each country in the list passed.
        {
            List<Case> AllRelevantCases = new List<Case>();

            // For each country, join all of its cases together into a single list.
            CountrySlugs.ForEach(async cs =>
                AllRelevantCases
                .AddRange(await GetAllCasesPerCountry(cs))
            );

            return AllRelevantCases;
        }

        public static List<Country> GetSummary()
        {
            string CountryListJSON = GetRequest("summary");
            var CountryListWrapper = JsonSerializer.Deserialize<SummaryWrapper>(CountryListJSON);

            return CountryListWrapper.Countries;
        }

        // ----------------------------------------------------------------------

        private static async Task<List<Case>> GetAllCasesPerCountry(string CountrySlug)
        {
            // Format strings to be used below.
            const string DAYONE_GET_CONFIRMED_FORMAT = "dayone/country/{0}/status/confirmed";
            const string DAYONE_GET_DEATHS_FORMAT = "dayone/country/{0}/status/deaths";
            const string DAYONE_GET_RECOVERED_FORMAT = "dayone/country/{0}/status/recovered";

            // Prepare get requests by formatting strings
            string ConfirmedCasesURL = String.Format(DAYONE_GET_CONFIRMED_FORMAT, CountrySlug);
            string DeathsURL = String.Format(DAYONE_GET_DEATHS_FORMAT, CountrySlug);
            string RecoveredURL = String.Format(DAYONE_GET_RECOVERED_FORMAT, CountrySlug);

            // Get all confirmed cases, deaths, and recoveries for the provided country by GET request
            List<Case> ConfirmedList = Case.FromJsonToList(await GetRequestAsync(ConfirmedCasesURL));
            List<Case> DeathsList = Case.FromJsonToList(await GetRequestAsync(DeathsURL));
            List<Case> RecoveredList = Case.FromJsonToList(await GetRequestAsync(RecoveredURL));

            // Merge these three lists into one singular list.
            List<Case> AllRelevantCases = new List<Case>();
            AllRelevantCases.AddRange(ConfirmedList);
            AllRelevantCases.AddRange(DeathsList);
            AllRelevantCases.AddRange(RecoveredList);

            // Return this list.
            return AllRelevantCases;
        }

        // ----------------------------------------------------------------

        private static string GetRequest(string EndpointName)
        // Sends a GET request to the specified endpoint and returns the result.
        {
            string EndpointResponseString;
            string URL = GetURL(EndpointName);

            HttpWebRequest Request = (HttpWebRequest)WebRequest.Create(URL);
            Request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using (HttpWebResponse Response = (HttpWebResponse)Request.GetResponse())
            {
                using (Stream stream = Response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        EndpointResponseString = reader.ReadToEnd();
                    }
                }
            }

            return EndpointResponseString;
        }

        private static string PostRequest(string EndpointName, string JsonEncodedRequestBody)
        {
            byte[] Data = Encoding.UTF8.GetBytes(JsonEncodedRequestBody);

            string EndpointResponseString;
            string URL = GetURL(EndpointName);

            // Set up the post request format and way to be used
            HttpWebRequest PostRequest = (HttpWebRequest)WebRequest.Create(URL);
            PostRequest.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            PostRequest.ContentLength = Data.Length;
            PostRequest.ContentType = "JSON";
            PostRequest.Method = "POST";

            // Write the JsonEncodedData to the request's body
            using (Stream requestBody = PostRequest.GetRequestStream())
            {
                requestBody.Write(Data, 0, Data.Length);
            }

            // Return the string-formatted response from the endpoint to the user, passing the body.
            using (HttpWebResponse Response = (HttpWebResponse)PostRequest.GetResponse())
            {
                using (Stream stream = Response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        EndpointResponseString = reader.ReadToEnd();
                    }
                }
            }

            return EndpointResponseString;
        }

        private static async Task<string> GetRequestAsync(string EndpointName)
        // Sends a GET request to the specified endpoint and returns the result.
        {
            string EndpointResponseString;
            string URL = GetURL(EndpointName);

            HttpWebRequest Request = (HttpWebRequest)WebRequest.Create(URL);
            Request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using (HttpWebResponse Response = (HttpWebResponse)Request.GetResponse())
            {
                using (Stream stream = Response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        EndpointResponseString = await reader.ReadToEndAsync();
                    }
                }
            }

            return EndpointResponseString;
        }

        private static async Task<string> PostRequestAsync(string EndpointName, string JsonEncodedRequestBody)
        {
            byte[] Data = Encoding.UTF8.GetBytes(JsonEncodedRequestBody);

            string EndpointResponseString;
            string URL = GetURL(EndpointName);

            // Set up the post request format and way to be used
            HttpWebRequest PostRequest = (HttpWebRequest)WebRequest.Create(URL);
            PostRequest.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            PostRequest.ContentLength = Data.Length;
            PostRequest.ContentType = "JSON";
            PostRequest.Method = "POST";

            // Write the JsonEncodedData to the request's body
            using (Stream requestBody = PostRequest.GetRequestStream())
            {
                requestBody.Write(Data, 0, Data.Length);
            }

            // Return the string-formatted response from the endpoint to the user, passing the body.
            using (HttpWebResponse Response = (HttpWebResponse)PostRequest.GetResponse())
            {
                using (Stream stream = Response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        EndpointResponseString = await reader.ReadToEndAsync();
                    }
                }
            }

            return EndpointResponseString;
        }
    }
}
