namespace Roi.Data.Models
{
	public class ReportData
	{
		public string Uuid { get; set; }
		public long Time { get; set; }
		public string Company { get; set; }
		public string TestId { get; set; }
		public string OptionalId { get; set; }
		public string Gender { get; set; }
		public string Age { get; set; }
		public string DominantHand { get; set; }
		public bool RightHandDominant { get; set; }
		public string TestDate { get; set; } // used for Last Date
		public string TestDateHist1 { get; set; }
		public string TestDateHist2 { get; set; }
		public string StartDate { get; set; }
		public int TotalTests { get; set; } = 1;

		public double LeftCognitiveReactionTime { get; set; }
		public double RightCognitiveReactionTime { get; set; }
		public double LeftCorrelation { get; set; }
		public double RightCorrelation { get; set; }
		public double LeftCorrelation2s { get; set; }
		public double LeftCorrelation3s { get; set; }
		public double RightCorrelation2s { get; set; }
		public double RightCorrelation3s { get; set; }
		public double LeftFatigueVariance { get; set; }
		public double RightFatigueVariance { get; set; }
		public double LeftMotorControl { get; set; }
		public double RightMotorControl { get; set; }
		public double LeftReactionTime { get; set; }
		public double RightReactionTime { get; set; }
		public double LeftSensoryControl { get; set; }
		public double RightSensoryControl { get; set; }
		public double LeftStrength { get; set; }
		public double RightStrength { get; set; }
		public string LeftStrengthRatio { get; set; }
		public string RightStrengthRatio { get; set; }

		public double LeftCognitiveReaction { get; set; }
		public double RightCognitiveReaction { get; set; }

		public double LeftMotorControl2s { get; set; }
		public double RightMotorControl2s { get; set; }
		public double LeftMotorControl3s { get; set; }
		public double RightMotorControl3s { get; set; }

		public string LeftPulse_2s { get; set; }
		public string LeftPulse3_2s { get; set; }
		public string LeftPulseForce3_2s { get; set; }
		public string LeftPulse7_2s { get; set; }
		public string LeftPulseForce7_2s { get; set; }

		public string RightPulse_2s { get; set; }
		public string RightPulse3_2s { get; set; }
		public string RightPulseForce3_2s { get; set; }
		public string RightPulse7_2s { get; set; }
		public string RightPulseForce7_2s { get; set; }

		public string LeftPulse_3s { get; set; }
		public string LeftPulse3_3s { get; set; }
		public string LeftPulseForce3_3s { get; set; }
		public string LeftPulse7_3s { get; set; }
		public string LeftPulseForce7_3s { get; set; }

		public string RightPulse_3s { get; set; }
		public string RightPulse3_3s { get; set; }
		public string RightPulseForce3_3s { get; set; }
		public string RightPulse7_3s { get; set; }
		public string RightPulseForce7_3s { get; set; }

		public double LeftSensoryControl2s { get; set; }
		public double RightSensoryControl2s { get; set; }
		public double LeftSensoryControl3s { get; set; }
		public double RightSensoryControl3s { get; set; }

		public HistoryColumn LeftHist1 { get; set; } = new HistoryColumn();
		public HistoryColumn RightHist1 { get; set; } = new HistoryColumn();
		public HistoryColumn LeftHist2 { get; set; } = new HistoryColumn();
		public HistoryColumn RightHist2 { get; set; } = new HistoryColumn();

		public double LeftThumbIndexPath2s { get; set; }
		public double LeftThumbSmallPath2s { get; set; }
		public double LeftIndexSmallPath2s { get; set; }

		public double RightThumbIndexPath2s { get; set; }
		public double RightThumbSmallPath2s { get; set; }
		public double RightIndexSmallPath2s { get; set; }

		public double LeftThumbIndexPath3s { get; set; }
		public double LeftThumbSmallPath3s { get; set; }
		public double LeftIndexSmallPath3s { get; set; }

		public double RightThumbIndexPath3s { get; set; }
		public double RightThumbSmallPath3s { get; set; }
		public double RightIndexSmallPath3s { get; set; }

		/// <summary>
		/// New One Page Report Data
		/// </summary>
		public double LeftInjuryEvidence { get; set; }
		public double RightInjuryEvidence { get; set; }

		public double InjuryFlag { get; set; }

		public string TestHistory { get; set; }
	}
}
