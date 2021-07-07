using Roi.Analysis.Api.Filters;
using Roi.Analysis.Api.Models;
using Roi.Data;
using Roi.Logic;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Web.Mvc;

namespace Roi.Analysis.Api.Controllers
{
	[WebEndpoint]
	public class GenerateResultsController : ControllerBase
	{
        // POST: GenerateResults
        public string Post(int type)
		{
			try
			{
                //var res = AnalysisLogic.Analyze(Test, );

				//var funzizizies = File.ReadAllText(outputFilePath);

				return "";
			}
			catch (Exception e)
			{
				return e.Message + "\n\r" + e.StackTrace;
			}
		}
	}
}