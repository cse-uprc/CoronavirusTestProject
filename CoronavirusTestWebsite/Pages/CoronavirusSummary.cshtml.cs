using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

using CoronavirusWebHandler;
using CoronavirusWebHandler.Models;
using System.Reflection;
using System.Text;
using System.ComponentModel;

namespace CoronavirusTestWebsite.Pages
{
    public class CoronavirusSummaryModel : PageModel
    {
        private readonly ILogger<CoronavirusSummaryModel> _logger;
        public DataTable SummaryTable { get; set; }

        public CoronavirusSummaryModel(ILogger<CoronavirusSummaryModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            var CountrySummaryList = CoronaDataRequester.GetSummary();

            SummaryTable = PageHelper.ToDataTable(CountrySummaryList);
        }
        public List<Case> GetListOfAllConfirmedCasesInUnitedStates()
        {
            return CoronaDataRequester.GetListOfCasesSinceDayOne(new List<string> { "us"});
        }
    }
}
