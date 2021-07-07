using System.Collections.Generic;

namespace Roi.Data.Models
{
	public class ChartData1Series
	{
		public List<double> X { get; set; } = new List<double>();
		public List<double> Y { get; set; } = new List<double>();
	}

	public class ChartData2Series
	{
		public List<double> X { get; set; } = new List<double>();
		public List<double> Y1 { get; set; } = new List<double>();
		public List<double> Y2 { get; set; } = new List<double>();
	}

	public class ChartData3Series
	{
		public List<double> X { get; set; } = new List<double>();
		public List<double> Y1 { get; set; } = new List<double>();
		public List<double> Y2 { get; set; } = new List<double>();
		public List<double> Y3 { get; set; } = new List<double>();
	}
}
