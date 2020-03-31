using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoronavirusDashboardWebAPI.Models
{
    public class LocaleSpreadModel
    {
        public string Country { get; set; }

        public long NumberOfCases { get; set; }
    }
}
