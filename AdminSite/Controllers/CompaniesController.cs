using Newtonsoft.Json;
using Roi.Data;
using System;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Script.Serialization;

namespace RedoakAdmin.Controllers
{
    public class CompaniesController : ApiController
    {
        // GET api/<controller>
        public string Get()
        {
            try
            {
                using (var ctx = new RoiDb())
                {
                    var res = ctx.Companies.Select(c => new ExistingCompanies() { Id = c.Id.ToString(), Name = c.Name }).ToArray();
                    return new JavaScriptSerializer().Serialize(res);
                }
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

		// GET api/<controller>/5
		[HttpGet]
		public string Get(string id)
        {
            try
            {
                using (var ctx = new RoiDb())
                {
                    var res = ctx.Companies.Where(c => c.Id.ToString() == id);
                    return new JavaScriptSerializer().Serialize(res);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [Route("api/companies/getall")]
        [HttpGet]
        public string GetAll()
        {
            try
            {
                using (var ctx = new RoiDb())
                {
                    var res = ctx.Companies.Where(c => c.IsActive == true).Select(c => new ExistingCompany()
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Address = c.Address,
                        Contact = c.Contact,
                        City = c.City,
                        State = c.State,
                        Zip = c.Zip,
                        Phone = c.Phone,
                        Email = c.Email,
                        IsActive = true
                    }).OrderBy(c => c.Name);
                    return JsonConvert.SerializeObject(res);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [Route("api/companies/{companyId}/devices")]
        [HttpGet]
        public string GetDevicesByCompanyId(Guid companyId)
        {
            try
            {
                using (var ctx = new Roi.Data.RoiDb())
                {
                    var res = ctx.Devices.Where(c => c.CompanyId == companyId).Select(d => new CompanyDevice()
                    {
                        Name = d.Serial,
                        Enabled = d.Enabled,
                        device_id = d.Id.ToString()
                    });
                    return JsonConvert.SerializeObject(res);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [Route("api/companies/update")]
        [HttpPost]
        public bool Update([FromBody]string value)
        {
            try
            {
                using (var ctx = new Roi.Data.RoiDb())
                {
                    Roi.Data.Company updateCompany = JsonConvert.DeserializeObject<Roi.Data.Company>(value);
                    ctx.Companies.AddOrUpdate(updateCompany);
                    return (ctx.SaveChanges() == 1) ? true : false;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [Route("api/companies/new")]
        [HttpPost]
		[ResponseType(typeof(Company))]
		public IHttpActionResult AddCompany([FromBody]string value)
        {
            try
            {
                Roi.Data.Company newCompany = JsonConvert.DeserializeObject<Company>(value);
                using (var ctx = new RoiDb())
                {
                    // create a new company
                    newCompany.Id = Guid.NewGuid();
					newCompany.IsActive = true;
                    // add new company
                    ctx.Companies.Add(newCompany);
					ctx.SaveChanges();
                    return Ok(newCompany);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        // this one needs to be combined with the method above once setup.aspx has been updated

        // POST api/<controller>
        public bool Post([FromBody]string value)
        {
            var company = new NewCompany();
            try
            {
                company = JsonConvert.DeserializeObject<NewCompany>(value);
                using (var ctx = new Roi.Data.RoiDb())
                {
                    // create a new company
                    var newCompany = new Roi.Data.Company()
                    {
                        Id = Guid.NewGuid(),
                        Name = company.Name,
                        Address = company.Address,
                        Contact = company.Contact,
                        City = company.City,
                        State = company.State,
                        Zip = company.Zip,
                        Phone = company.Phone,
                        Email = company.Email,
                        IsActive = true
                    };
                    // add new company
                    ctx.Companies.Add(newCompany);
                    return (ctx.SaveChanges() == 1) ? true : false;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public bool Delete(string id)
        {
            try
            {
                using (var ctx = new Roi.Data.RoiDb())
                {
                    // get company
                    var company = ctx.Companies.Where(c => c.Id == new Guid(id)).FirstOrDefault();

                    // update the users assigned to the company
                    //var users = ctx.Testers.Where(u => u. == company.Name).ToArray();
                    //foreach (var user in users)
                    //{
                    //    user.IsDeleted = true;
                    //    ctx.Testers.AddOrUpdate(user);
                    //}

                    // update the status of each device assigned to the company
                    //var devices = ctx.machines.Where(m => m.company_name == company.Name).ToArray();
                    //foreach(var dev in devices)
                    //{
                    //}


                    // update the company
                    company.Name = "_" + company.Name;
                    company.IsActive = false;
                    ctx.Companies.AddOrUpdate(company);
                    return (ctx.SaveChanges() == 1) ? true : false;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public class CompanyDevice
        {
            public string Name { get; set; }
            public bool Enabled { get; set; }
            public string device_id { get; set; }
        }

        public class ExistingCompany
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Company { get; set; }
            public string Contact { get; set; }
            public string Address { get; set; }
            public string City { get; set; }
            public string State { get; set; }
            public string Zip { get; set; }
            public string Phone { get; set; }
            public string Email { get; set; }
            public bool IsActive { get; set; }
        }

        public class NewCompany
        {
            public string Name { get; set; }
            public string Contact { get; set; }
            public string Address { get; set; }
            public string City { get; set; }
            public string State { get; set; }
            public string Zip { get; set; }
            public string Phone { get; set; }
            public string Email { get; set; }
            public bool IsActive { get; set; }
        }

        public class ExistingCompanies
        {
            public string Name { get; set; }
            public string Id { get; set; }
        }
    }
}