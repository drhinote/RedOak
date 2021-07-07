using MathNet.Numerics.Distributions;
using Roi.Data.Models;
using System;

namespace Roi.Data
{
    public static class Calculations
	{
		public static double GetNormDist(double x, double mean, double standardDeviation, double filter)
		{
			var normalDist = new Normal(mean * 2, standardDeviation);
			return (x >= filter) ? (1 - normalDist.CumulativeDistribution(x)) : filter;
		}

		static double GetDelay(double time1, double time2, double average)
		{
			return Math.Abs(time1 - time2) * 268 / average;
		}

		static double GetAverage(double a, double b)
		{
			return (a + b) / 2;
		}

		static double GetAverage(double a, double b, double c)
		{
			return (a + b + c) / 3;
		}

		static double GetAverage(double a, double b, double c, double d)
		{
			return (a + b + c + d) / 4;
		}

		static double GetAverage(double a, double b, double c, double d, double e)
		{
			return (a + b + c + d + e) / 5;
		}

		static double GetStandardDeviation(double a, double b, double c)
		{
			var mean = GetAverage(a, b, c);
			var variance = (Math.Pow((a - mean), 2) + Math.Pow((b - mean), 2) + Math.Pow((c - mean), 2)) / 2;

			return Math.Sqrt(variance);
		}

		static double FilterLessThan(double value, double filter)
		{
			return (value < filter) ? filter : value;
		}

		static double FilterLessThan(double value, double filter, double min)
		{
			return (value < filter) ? min : value;
		}

		static double FilterGreaterThan(double value, double filter)
		{
			return (value < filter) ? value : filter;
		}

		static double Square(double value)
		{
			return Math.Pow(value, 2);
		}

		static double GetAbs(double fatigueMt, double fatigueMi, double fatigueM5)
		{
			return Math.Abs(fatigueMt + fatigueMi + fatigueM5) * 100 / 3;
		}

		static double GetQ(double x, double y, double a, double b, double c)
		{
			return x * Math.Pow(((Square(a - b) + Square(a - c) + Square(b - c)) / y), .5);
		}

		public static double CognativeReaction(ReactionTime rt)
		{
			return GetAverage(rt.ThumbStart_3s, rt.IndexStart_3s, rt.PinkyStart_3s) -
				GetAverage(rt.ThumbStart_2s, rt.IndexStart_2s, rt.PinkyStart_2s);
		}

		public static double CognativeReactionTime(ReactionTime rt)
		{
			return GetAverage(rt.IndexStart_3s, rt.ThumbStart_3s, rt.PinkyStart_3s);
		}

		public static double Correlation(double riseIndex2s, double riseThumb2s, double risePinky2s,
											double startIndex2s, double startThumb2s, double startPinky2s,
											double riseIndex3s, double riseThumb3s, double risePinky3s,
											double startIndex3s, double startThumb3s, double startPinky3s)
		{
			return GetAverage(
				Correlation_2s(riseIndex2s, riseThumb2s, risePinky2s,
								startIndex2s, startThumb2s, startPinky2s),
				Correlation_3s(riseIndex3s, riseThumb3s, risePinky3s,
								startIndex3s, startThumb3s, startPinky3s)
				);
		}

		public static double Correlation_2s(double riseIndex2s, double riseThumb2s, double risePinky2s,
											double startIndex2s, double startThumb2s, double startPinky2s)
		{
			var stdDeviationFilter = 0.0;
			/*K48*/var mean = 50.0;
			/*K49*/var standardDeviation = 48.0;

			/*F34*/var riseAverage = GetAverage(riseThumb2s, riseIndex2s, risePinky2s);
			/*H30*/var rtDelay2sThumb = GetDelay(riseIndex2s, risePinky2s, riseAverage);
			/*H31*/var rtDelay2sIndex = GetDelay(riseThumb2s, riseIndex2s, riseAverage);
			/*H32*/var rtDelay2sPinky =  GetDelay(riseThumb2s, risePinky2s, riseAverage);
			/*B50*/var rtDelay2sAve = GetAverage(rtDelay2sThumb, rtDelay2sIndex, rtDelay2sPinky);
			/*B51*/var norm2RtDelay = GetNormDist(rtDelay2sAve, mean, standardDeviation, stdDeviationFilter);
			/*B52*/var lim2RtDelay = FilterLessThan(norm2RtDelay, .01);

			/*B34*/var react2Average = GetAverage(startThumb2s, startIndex2s, startPinky2s);
			/*D30*/var reactDelay2sThumb = GetDelay(startIndex2s, startPinky2s, react2Average);
			/*D31*/var reactDelay2sIndex = GetDelay(startThumb2s, startIndex2s, react2Average);
			/*D32*/var reactDelay2sPinky = GetDelay(startThumb2s, startPinky2s, react2Average);
			/*F50*/var reactDelay2sAve = GetAverage(reactDelay2sThumb, reactDelay2sIndex, reactDelay2sPinky);
			/*F51*/var norm2ReactDelay = GetNormDist(reactDelay2sAve, mean, standardDeviation, stdDeviationFilter);
			/*F52*/var lim2ReactDelay = FilterLessThan(norm2ReactDelay, .01);
			/*N51*/return GetAverage(lim2RtDelay, lim2ReactDelay, lim2ReactDelay);
		}

		public static double Correlation_3s(double riseIndex3s, double riseThumb3s, double risePinky3s,
											double startIndex3s, double startThumb3s, double startPinky3s)
		{
			var stdDeviationFilter = 0.0;
			/*K48*/var mean = 50.0;
			/*K49*/var standardDeviation = 48.0;

			/*F44*/var riseTime3sAve = GetAverage(riseThumb3s, riseIndex3s, risePinky3s);
			/*H40*/var rtDelay3sThumb = GetDelay(riseIndex3s, risePinky3s, riseTime3sAve);
			/*H41*/var rtDelay3sIndex = GetDelay(riseThumb3s, riseIndex3s, riseTime3sAve);
			/*H42*/var trDelay3sPinky = GetDelay(riseThumb3s, risePinky3s, riseTime3sAve);
			/*D50*/var rtDelay3sAve = GetAverage(rtDelay3sThumb, rtDelay3sIndex, trDelay3sPinky);
			/*D51*/var norm3RtDelay = GetNormDist(rtDelay3sAve, mean, standardDeviation, stdDeviationFilter);
			/*D52*/var lim3RtDelay = FilterLessThan(norm3RtDelay, .01);
			/*B44*/var react3Average = GetAverage(startThumb3s, startIndex3s, startPinky3s);
			/*D40*/var reactDelay3sThumb = GetDelay(startIndex3s, startPinky3s, react3Average);
			/*D41*/var reactDelay3sIndex = GetDelay(startThumb3s, startIndex3s, react3Average);
			/*D42*/var reactDelay3sPinky = GetDelay(startThumb3s, startPinky3s, react3Average);
			/*H50*/var react3DelayAve = GetAverage(reactDelay3sThumb, reactDelay3sIndex, reactDelay3sPinky);
			/*H51*/var norm3ReactDelay = GetNormDist(react3DelayAve, mean, standardDeviation, stdDeviationFilter);
			/*H52*/var lim3ReactDelay = FilterLessThan(norm3ReactDelay, .01);
			/*P51*/return GetAverage(lim3RtDelay, lim3ReactDelay, lim3ReactDelay);
		}

		public static double FatigueVariance(FatigueVariance fv)
		{
			/*G8*/var fatigueStDev_2s = GetStandardDeviation(fv.FatigueMi_2s, fv.FatigueMt_2s, fv.FatigueM5_2s);
			/*G20*/var fatigueStDev_3s = GetStandardDeviation(fv.FatigueMi_3s, fv.FatigueMt_3s, fv.FatigueM5_3s);
			/*K8*/var fatigueVariance_2s = fatigueStDev_2s / 0.0044;
			/*K20*/var fatigueVariance_3s = fatigueStDev_3s / 0.0044;

			return GetAverage(fatigueVariance_2s, fatigueVariance_3s);
		}

		public static double InjuryEvidence(double sensory, double correlation)
		{
			// convert from percentage
			sensory *= 100;
			correlation *= 100;
			var ave = GetAverage(sensory, correlation);
			// scale value
			return (100 - ave) / 10;
		}

		public static double InjuryFlag(double left, double right)
		{
			// remove scale
			left = 100 - (left * 10);
			right = 100 - (right * 10);
			return Math.Min(left, right);
		}

		public static double MotorControl(MotorControl mc)
		{
			return GetAverage(MotorControl_2s(mc), MotorControl_3s(mc));
		}

		public static double MotorControl_2s(MotorControl mc)
		{
			/*D7*/var fatigueQf1_2s = GetQ(1.25, 0.003, mc.FatigueMt_2s, mc.FatigueMi_2s, mc.FatigueM5_2s);
			/*D8*/var fatigueQf2_2s = GetAbs(mc.FatigueMt_2s, mc.FatigueMi_2s, mc.FatigueM5_2s);
			/*D9*/var fatigueQf_2s = Math.Max(fatigueQf1_2s, fatigueQf2_2s);

			/*I7*/var correlationQr_2s = GetQ(50, mc.CorrelationR15_2s, mc.CorrelationR12_2s, mc.CorrelationR15_2s, mc.CorrelationR25_2s);
			/*I9*/var correlationQf_2s = FilterGreaterThan(correlationQr_2s, 6.25);

			/*L7*/var releaseReactionSdt_2s = mc.ReleaseReactionSdtDecay_2s / mc.ReleaseReactionSdtForce_2s;
			/*L8*/var releaseReactionDsi_2s = mc.ReleaseReactionSdiDecay_2s / mc.ReleaseReactionSdiForce_2s;
			/*L9*/var releaseReactionSdf_2s = mc.ReleaseReactionSdfDecay_2s / mc.ReleaseReactionSdfForce_2s;
			/*N7*/var releaseReactionQr_2s = 3 * (releaseReactionDsi_2s + releaseReactionSdt_2s + releaseReactionSdf_2s);
			/*N9*/var releaseReactionQf_2s = FilterGreaterThan(releaseReactionQr_2s, 6.25);

			/*Q7*/var startReactionSdt_2s = mc.StartReactionSdtDecay_2s / mc.StartReactionSdtForce_2s;
			/*Q8*/var startReactionSdi_2s = mc.StartReactionSdiDecay_2s / mc.StartReactionSdiForce_2s;
			/*Q9*/var startReactionSdf_2s = mc.StartReactionSdfDecay_2s / mc.StartReactionSdfForce_2s;
			/*S7*/var startReactionQr_2s = 3 * (startReactionSdi_2s + startReactionSdt_2s + startReactionSdf_2s);
			/*S9*/var startReactionQf_2s = FilterGreaterThan(startReactionQr_2s, 6.25);

			/*U9*/var motorSkg_2s = GetAverage(fatigueQf_2s, correlationQf_2s, releaseReactionQf_2s, startReactionQf_2s);

			/*Z9*/var morotFast2s = GetNormDist(motorSkg_2s, 1.6, 0.6, 0.02);

			/*Z10*/return FilterLessThan(morotFast2s, 0.01, 0.02);
		}

		public static double MotorControl_3s(MotorControl mc)
		{
			/*D29*/var fatigueQf1_3s = GetQ(1.25, 0.003, mc.FatigueMt_3s, mc.FatigueMi_3s, mc.FatigueM5_3s);
			/*D30*/var fatiqueQf2_3s = GetAbs(mc.FatigueMt_3s, mc.FatigueMi_3s, mc.FatigueM5_3s);
			/*D31*/var fatigueQf_3s = Math.Max(fatigueQf1_3s, fatiqueQf2_3s);

			/*I29*/var correlationQr_3s = GetQ(50, mc.CorrelationR15_3s, mc.CorrelationR12_3s, mc.CorrelationR15_3s, mc.CorrelationR25_3s);
			/*I31*/var correlationQf_3s = FilterGreaterThan(correlationQr_3s, 6.25);

			/*L29*/var releaseReactionSdt_3s = mc.ReleaseReactionSdtDecay_3s / mc.ReleaseReactionSdtForce_3s;
			/*L30*/var releaseReactionDsi_3s = mc.ReleaseReactionSdiDecay_3s / mc.ReleaseReactionSdiForce_3s;
			/*L31*/var releaseReactionSdf_3s = mc.ReleaseReactionSdfDecay_3s / mc.ReleaseReactionSdfForce_3s;
			/*N29*/var releaseReactionQr_3s = 3 * (releaseReactionDsi_3s + releaseReactionSdt_3s + releaseReactionSdf_3s);
			/*N31*/var releaseReactionQf_3s = FilterGreaterThan(releaseReactionQr_3s, 6.25);

			/*Q29*/var startReactionSdt_3s = mc.StartReactionSdtDecay_3s / mc.StartReactionSdtForce_3s;
			/*Q30*/var startReactionSdi_3s = mc.StartReactionSdiDecay_3s / mc.StartReactionSdiForce_3s;
			/*Q31*/var startReactionSdf_3s = mc.StartReactionSdfDecay_3s / mc.StartReactionSdfForce_3s;
			/*S29*/var startReaction_Qr_3s = 3 * (startReactionSdi_3s + startReactionSdt_3s + startReactionSdf_3s);
			/*S31*/var startReactionQf_3s = FilterGreaterThan(startReaction_Qr_3s, 6.25);

			/*U31*/var motorSkg_3s = GetAverage(fatigueQf_3s, correlationQf_3s, releaseReactionQf_3s, startReactionQf_3s);

			/*Z31*/var motorFast_3s = GetNormDist(motorSkg_3s, 1.6, 0.6, 0.01);

			/*Z32*/return FilterLessThan(motorFast_3s, 0.01, 0.02);
		}

		public static double ReactionTime(ReactionTime rt)
		{
			return GetAverage(rt.IndexStart_2s, rt.ThumbStart_2s, rt.PinkyStart_2s);
		}

		public static double SensoryControl(SensoryControl sc)
		{
			return GetAverage(SensoryControl_2s(sc), SensoryControl_3s(sc));
		}

		public static double SensoryControl_2s(SensoryControl sc)
		{
			/*D18*/var path12_2s = sc.Path12_2s;
			/*D19*/var sqrtPath12_2s = Math.Sqrt(path12_2s);
			/*D20*/var minPath12_2s = Math.Min(path12_2s, sqrtPath12_2s);

			/*I18*/var path15_2s = sc.Path15_2s;
			/*I19*/var sqrtPath15_2s = Math.Sqrt(path15_2s);
			/*I20*/var minPath15_2s = Math.Min(path15_2s, sqrtPath15_2s);

			/*N18*/var path25_2s = sc.Path25_2s;
			/*N19*/var sqrtPath25_2s = Math.Sqrt(path25_2s);
			/*N20*/var minPath25_2s = Math.Min(path25_2s, sqrtPath25_2s);

			/*Q18*/var releaseSdt_2s = sc.RiseTimeSdtMAD_2s / sc.RiseTimeSdt_2s;
			/*Q19*/var releaseSdi_2s = sc.RiseTimeSdiMAD_2s / sc.RiseTimeSdi_2s;
			/*Q20*/var releaseSdf_2s = sc.RiseTimeSdfMAD_2s / sc.RiseTimeSdf_2s;
			/*S18*/var releaseQbl_2s = 3 * (releaseSdt_2s + releaseSdi_2s + releaseSdf_2s);
			/*S20*/var releaseFiltered_2s = FilterGreaterThan(releaseQbl_2s, 6.25);

			/*V18*/var fatigueSdt_2s = sc.FallTimeSdtMAD_2s / sc.FallTimeSdt_2s;
			/*V19*/var fatigueSdi_2s = sc.FallTimeSdiMAD_2s / sc.FallTimeSdi_2s;
			/*V20*/var fatigueSdf_2s = sc.FallTimeSdfMAD_2s / sc.FallTimeSdf_2s;
			/*X18*/var fatigueQbl_2s = 3 * (fatigueSdt_2s + fatigueSdi_2s + fatigueSdf_2s);
			/*X20*/var fatigueFiltered_2s = FilterGreaterThan(fatigueQbl_2s, 6.25);

			/*V9*/var sensorySkg_2s = GetAverage(minPath12_2s, minPath15_2s, minPath25_2s, releaseFiltered_2s, fatigueFiltered_2s);
			/*AA9*/var sensoryNormalized_2s = GetNormDist(sensorySkg_2s, 1.6, 0.6, 0.01);
			/*AA10*/return FilterLessThan(sensoryNormalized_2s, 0.05);
		}

		public static double SensoryControl_3s(SensoryControl sc)
		{
			/*D40*/var path12_3s = sc.Path12_3s;
			/*D41*/var sqrtPath12_3s = Math.Sqrt(path12_3s);
			/*D42*/var minPath12_3s = Math.Min(path12_3s, sqrtPath12_3s);

			/*I40*/var path15_3s = sc.Path15_3s;
			/*I41*/var sqrtPath15_3s = Math.Sqrt(path15_3s);
			/*I42*/var minPath15_3s = Math.Min(path15_3s, sqrtPath15_3s);

			/*N40*/var path25_3s = sc.Path25_3s;
			/*N41*/var sqrtPath25_3s = Math.Sqrt(path25_3s);
			/*N42*/var minPath25_3s = Math.Min(path25_3s, sqrtPath25_3s);

			/*Q40*/var releaseSdt_3s = sc.RiseTimeSdtMAD_3s / sc.RiseTimeSdt_3s;
			/*Q41*/var releaseSdi_3s = sc.RiseTimeSdiMAD_3s / sc.RiseTimeSdi_3s;
			/*Q42*/var releaseSdf_3s = sc.RiseTimeSdfMAD_3s / sc.RiseTimeSdf_3s;
			/*S40*/var releaseQbl_3s = 3 * (releaseSdt_3s + releaseSdi_3s + releaseSdf_3s);
			/*S42*/var releaseFiltered_3s = FilterGreaterThan(releaseQbl_3s, 6.25);

			/*V40*/var fatigueSdt_3s = sc.FallTimeSdtMAD_3s / sc.FallTimeSdt_3s;
			/*V41*/var fatigueSdi_3s = sc.FallTimeSdiMAD_3s / sc.FallTimeSdi_3s;
			/*V42*/var fatigueSdf_3s = sc.FallTimeSdfMAD_3s / sc.FallTimeSdf_3s;
			/*X40*/var fatigueQbl_3s = 3 * (fatigueSdt_3s + fatigueSdi_3s + fatigueSdf_3s);
			/*X42*/var fatigueFiltered_3s = FilterGreaterThan(fatigueQbl_3s, 6.25);

			/*V31*/var sensorySkg_3s = GetAverage(minPath12_3s, minPath15_3s, minPath25_3s, releaseFiltered_3s, fatigueFiltered_3s);
			/*AA31*/var sensoryNormalized_3s = GetNormDist(sensorySkg_3s, 1.6, 0.6, 0.01);
			/*AA32*/return FilterLessThan(sensoryNormalized_3s, 0.05);
		}

		public static double Strength(double force_2s, double force_3s)
		{
			return GetAverage(force_2s, force_3s);
		}

		public static double StrengthRatio(double leftForce_2s, double leftForce_3s, double rightForce_2s, double rightForce_3s, bool rightHandDominant)
		{
			var leftForceAverage = (leftForce_2s + leftForce_3s) / 2;
			var rightForceAverage = (rightForce_2s + rightForce_3s) / 2;
			return (rightHandDominant) ? rightForceAverage / leftForceAverage : leftForceAverage / rightForceAverage;
		}
	}
}
