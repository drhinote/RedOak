using Roi.Data.Models;
using System;

namespace Roi.Data.BusinessLogic
{
    public static class SensoryControlData
	{
		public static void PopulateSensoryData(this ReportData data, AnalysisResult excel)
		{
			try
			{
				var leftSensoryControl = new SensoryControl()
				{
					RiseTimeSdi_2s = excel.TestData.LeftHandTwoSymbol.StatisticalAnalysis.RiseTime.Index.Median,
					RiseTimeSdt_2s = excel.TestData.LeftHandTwoSymbol.StatisticalAnalysis.RiseTime.Thumb.Median,
					RiseTimeSdf_2s = excel.TestData.LeftHandTwoSymbol.StatisticalAnalysis.RiseTime.Pinky.Median,

					FallTimeSdi_2s = excel.TestData.LeftHandTwoSymbol.StatisticalAnalysis.FallTime.Index.Median,
					FallTimeSdt_2s = excel.TestData.LeftHandTwoSymbol.StatisticalAnalysis.FallTime.Thumb.Median,
					FallTimeSdf_2s = excel.TestData.LeftHandTwoSymbol.StatisticalAnalysis.FallTime.Pinky.Median,

					RiseTimeSdiMAD_2s = excel.TestData.LeftHandTwoSymbol.StatisticalAnalysis.RiseTime.Index.MAD,
					RiseTimeSdtMAD_2s = excel.TestData.LeftHandTwoSymbol.StatisticalAnalysis.RiseTime.Thumb.MAD,
					RiseTimeSdfMAD_2s = excel.TestData.LeftHandTwoSymbol.StatisticalAnalysis.RiseTime.Pinky.MAD,					

					FallTimeSdiMAD_2s = excel.TestData.LeftHandTwoSymbol.StatisticalAnalysis.FallTime.Index.MAD,
					FallTimeSdtMAD_2s = excel.TestData.LeftHandTwoSymbol.StatisticalAnalysis.FallTime.Thumb.MAD,
					FallTimeSdfMAD_2s = excel.TestData.LeftHandTwoSymbol.StatisticalAnalysis.FallTime.Pinky.MAD,

					Path12_2s = excel.TestData.LeftHandTwoSymbol.StatisticalAnalysis.ThumbIndexPathRatio,
					Path15_2s = excel.TestData.LeftHandTwoSymbol.StatisticalAnalysis.ThumbPinkyPathRatio,
					Path25_2s = excel.TestData.LeftHandTwoSymbol.StatisticalAnalysis.IndexPinkyPathRatio,

                    RiseTimeSdi_3s = excel.TestData.LeftHandThreeSymbol.StatisticalAnalysis.RiseTime.Index.Median,
                    RiseTimeSdt_3s = excel.TestData.LeftHandThreeSymbol.StatisticalAnalysis.RiseTime.Thumb.Median,
                    RiseTimeSdf_3s = excel.TestData.LeftHandThreeSymbol.StatisticalAnalysis.RiseTime.Pinky.Median,

                    FallTimeSdi_3s = excel.TestData.LeftHandThreeSymbol.StatisticalAnalysis.FallTime.Index.Median,
                    FallTimeSdt_3s = excel.TestData.LeftHandThreeSymbol.StatisticalAnalysis.FallTime.Thumb.Median,
                    FallTimeSdf_3s = excel.TestData.LeftHandThreeSymbol.StatisticalAnalysis.FallTime.Pinky.Median,

                    RiseTimeSdiMAD_3s = excel.TestData.LeftHandThreeSymbol.StatisticalAnalysis.RiseTime.Index.MAD,
                    RiseTimeSdtMAD_3s = excel.TestData.LeftHandThreeSymbol.StatisticalAnalysis.RiseTime.Thumb.MAD,
                    RiseTimeSdfMAD_3s = excel.TestData.LeftHandThreeSymbol.StatisticalAnalysis.RiseTime.Pinky.MAD,

                    FallTimeSdiMAD_3s = excel.TestData.LeftHandThreeSymbol.StatisticalAnalysis.FallTime.Index.MAD,
                    FallTimeSdtMAD_3s = excel.TestData.LeftHandThreeSymbol.StatisticalAnalysis.FallTime.Thumb.MAD,
                    FallTimeSdfMAD_3s = excel.TestData.LeftHandThreeSymbol.StatisticalAnalysis.FallTime.Pinky.MAD,

                    Path12_3s = excel.TestData.LeftHandThreeSymbol.StatisticalAnalysis.ThumbIndexPathRatio,
                    Path15_3s = excel.TestData.LeftHandThreeSymbol.StatisticalAnalysis.ThumbPinkyPathRatio,
                    Path25_3s = excel.TestData.LeftHandThreeSymbol.StatisticalAnalysis.IndexPinkyPathRatio,
                };

				var rightSensoryControl = new SensoryControl()
				{
                    RiseTimeSdi_2s = excel.TestData.RightHandTwoSymbol.StatisticalAnalysis.RiseTime.Index.Median,
                    RiseTimeSdt_2s = excel.TestData.RightHandTwoSymbol.StatisticalAnalysis.RiseTime.Thumb.Median,
                    RiseTimeSdf_2s = excel.TestData.RightHandTwoSymbol.StatisticalAnalysis.RiseTime.Pinky.Median,

                    FallTimeSdi_2s = excel.TestData.RightHandTwoSymbol.StatisticalAnalysis.FallTime.Index.Median,
                    FallTimeSdt_2s = excel.TestData.RightHandTwoSymbol.StatisticalAnalysis.FallTime.Thumb.Median,
                    FallTimeSdf_2s = excel.TestData.RightHandTwoSymbol.StatisticalAnalysis.FallTime.Pinky.Median,
              
                    RiseTimeSdiMAD_2s = excel.TestData.RightHandTwoSymbol.StatisticalAnalysis.RiseTime.Index.MAD,
                    RiseTimeSdtMAD_2s = excel.TestData.RightHandTwoSymbol.StatisticalAnalysis.RiseTime.Thumb.MAD,
                    RiseTimeSdfMAD_2s = excel.TestData.RightHandTwoSymbol.StatisticalAnalysis.RiseTime.Pinky.MAD,

                    FallTimeSdiMAD_2s = excel.TestData.RightHandTwoSymbol.StatisticalAnalysis.FallTime.Index.MAD,
                    FallTimeSdtMAD_2s = excel.TestData.RightHandTwoSymbol.StatisticalAnalysis.FallTime.Thumb.MAD,
                    FallTimeSdfMAD_2s = excel.TestData.RightHandTwoSymbol.StatisticalAnalysis.FallTime.Pinky.MAD,

                    Path12_2s = excel.TestData.RightHandTwoSymbol.StatisticalAnalysis.ThumbIndexPathRatio,
                    Path15_2s = excel.TestData.RightHandTwoSymbol.StatisticalAnalysis.ThumbPinkyPathRatio,
                    Path25_2s = excel.TestData.RightHandTwoSymbol.StatisticalAnalysis.IndexPinkyPathRatio,

                    RiseTimeSdi_3s = excel.TestData.RightHandThreeSymbol.StatisticalAnalysis.RiseTime.Index.Median,
                    RiseTimeSdt_3s = excel.TestData.RightHandThreeSymbol.StatisticalAnalysis.RiseTime.Thumb.Median,
                    RiseTimeSdf_3s = excel.TestData.RightHandThreeSymbol.StatisticalAnalysis.RiseTime.Pinky.Median,

                    FallTimeSdi_3s = excel.TestData.RightHandThreeSymbol.StatisticalAnalysis.FallTime.Index.Median,
                    FallTimeSdt_3s = excel.TestData.RightHandThreeSymbol.StatisticalAnalysis.FallTime.Thumb.Median,
                    FallTimeSdf_3s = excel.TestData.RightHandThreeSymbol.StatisticalAnalysis.FallTime.Pinky.Median,

                    RiseTimeSdiMAD_3s = excel.TestData.RightHandThreeSymbol.StatisticalAnalysis.RiseTime.Index.MAD,
                    RiseTimeSdtMAD_3s = excel.TestData.RightHandThreeSymbol.StatisticalAnalysis.RiseTime.Thumb.MAD,
                    RiseTimeSdfMAD_3s = excel.TestData.RightHandThreeSymbol.StatisticalAnalysis.RiseTime.Pinky.MAD,

                    FallTimeSdiMAD_3s = excel.TestData.RightHandThreeSymbol.StatisticalAnalysis.FallTime.Index.MAD,
                    FallTimeSdtMAD_3s = excel.TestData.RightHandThreeSymbol.StatisticalAnalysis.FallTime.Thumb.MAD,
                    FallTimeSdfMAD_3s = excel.TestData.RightHandThreeSymbol.StatisticalAnalysis.FallTime.Pinky.MAD,

                    Path12_3s = excel.TestData.RightHandThreeSymbol.StatisticalAnalysis.ThumbIndexPathRatio,
                    Path15_3s = excel.TestData.RightHandThreeSymbol.StatisticalAnalysis.ThumbPinkyPathRatio,
                    Path25_3s = excel.TestData.RightHandThreeSymbol.StatisticalAnalysis.IndexPinkyPathRatio,
                };

				data.LeftSensoryControl = Math.Round(Calculations.SensoryControl(leftSensoryControl), 2);
				data.RightSensoryControl = Math.Round(Calculations.SensoryControl(rightSensoryControl), 2);

				data.LeftSensoryControl2s = Math.Round(Calculations.SensoryControl_2s(leftSensoryControl), 2);
				data.RightSensoryControl2s = Math.Round(Calculations.SensoryControl_2s(rightSensoryControl), 2);
				data.LeftSensoryControl3s = Math.Round(Calculations.SensoryControl_3s(leftSensoryControl), 2);
				data.RightSensoryControl3s = Math.Round(Calculations.SensoryControl_3s(rightSensoryControl), 2);
			}
			catch (Exception e)
			{
				Error.LogError(e);
			}
		}
	}
}
