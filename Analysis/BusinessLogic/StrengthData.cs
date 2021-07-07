using Roi.Data.Models;
using System;

namespace Roi.Data.BusinessLogic
{
    public static class StrengthData
	{
		static ReportData _data;
		static double leftForce_2s;
		static double leftForce_3s;
		static double rightForce_2s;
		static double rightForce_3s;

		public static void PopulateStrengthData(this ReportData data, AnalysisResult excel)
		{
			_data = data;

			leftForce_2s = excel.TestData.LeftHandTwoSymbol.StatisticalAnalysis.MaxThumbForce;
			leftForce_3s = excel.TestData.LeftHandThreeSymbol.StatisticalAnalysis.MaxThumbForce;
			rightForce_2s = excel.TestData.RightHandTwoSymbol.StatisticalAnalysis.MaxThumbForce;
			rightForce_3s = excel.TestData.RightHandThreeSymbol.StatisticalAnalysis.MaxThumbForce;

			Strength();
			StrengthRatio();
		}

		public static void Strength()
		{
			try
			{
				_data.LeftStrength = Math.Round(Calculations.Strength(leftForce_2s, leftForce_3s), 2);
				_data.RightStrength = Math.Round(Calculations.Strength(rightForce_2s, rightForce_3s), 2);
			}
			catch (Exception e)
			{
				Error.LogError(e);
			}
		}

		public static void StrengthRatio()
		{
			try
			{
                _data.LeftStrengthRatio = (!_data.RightHandDominant) ? Math.Round(Calculations.StrengthRatio(leftForce_2s, leftForce_3s, rightForce_2s, rightForce_3s, _data.RightHandDominant), 2).ToString() : "";
                _data.RightStrengthRatio = (_data.RightHandDominant) ? Math.Round(Calculations.StrengthRatio(leftForce_2s, leftForce_3s, rightForce_2s, rightForce_3s, _data.RightHandDominant), 2).ToString() : "";
            }
			catch (Exception e)
			{
				Error.LogError(e);
			}
		}
	}
}
