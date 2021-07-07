using System;
using System.Collections.Generic;

namespace Roi.Data
{

    public class AnalysisResult
    {
        public class TimeForce<T>
        {
            public double Time { get; set; } // seconds
            public T Force { get; set; } // kg
            public bool IsDelimiter { get; set; }
            public bool IsMarker { get;  set; }
            public bool MaxSample { get;  set; }
            public bool MinSample { get; set; }
            public int Index { get;  set; }
        }

        public class Pulse
        {
            public TimeForce<Trio<double>> Max { get; set; }
            public TimeForce<Trio<double>> Min { get; set; }
            public List<TimeForce<Trio<double>>> Samples { get; set; } = new List<TimeForce<Trio<double>>>();
            public TimeForce<Trio<double>> Start { get; set; }
            public TimeForce<Trio<double>> Stop { get; set; }
        }

        public class Charts
        {
            public List<TimeForce<double>> Fatigue { get; set; }
            public List<TimeForce<double>> Main { get; set; }
        }

        public class MedianAndError
        {
            public double Median { get; set; }
            public double MAD { get; set; }
        }

        public class Trio<T>
        {
            public T Index { get; set; }
            public T Thumb { get; set; }
            public T Pinky { get; set; }
        }

        public class Stats
        {
            public double ThumbIndexCorr { get; set; }
            public double ThumbPinkyCorr { get; set; }
            public double IndexPinkyCorr { get; set; }

			// data for new report
			public double ThumbIndexPath { get; set; }
			public double ThumbSmallPath { get; set; }
			public double IndexSmallPath { get; set; }

			public Trio<double> FatigueSlope { get; set; }    // kg/s

            public double ThumbIndexPathRatio { get; set; }
            public double ThumbPinkyPathRatio { get; set; }
            public double IndexPinkyPathRatio { get; set; }
            public double MaxThumbForce { get; set; } // kg
            public double ThumbForceDecay { get; set; } // percentage

            public Trio<MedianAndError> RiseTime { get; set; } // milliseconds
            public Trio<MedianAndError> StartReaction { get; set; } // milliseconds     
            public Trio<MedianAndError> FallTime { get; set; } // milliseconds
            public Trio<MedianAndError> ReleaseReaction { get; set; } // milliseconds

            public double ForceRatioIndexPinkyVsThumb { get; set; }
        }

        public Guid Id { get; set; }
        public DateTime Time { get; set; }
        public string CompanyName { get; set; }
        public string UuId { get; set; }
        public string OpId { get; set; }
        public decimal SampleRatePerSec { get; set; }
        public string TestType { get; set; }
        public double Age { get; set; }
        public string DominantHand { get; set; }
        public string Sex { get; set; }
        public string Height { get; set; }
        public string Weight { get; set; }
        public string GeneralHealth { get; set; }
        public string Smoker { get; set; }
        public string CaffeineDrinksDay { get; set; }
        public string RepetitiveActivitiesHrsDay { get; set; }
        public string FineMotorSkillActivitiesHrsDay { get; set; }
        public string WorkType { get; set; }
        public string LeftHandNumbness { get; set; }
        public string RightHandNumbness { get; set; }
        public string LeftHandPain { get; set; }
        public string RightHandPain { get; set; }
        public string LeftCoordination { get; set; }
        public string RightCoordination { get; set; }
        public string LeftNormal { get; set; }
        public string RightNormal { get; set; }
        public string Race { get; set; }
        public string DiagnosedWithTBI { get; set; }
        public string DiagnosedWithPTSD { get; set; }
        public string HedachesRingingInEars { get; set; }
        public string Nausea { get; set; }
        public string Balance { get; set; }
        public string Fatigue { get; set; }
        public string ConcentrationOrMemoryProblems { get; set; }
        public string BeenInMilitary { get; set; }
        public string ToursInMilitary { get; set; }
        public string PlaysSports { get; set; }
        public string YearsPlayingSports { get; set; }
        public string DateOfFirstTBI { get; set; }
        public string CauseOfFirstTBI { get; set; }
        public string TreatmentForFirstTBI { get; set; }
        public string FirstTBIDiagnosedAsTBI { get; set; }
        public string RecoveryTimeForFirstTBI { get; set; }
        public string DateOfSecondTBI { get; set; }
        public string CauseOfSecondTBI { get; set; }
        public string TreatmentForSecondTBI { get; set; }
        public string SecondTBIDiagnosedAsTBI { get; set; }
        public string RecoveryTimeForSecondTBI { get; set; }
        public string MilitaryComments { get; set; }
        public string SportsComments { get; set; }
        public string TestedBy { get; set; }
        public StructuredTestData TestData { get; set; } = new StructuredTestData();
        public Trio<DeviceData> Hardware { get; set; }
        public string LongId { get; set; }

        public class DeviceData
        {
            public string Serial { get; set; }
            public int Finger { get; set; }
            public int DataIndex { get; set; }
            public double Min { get; set; }
            public double Max { get; set; }
            public double Capacity { get; set; }
        }

        public class TestContainer
        {
            public List<Pulse> Pulses { get; set; }
            public Stats StatisticalAnalysis { get; set; } = new Stats();
            public Charts Charts { get; set; } = new Charts();
        }

        public class StructuredTestData
        {
            public TestContainer LeftHandTwoSymbol { get; set; } = new TestContainer();
            public TestContainer LeftHandThreeSymbol { get; set; } = new TestContainer();
            public TestContainer RightHandTwoSymbol { get; set; } = new TestContainer();
            public TestContainer RightHandThreeSymbol { get; set; } = new TestContainer();
        }
    }

}
