using Roi.Data.Models;
using System;

namespace Roi.Data.BusinessLogic
{
    public static class FatigueVarianceData
	{
		public static void PopulateFatigueVarianceData(this ReportData data, AnalysisResult excel)
		{
			try
			{
				var leftFatigueVariance = new FatigueVariance()
				{
					FatigueMi_2s = excel.TestData.LeftHandTwoSymbol.StatisticalAnalysis.FatigueSlope.Index,
					FatigueMt_2s = excel.TestData.LeftHandTwoSymbol.StatisticalAnalysis.FatigueSlope.Thumb,
					FatigueM5_2s = excel.TestData.LeftHandTwoSymbol.StatisticalAnalysis.FatigueSlope.Pinky,

					FatigueMi_3s = excel.TestData.LeftHandThreeSymbol.StatisticalAnalysis.FatigueSlope.Index,
					FatigueMt_3s = excel.TestData.LeftHandThreeSymbol.StatisticalAnalysis.FatigueSlope.Thumb,
					FatigueM5_3s = excel.TestData.LeftHandThreeSymbol.StatisticalAnalysis.FatigueSlope.Pinky,
				};

				var rightFatigueVariance = new FatigueVariance()
				{
					FatigueMi_2s = excel.TestData.RightHandTwoSymbol.StatisticalAnalysis.FatigueSlope.Index,
					FatigueMt_2s = excel.TestData.RightHandTwoSymbol.StatisticalAnalysis.FatigueSlope.Thumb,
					FatigueM5_2s = excel.TestData.RightHandTwoSymbol.StatisticalAnalysis.FatigueSlope.Pinky,

					FatigueMi_3s = excel.TestData.RightHandThreeSymbol.StatisticalAnalysis.FatigueSlope.Index,
					FatigueMt_3s = excel.TestData.RightHandThreeSymbol.StatisticalAnalysis.FatigueSlope.Thumb,
					FatigueM5_3s = excel.TestData.RightHandThreeSymbol.StatisticalAnalysis.FatigueSlope.Pinky,
				};

				data.LeftFatigueVariance = Math.Round(Calculations.FatigueVariance(leftFatigueVariance), 3);
				data.RightFatigueVariance = Math.Round(Calculations.FatigueVariance(rightFatigueVariance), 3);
			}
			catch (Exception e)
			{
				Error.LogError(e);
			}
		}
	}
}
