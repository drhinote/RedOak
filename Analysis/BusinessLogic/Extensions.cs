using Newtonsoft.Json;
using Roi.Data.Models;
using System;

namespace Roi.Data.BusinessLogic
{
	public static class Extensions
	{
		public static int UtcOffset { get; set; }

		public static HistoryColumn BlankRecord(this HistoryColumn record)
		{
			return new HistoryColumn()
			{
				Uuid = "",
				Side = "",
				Time = 0,
				TestType = 0,
				FatigueVarience = 0,
				Strength = 0,
				StrengthRatio = "",
				MotorControl = 0,
				SensoryControl = 0,
				ReactionTime = 0,
				CognitiveReactionTime = 0,
				Correlation = 0,
			};
		}

		public static decimal Round(this decimal? number, int precision = 2)
		{

			return Math.Round(number ?? 0, precision);
		}

		public static string ToDateTimeString(this long testTimeMiliseconds)
		{
			var testDate = new DateTime(1970, 1, 1, 0, 0, 0);
			testDate = testDate.AddMilliseconds(testTimeMiliseconds);
			testDate = testDate.AddMinutes(UtcOffset);
			return testDate.ToString("MM/dd/yyyy hh:mm tt");
		}

		public static decimal ToDecimal(this double number)
		{
			return Convert.ToDecimal(number);
		}

		public static decimal? ToDecimal(this string number)
		{
			return number == "" ? 0 : Convert.ToDecimal(number);
		}

		public static double ToDouble(this object number)
		{
			return Convert.ToDouble(number);
		}

		
		public static string ToJson(this ChartData1Series data)
		{
			var size = data.Y.Count;

			var arr = new double[size][];

			for (int i = 0; i < size; i++)
			{
				arr[i] = new double[2];
				arr[i][0] = data.X[i];
				arr[i][1] = data.Y[i];
			}

			return arr.ToJson();;
		}

		public static string ToJson(this ChartData2Series data)
		{
			var size = data.Y1.Count;

			var arr = new double[size][];

			for (int i = 0; i < size; i++)
			{
				arr[i] = new double[3];
				arr[i][0] = data.X[i];
				arr[i][1] = data.Y1[i];
				arr[i][2] = data.Y2[i];
			}

			return arr.ToJson();;
		}

		public static string ToInjuryColor(this double value, string side)
		{
			// remove scale
			value = 100 - (value * 10);

			if (value > 89 && side == "left") return "lightGreen";
			if (value > 89 && side == "right") return "green";

			if (value > 79 && side == "left") return "lightYellow";
			if (value > 79 && side == "right") return "yellow";

			if (side == "left") return "pink";
			return "red";
		}

		public static double ToInjuryEvidence(this History hist)
		{
			return Calculations.InjuryEvidence(hist.SensoryControl.ToDouble(), hist.Correlation.ToDouble());
		}

		public static string ToJson(this ChartData2Series data, string s1Name, string s2Name)
		{
			var size = data.Y1.Count;

			var arr = new double[size][];

			for (int i = 0; i < size; i++)
			{
				arr[i] = new double[3];
				arr[i][0] = data.X[i];
				arr[i][1] = data.Y1[i];
				arr[i][2] = data.Y2[i];
			}

			var dataTable = arr.ToJson();;
			// insert the series labels
			dataTable = dataTable.Insert(1, "['X', '" + s1Name + "', '" + s2Name + "'], ");

			return dataTable;
		}

		public static string ToJson(this ChartData3Series data)
		{
			var size = data.Y1.Count;

			var arr = new double[size][];

			for (int i = 0; i < size; i++)
			{
				arr[i] = new double[4];
				arr[i][0] = data.X[i];
				arr[i][1] = data.Y1[i];
				arr[i][2] = data.Y2[i];
				arr[i][3] = data.Y3[i];
			}

			return arr.ToJson();;
		}

		public static string ToJson(this ChartData3Series data, string s1Name, string s2Name, string s3Name)
		{
			var size = data.Y1.Count;

			var arr = new double[size][];

			for (int i = 0; i < size; i++)
			{
				arr[i] = new double[4];
				arr[i][0] = data.X[i];
				arr[i][1] = data.Y1[i];
				arr[i][2] = data.Y2[i];
				arr[i][3] = data.Y3[i];
			}

			var dataTable = arr.ToJson();
			// insert the series labels
			dataTable = dataTable.Insert(1, "['X', '" + s1Name + "', '" + s2Name + "', '" + s3Name + "'], ");

			return dataTable;
		}

		public static string ToJson(this object data)
		{
			return JsonConvert.SerializeObject(data, new JsonSerializerSettings { FloatFormatHandling = FloatFormatHandling.Symbol });
		}

		public static string ToJson(this HistoryList hist)
		{ // need to go to single series
			var size = hist.List.Count;

			string dt = "[['Date', 'Left', {type: 'string', role: 'annotation'}, 'Right', {type: 'string', role: 'annotation'}], ";

			for (int i = 0; i < size; i++)
			{
				if (i != 0) dt += ", ";
				dt += "['" + hist.List[i].Date + "', ";
				dt += hist.List[i].Left + ", ";
				dt += "'Left', ";
				dt += hist.List[i].Right + ", ";
				dt += "'Right' ";
				dt += "] ";
			}

			dt += "]";

			return dt;
		}
	}
}
