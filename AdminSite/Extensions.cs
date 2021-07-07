using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RedoakAdmin
{
    public static class Extensions
    {
        public static DateTime ToDateTime(this long testTimeMiliseconds, int UtcOffset)
        {
            var testDate = new DateTime(1970, 1, 1, 0, 0, 0);
            testDate = testDate.AddMilliseconds(testTimeMiliseconds);
            return testDate.AddMinutes(UtcOffset);
        }

        public static DateTime ToDateTime(this long testTimeMiliseconds)
        {
            var testDate = new DateTime(1970, 1, 1, 0, 0, 0);
            testDate = testDate.AddMilliseconds(testTimeMiliseconds);
            return testDate.AddMinutes(-360);
        }

        public static string ToDateTimeString(this long testTimeMiliseconds)
        {
            var testDate = new DateTime(1970, 1, 1, 0, 0, 0);
            testDate = testDate.AddMilliseconds(testTimeMiliseconds);
            testDate = testDate.AddMinutes(-360);
            return testDate.ToString("MM/dd/yyyy hh:mm tt");
        }

        public static string ToDateTimeString(this long testTimeMiliseconds, int UtcOffset)
        {
            var testDate = new DateTime(1970, 1, 1, 0, 0, 0);
            testDate = testDate.AddMilliseconds(testTimeMiliseconds);
            testDate = testDate.AddMinutes(UtcOffset);
            return testDate.ToString("MM/dd/yyyy hh:mm tt");
        }
    }
}