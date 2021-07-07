using Newtonsoft.Json;
using Roi.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Script.Serialization;

namespace RedoakAdmin.Controllers
{
    public class UsersController : ApiController
    {
        // this will replace Get() at once setup wizard is reworked
        [Route("api/users/getall")]
        [HttpGet]
        public string GetAll()
        {
            try
            {
                using (var ctx = new RoiDb())
                {
                    var res = ctx.Testers.Where(u => u.Active == true).Select(u => new usr()
					{
						Id = u.Id,
						Name = u.Name,
					}).OrderBy(u => u.Name);
                    return JsonConvert.SerializeObject(res);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [Route("api/users/company/{UserId}")]
        [HttpGet]
        public string GetCompany(Guid UserId)
        {
            try
            {
                using (var ctx = new RoiDb())
                {
					var companies = ctx.Testers.Where(u => u.Id == UserId).Select(u => u.Companies).ToList();

					List<CompanyContainer> companiesList = new List<CompanyContainer>();

					foreach (var company in companies[0])
					{
						companiesList.Add(new CompanyContainer()
						{
							Id = company.Id,
							Name = company.Name,
							Address = company.Address,
							Contact = company.Contact,
							City = company.City,
							State = company.State,
							Zip = company.Zip,
							Phone = company.Phone,
							Email = company.Email
						});
					}
					return JsonConvert.SerializeObject(companiesList);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

		// GET api/<controller>
		[HttpGet]
		public string Get()
        {
            try
            {
                using (var ctx = new RoiDb())
                {
                    var res = ctx.Testers.Select(u => new CompanyUser()
                    {
                        Name = u.Name,
                    });
                    return new JavaScriptSerializer().Serialize(res);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

		// GET api/<controller>/5
		[HttpGet]
		[Route("api/users/{companyId}")]
		public string Get(Guid companyId)
        {
            try
            {
                using (var ctx = new RoiDb())
                {
					var res = ctx.Companies.Where(c => c.Id == companyId).FirstOrDefault();
					List<usr> testers = new List<usr>();
					foreach(var tester in res.Testers)
					{
						testers.Add(new usr() { Name = tester.Name });
					}
                    return new JavaScriptSerializer().Serialize(testers);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

		// POST api/<controller>
		[ResponseType(typeof(TesterCompanies))]
		public IHttpActionResult Post([FromBody]TesterCompanies update)
        {
            try
            {
				Tester tester;

                using (var ctx = new Roi.Data.RoiDb())
                {
					// new user
					if (update.Id == null)
					{
						tester = new Tester()
						{
							Id = Guid.NewGuid(),
							Name = update.Name,
							Password = update.Password,
							Info = update.Info,
							Active = true
						};
					}
					else
					{
						tester = ctx.Testers.Where(t => t.Id == update.Id).FirstOrDefault();
						if (tester == null)
						{
							tester = new Tester()
							{
								Id = update.Id
							};
						}
						tester.Name = update.Name;
						tester.Password = update.Password;
						tester.Info = update.Info;
						tester.Active = true;
					}

					ctx.Testers.AddOrUpdate(tester);
					ctx.SaveChanges();

					var company = ctx.Companies.Where(c => c.Id == update.CompanyId).FirstOrDefault();
					//tester.Companies.Add(company);
					
					company.Testers.Add(tester);
					ctx.Companies.AddOrUpdate(company);
					

					ctx.SaveChanges();

					return Ok(update);
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
                    // get user
                    var user = ctx.Testers.Where(u => u.Name == id).FirstOrDefault();

                    // update the user
                    //user.userid = "_" + user.userid;
                    user.Active = false;
                    ctx.Testers.AddOrUpdate(user);
                    return (ctx.SaveChanges() == 1) ? true : false;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public class CompanyUser : Tester
        {
            public string CompanyName { get; set; }
        }

        public class CompanyContainer
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
        }

		public class usr
		{
			public Guid Id { get; set; }
			public string Name { get; set; }
		}

		public class TesterCompanies : Tester
		{
			public Guid CompanyId { get; set; }
		}
    }
}