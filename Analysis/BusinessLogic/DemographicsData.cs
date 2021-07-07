using Roi.Data.Models;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Roi.Data.BusinessLogic
{
	public static class DemographicsData
	{
		public static void PopulateDemographicsData(this ReportData data, AnalysisResult excel)
		{
			try
			{
				data.Company = excel.CompanyName;
			
				//var longId = excel.Workbook.Worksheets["Insert 2S Data File"].Cells["A1"].Value.ToString();
				data.TestId = excel.LongId; //longId.Remove(longId.Length - 14);
				data.OptionalId = excel.OpId;
				data.Gender = excel.Sex;
				data.Age = ((int)excel.Age).ToString();
				data.DominantHand = excel.DominantHand;
				data.RightHandDominant = data.DominantHand.ToLower() == "right";
			//	data.Gender = ParseGender(data.Uuid, data.Time);
			}
			catch (Exception e)
			{
				Error.LogError(e);
			}
		}
	}
}
