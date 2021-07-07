using Roi.Data.Models;
using System;

namespace Roi.Data.BusinessLogic
{
    public static class CorrelationData
	{
		public static void PopulateCorrelation(this ReportData data, AnalysisResult excel)
		{
			var LriseIndex2s = excel.TestData.LeftHandTwoSymbol.StatisticalAnalysis.RiseTime.Index.Median;
			var LriseThumb2s = excel.TestData.LeftHandTwoSymbol.StatisticalAnalysis.RiseTime.Thumb.Median;
			var LrisePinky2s = excel.TestData.LeftHandTwoSymbol.StatisticalAnalysis.RiseTime.Pinky.Median;
			var LstartIndex2s = excel.TestData.LeftHandTwoSymbol.StatisticalAnalysis.StartReaction.Index.Median;
			var LstartThumb2s = excel.TestData.LeftHandTwoSymbol.StatisticalAnalysis.StartReaction.Thumb.Median;
			var LstartPinky2s = excel.TestData.LeftHandTwoSymbol.StatisticalAnalysis.StartReaction.Pinky.Median;

			var LriseIndex3s = excel.TestData.LeftHandThreeSymbol.StatisticalAnalysis.RiseTime.Index.Median;
			var LriseThumb3s = excel.TestData.LeftHandThreeSymbol.StatisticalAnalysis.RiseTime.Thumb.Median;
			var LrisePinky3s = excel.TestData.LeftHandThreeSymbol.StatisticalAnalysis.RiseTime.Pinky.Median;
			var LstartIndex3s = excel.TestData.LeftHandThreeSymbol.StatisticalAnalysis.StartReaction.Index.Median;
			var LstartThumb3s = excel.TestData.LeftHandThreeSymbol.StatisticalAnalysis.StartReaction.Thumb.Median;
			var LstartPinky3s = excel.TestData.LeftHandThreeSymbol.StatisticalAnalysis.StartReaction.Pinky.Median;

			data.LeftCorrelation = Math.Round(Calculations.Correlation(LriseIndex2s, LriseThumb2s, LrisePinky2s,
									LstartIndex2s, LstartThumb2s, LstartPinky2s,
									LriseIndex3s, LriseThumb3s, LrisePinky3s,
									LstartIndex3s, LstartThumb3s, LstartPinky3s), 2);

			data.LeftCorrelation2s = Math.Round(Calculations.Correlation_2s(LriseIndex2s, LriseThumb2s, LrisePinky2s,
									LstartIndex2s, LstartThumb2s, LstartPinky2s), 2);

			data.LeftCorrelation3s = Math.Round(Calculations.Correlation_3s(LriseIndex3s, LriseThumb3s, LrisePinky3s,
									LstartIndex3s, LstartThumb3s, LstartPinky3s), 2);

			var RriseIndex2s = excel.TestData.RightHandTwoSymbol.StatisticalAnalysis.RiseTime.Index.Median;
            var RriseThumb2s = excel.TestData.RightHandTwoSymbol.StatisticalAnalysis.RiseTime.Thumb.Median;
            var RrisePinky2s = excel.TestData.RightHandTwoSymbol.StatisticalAnalysis.RiseTime.Pinky.Median;
            var RstartIndex2s = excel.TestData.RightHandTwoSymbol.StatisticalAnalysis.StartReaction.Index.Median;
            var RstartThumb2s = excel.TestData.RightHandTwoSymbol.StatisticalAnalysis.StartReaction.Thumb.Median;
            var RstartPinky2s = excel.TestData.RightHandTwoSymbol.StatisticalAnalysis.StartReaction.Pinky.Median;

            var RriseIndex3s = excel.TestData.RightHandThreeSymbol.StatisticalAnalysis.RiseTime.Index.Median;
			var RriseThumb3s = excel.TestData.RightHandThreeSymbol.StatisticalAnalysis.RiseTime.Thumb.Median;
			var RrisePinky3s = excel.TestData.RightHandThreeSymbol.StatisticalAnalysis.RiseTime.Pinky.Median;
			var RstartIndex3s = excel.TestData.RightHandThreeSymbol.StatisticalAnalysis.StartReaction.Index.Median;
			var RstartThumb3s = excel.TestData.RightHandThreeSymbol.StatisticalAnalysis.StartReaction.Thumb.Median;
			var RstartPinky3s = excel.TestData.RightHandThreeSymbol.StatisticalAnalysis.StartReaction.Pinky.Median;

			data.RightCorrelation = Math.Round(Calculations.Correlation(RriseIndex2s, RriseThumb2s, RrisePinky2s,
											RstartIndex2s, RstartThumb2s, RstartPinky2s,
											RriseIndex3s, RriseThumb3s, RrisePinky3s,
											RstartIndex3s, RstartThumb3s, RstartPinky3s), 2);

			data.RightCorrelation2s = Math.Round(Calculations.Correlation_2s(RriseIndex2s, RriseThumb2s, RrisePinky2s,
											RstartIndex2s, RstartThumb2s, RstartPinky2s), 2);

			data.RightCorrelation3s = Math.Round(Calculations.Correlation_3s(RriseIndex3s, RriseThumb3s, RrisePinky3s,
											RstartIndex3s, RstartThumb3s, RstartPinky3s), 2);
		}
	}
}
