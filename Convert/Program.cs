using Microsoft.OData.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Convert
{
    class Program
    {
        static bool isDelim(byte[] d, int idx)
        {
            for (int j = idx; j < Math.Min(idx + 10, d.Length); j++)
            {
                if (d[j] != 127 && d[j] != 128 && d[j] != 123 && d[j] != 124) return false;
            }
            return true;
        }

        static string hwToHist(byte[] hw)
        {
            string res = "";
            for(int i = 3; i < hw.Length; i += 3)
            {
               res += BitConverter.ToInt32(new byte[] { hw[i + 2], hw[i + 1], hw[i], 0 }, 0) + ",";
            }

            res += "{*****}Gender*{&}$Male${&}*Race*{&}$${&}*Dominant hand*{&}$Right${&}*Height*{&}$${&}*Height units*{&}$${&}*Weight*{&}$${&}*Weight units*{&}$${&}*General health*{&}$${&}*Smoker*{&}$${&}*Caffeine*{&}$${&}*Repetitive activities*{&}$${&}*Fine Motor activities*{&}$${&}*Type of work*{&}$${&}*Left hand/wrist numbness*{&}$${&}*Right hand/wrist numbness*{&}$${&}*Left physical pain*{&}$${&}*Right physical pain*{&}$${&}*Left hand weakness/poor coordination*{&}$${&}*Right hand weakness/poor coordination*{&}$${&}*Considers left hand normal*{&}$${&}*Considers right hand normal*{&}$${&}*Diagnosed with TBI*{&}$${&}*Diagnosed with PTSD*{&}$${&}*Headaches*{&}$${&}*Nausea*{&}$${&}*Balance problems*{&}$${&}*Fatigued*{&}$${&}*Concentration/Memory problems*{&}$${&}*TBI Date, event 1*{&}$${&}*TBI Cause, event 1*{&}$${&}*TBI Care Received, event 1*{&}$${&}*TBI was Diagnosed, event 1*{&}$${&}*TBI allowed recovery, event 1*{&}$${&}*TBI Date, event 2*{&}$${&}*TBI Cause, event 2*{&}$${&}*TBI Care Received, event 2*{&}$${&}*TBI was Diagnosed, event 2*{&}$${&}*TBI allowed recovery, event 2*{&}$${&}*Military service*{&}$${&}*Number of tours*{&}$${&}*Military comments*{&}$${&}*Sport(s) played*{&}$${&}*Sports # years played*{&}$${&}*Sports comments*{&}$${&}*";
            return res;
        }

        //Every time a OData request is build it adds an Authorization Header with the acesstoken 
        private static void OnBuildingRequest(object sender, BuildingRequestEventArgs e)
        {
            e.Headers.Add("Authorization", "Bearer " + "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJncm91cHNpZCI6IjljYTYxNjJhLWRjOTYtNDRiYS04YzA4LTY3YzEyNjA1ODc1YSIsInVuaXF1ZV9uYW1lIjoidGFybm93LTEiLCJuYmYiOjE1NjQ2NzcxMjksImV4cCI6MTU2NDcxMzEyOSwiaWF0IjoxNTY0Njc3MTI5fQ.YGlcmNBYPoU68Z7zqlhTNLdjWWhJ5-7wh8XALor3Sms");
        }


        static void Main(string[] args)
        {
            var path = @"C:\Users\David\Desktop\Roi\nor\";
            byte[] sep1 = new byte[] { 127, 127, 127, 127, 127, 127, 127, 127, 127, 127 };
            byte[] sep2 = new byte[] { 128, 128, 128, 128, 128, 128, 128, 128, 128, 128 };
            byte[] rep1 = new byte[] { 123, 123, 123, 123, 123, 123, 123, 123, 123, 123 };
            byte[] rep2 = new byte[] { 124, 124, 124, 124, 124, 124, 124, 124, 124, 124 };
            var ids = new HashSet<string>();
            var subjs = new HashSet<string>();
           
           


       
            foreach (var file in Directory.EnumerateFiles(path).Select(f => f.Substring(f.LastIndexOf("\\") + 1)))
            {
           //     var client = new Default.Container(new Uri("https://roidata.azurewebsites.net/"));
            //    client.BuildingRequest += (sender, e) => OnBuildingRequest(sender, e);
                var id = file.Substring(0, file.Length - 2);
                var uuid = id.Substring(0, id.LastIndexOf("-"));
                var type = file.Substring(file.Length - 1);
                var bin = File.ReadAllBytes(path + file);
                var hw = bin.Take(48).ToArray();
                var time = long.Parse(id.Substring(id.LastIndexOf("-") + 1));
                //if (!subjs.Contains(uuid))
                //{
                //    client.AddToSubjects(new Roi.Data.Subject
                //    {
                //        Id = Guid.NewGuid(),
                //        CompanyId = Guid.Parse("9CA6162A-DC96-44BA-8C08-67C12605875A"),
                //        Dob = "1/1/1980",
                //        Name = uuid,
                //        OpId = uuid,
                //        Social = "1234",
                //        UuId = uuid,
                //    });
                //}
                //if (!ids.Contains(id))
                //{
                //    client.AddToTests(new Roi.Data.Test
                //    {
                //        Id = Guid.NewGuid(),
                //        UuId = uuid,
                //        CompanyId = Guid.Parse("9CA6162A-DC96-44BA-8C08-67C12605875A"),
                //        UnixTimeStamp = time,
                //        Dob = "1/1/1980",
                //        History = hwToHist(hw),
                //        OpId = uuid,
                //        TesterId = Guid.Parse("6C6C6962-0000-0000-0000-000000000000"),
                //    });
                //}
                ids.Add(id);
                subjs.Add(uuid);
               
                Directory.CreateDirectory(path + @"converted\" + id);
                File.WriteAllBytes(path + @"converted\" + id + "\\" + "hardware", hw);
                List<byte> right = new List<byte>();
                List<byte> left = new List<byte>();
               // bool sw = false;
                bool delim = false;
                byte td = 0;
                List<byte> cur = right;
                for (int i = 48; i < bin.Length; i++)
                {
                    if (!delim && isDelim(bin, i))
                    {
                        td = bin[i];
                        delim = true;
                        if (td == 128 || td == 124) cur = right;
                        if (td == 127 || td == 123) cur = left;
                    }
                    if (delim && bin[i] != td) delim = false;
                    if (delim && td == 127) cur.Add(123);
                    else if (delim && td == 128) cur.Add(124);
                    else cur.Add(bin[i]);
                }

                File.WriteAllBytes(path + @"converted\" + id + "\\" + "right" + type, right.ToArray());
                File.WriteAllBytes(path + @"converted\" + id + "\\" + "left" + type, left.ToArray());

               // client.SaveChanges();
            }
            File.WriteAllText(path + "ids.txt", String.Join("\r\n", subjs));

        }
    }
}
