using Roi.Data.Models;
using System;

namespace Roi.Data.BusinessLogic
{
	public class ModelData
	{
		AnalysisResult _excel;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="excel"></param>
		public ModelData(AnalysisResult excel)
		{
			_excel = excel;
		}

		/// <summary>
		/// Process the data in the excel template and then populate the report data model with the values
		/// </summary>
		public ReportData LoadModels(string uuid, long time)
		{
			try
			{
				// create a new ReportData
				var data = new ReportData();

				// set general test data (used by HistoryData)
				data.Uuid = uuid;
				data.Time = time;

				data.PopulateCorrelation(_excel);
				data.PopulateDemographicsData(_excel);
				data.PopulateFatigueVarianceData(_excel);
				// added data for new reprot in PopulateMotorControl
				data.PopulateMotorControl(_excel);
				data.PopulatePulseData(_excel);
				data.PopulateSensoryData(_excel);
				data.PopulateReactionTimeData(_excel);
				
				// this class depends on values from Demographics
				data.PopulateStrengthData(_excel);

				// This class depends on values from SensoryControl and Correlation
				data.PopulateInjuryEvidenceData();
				
				// This class depends on values from Strength
				data.PopulateHistoryData();

				return data;
			}
			catch (Exception e)
			{
				Error.LogError(e);
				return null;
			}
		}
	}
}
