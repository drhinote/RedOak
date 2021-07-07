using Microsoft.Azure.WebJobs;
using Microsoft.WindowsAzure.Storage.Blob;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Roi.Data.Models;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Roi.Data
{
    public partial class RawToText : ServiceBase
    {
        public RawToText()
        {
            InitializeComponent();
        }

        private JobHost Host { get; set; }

        protected override void OnStart(string[] args)
        {
            Host = new JobHost();
            
            Host.Start();
        }

        public static void ProcessRawTest([QueueTrigger("rawtoxlsx")] string name, [Blob("raw/{queueTrigger}", FileAccess.Read)] TextReader blob, TextWriter logger)
        {
            var test = JToken.ReadFrom(new JsonTextReader(blob)).ToObject<TestContainer>();
            test.ProcessRawData();
        }


        protected override void OnStop()
        {
            Host.Dispose();
        }
    }
}
