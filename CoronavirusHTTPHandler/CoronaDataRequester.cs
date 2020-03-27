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

        public static List<Case> GetListOfCasesSinceDayOne(List<string> CountrySlugs)
        // Returns all cases since Day one of COVID-19 for each country in the list passed.
        {
            List<Case> AllRelevantCases = new List<Case>();

            // For each country, join all of its cases together into a single list.
            CountrySlugs.ForEach(async cs =>
                AllRelevantCases
                .AddRange(await GetCasesPerCountry(cs))
            );

            return AllRelevantCases;
        }

        public static List<Country> GetSummary()
        {
            string CountryListJSON = GetRequest("summary");
            var CountryListWrapper = JsonSerializer.Deserialize<SummaryWrapper>(CountryListJSON);

            return CountryListWrapper.Countries;
        }

        private static async Task<List<Case>> GetCasesPerCountry(string CountrySlug)
        {
            const string DAYONE_GET_FORMAT = "dayone/country/{0}/status/confirmed";
            string DayOneGet = String.Format(DAYONE_GET_FORMAT, CountrySlug);

            string SerializedResponse = await GetRequestAsync(DayOneGet);

            return JsonSerializer.Deserialize<List<Case>>(SerializedResponse);
        }

        private static string GetRequest(string EndpointName)
        // Sends a GET request to the specified endpoint and returns the result.
        {
            string URL = URI(EndpointName);
            string EndpointResponseString;

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
            string URL = URI(EndpointName);

            string EndpointResponseString;

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
            string URL = URI(EndpointName);
            string EndpointResponseString;

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
            string URL = URI(EndpointName);

            string EndpointResponseString;

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

        private static string URI(string EndpointName)
        {
            return CoronaDataRequester.COVID19_API_URL + EndpointName;
        }
    }
}
