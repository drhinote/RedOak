using System.Collections.Generic;

namespace Roi.Data.Models
{
    public class InjuryEvidenceHistory
	{
		public string Date { get; set; }
		public double Left { get; set; }
		public double Right { get; set; }
	}

	public class HistoryList
	{
		public List<InjuryEvidenceHistory> List { get; set; } = new List<InjuryEvidenceHistory>();
	}

	public class History
	{
		public long Time { get; set; }
		public decimal? SensoryControl { get; set; }
		public decimal? Correlation { get; set; }
		public string Side { get; set; }
	}
}
