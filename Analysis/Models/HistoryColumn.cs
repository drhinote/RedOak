namespace Roi.Data.Models
{
	public class HistoryColumn
	{
		public string Uuid { get; set; } = "";
		public string Side { get; set; } = "";
		public long Time { get; set; } = 0;
		public int TestType { get; set; } = 0;
		public double? FatigueVarience { get; set; } = 0;
		public double? Strength { get; set; } = 0;
		public string StrengthRatio { get; set; } = "";
		public double? MotorControl { get; set; } = 0;
		public double? SensoryControl { get; set; } = 0;
		public double? ReactionTime { get; set; } = 0;
		public double? CognitiveReactionTime { get; set; } = 0;
		public double? Correlation { get; set; } = 0;
	}
}
