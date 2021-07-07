using Roi.Data.Models;
using System;
using System.Collections.Generic;

namespace Roi.Data.BusinessLogic
{
    public static class PulseData
    {
        private static AnalysisResult _excel;

        public static void PopulatePulseData(this ReportData data, AnalysisResult excel)
        {
            _excel = excel;

            // add pulse and fatigue to report data
            data.LeftPulse_2s = Pulse(excel.TestData.LeftHandTwoSymbol.Charts.Main, excel.TestData.LeftHandTwoSymbol.Charts.Fatigue);
            data.RightPulse_2s = Pulse(excel.TestData.RightHandTwoSymbol.Charts.Main, excel.TestData.RightHandTwoSymbol.Charts.Fatigue);
            data.LeftPulse_3s = Pulse(excel.TestData.LeftHandThreeSymbol.Charts.Main, excel.TestData.LeftHandThreeSymbol.Charts.Fatigue);
            data.RightPulse_3s = Pulse(excel.TestData.RightHandThreeSymbol.Charts.Main, excel.TestData.RightHandThreeSymbol.Charts.Fatigue);

            // add 3rd pulse
            data.LeftPulse3_2s = PulseSingle(excel.TestData.LeftHandTwoSymbol.Pulses[2]);
            data.RightPulse3_2s = PulseSingle(excel.TestData.RightHandTwoSymbol.Pulses[2]);
            data.LeftPulse3_3s = PulseSingle(excel.TestData.LeftHandThreeSymbol.Pulses[2]);
            data.RightPulse3_3s = PulseSingle(excel.TestData.RightHandThreeSymbol.Pulses[2]);

            // add 3rd pulse force
            data.LeftPulseForce3_2s = PulseForce(excel.TestData.LeftHandTwoSymbol.Pulses[2]);
            data.RightPulseForce3_2s = PulseForce(excel.TestData.RightHandTwoSymbol.Pulses[2]);
            data.LeftPulseForce3_3s = PulseForce(excel.TestData.LeftHandThreeSymbol.Pulses[2]);
            data.RightPulseForce3_3s = PulseForce(excel.TestData.RightHandThreeSymbol.Pulses[2]);

            // add 7th pulse
            data.LeftPulse7_2s = PulseSingle(excel.TestData.LeftHandTwoSymbol.Pulses[6]);
            data.RightPulse7_2s = PulseSingle(excel.TestData.RightHandTwoSymbol.Pulses[6]);
            data.LeftPulse7_3s = PulseSingle(excel.TestData.LeftHandThreeSymbol.Pulses[6]);
            data.RightPulse7_3s = PulseSingle(excel.TestData.RightHandThreeSymbol.Pulses[6]);

            //add 7th pulse force
            data.LeftPulseForce7_2s = PulseForce(excel.TestData.LeftHandTwoSymbol.Pulses[6]);
            data.RightPulseForce7_2s = PulseForce(excel.TestData.RightHandTwoSymbol.Pulses[6]);
            data.LeftPulseForce7_3s = PulseForce(excel.TestData.LeftHandThreeSymbol.Pulses[6]);
            data.RightPulseForce7_3s = PulseForce(excel.TestData.RightHandThreeSymbol.Pulses[6]);
        }

        private static string Pulse(List<AnalysisResult.TimeForce<double>> main, List<AnalysisResult.TimeForce<double>> fatigue)
        {
            try
            {
                var lpData = new ChartData2Series();

                for (int i = 0; i < main.Count / 25; i++)
                {
                    var s = main[i * 25];
                    // time
                    lpData.X.Add(s.Time / 1000.0);
                    // force
                    lpData.Y1.Add(s.Force);
                    lpData.Y2.Add(double.NaN);
                }

                // fatigue slope
                fatigue.ForEach(s =>
                {
                    // time
                    lpData.X.Add(s.Time / 1000.0);
                    // force
                    lpData.Y2.Add(s.Force);
                    lpData.Y1.Add(double.NaN);
                });

                return lpData.ToJson("Pulse", "Fatigue");
            }
            catch (Exception e)
            {
                Error.LogError(e);
                return "";
            }
        }

        private static string PulseSingle(AnalysisResult.Pulse pulse)
        {
            try
            {
                var lp3Data = new ChartData3Series();
                for (int i = 0; i < pulse.Samples.Count / 25; i++)
                {
                    var s = pulse.Samples[i * 25];
                    // time
                    lp3Data.X.Add(s.Index / 1000.0);
                    // thumb
                    lp3Data.Y1.Add(s.Force.Thumb);
                    // index
                    lp3Data.Y2.Add(s.Force.Index);
                    // pinky
                    lp3Data.Y3.Add(s.Force.Pinky);
                }
                return lp3Data.ToJson();
            }
            catch (Exception e)
            {
                Error.LogError(e);
                return "";
            }
        }

        private static string PulseForce(AnalysisResult.Pulse pulse)
        {
            try
            {
                var lpf3Data = new ChartData3Series();
                for (int i = 0; i < pulse.Samples.Count / 25; i++)
                {
                    var s = pulse.Samples[i * 25];
                    // index
                    lpf3Data.X.Add(s.Force.Index);
                    // thumb
                    lpf3Data.Y1.Add(s.Force.Thumb);
                    lpf3Data.Y2.Add(double.NaN);
                    lpf3Data.Y3.Add(double.NaN);
                }
                for (int i = 0; i < pulse.Samples.Count / 25; i++)
                {
                    var s = pulse.Samples[i * 25];
                    // pinky
                    lpf3Data.X.Add(s.Force.Pinky);
                    // index
                    lpf3Data.Y2.Add(s.Force.Index);
                    lpf3Data.Y1.Add(double.NaN);
                    lpf3Data.Y3.Add(double.NaN);
                }
                for (int i = 0; i < pulse.Samples.Count / 25; i++)
                {
                    var s = pulse.Samples[i * 25];
                    // pinky
                    lpf3Data.X.Add(s.Force.Pinky);
                    // thumb
                    lpf3Data.Y3.Add(s.Force.Thumb);
                    lpf3Data.Y1.Add(double.NaN);
                    lpf3Data.Y2.Add(double.NaN);
                }
                
                return lpf3Data.ToJson("Thumb-Index", "Index-Small", "Thumb-Small");
            }
            catch (Exception e)
            {
                Error.LogError(e);
                return "";
            }
        }
    }
}
