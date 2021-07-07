using Newtonsoft.Json;
using Roi.Data.Models;
using Roi.Logic;
using System;
using System.Data.Entity.Migrations;
using System.Linq;

namespace Roi.Data.BusinessLogic
{
    public class HistoryColumns
    {
        public HistoryColumn Left { get; set; } = new HistoryColumn();
        public HistoryColumn Right { get; set; } = new HistoryColumn();
    }

    public static class HistoryData
    {


        public static void PopulateHistoryData(this ReportData data)
        {
            try
            {
                var historyList = new HistoryList();

                // add the date of the current test (Last Date)
                data.TestDate = data.Time.ToDateTimeString();

                // if there is only one test, the start date and last date is the curent test's date
                data.StartDate = data.TestDate;

                InjuryEvidenceHistory history = new InjuryEvidenceHistory();
                HistoryColumns pastTest;

                using (var db = new RoiDb())
                {
                    int idx = db.Tests.Where(s => s.UuId == data.Uuid && s.UnixTimeStamp < data.Time).Count();
                    // add past tests
                    foreach (var t in db.Tests.Where(s => s.UuId == data.Uuid && s.UnixTimeStamp < data.Time).OrderBy(s => s.UnixTimeStamp))
                    {

                        idx--;

                        if (string.IsNullOrEmpty(t.Analysis))
                        {
                            var r = AnalysisLogic.GenerateReport(t.Id.ToString()).ReportData;
                            pastTest = new HistoryColumns();

                            pastTest.Left.FatigueVarience = r.LeftFatigueVariance;
                            pastTest.Left.Strength = r.LeftStrength;
                            pastTest.Left.StrengthRatio = r.LeftStrengthRatio == null ? "" : r.LeftStrengthRatio.ToString();
                            pastTest.Left.MotorControl = r.LeftMotorControl;
                            pastTest.Left.SensoryControl = r.LeftSensoryControl;
                            pastTest.Left.ReactionTime = r.LeftReactionTime;
                            pastTest.Left.CognitiveReactionTime = r.LeftCognitiveReactionTime;
                            pastTest.Left.Correlation = r.LeftCorrelation;

                            pastTest.Right.FatigueVarience = r.RightFatigueVariance;
                            pastTest.Right.Strength = r.RightStrength;
                            pastTest.Right.StrengthRatio = r.RightStrengthRatio == null ? "" : r.RightStrengthRatio.ToString();
                            pastTest.Right.MotorControl = r.RightMotorControl;
                            pastTest.Right.SensoryControl = r.RightSensoryControl;
                            pastTest.Right.ReactionTime = r.RightReactionTime;
                            pastTest.Right.CognitiveReactionTime = r.RightCognitiveReactionTime;
                            pastTest.Right.Correlation = r.RightCorrelation;

                        }
                        else
                        {
                            pastTest = JsonConvert.DeserializeObject<HistoryColumns>(t.Analysis);
                        }
                        history = new InjuryEvidenceHistory()
                        {
                            Date = t.UnixTimeStamp.ToDateTimeString(),
                            Left = Calculations.InjuryEvidence(pastTest.Left.SensoryControl.Value, pastTest.Left.Correlation.Value),
                            Right = Calculations.InjuryEvidence(pastTest.Right.SensoryControl.Value, pastTest.Right.Correlation.Value),
                        };

                        historyList.List.Add(history);
                        if (idx == 0)
                        {
                            data.TestDateHist1 = t.UnixTimeStamp.ToDateTimeString();
                            data.LeftHist1 = pastTest.Left;

                            data.RightHist1 = pastTest.Right;
                        }
                        if (idx == 1)
                        {

                            data.TestDateHist2 = t.UnixTimeStamp.ToDateTimeString();
                            data.LeftHist2 = pastTest.Left;

                            data.RightHist2 = pastTest.Right;

                        }
                    }

                    pastTest = new HistoryColumns();
                    pastTest.Left.FatigueVarience = data.LeftFatigueVariance;
                    pastTest.Left.Strength = data.LeftStrength;
                    pastTest.Left.StrengthRatio = data.LeftStrengthRatio == null ? "" : data.LeftStrengthRatio.ToString();
                    pastTest.Left.MotorControl = data.LeftMotorControl;
                    pastTest.Left.SensoryControl = data.LeftSensoryControl;
                    pastTest.Left.ReactionTime = data.LeftReactionTime;
                    pastTest.Left.CognitiveReactionTime = data.LeftCognitiveReactionTime;
                    pastTest.Left.Correlation = data.LeftCorrelation;

                    pastTest.Right.FatigueVarience = data.RightFatigueVariance;
                    pastTest.Right.Strength = data.RightStrength;
                    pastTest.Right.StrengthRatio = data.RightStrengthRatio == null ? "" : data.RightStrengthRatio.ToString();
                    pastTest.Right.MotorControl = data.RightMotorControl;
                    pastTest.Right.SensoryControl = data.RightSensoryControl;
                    pastTest.Right.ReactionTime = data.RightReactionTime;
                    pastTest.Right.CognitiveReactionTime = data.RightCognitiveReactionTime;
                    pastTest.Right.Correlation = data.RightCorrelation;

                    var currentTest = db.Tests.FirstOrDefault(s => s.UuId == data.Uuid && s.UnixTimeStamp == data.Time);
                    currentTest.Analysis = JsonConvert.SerializeObject(pastTest);
                    db.SaveChanges();

                    historyList.List.Add(new InjuryEvidenceHistory()
                    {
                        Date = data.Time.ToDateTimeString(),
                        Left = Calculations.InjuryEvidence(pastTest.Left.SensoryControl.Value, pastTest.Left.Correlation.Value),
                        Right = Calculations.InjuryEvidence(pastTest.Right.SensoryControl.Value, pastTest.Right.Correlation.Value),
                    });

                    // record the number of records
                    data.TotalTests = historyList.List.Count;

                    if (historyList.List.Count > 1)
                    {
                        // save earliest test record's test date
                        data.StartDate = historyList.List[0].Date;
                        // only write history if there's more than one test in history
                        data.TestHistory = historyList.ToJson();
                    }
                }
            }
            catch (Exception e)
            {
                Error.LogError(e);
                //return false;
            }
        }
    }
}
