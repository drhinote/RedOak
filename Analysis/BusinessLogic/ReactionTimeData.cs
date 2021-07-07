using Roi.Data.Models;
using System;

namespace Roi.Data.BusinessLogic
{
    public static class ReactionTimeData
	{
		static ReportData _data;

		static double leftIndexStart_2s;
		static double leftThumbStart_2s;
		static double leftPinkyStart_2s;
		static double rightIndexStart_2s;
		static double rightThumbStart_2s;
		static double rightPinkyStart_2s;
		static double leftIndexStart_3s;
		static double leftThumbStart_3s;
		static double leftPinkyStart_3s;
		static double rightIndexStart_3s;
		static double rightThumbStart_3s;
		static double rightPinkyStart_3s;

		public static void PopulateReactionTimeData(this ReportData data, AnalysisResult excel)
		{
			_data = data;

			leftIndexStart_2s = excel.TestData.LeftHandTwoSymbol.StatisticalAnalysis.StartReaction.Index.Median;
			leftThumbStart_2s = excel.TestData.LeftHandTwoSymbol.StatisticalAnalysis.StartReaction.Thumb.Median;
			leftPinkyStart_2s = excel.TestData.LeftHandTwoSymbol.StatisticalAnalysis.StartReaction.Pinky.Median;

			rightIndexStart_2s = excel.TestData.RightHandTwoSymbol.StatisticalAnalysis.StartReaction.Index.Median;
			rightThumbStart_2s = excel.TestData.RightHandTwoSymbol.StatisticalAnalysis.StartReaction.Thumb.Median;
			rightPinkyStart_2s = excel.TestData.RightHandTwoSymbol.StatisticalAnalysis.StartReaction.Pinky.Median;

			leftIndexStart_3s = excel.TestData.LeftHandThreeSymbol.StatisticalAnalysis.StartReaction.Index.Median;
			leftThumbStart_3s = excel.TestData.LeftHandThreeSymbol.StatisticalAnalysis.StartReaction.Thumb.Median;
			leftPinkyStart_3s = excel.TestData.LeftHandThreeSymbol.StatisticalAnalysis.StartReaction.Pinky.Median;

			rightIndexStart_3s = excel.TestData.RightHandThreeSymbol.StatisticalAnalysis.StartReaction.Index.Median;
			rightThumbStart_3s = excel.TestData.RightHandThreeSymbol.StatisticalAnalysis.StartReaction.Thumb.Median;
			rightPinkyStart_3s = excel.TestData.RightHandThreeSymbol.StatisticalAnalysis.StartReaction.Pinky.Median;

			PopulateCognativeReaction();
			PopulateCognativeReactionTimeData();
			PopulateReactionTimeData();
		}

		static void PopulateCognativeReaction()
		{
			var leftCognativeReaction = new ReactionTime()
			{
				ThumbStart_2s = leftThumbStart_2s,
				IndexStart_2s = leftIndexStart_2s,
				PinkyStart_2s = leftPinkyStart_2s,

				ThumbStart_3s = leftThumbStart_3s,
				IndexStart_3s = leftIndexStart_3s,
				PinkyStart_3s = leftPinkyStart_3s,
			};

			var rightCognativeReaction = new ReactionTime()
			{
				ThumbStart_2s = rightThumbStart_2s,
				IndexStart_2s = rightIndexStart_2s,
				PinkyStart_2s = rightPinkyStart_2s,

				ThumbStart_3s = rightThumbStart_3s,
				IndexStart_3s = rightIndexStart_3s,
				PinkyStart_3s = rightPinkyStart_3s,
			};

			_data.LeftCognitiveReaction = Math.Round(Calculations.CognativeReaction(leftCognativeReaction));
			_data.RightCognitiveReaction = Math.Round(Calculations.CognativeReaction(rightCognativeReaction));
		}

		public static void PopulateCognativeReactionTimeData()
		{
			var leftCognativeReactionTime = new ReactionTime()
			{
				IndexStart_3s = leftIndexStart_3s,
				ThumbStart_3s = leftThumbStart_3s,
				PinkyStart_3s = leftPinkyStart_3s,
			};

			var rightCognativeReactionTime = new ReactionTime()
			{
				IndexStart_3s = rightIndexStart_3s,
				ThumbStart_3s = rightThumbStart_3s,
				PinkyStart_3s = rightPinkyStart_3s,
			};

			_data.LeftCognitiveReactionTime = Math.Round(Calculations.CognativeReactionTime(leftCognativeReactionTime));
			_data.RightCognitiveReactionTime = Math.Round(Calculations.CognativeReactionTime(rightCognativeReactionTime));
		}

		public static void PopulateReactionTimeData()
		{
			var leftReactionTime = new ReactionTime()
			{
				IndexStart_2s = leftIndexStart_2s,
				ThumbStart_2s = leftThumbStart_2s,
				PinkyStart_2s = leftPinkyStart_2s,
			};

			var rightReactionTime = new ReactionTime()
			{
				IndexStart_2s = rightIndexStart_2s,
				ThumbStart_2s = rightThumbStart_2s,
				PinkyStart_2s = rightPinkyStart_2s,
			};

			_data.LeftReactionTime = Math.Round(Calculations.ReactionTime(leftReactionTime));
			_data.RightReactionTime = Math.Round(Calculations.ReactionTime(rightReactionTime));
		}
	}
}
