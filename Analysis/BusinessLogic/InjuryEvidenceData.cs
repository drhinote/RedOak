using Roi.Data.Models;
using System;

namespace Roi.Data.BusinessLogic
{
	public static class InjuryEvidenceData
	{
		public static void PopulateInjuryEvidenceData(this ReportData data)
		{
			try
			{
				var leftSensory = data.LeftSensoryControl;
				var leftCorrelation = data.LeftCorrelation;
				data.LeftInjuryEvidence = Math.Round(Calculations.InjuryEvidence(leftSensory, leftCorrelation), 2);

				var rightSensory = data.RightSensoryControl;
				var rightCorrelation = data.RightCorrelation;
				data.RightInjuryEvidence = Math.Round(Calculations.InjuryEvidence(rightSensory, rightCorrelation), 2);

				data.InjuryFlag = Calculations.InjuryFlag(data.LeftInjuryEvidence, data.RightInjuryEvidence);
			}
			catch (Exception e)
			{
				Error.LogError(e);
			}
		}
	}
}
