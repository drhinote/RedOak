using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Roi.Data;
using Roi.Data.BusinessLogic;
using Roi.Data.Models;
using Roi.Logic;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Http;
using static Roi.Data.BusinessLogic.DeviceData;
using static Roi.Logic.AnalysisLogic;

namespace Roi.Analysis.LabView.Controllers
{

    public class AnalysisController : ApiController
    {

        public static string RemoveTime(string UuidTime)
        {
            var uuid = UuidTime.Substring(0, UuidTime.LastIndexOf('-'));
            return uuid;
        }

        [HttpGet]
        public ReportBundle Get(Guid id)
        {
            AnalysisResult result;
            long time;
            Guid companyId;

            BinaryData data;

            using (var db = new RoiDb())
            {
                var test = db.Tests.Find(id);
                data = DownloadFromBlob(test);
                time = test.UnixTimeStamp;
                companyId = test.CompanyId;
                result = test.Analyze(data);
            }

            GetResult(id, result, data);
            GenerateReport = nid =>
            {
                return Get(Guid.Parse(nid));
            };
            // load the models with the data from the excel template
            ReportData report = new ModelData(result).LoadModels(result.UuId, time);

            // remove the time portion from the uuid
            report.TestId = RemoveTime(report.TestId);
            return new ReportBundle { ReportData = report, CompanyId = companyId.ToString(), id = id.ToString() };
        }

        const string baseURL = @"C:\Reports\";
        const string LabViewExe = @"C:\Analysis\Application.exe";
        static object locker = new object();
        public void ProcessRawData(RawTestData test)
        {
            lock (locker)
            {
                File.WriteAllBytes(test.Path, test.Data);
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = LabViewExe;
                startInfo.Arguments = test.Args;
                var proc = Process.Start(startInfo);
                proc.WaitForExit();
            }
            //return $"C:\\Reports\\Results\\{test.Company}\\{test.Uuid}-{test.Time}-{testType}.txt";
        }

        RawTestData PopulateTest(byte[] header, byte[] r2, byte[] l2, long time, string history,
            string tester, string uuid, string opid, string dob, string testDataPath, string testFolder, string company)
        {
            return new RawTestData
            {
                Time = time,
                Uuid = uuid,
                Path = testFolder + testDataPath,
                Data = header.Concat(r2).Concat(l2).ToArray(),
                Args = " \"" + testDataPath + "\" \"" + new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(time).ToString("MM/dd/yyyy hh:mm") + "\" \""
                + dob + "\" \"" + opid + "\" " + "\"" + history + "tester*{&}$" + tester + "${&}*\" \"c:\\Reports\\\"",
                Company = company
            };
        }


        RawTestData CreateTestObject(string uuid, string opid, string dob, long time, byte[] header, string history,
             byte[] rightHandData, byte[] leftHandData, string tester, string company, TestType type)
        {
            var testFolder = "c:\\Reports\\Tests\\";
            var testDataPath = company + "\\" + uuid + '-' + time + "-" + (int)type + ".roi";
            header[0] = 1;
            return PopulateTest(header, rightHandData, leftHandData, time, history, tester, uuid, opid, dob, testDataPath, testFolder, company);
        }

        string GetFilePath(string uuid, long time, string company)
        {
            Directory.CreateDirectory($@"{baseURL}Results\{company}\");
            return $@"{baseURL}Results\{company}\{uuid}-{time}";
        }

        string GetTextFilePath(string uuid, long time, string company, TestType type)
        {
            return $"{GetFilePath(uuid, time, company)}-{(int)type}.txt";
        }


        private void AddTextOutputForComparison(AnalysisResult.TestContainer left, AnalysisResult.TestContainer right, string[][] t, int num)
        {
            //File.WriteAllText("c:\\Roi\\test" + num + ".txt", string.Join("\r\n", textFile.Select((l, i) => string.Join(" ", l.Select((m, j) => m + " (" + i + "," + j + ")  ")))));
            right.StatisticalAnalysis.FallTime = new AnalysisResult.Trio<AnalysisResult.MedianAndError>
            {
                Index = new AnalysisResult.MedianAndError { Median = double.Parse(t[77][1]), MAD = double.Parse(t[77][2]) },
                Thumb = new AnalysisResult.MedianAndError { Median = double.Parse(t[78][1]), MAD = double.Parse(t[78][2]) },
                Pinky = new AnalysisResult.MedianAndError { Median = double.Parse(t[79][1]), MAD = double.Parse(t[79][2]) },
            };
            right.StatisticalAnalysis.RiseTime = new AnalysisResult.Trio<AnalysisResult.MedianAndError>
            {
                Index = new AnalysisResult.MedianAndError { Median = double.Parse(t[69][1]), MAD = double.Parse(t[69][2]) },
                Thumb = new AnalysisResult.MedianAndError { Median = double.Parse(t[70][1]), MAD = double.Parse(t[70][2]) },
                Pinky = new AnalysisResult.MedianAndError { Median = double.Parse(t[71][1]), MAD = double.Parse(t[71][2]) },
            };
            right.StatisticalAnalysis.StartReaction = new AnalysisResult.Trio<AnalysisResult.MedianAndError>
            {
                Index = new AnalysisResult.MedianAndError { Median = double.Parse(t[73][1]), MAD = double.Parse(t[73][2]) },
                Thumb = new AnalysisResult.MedianAndError { Median = double.Parse(t[74][1]), MAD = double.Parse(t[74][2]) },
                Pinky = new AnalysisResult.MedianAndError { Median = double.Parse(t[75][1]), MAD = double.Parse(t[75][2]) },
            };
            right.StatisticalAnalysis.ReleaseReaction = new AnalysisResult.Trio<AnalysisResult.MedianAndError>
            {
                Index = new AnalysisResult.MedianAndError { Median = double.Parse(t[81][1]), MAD = double.Parse(t[81][2]) },
                Thumb = new AnalysisResult.MedianAndError { Median = double.Parse(t[82][1]), MAD = double.Parse(t[82][2]) },
                Pinky = new AnalysisResult.MedianAndError { Median = double.Parse(t[83][1]), MAD = double.Parse(t[83][2]) },
            };

            left.StatisticalAnalysis.FallTime = new AnalysisResult.Trio<AnalysisResult.MedianAndError>
            {
                Index = new AnalysisResult.MedianAndError { Median = double.Parse(t[77][5]), MAD = double.Parse(t[77][6]) },
                Thumb = new AnalysisResult.MedianAndError { Median = double.Parse(t[78][5]), MAD = double.Parse(t[78][6]) },
                Pinky = new AnalysisResult.MedianAndError { Median = double.Parse(t[79][5]), MAD = double.Parse(t[79][6]) },
            };
            left.StatisticalAnalysis.RiseTime = new AnalysisResult.Trio<AnalysisResult.MedianAndError>
            {
                Index = new AnalysisResult.MedianAndError { Median = double.Parse(t[69][5]), MAD = double.Parse(t[69][6]) },
                Thumb = new AnalysisResult.MedianAndError { Median = double.Parse(t[70][5]), MAD = double.Parse(t[70][6]) },
                Pinky = new AnalysisResult.MedianAndError { Median = double.Parse(t[71][5]), MAD = double.Parse(t[71][6]) },
            };
            left.StatisticalAnalysis.StartReaction = new AnalysisResult.Trio<AnalysisResult.MedianAndError>
            {
                Index = new AnalysisResult.MedianAndError { Median = double.Parse(t[73][5]), MAD = double.Parse(t[73][6]) },
                Thumb = new AnalysisResult.MedianAndError { Median = double.Parse(t[74][5]), MAD = double.Parse(t[74][6]) },
                Pinky = new AnalysisResult.MedianAndError { Median = double.Parse(t[75][5]), MAD = double.Parse(t[75][6]) },
            };
            left.StatisticalAnalysis.ReleaseReaction = new AnalysisResult.Trio<AnalysisResult.MedianAndError>
            {
                Index = new AnalysisResult.MedianAndError { Median = double.Parse(t[81][5]), MAD = double.Parse(t[81][6]) },
                Thumb = new AnalysisResult.MedianAndError { Median = double.Parse(t[82][5]), MAD = double.Parse(t[82][6]) },
                Pinky = new AnalysisResult.MedianAndError { Median = double.Parse(t[83][5]), MAD = double.Parse(t[83][6]) },
            };

            right.StatisticalAnalysis.FatigueSlope = new AnalysisResult.Trio<double>
            {
                Index = double.Parse(t[58][1]),
                Thumb = double.Parse(t[59][1]),
                Pinky = double.Parse(t[60][1]),
            };

            left.StatisticalAnalysis.FatigueSlope = new AnalysisResult.Trio<double>
            {
                Index = double.Parse(t[58][3]),
                Thumb = double.Parse(t[59][3]),
                Pinky = double.Parse(t[60][3]),
            };

            right.StatisticalAnalysis.ForceRatioIndexPinkyVsThumb = double.Parse(t[86][0].Contains("Inf") ? "0" : t[86][0]);

            left.StatisticalAnalysis.ForceRatioIndexPinkyVsThumb = double.Parse(t[86][1].Contains("Inf") ? "0" : t[86][1]);

            right.StatisticalAnalysis.ThumbIndexCorr = double.Parse(t[53][2]);
            right.StatisticalAnalysis.ThumbPinkyCorr = double.Parse(t[54][2]);
            right.StatisticalAnalysis.IndexPinkyCorr = double.Parse(t[55][2]);
            left.StatisticalAnalysis.ThumbIndexCorr = double.Parse(t[53][5]);
            left.StatisticalAnalysis.ThumbPinkyCorr = double.Parse(t[54][5]);
            left.StatisticalAnalysis.IndexPinkyCorr = double.Parse(t[55][5]);

            right.StatisticalAnalysis.ThumbIndexPathRatio = double.Parse(t[62][3]);
            right.StatisticalAnalysis.ThumbPinkyPathRatio = double.Parse(t[63][3]);
            right.StatisticalAnalysis.IndexPinkyPathRatio = double.Parse(t[64][3]);
            left.StatisticalAnalysis.ThumbIndexPathRatio = double.Parse(t[62][7]);
            left.StatisticalAnalysis.ThumbPinkyPathRatio = double.Parse(t[63][7]);
            left.StatisticalAnalysis.IndexPinkyPathRatio = double.Parse(t[64][7]);

            right.StatisticalAnalysis.MaxThumbForce = double.Parse(t[65][4]);
            left.StatisticalAnalysis.MaxThumbForce = double.Parse(t[65][9]);

            right.StatisticalAnalysis.ThumbForceDecay = double.Parse(t[66][3].TrimEnd('%'));
            left.StatisticalAnalysis.ThumbForceDecay = double.Parse(t[66][7].TrimEnd('%'));

        }



        private AnalysisResult GetResult(Guid id, AnalysisResult old, BinaryData data)
        {
            try
            {
                using (var ctx = new RoiDb())
                {
                    var test = ctx.Tests.Find(id);



                    var testContainer = new TestContainer
                    {
                        test2 = CreateTestObject(test.UuId, test.OpId, test.Dob, test.UnixTimeStamp, data.Hardware, test.History, data.right2, data.left2, test.Tester.Name, test.Company.Name, TestType.TwoSymbol),
                        test3 = CreateTestObject(test.UuId, test.OpId, test.Dob, test.UnixTimeStamp, data.Hardware, test.History, data.right3, data.left3, test.Tester.Name, test.Company.Name, TestType.ThreeSymbol),
                    };

                    // create the directory where the raw .roi files are stored
                    Directory.CreateDirectory(baseURL + "Tests\\" + testContainer.test2.Company);

                    // create the directory where the raw .txt file are stored
                    Directory.CreateDirectory(baseURL + "Results\\" + testContainer.test3.Company);

                    // process the raw data for the two symbol test
                    ProcessRawData(testContainer.test2);

                    // process the raw data for the three symbol test
                    ProcessRawData(testContainer.test3);

                    // create the path for the two symbol data file
                    var Insert2SDataFilePath = new FileInfo(GetTextFilePath(testContainer.test2.Uuid, (long)testContainer.test2.Time, testContainer.test2.Company, TestType.TwoSymbol));

                    // create the path for the three symbol data file
                    var Insert3SDataFilePath = new FileInfo(GetTextFilePath(testContainer.test3.Uuid, (long)testContainer.test3.Time, testContainer.test3.Company, TestType.ThreeSymbol));


                    AddTextOutputForComparison(old.TestData.LeftHandTwoSymbol, old.TestData.RightHandTwoSymbol, File.ReadAllLines(Insert2SDataFilePath.ToString()).Select(l => Regex.Split(l, "\\s+")).ToArray(), 2);
                    AddTextOutputForComparison(old.TestData.LeftHandThreeSymbol, old.TestData.RightHandThreeSymbol, File.ReadAllLines(Insert3SDataFilePath.ToString()).Select(l => Regex.Split(l, "\\s+")).ToArray(), 3);

                    return old;
                }
            }
            catch (Exception ex)
            {
                Error.LogError(ex);
                return null;
            }
        }
    }
}
