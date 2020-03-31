using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoronavirusDashboardWebAPI.Models;
using CoronavirusWebHandler;
using CoronavirusWebHandler.Models;

namespace CoronavirusDashboardWebAPI.Services
{
    public class LocaleSpreadService
    {
        public LocaleSpreadModel getLocaleSpreadByCountries(string country)
        {
            LocaleSpreadModel localeSpreadModel = new LocaleSpreadModel();
            List<Case> cases = CoronaDataRequester.GetListOfCasesSinceDayOne(new List<string>() {country});
            localeSpreadModel.Country = cases.Select(x => x.Country).First();
            localeSpreadModel.NumberOfCases = cases.Select(x => x.Cases).Sum();

            return localeSpreadModel;
        }
    }
}
