using Newtonsoft.Json;
using Roi.Data.Models;
using Roi.Logic;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Roi.Data.BusinessLogic
{
    public class DeviceData
    {
        string _uuid;
        long _time;

        public enum TestType
        {
            TwoSymbol = 2,
            ThreeSymbol = 3
        }
        public enum SideOfBody
        {
            Left = 'l',
            Right = 'r'
        }

        public DeviceData(string uuid, long time)
        {
            _uuid = uuid;
            _time = time;
        }

       
    }

}
