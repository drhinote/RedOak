using Roi.Data.Models;
using System;

namespace Roi.Data.BusinessLogic
{
    public static class MotorControlData
	{
		public static void PopulateMotorControl(this ReportData data, AnalysisResult excel)
		{
			try
			{
				var leftMotorControl = new MotorControl()
				{
					FatigueMi_2s = excel.TestData.LeftHandTwoSymbol.StatisticalAnalysis.FatigueSlope.Index,
					FatigueMt_2s = excel.TestData.LeftHandTwoSymbol.StatisticalAnalysis.FatigueSlope.Thumb,
					FatigueM5_2s = excel.TestData.LeftHandTwoSymbol.StatisticalAnalysis.FatigueSlope.Pinky,
					CorrelationR12_2s = excel.TestData.LeftHandTwoSymbol.StatisticalAnalysis.ThumbIndexCorr,
					CorrelationR15_2s = excel.TestData.LeftHandTwoSymbol.StatisticalAnalysis.ThumbPinkyCorr,
					CorrelationR25_2s = excel.TestData.LeftHandTwoSymbol.StatisticalAnalysis.IndexPinkyCorr,
					StartReactionSdiForce_2s = excel.TestData.LeftHandTwoSymbol.StatisticalAnalysis.StartReaction.Index.Median,
					StartReactionSdtForce_2s = excel.TestData.LeftHandTwoSymbol.StatisticalAnalysis.StartReaction.Thumb.Median,
					StartReactionSdfForce_2s = excel.TestData.LeftHandTwoSymbol.StatisticalAnalysis.StartReaction.Pinky.Median,
					ReleaseReactionSdiForce_2s = excel.TestData.LeftHandTwoSymbol.StatisticalAnalysis.ReleaseReaction.Index.Median,
					ReleaseReactionSdtForce_2s = excel.TestData.LeftHandTwoSymbol.StatisticalAnalysis.ReleaseReaction.Thumb.Median,
					ReleaseReactionSdfForce_2s = excel.TestData.LeftHandTwoSymbol.StatisticalAnalysis.ReleaseReaction.Pinky.Median,
					StartReactionSdiDecay_2s = excel.TestData.LeftHandTwoSymbol.StatisticalAnalysis.StartReaction.Index.MAD,
					StartReactionSdtDecay_2s = excel.TestData.LeftHandTwoSymbol.StatisticalAnalysis.StartReaction.Thumb.MAD,
					StartReactionSdfDecay_2s = excel.TestData.LeftHandTwoSymbol.StatisticalAnalysis.StartReaction.Pinky.MAD,
					ReleaseReactionSdiDecay_2s = excel.TestData.LeftHandTwoSymbol.StatisticalAnalysis.ReleaseReaction.Index.MAD,
					ReleaseReactionSdtDecay_2s = excel.TestData.LeftHandTwoSymbol.StatisticalAnalysis.ReleaseReaction.Thumb.MAD,
					ReleaseReactionSdfDecay_2s = excel.TestData.LeftHandTwoSymbol.StatisticalAnalysis.ReleaseReaction.Pinky.MAD,


                    FatigueMi_3s = excel.TestData.LeftHandThreeSymbol.StatisticalAnalysis.FatigueSlope.Index,
                    FatigueMt_3s = excel.TestData.LeftHandThreeSymbol.StatisticalAnalysis.FatigueSlope.Thumb,
                    FatigueM5_3s = excel.TestData.LeftHandThreeSymbol.StatisticalAnalysis.FatigueSlope.Pinky,
                    CorrelationR12_3s = excel.TestData.LeftHandThreeSymbol.StatisticalAnalysis.ThumbIndexCorr,
                    CorrelationR15_3s = excel.TestData.LeftHandThreeSymbol.StatisticalAnalysis.ThumbPinkyCorr,
                    CorrelationR25_3s = excel.TestData.LeftHandThreeSymbol.StatisticalAnalysis.IndexPinkyCorr,
					StartReactionSdiForce_3s = excel.TestData.LeftHandThreeSymbol.StatisticalAnalysis.StartReaction.Index.Median,
                    StartReactionSdtForce_3s = excel.TestData.LeftHandThreeSymbol.StatisticalAnalysis.StartReaction.Thumb.Median,
                    StartReactionSdfForce_3s = excel.TestData.LeftHandThreeSymbol.StatisticalAnalysis.StartReaction.Pinky.Median,
                    ReleaseReactionSdiForce_3s = excel.TestData.LeftHandThreeSymbol.StatisticalAnalysis.ReleaseReaction.Index.Median,
                    ReleaseReactionSdtForce_3s = excel.TestData.LeftHandThreeSymbol.StatisticalAnalysis.ReleaseReaction.Thumb.Median,
                    ReleaseReactionSdfForce_3s = excel.TestData.LeftHandThreeSymbol.StatisticalAnalysis.ReleaseReaction.Pinky.Median,
                    StartReactionSdiDecay_3s = excel.TestData.LeftHandThreeSymbol.StatisticalAnalysis.StartReaction.Index.MAD,
                    StartReactionSdtDecay_3s = excel.TestData.LeftHandThreeSymbol.StatisticalAnalysis.StartReaction.Thumb.MAD,
                    StartReactionSdfDecay_3s = excel.TestData.LeftHandThreeSymbol.StatisticalAnalysis.StartReaction.Pinky.MAD,
                    ReleaseReactionSdiDecay_3s = excel.TestData.LeftHandThreeSymbol.StatisticalAnalysis.ReleaseReaction.Index.MAD,
                    ReleaseReactionSdtDecay_3s = excel.TestData.LeftHandThreeSymbol.StatisticalAnalysis.ReleaseReaction.Thumb.MAD,
                    ReleaseReactionSdfDecay_3s = excel.TestData.LeftHandThreeSymbol.StatisticalAnalysis.ReleaseReaction.Pinky.MAD,
                };

				var rightMotorControl = new MotorControl()
				{
                    FatigueMi_2s = excel.TestData.RightHandTwoSymbol.StatisticalAnalysis.FatigueSlope.Index,
                    FatigueMt_2s = excel.TestData.RightHandTwoSymbol.StatisticalAnalysis.FatigueSlope.Thumb,
                    FatigueM5_2s = excel.TestData.RightHandTwoSymbol.StatisticalAnalysis.FatigueSlope.Pinky,
                    CorrelationR12_2s = excel.TestData.RightHandTwoSymbol.StatisticalAnalysis.ThumbIndexCorr,
                    CorrelationR15_2s = excel.TestData.RightHandTwoSymbol.StatisticalAnalysis.ThumbPinkyCorr,
                    CorrelationR25_2s = excel.TestData.RightHandTwoSymbol.StatisticalAnalysis.IndexPinkyCorr,
					StartReactionSdiForce_2s = excel.TestData.RightHandTwoSymbol.StatisticalAnalysis.StartReaction.Index.Median,
                    StartReactionSdtForce_2s = excel.TestData.RightHandTwoSymbol.StatisticalAnalysis.StartReaction.Thumb.Median,
                    StartReactionSdfForce_2s = excel.TestData.RightHandTwoSymbol.StatisticalAnalysis.StartReaction.Pinky.Median,
                    ReleaseReactionSdiForce_2s = excel.TestData.RightHandTwoSymbol.StatisticalAnalysis.ReleaseReaction.Index.Median,
                    ReleaseReactionSdtForce_2s = excel.TestData.RightHandTwoSymbol.StatisticalAnalysis.ReleaseReaction.Thumb.Median,
                    ReleaseReactionSdfForce_2s = excel.TestData.RightHandTwoSymbol.StatisticalAnalysis.ReleaseReaction.Pinky.Median,
                    StartReactionSdiDecay_2s = excel.TestData.RightHandTwoSymbol.StatisticalAnalysis.StartReaction.Index.MAD,
                    StartReactionSdtDecay_2s = excel.TestData.RightHandTwoSymbol.StatisticalAnalysis.StartReaction.Thumb.MAD,
                    StartReactionSdfDecay_2s = excel.TestData.RightHandTwoSymbol.StatisticalAnalysis.StartReaction.Pinky.MAD,
                    ReleaseReactionSdiDecay_2s = excel.TestData.RightHandTwoSymbol.StatisticalAnalysis.ReleaseReaction.Index.MAD,
                    ReleaseReactionSdtDecay_2s = excel.TestData.RightHandTwoSymbol.StatisticalAnalysis.ReleaseReaction.Thumb.MAD,
                    ReleaseReactionSdfDecay_2s = excel.TestData.RightHandTwoSymbol.StatisticalAnalysis.ReleaseReaction.Pinky.MAD,


                    FatigueMi_3s = excel.TestData.RightHandThreeSymbol.StatisticalAnalysis.FatigueSlope.Index,
                    FatigueMt_3s = excel.TestData.RightHandThreeSymbol.StatisticalAnalysis.FatigueSlope.Thumb,
                    FatigueM5_3s = excel.TestData.RightHandThreeSymbol.StatisticalAnalysis.FatigueSlope.Pinky,
                    CorrelationR12_3s = excel.TestData.RightHandThreeSymbol.StatisticalAnalysis.ThumbIndexCorr,
                    CorrelationR15_3s = excel.TestData.RightHandThreeSymbol.StatisticalAnalysis.ThumbPinkyCorr,
                    CorrelationR25_3s = excel.TestData.RightHandThreeSymbol.StatisticalAnalysis.IndexPinkyCorr,
					StartReactionSdiForce_3s = excel.TestData.RightHandThreeSymbol.StatisticalAnalysis.StartReaction.Index.Median,
                    StartReactionSdtForce_3s = excel.TestData.RightHandThreeSymbol.StatisticalAnalysis.StartReaction.Thumb.Median,
                    StartReactionSdfForce_3s = excel.TestData.RightHandThreeSymbol.StatisticalAnalysis.StartReaction.Pinky.Median,
                    ReleaseReactionSdiForce_3s = excel.TestData.RightHandThreeSymbol.StatisticalAnalysis.ReleaseReaction.Index.Median,
                    ReleaseReactionSdtForce_3s = excel.TestData.RightHandThreeSymbol.StatisticalAnalysis.ReleaseReaction.Thumb.Median,
                    ReleaseReactionSdfForce_3s = excel.TestData.RightHandThreeSymbol.StatisticalAnalysis.ReleaseReaction.Pinky.Median,
                    StartReactionSdiDecay_3s = excel.TestData.RightHandThreeSymbol.StatisticalAnalysis.StartReaction.Index.MAD,
                    StartReactionSdtDecay_3s = excel.TestData.RightHandThreeSymbol.StatisticalAnalysis.StartReaction.Thumb.MAD,
                    StartReactionSdfDecay_3s = excel.TestData.RightHandThreeSymbol.StatisticalAnalysis.StartReaction.Pinky.MAD,
                    ReleaseReactionSdiDecay_3s = excel.TestData.RightHandThreeSymbol.StatisticalAnalysis.ReleaseReaction.Index.MAD,
                    ReleaseReactionSdtDecay_3s = excel.TestData.RightHandThreeSymbol.StatisticalAnalysis.ReleaseReaction.Thumb.MAD,
                    ReleaseReactionSdfDecay_3s = excel.TestData.RightHandThreeSymbol.StatisticalAnalysis.ReleaseReaction.Pinky.MAD,
                };

				data.LeftMotorControl = Math.Round(Calculations.MotorControl(leftMotorControl), 2);
				data.RightMotorControl = Math.Round(Calculations.MotorControl(rightMotorControl), 2);

				data.LeftMotorControl2s = Math.Round(Calculations.MotorControl_2s(leftMotorControl), 2);
				data.RightMotorControl2s = Math.Round(Calculations.MotorControl_2s(rightMotorControl), 2);
				data.LeftMotorControl3s = Math.Round(Calculations.MotorControl_3s(leftMotorControl), 2);
				data.RightMotorControl3s = Math.Round(Calculations.MotorControl_3s(rightMotorControl), 2);

				// data for new report
				data.LeftThumbIndexPath2s = excel.TestData.LeftHandTwoSymbol.StatisticalAnalysis.ThumbIndexPath;
				data.LeftThumbSmallPath2s = excel.TestData.LeftHandTwoSymbol.StatisticalAnalysis.ThumbSmallPath;
				data.LeftIndexSmallPath2s = excel.TestData.LeftHandTwoSymbol.StatisticalAnalysis.IndexSmallPath;

				data.LeftThumbIndexPath3s = excel.TestData.LeftHandThreeSymbol.StatisticalAnalysis.ThumbIndexPath;
				data.LeftThumbSmallPath3s = excel.TestData.LeftHandThreeSymbol.StatisticalAnalysis.ThumbSmallPath;
				data.LeftIndexSmallPath3s = excel.TestData.LeftHandThreeSymbol.StatisticalAnalysis.IndexSmallPath;

				data.RightThumbIndexPath2s = excel.TestData.RightHandTwoSymbol.StatisticalAnalysis.ThumbIndexPath;
				data.RightThumbSmallPath2s = excel.TestData.RightHandTwoSymbol.StatisticalAnalysis.ThumbSmallPath;
				data.RightIndexSmallPath2s = excel.TestData.RightHandTwoSymbol.StatisticalAnalysis.IndexSmallPath;

				data.RightThumbIndexPath3s = excel.TestData.RightHandThreeSymbol.StatisticalAnalysis.ThumbIndexPath;
				data.RightThumbSmallPath3s = excel.TestData.RightHandThreeSymbol.StatisticalAnalysis.ThumbSmallPath;
				data.RightIndexSmallPath3s = excel.TestData.RightHandThreeSymbol.StatisticalAnalysis.IndexSmallPath;
			}
			catch (Exception e)
			{
				Error.LogError(e);
			}
		}
	}
}
