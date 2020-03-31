using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoronavirusDashboardWebAPI.Models;
using CoronavirusDashboardWebAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace CoronavirusDashboardWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LocaleSpreadController
    {
        private readonly LocaleSpreadService _localeSpreadService;

        public LocaleSpreadController(LocaleSpreadService localeSpreadService)
        {
            _localeSpreadService = localeSpreadService;
        }

        [HttpGet("{country}")]
        public LocaleSpreadModel Get(string country)
        {
            return _localeSpreadService.getLocaleSpreadByCountries(country);
        }
    }
}
