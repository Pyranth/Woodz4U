using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Woodz4U.Models;

namespace Woodz4U
{
    /// <summary>
    /// Static class as storage for summary reports. Maybe singleton is better choice.
    /// If data needs to be preserved, use DB or file system.
    /// </summary>
    public static class SummariesStorage
    {
        public static List<SummaryModel> Summaries { get; set; }
    }
}
