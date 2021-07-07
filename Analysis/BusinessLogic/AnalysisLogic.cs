using System;
using System.Linq;
using System.Collections.Generic;
using Roi.Data;
using static Roi.Data.AnalysisResult;
using MathNet.Numerics.Statistics;
using MathNet.Filtering.FIR;
using System.Net;
using Newtonsoft.Json;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Roi.Analysis.Api.Models;
using System.Threading.Tasks;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents;
using System.Configuration;

namespace Roi.Logic
{
    //uuid           time            opid    dob                  hardware                                                                                           
    //RICE-1-00023	1463258354095	pain5	12/12/1973	0x006403076B5A0000007FEB500000089DCD5B076B58000001812016000010BA18A4076B590000027E6D240000089B39FC	
    // history
    // 486234,0,8383312,8,10341723,486232,1,8462358,16,12196004,486233,2,8285476,8,10172924,{*****}Gender*{&}$Male${&}*Race*{&}$${&}*Dominant hand*{&}$Right${&}*Height*{&}$${&}*Height units*{&}$${&}*Weight*{&}$${&}*Weight units*{&}$${&}*General health*{&}$${&}*Smoker*{&}$${&}*Caffeine*{&}$${&}*Repetitive activities*{&}$${&}*Fine Motor activities*{&}$${&}*Type of work*{&}$${&}*Left hand/wrist numbness*{&}$${&}*Right hand/wrist numbness*{&}$${&}*Left physical pain*{&}$${&}*Right physical pain*{&}$${&}*Left hand weakness/poor coordination*{&}$${&}*Right hand weakness/poor coordination*{&}$${&}*Considers left hand normal*{&}$${&}*Considers right hand normal*{&}$${&}*Diagnosed with TBI*{&}$${&}*Diagnosed with PTSD*{&}$${&}*Headaches*{&}$${&}*Nausea*{&}$${&}*Balance problems*{&}$${&}*Fatigued*{&}$${&}*Concentration/Memory problems*{&}$${&}*TBI Date, event 1*{&}$${&}*TBI Cause, event 1*{&}$${&}*TBI Care Received, event 1*{&}$${&}*TBI was Diagnosed, event 1*{&}$${&}*TBI allowed recovery, event 1*{&}$${&}*TBI Date, event 2*{&}$${&}*TBI Cause, event 2*{&}$${&}*TBI Care Received, event 2*{&}$${&}*TBI was Diagnosed, event 2*{&}$${&}*TBI allowed recovery, event 2*{&}$${&}*Military service*{&}$${&}*Number of tours*{&}$${&}*Military comments*{&}$${&}*Sport(s) played*{&}$${&}*Sports # years played*{&}$${&}*Sports comments*{&}$${&}*
    // 0x7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C	
    // 0x7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B	
    // 0x7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C7C	
    // 0x7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B7B	
    // Alice	riceUniversity	NULL	NULL	NULL	NULL	NULL
    public static class AnalysisLogic
    {
        public static int THUMB = 1;
        public static int INDEX = 0;
        public static int PINKY = 2;
        /**
         * Index of the serial number  
         */
        public static int SERIAL = 0;
        /**
         * Base data value, ~8,000,000
         */
        public static int OFFSET = 1;
        /**
         * Index of the Upper data value, ~10,000,000
         */
        public static int MAX = 3;
        /**
         * Index of the maximum load value EX: 8 lbs
         */
        public static int LOAD = 2;

        private static Func<double, AnalysisResult.DeviceData, double> BytesToForce = (raw, device) => Math.Round(((raw - device.Min)
            / (device.Max - device.Min)) * (device.Capacity / 2.20462), 6);

        private static int RightPulseDelimiter = 124;
        private static int LeftPulseDelimiter = 123;

        static DocumentClient Client = new DocumentClient(new Uri("https://roi.documents.azure.com:443/"), "mdKXWHu7FgVhDaFxTnUzgn8FjikFTssQh7Hx0Y8kIn4M03Pa9PRgmgQcNPMQMcRvgLyt55gXcL4GwLshFGs5bg==");

        private class MyWebClient : WebClient
        {
            protected override WebRequest GetWebRequest(Uri uri)
            {
                WebRequest w = base.GetWebRequest(uri);
                w.Timeout = 20 * 60 * 1000;
                return w;
            }
        }

        public static Func<string, ReportBundle> GenerateReport { get; set; } = nid =>
        {
            var client = new MyWebClient();
            client.Headers.Add(HttpRequestHeader.Accept, "application/json");           
            return JsonConvert.DeserializeObject<ReportBundle>(client.DownloadString("http://" + ConfigurationManager.AppSettings["labviewServer"] + "/Analysis/" + nid));

        };

        public static async Task<ReportBundle> RetrieveReport(string id, string companyId)
        {
            //var uri = UriFactory.CreateDocumentUri("Reports", "Models", id);
            ReportBundle model;
            //try
            //{
            //    model = await Client.ReadDocumentAsync<ReportBundle>(uri, new RequestOptions { PartitionKey = new PartitionKey(companyId) });
            //}
            //catch (DocumentClientException ex)
            //{
            //    if (ex.StatusCode == HttpStatusCode.NotFound && GenerateReport != null)
            //    {
                   model = GenerateReport(id);
            //       await Client.UpsertDocumentAsync(UriFactory.CreateDocumentCollectionUri("Reports", "Models"), model);
            //    }
            //    else
            //    {
            //        throw ex;
            //    }
            //}

            return model;
        }


        private static List<Pulse> LoadTestPart(AnalysisResult analysis, byte[] data)
        {
            List<Pulse> res = new List<Pulse>();
            Pulse Current = new Pulse();
            TimeForce<Trio<Double>> max = null;

            TimeForce<Trio<Double>> min = null;
            TimeForce<Trio<Double>> start = null;
            TimeForce<Trio<Double>> stop = null;
            List<Trio<Double>> window = new List<Trio<double>>();

            res.Add(Current);
            bool started = true;
            var nextDeim = false;
            for (int i = 0; i < data.Length; i += 6)
            {
                var delimiter = IsDelimitSample(data, i);
                if (delimiter) nextDeim = true;
                if (started && !delimiter)
                {
                    started = false;
                }
                else if (!started && delimiter)
                {
                    max.MaxSample = true;
                    min.MinSample = true;
                    start.IsMarker = true;
                    stop.IsMarker = true;
                    Current.Max = max;
                    Current.Min = min;
                    Current.Start = start;
                    Current.Stop = stop;
                    window.Clear();
                    start = null;
                    stop = null;
                    max = null;
                    min = null;
                    started = true;
                    Current = new Pulse();
                    res.Add(Current);
                }

                var newSample = new TimeForce<Trio<Double>>
                {
                    Time = i / 6,
                    IsDelimiter = false,
                    IsMarker = IsMarkerSample(data, i),
                    MaxSample = false
                };
                if (newSample.IsMarker)
                {
                    if (start == null) start = Current.Samples.Last();
                    else if (stop == null) stop = Current.Samples.Last();
                }
                else if (!delimiter)
                {
                    if (nextDeim)
                    {
                        nextDeim = false;
                        newSample.IsDelimiter = true;
                    }
                    var values = new Trio<double>
                    {
                        Index = (data[i + (analysis.Hardware.Index.DataIndex * 2) + 1] /*& ~0x7*/) * 256 + data[i + (analysis.Hardware.Index.DataIndex * 2)] * 65536,
                        Thumb = (data[i + (analysis.Hardware.Thumb.DataIndex * 2) + 1] /*& ~0x7*/) * 256 + data[i + (analysis.Hardware.Thumb.DataIndex * 2)] * 65536,
                        Pinky = (data[i + (analysis.Hardware.Pinky.DataIndex * 2) + 1] /*& ~0x7*/) * 256 + data[i + (analysis.Hardware.Pinky.DataIndex * 2)] * 65536,
                    };
                    if (!(values.Index < 200000 || values.Pinky < 200000 || values.Thumb < 200000))
                    {
                        window.Add(values);
                        if (window.Count > 17)
                        {
                            window.RemoveAt(0);
                            AddSample(analysis, Current, window, newSample);
                            if ((min == null || min.Force == null || newSample.Force.Thumb < min.Force.Thumb) && max != null && stop == null) min = newSample;
                            if (max == null || max.Force == null || newSample.Force.Thumb > max.Force.Thumb) max = newSample;

                        }
                    }
                    else
                    {
                        AddSample(analysis, Current, window, newSample);
                    }
                }


            }
            max.MaxSample = true;
            min.MinSample = true;
            start.IsMarker = true;
            stop.IsMarker = true;
            Current.Max = max;
            Current.Min = min;
            Current.Start = start;
            Current.Stop = stop;



            return res;
        }

        private static void AddSample(AnalysisResult analysis, Pulse Current, List<Trio<double>> window, TimeForce<Trio<double>> newSample)
        {
            newSample.Index = Current.Samples.Count;
            Current.Samples.Add(newSample);
            newSample.Force = new Trio<double>
            {
                Index = Math.Round(BytesToForce(window.Sum(w => w.Index) / window.Count, analysis.Hardware.Index), 6, MidpointRounding.AwayFromZero),
                Thumb = Math.Round(BytesToForce(window.Sum(w => w.Thumb) / window.Count, analysis.Hardware.Thumb), 6, MidpointRounding.AwayFromZero),
                Pinky = Math.Round(BytesToForce(window.Sum(w => w.Pinky) / window.Count, analysis.Hardware.Pinky), 6, MidpointRounding.AwayFromZero),
            };
        }

        public class PulseParts
        {
            public List<TimeForce<Trio<Double>>> Rise { get; set; }
            public List<TimeForce<Trio<Double>>> Fall { get; set; }
        }

        public static void GetReactionTimes(this TestContainer test)
        {
            var parts = test.Pulses.Select(p => new PulseParts
            {
                Rise = p.Samples.Skip(80 + p.Start.Index).Take(620).ToList(),
                Fall = p.Samples.Skip(80 + p.Stop.Index).Take(620).ToList(),
            });
            var reactionTimes = parts.Select(p =>
            {
                return ExtractTiming(p);
            });
            test.StatisticalAnalysis.StartReaction = new Trio<MedianAndError>
            {
                Index = MedianMAD(reactionTimes.Select(r => r.Index.riseReaction)),
                Thumb = MedianMAD(reactionTimes.Select(r => r.Thumb.riseReaction)),
                Pinky = MedianMAD(reactionTimes.Select(r => r.Pinky.riseReaction))
            };
            test.StatisticalAnalysis.RiseTime = new Trio<MedianAndError>
            {
                Index = MedianMAD(reactionTimes.Select(r => r.Index.rise)),
                Thumb = MedianMAD(reactionTimes.Select(r => r.Thumb.rise)),
                Pinky = MedianMAD(reactionTimes.Select(r => r.Pinky.rise))
            };
            test.StatisticalAnalysis.ReleaseReaction = new Trio<MedianAndError>
            {
                Index = MedianMAD(reactionTimes.Select(r => r.Index.fallReaction)),
                Thumb = MedianMAD(reactionTimes.Select(r => r.Thumb.fallReaction)),
                Pinky = MedianMAD(reactionTimes.Select(r => r.Pinky.fallReaction))
            };
            test.StatisticalAnalysis.FallTime = new Trio<MedianAndError>
            {
                Index = MedianMAD(reactionTimes.Select(r => r.Index.fall)),
                Thumb = MedianMAD(reactionTimes.Select(r => r.Thumb.fall)),
                Pinky = MedianMAD(reactionTimes.Select(r => r.Pinky.fall))
            };
        }

        private static double ParseDouble(this string input)
        {
            return String.IsNullOrWhiteSpace(input) ? 0L : double.Parse(input);
        }

        public static Dictionary<string, string> ParseHistory(string whackSerialization)
        {
            if (whackSerialization.Contains("{*****}")) whackSerialization = whackSerialization.Split(new string[] { "{*****}" }, StringSplitOptions.None)[1];
            var history = whackSerialization.Replace('/', ' ').Split(new string[] { "${&}*" }, StringSplitOptions.RemoveEmptyEntries).ToDictionary(k => k.Remove(k.IndexOf('*')), v => v.Substring(v.IndexOf('$') + 1));
            return history;
        }

        public static string UnparseHistory(Dictionary<string, string> answers, string hardware)
        {
            return hardware + ",{*****}" + string.Join("${&}*", answers.Select(a => a.Key + "*{&}$" + a.Value));
        }

        public static AnalysisResult Analyze(this Test test, BinaryData data)
        {
            var res = new AnalysisResult();
            if (!test.History.Contains("{*****}"))
            {
                test.History += "486234,0,8383312,8,10341723,486232,1,8462358,16,12196004,486233,2,8285476,8,10172924,{*****}";
            }
            var info = test.History.Split(new string[] { "{*****}" }, StringSplitOptions.None);
            var hardware = info[0].Split(',');

            var history = ParseHistory(info[1]);
            res.CompanyName = test.Company.Name;
            res.Age = (DateTime.Today - DateTime.Parse(test.Dob)).TotalDays / 365.25;
            res.TestedBy = test.Tester.Name;
            res.LongId = test.UuId + "-" + test.UnixTimeStamp.ToString();
           
            res.SampleRatePerSec = 1000m;
            res.Hardware = new Trio<DeviceData>
            {
                Index = ParseDeviceData(hardware, INDEX),
                Thumb = ParseDeviceData(hardware, THUMB),
                Pinky = ParseDeviceData(hardware, PINKY)
            };

            // TODO Dont forget to un-hardcode this
            res.Hardware.Thumb.Capacity = 16;
            res.UuId = test.UuId;
            res.Time = test.DateTime.DateTime;
            res.OpId = test.OpId;
            res.Id = test.Id;
           
            res.Sex = history["Gender"];
            res.Race = history["Race"];
            res.DominantHand = history["Dominant hand"];
            res.Height = history["Height"].ParseDouble() + " " + history["Height units"];
            res.Weight = history["Weight"] + " " + history["Weight units"];
            res.GeneralHealth = history["General health"];
            res.Smoker = history["Smoker"];
            res.CaffeineDrinksDay = history["Caffeine"];
            res.RepetitiveActivitiesHrsDay = history["Repetitive activities"];
            res.FineMotorSkillActivitiesHrsDay = history["Fine Motor activities"];
            res.WorkType = history["Type of work"];
            res.LeftHandNumbness = history["Left hand wrist numbness"];
            res.RightHandNumbness = history["Right hand wrist numbness"];
            res.LeftHandPain = history["Left physical pain"];
            res.RightHandPain = history["Right physical pain"];
            res.LeftCoordination = history["Left hand weakness poor coordination"];
            res.RightCoordination = history["Right hand weakness poor coordination"];
            res.LeftNormal = history["Considers left hand normal"];
            res.RightNormal = history["Considers right hand normal"];
            res.DiagnosedWithTBI = history["Diagnosed with TBI"];
            res.DiagnosedWithPTSD = history["Diagnosed with PTSD"];
            res.HedachesRingingInEars = history["Headaches"];
            res.Nausea = history["Nausea"];
            res.Balance = history["Balance problems"];
            res.Fatigue = history["Fatigued"];
            res.ConcentrationOrMemoryProblems = history["Concentration Memory problems"];
            res.DateOfFirstTBI = history["TBI Date, event 1"];
            res.CauseOfFirstTBI = history["TBI Cause, event 1"];
            res.TreatmentForFirstTBI = history["TBI Care Received, event 1"];
            res.FirstTBIDiagnosedAsTBI = history["TBI was Diagnosed, event 1"];
            res.RecoveryTimeForFirstTBI = history["TBI allowed recovery, event 1"];
            res.DateOfSecondTBI = history["TBI Date, event 2"];
            res.CauseOfSecondTBI = history["TBI Cause, event 2"];
            res.TreatmentForSecondTBI = history["TBI Care Received, event 2"];
            res.SecondTBIDiagnosedAsTBI = history["TBI was Diagnosed, event 2"];
            res.RecoveryTimeForSecondTBI = history["TBI allowed recovery, event 2"];
            res.BeenInMilitary = history["Military service"];
            res.ToursInMilitary = history["Number of tours"];
            res.MilitaryComments = history["Military comments"];
            res.PlaysSports = history["Sport(s) played"];
            res.YearsPlayingSports = history["Sports # years played"];
            res.SportsComments = history["Sports comments"];
            res.ShapeTestData(test, data);
            //  res.PerformStatisticalAnalysis();
            res.TestData.RightHandTwoSymbol.PopulateChartData();
            res.TestData.RightHandThreeSymbol.PopulateChartData();
            res.TestData.LeftHandTwoSymbol.PopulateChartData();
            res.TestData.LeftHandThreeSymbol.PopulateChartData();

            return res;
        }

        public static DeviceData ParseDeviceData(string[] info, int finger)
        {
            var offset = int.Parse(info[6]) == finger ? 5 : int.Parse(info[11]) == finger ? 10 : 0;
            return new DeviceData
            {
                Serial = info[0 + offset],
                Finger = finger,
                DataIndex = int.Parse(info[1 + offset]),
                Min = double.Parse(info[2 + offset]),
                Capacity = double.Parse(info[3 + offset]),
                Max = double.Parse(info[4 + offset]),
            };
        }

        public static void PerformStatisticalAnalysis(this AnalysisResult result)
        {
            //var client = new WebClient();
            //client.Headers.Add(HttpRequestHeader.Accept, "application/json");
            //var data = JsonConvert.DeserializeObject<AnalysisResult>(client.DownloadString("http://labview1.southcentralus.cloudapp.azure.com/Analysis/" + result.LongId));
            //result.TestData.LeftHandThreeSymbol.StatisticalAnalysis = data.TestData.LeftHandThreeSymbol.StatisticalAnalysis;
            //result.TestData.RightHandThreeSymbol.StatisticalAnalysis = data.TestData.RightHandThreeSymbol.StatisticalAnalysis;
            //result.TestData.LeftHandTwoSymbol.StatisticalAnalysis = data.TestData.LeftHandTwoSymbol.StatisticalAnalysis;
            //result.TestData.RightHandTwoSymbol.StatisticalAnalysis = data.TestData.RightHandTwoSymbol.StatisticalAnalysis;
            // Uncomment to run .NET analysis
            //StatisticsBattery(result.TestData.LeftHandThreeSymbol);
            //StatisticsBattery(result.TestData.RightHandThreeSymbol);
            //StatisticsBattery(result.TestData.LeftHandTwoSymbol);
            //StatisticsBattery(result.TestData.RightHandTwoSymbol);
        }

        private static double GetSlope(List<Pulse> pulses, Func<TimeForce<Trio<double>>, double> select)
        {
            var peaks = pulses.Select(p => p.Max);
            var dist = peaks.Take(pulses.Count - 1).Zip(peaks.Skip(1), (i, j) => select(i) - select(j)).Sum();
            var time = (peaks.Last().Time - peaks.First().Time) / 1000;
            return dist / time;
        }

        private static IEnumerable<double> Reduce(this List<double> samples)
        {
            return samples.Take(samples.Count - 1).Zip(samples.Skip(1), (i, j) => Math.Abs(j - i));
        }

        private static List<double> WindowAverage(this List<double> samples, int limit)
        {
            var res = new List<double>(samples.Count);
            for (int i = 0; i < samples.Count; i++)
            {
                double current = samples[i];
                double count = 1;
                for (int j = 1; j < limit && j < i && j < samples.Count - i; j++)
                {
                    current = (samples[i + j] + samples[i - j] + (current * count)) / (count + 2.0);
                    count += 2.0;
                }
                res.Add(current);
            }
            return res;
        }

        private static void StatisticsBattery(TestContainer test)
        {
            var AllThumb = test.Pulses.SelectMany(p => p.Samples.Select(s => s.Force.Thumb)).ToList();
            var AllIndex = test.Pulses.SelectMany(p => p.Samples.Select(s => s.Force.Index)).ToList();
            var AllPinky = test.Pulses.SelectMany(p => p.Samples.Select(s => s.Force.Pinky)).ToList();
            test.StatisticalAnalysis.ThumbIndexCorr = Correlation.Pearson(AllThumb, AllIndex);
            test.StatisticalAnalysis.ThumbPinkyCorr = Correlation.Pearson(AllThumb, AllPinky);
            test.StatisticalAnalysis.IndexPinkyCorr = Correlation.Pearson(AllPinky, AllIndex);

			// new report values here
			test.StatisticalAnalysis.ThumbIndexPath = (1 - test.StatisticalAnalysis.ThumbIndexCorr) * 200;
			test.StatisticalAnalysis.ThumbSmallPath = (1 - test.StatisticalAnalysis.ThumbPinkyCorr) * 200;
			test.StatisticalAnalysis.IndexSmallPath = (1 - test.StatisticalAnalysis.ThumbIndexCorr) * 200;

			test.StatisticalAnalysis.FatigueSlope = new Trio<double>
            {
                Index = -GetSlope(test.Pulses, x => x.Force.Index),
                Thumb = -GetSlope(test.Pulses, x => x.Force.Thumb),
                Pinky = -GetSlope(test.Pulses, x => x.Force.Pinky),
            };
            var smoothThumbSignal = Smooth(AllThumb);
            var smoothIndexSignal = Smooth(AllIndex);
            var smoothPinkySignal = Smooth(AllPinky);
            test.StatisticalAnalysis.IndexPinkyPathRatio = GetPathLength(AllIndex, AllPinky) / GetPathLength(smoothIndexSignal, smoothPinkySignal);
            test.StatisticalAnalysis.ThumbIndexPathRatio = GetPathLength(AllThumb, AllIndex) / GetPathLength(smoothThumbSignal, smoothIndexSignal);
            test.StatisticalAnalysis.ThumbPinkyPathRatio = GetPathLength(AllThumb, AllPinky) / GetPathLength(smoothThumbSignal, smoothPinkySignal);
            test.StatisticalAnalysis.MaxThumbForce = AllThumb.Max();
            test.StatisticalAnalysis.ThumbForceDecay = test.Pulses.Select(p => 100 * ((p.Max.Force.Thumb - p.Min.Force.Thumb) / (p.Max.Force.Thumb))).Sum() / test.Pulses.Count;
            test.GetReactionTimes();
            var nr = test.Pulses.Select(p => p.Stop.Force).ToList();
            test.StatisticalAnalysis.ForceRatioIndexPinkyVsThumb = ((nr.Select(p => p.Index).Sum() + nr.Select(p => p.Pinky).Sum()) / nr.Select(p => p.Thumb).Sum());
        }

        public static void ShapeTestData(this AnalysisResult analysis, Test test, BinaryData data)
        {
            analysis.TestData.LeftHandTwoSymbol.Pulses = LoadTestPart(analysis, data.left2);
            analysis.TestData.LeftHandThreeSymbol.Pulses = LoadTestPart(analysis, data.left3);
            analysis.TestData.RightHandTwoSymbol.Pulses = LoadTestPart(analysis, data.right2);
            analysis.TestData.RightHandThreeSymbol.Pulses = LoadTestPart(analysis, data.right3);
        }

        public class BinaryData
        {
            public byte[] Hardware { get; set; }

            public byte[] right2 { get; set; }

            public byte[] left2 { get; set; }

            public byte[] right3 { get; set; }

            public byte[] left3 { get; set; }
        }

        public static BinaryData DownloadFromBlob(Test test)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(System.Configuration.ConfigurationManager.AppSettings["testblob"]);

            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Retrieve reference to a previously created container.
            CloudBlobContainer container = blobClient.GetContainerReference("testbinary");

            var buffer = new byte[1000000];
            var folder = container.GetDirectoryReference(test.Id.ToString());
            if(!folder.GetBlockBlobReference("hardware").Exists())
            {
                container = blobClient.GetContainerReference("tests");
                folder = container.GetDirectoryReference(test.UuId + "-" + test.UnixTimeStamp.ToString());
            }

            var data = new BinaryData();

            var size = folder.GetBlockBlobReference("hardware").DownloadToByteArray(buffer, 0);
            data.Hardware = new byte[size];
            Array.Copy(buffer, data.Hardware, size);
            size = folder.GetBlockBlobReference("right2").DownloadToByteArray(buffer, 0);
            data.right2 = new byte[size];
            Array.Copy(buffer, data.right2, size);
            size = folder.GetBlockBlobReference("left2").DownloadToByteArray(buffer, 0);
            data.left2 = new byte[size];
            Array.Copy(buffer, data.left2, size);
            size = folder.GetBlockBlobReference("right3").DownloadToByteArray(buffer, 0);
            data.right3 = new byte[size];
            Array.Copy(buffer, data.right3, size);
            size = folder.GetBlockBlobReference("left3").DownloadToByteArray(buffer, 0);
            data.left3 = new byte[size];
            Array.Copy(buffer, data.left3, size);
            return data;
        }



        private static List<double> Smooth(List<double> AllThumb)
        {
            return AllThumb.WindowAverage(124);
        }

        private static double GetPathLength(List<double> sig1, List<double> sig2)
        {
            return sig1.Reduce().Zip(sig2.Reduce(), (i, j) => Math.Sqrt((i * i) + (j * j))).Sum();
        }

        private static void PopulateChartData(this TestContainer test)
        {
            var peaks = test.Pulses.Select(p => p.Max);
            var fit = MathNet.Numerics.Fit.Line(peaks.Select(p => p.Time).ToArray(), peaks.Select(p => p.Force.Thumb).ToArray());
            test.Charts.Fatigue = peaks.Select(p => new TimeForce<double> { Force = fit.Item2 * p.Time + fit.Item1, Time = p.Time }).ToList();
            test.Charts.Main = test.Pulses.SelectMany(p => p.Samples).Select(s => new TimeForce<double> { Force = s.Force.Thumb, Time = s.Time }).ToList();
        }

        private static bool IsDelimitSample(byte[] data, int i)
        {
            return (data[i] == RightPulseDelimiter || data[i] == LeftPulseDelimiter) && (data[i + 1] == RightPulseDelimiter || data[i + 1] == LeftPulseDelimiter) && (data[i + 2] == RightPulseDelimiter || data[i + 2] == LeftPulseDelimiter) &&
                (data[i + 3] == RightPulseDelimiter || data[i + 3] == LeftPulseDelimiter) && (data[i + 4] == RightPulseDelimiter || data[i + 4] == LeftPulseDelimiter) && (data[i + 5] == RightPulseDelimiter || data[i + 5] == LeftPulseDelimiter);
        }

        private static bool IsMarkerSample(byte[] data, int i)
        {
            return ((data[i] == 125) && (data[i + 1] == 100)) || ((data[i + 2] == 125) &&
                (data[i + 3] == 100)) || ((data[i + 4] == 125) && (data[i + 5] == 100));
        }

        //private static double Med(this IEnumerable<double> samples)
        //{
        //    var l = samples.ToList();
        //    l.Sort();
        //    return l[l.Count / 2];
        //}

        private static MedianAndError MedianMAD(IEnumerable<double> reactionTimes)
        {

            var median = reactionTimes.Median();
            var mad = reactionTimes.Select(r => Math.Abs(r - median)).Median();

            return new MedianAndError
            {
                Median = median,
                MAD = mad
            };
        }

        public class ReactionContainer
        {
            public double riseReaction { get; set; }
            public double rise { get; set; }
            public double fallReaction { get; set; }
            public double fall { get; set; }
        }

        private static Trio<ReactionContainer> ExtractTiming(PulseParts p)
        {
            var Rise = AddTimeIndex(p.Rise);
            var Fall = AddTimeIndex(p.Fall);
            return new Trio<ReactionContainer>
            {
                Index = GetReaction(Rise, Fall, Rise.Item1.Index, Fall.Item1.Pinky, x => x.Index),
                Thumb = GetReaction(Rise, Fall, Rise.Item1.Thumb, Fall.Item1.Pinky, x => x.Thumb),
                Pinky = GetReaction(Rise, Fall, Rise.Item1.Pinky, Fall.Item1.Pinky, x => x.Pinky)
            };
        }

        private static Tuple<Trio<int>, List<Trio<double>>> AddTimeIndex(List<TimeForce<Trio<Double>>> p)
        {
            var center = new Trio<int>
            {
                Index = 0,
                Thumb = 0,
                Pinky = 0,
            };
            List<Trio<double>> indexed = new List<Trio<double>>(p.Count - 2);
            for (var cnt = 1; cnt < p.Count - 1; cnt++)
            {
                var i = p[cnt + 1];
                var j = p[cnt - 1];
                var force = new Trio<double>
                {
                    Index = Math.Abs(i.Force.Index - j.Force.Index),
                    Thumb = Math.Abs(i.Force.Thumb - j.Force.Thumb),
                    Pinky = Math.Abs(i.Force.Pinky - j.Force.Pinky),
                };
                if (indexed.Count > 0)
                {
                    if (force.Index > indexed[center.Index].Index) center.Index = cnt - 1;
                    if (force.Thumb > indexed[center.Thumb].Thumb) center.Thumb = cnt - 1;
                    if (force.Pinky > indexed[center.Pinky].Pinky) center.Pinky = cnt - 1;
                }
                indexed.Add(force);
            }
            return Tuple.Create(center, indexed);
        }

        private static ReactionContainer GetReaction(Tuple<Trio<int>, List<Trio<double>>> Rise, Tuple<Trio<int>, List<Trio<double>>> Fall, int maxIndex, int fallMax, Func<Trio<double>, double> select1)
        {
            var rise = FindActions(Rise.Item1, Rise.Item2, maxIndex, select1);
            var fall = FindActions(Fall.Item1, Fall.Item2, fallMax, select1);
            return new ReactionContainer
            {
                riseReaction = rise.Item1,
                rise = rise.Item2,
                fallReaction = fall.Item1,
                fall = fall.Item2,
            };
        }

        private static Tuple<int, int> FindActions(Trio<int> center, List<Trio<double>> curve, int maxIndex, Func<Trio<double>, double> select1)
        {
            //      var min = select1(curve[0]);
            //      var threshold = (select1(curve[max]) - min) * 0.1;
            var threshold = select1(curve[maxIndex]) / 10.0;
            int i = maxIndex;
            for (i = maxIndex; i > 0 && select1(curve[i]) <= threshold; i--) ;
            int j = maxIndex;
            for (j = maxIndex; j < curve.Count && select1(curve[j]) <= threshold; j++) ;
            i += 80;
            j += 80;
            return Tuple.Create(i, j - i);
        }
    }
}
