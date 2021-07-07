namespace Roi.Data.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Text;

    internal sealed class Configuration : DbMigrationsConfiguration<Roi.Data.RoiDb>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        Guid ToGuid(string text)
        {
            byte[] bytes;
            var data = Encoding.UTF8.GetBytes(text);
            if (data.Length < 16)
            {
                bytes = new byte[16];
                bytes.Initialize();
                data.CopyTo(bytes, 0);
            }
            else
            {
                bytes = data.Take(16).ToArray();
            }
            return new Guid(bytes);
        }

        protected override void Seed(Roi.Data.RoiDb context)
        {
            using (var oldDb = new OldDb.OldDbCtx())
            {
                var companies = new Dictionary<Guid, Company>();
                var nameToId = new Dictionary<string, Guid>();
                var testers = new HashSet<Guid>();
                foreach (var co in oldDb.Companies)
                {
                    var company = new Company
                    {
                        Id = co.Id,
                        Name = co.Name,
                        Address = co.Address,
                        City = co.city,
                        State = co.state,
                        Zip = co.zip,
                        Contact = co.contact,
                        Phone = co.phone,
                        IsActive = true,
                        Email = co.email
                    };
                    companies.Add(company.Id, company);
                    nameToId.Add(co.Name.ToLowerInvariant(), co.Id);
                    if(co.company1!=null && !nameToId.ContainsKey(co.company1.ToLowerInvariant())) nameToId.Add(co.company1.ToLowerInvariant(), co.Id);
                    context.Companies.AddOrUpdate(company);
                }

                foreach (var te in oldDb.authorized_ids)
                {

                    var tester = new Tester
                    {
                        Active = true,
                        Id = ToGuid(te.userid),
                        Name = te.userid,
                        Password = te.paw,

                    };
                    tester.Companies.Add(te.Company_Id.HasValue && companies.ContainsKey(te.Company_Id.Value)?companies[te.Company_Id.Value]:companies[nameToId.ContainsKey(te.company.ToLowerInvariant())? nameToId[te.company.ToLowerInvariant()]: nameToId["regcal"]] );
                    context.Testers.AddOrUpdate(tester);
                    testers.Add(tester.Id);
                }

                foreach (var ma in oldDb.machines)
                {
                    context.Devices.AddOrUpdate(new Device
                    {
                        CompanyId = ma.Company_Id.Value,
                        Enabled = true,
                        Serial = ma.name,
                        Id = ToGuid(ma.name),
                    });
                }

                foreach (var su in oldDb.subjects)
                {
                    context.Subjects.AddOrUpdate(new Subject
                    {
                        CompanyId = nameToId[su.company.ToLowerInvariant()],
                        Id = ToGuid((su.uuid.Length > 12?su.uuid.Substring(su.uuid.Length-12):su.uuid) + nameToId[su.company.ToLowerInvariant()].ToString()),
                        Name = su.name,
                        Dob = su.dob,
                        History = su.history,
                        OpId = su.opid,
                        Social = su.social,
                        UuId = su.uuid
                    });
                }

                foreach (var te in oldDb.tests)
                {
                    var testerId = ToGuid(te.tester);
                    if (!testers.Contains(testerId)) testerId = testers.First();
                    context.Tests.AddOrUpdate(new Test
                    {
                        Id = ToGuid(te.time.ToString()),
                        CompanyId = nameToId[te.company.ToLowerInvariant()],
                        UnixTimeStamp = te.time,
                        Dob = te.dob,
                        History = te.history,
                        OpId = te.opid,
                        UuId = te.uuid,
                        TesterId = testerId
                    });
                }

                context.SaveChanges();
            }
        }
    }
}
