using Microsoft.WindowsAzure.Storage.Table;
using Roi.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Roi.Data
{
    public class ReportBundle
    {
        public string id { get; set; }

        public string CompanyId { get; set; }
        
        public ReportData ReportData { get; set; }
    }
}