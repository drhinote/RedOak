using Newtonsoft.Json;
using Roi.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Script.Serialization;

namespace RedoakAdmin.Controllers
{
    public class DevicesController : ApiController
    {
        // GET api/<controller>
        public string Get()
        {
            try
            {
                using (var ctx = new Roi.Data.RoiDb())
                {
                    var res = ctx.Devices.Select(d => new ExistingDevice()
                    {
                        Name = d.Serial,
                        Company_Id = d.CompanyId.ToString(),
                        Enabled = d.Enabled,
                        Device_Id = d.Id.ToString()
                    });
                    return JsonConvert.SerializeObject(res);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        // GET api/<controller>/5
        public string Get(string deviceName)
        {
            using (var ctx = new Roi.Data.RoiDb())
            {

                return JsonConvert.SerializeObject(ctx.Devices.Where(d => d.Serial == deviceName).FirstOrDefault());
            }
            
        }

        [Route("api/devices/{deviceName}/isregistered")]
        [HttpGet]
        public bool IsRegistered(string deviceName)
        {
            using (var ctx = new Roi.Data.RoiDb())
            {
                return ctx.Devices.Any(d => d.Serial == deviceName);
            }

        }

        [Route("api/devices/{companyId}/company")]
        [HttpGet]
        public string GetCompanyById(string companyId)
        {
            try
            {
                using (var ctx = new Roi.Data.RoiDb())
                {
                    var res = ctx.Companies.Where(c => c.Id == new Guid(companyId)).Select(c => new ExistingCompany()
                    {
                        Id = c.Id,
                        Name = c.Name
                    }).FirstOrDefault();
                    return new JavaScriptSerializer().Serialize(res);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        //[Route("api/devices/{companyId}/company")]
        //[HttpGet]
        //public string GetTestsByDevicesName(string deviceName)
        //{
        //    try
        //    {
        //        using (var ctx = new Roi.Data.RoiDb())
        //        {
        //            var res = ctx.Companies.Where(c => c.Id == new Guid(companyId)).Select(c => new ExistingCompany()
        //            {
        //                Id = c.Id,
        //                Name = c.Name,
        //                Company = c.company
        //            });
        //            return new JavaScriptSerializer().Serialize(res);
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception(e.Message);
        //    }
        //}

        [Route("api/devices/update")]
        [HttpPost]
        public bool Update([FromBody]string value)
        {
            try
            {
                using (var ctx = new Roi.Data.RoiDb())
                {
                    var updateDevice = JsonConvert.DeserializeObject<UpdateDevice>(value);
                    var device = ctx.Devices.Where(d => d.Id.ToString() == updateDevice.DeviceId).FirstOrDefault();
                    var company = ctx.Companies.Where(c => c.Id == new Guid(updateDevice.CompanyId)).FirstOrDefault();
                    // update the unfortunate company name fields
                    device.Company = company;
                    device.CompanyId = company.Id;
                    // update the machine fields
                    ctx.Devices.AddOrUpdate(device);
                    // add the machine to the company
                    if (!company.Devices.Contains(device))
                    {
                        company.Devices.Add(device);
                    }

                    return (ctx.SaveChanges() == 1) ? true : false;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [Route("api/devices/new")]
        [HttpPost]
		[ResponseType(typeof(Device))]
		public IHttpActionResult AddDevice([FromBody]Device NewDevice)
        {
            try
            {
                using (var ctx = new RoiDb())
                {
					NewDevice.Id = Guid.NewGuid();
					ctx.Devices.Add(NewDevice);
					ctx.SaveChanges();

					return Ok(NewDevice);
                }
            }
            catch (Exception e)
            {
				var message = e.Message;
				message += (e.InnerException != null) ? e.InnerException.ToString() + "\n\r" : "";
				message += (e.StackTrace != null) ? e.StackTrace.ToString() : "";
				return BadRequest(message);
			}
        }

            // POST api/<controller>
        public string Post([FromBody]string value)
        {
            try
            {
                // get company name and device id
                var newDevice = JsonConvert.DeserializeObject<NewDevice>(value);

                using (var ctx = new Roi.Data.RoiDb())
                {
                    if (ctx.Devices.Any(d => d.Serial == newDevice.DeviceName))
                    {
                        var currentCompany = ctx.Devices.Where(d => d.Serial == newDevice.DeviceName).Select(d => d.Company.Name).FirstOrDefault();
                        return JsonConvert.SerializeObject(new DeviceRegistrationStatus()
                        {
                            Success = false,
                            Code = "DeviceRegistered",
                            Message = $"This device: { newDevice.DeviceId } is alreay registered company: { currentCompany }"
                        });
                    }

                    // create new device
                    var device = new Device()
                    {
						Id = Guid.NewGuid(),
						Serial = newDevice.DeviceName,
                        CompanyId = new Guid(newDevice.CompanyId),
                        Enabled = true,
                    };

                    // add device to company
                    var company = ctx.Companies.Where(c => c.Name == newDevice.CompanyName).FirstOrDefault();

                    company.Devices.Add(device);

                    ctx.SaveChanges();

                    return JsonConvert.SerializeObject(new DeviceRegistrationStatus()
                    {
                        Success = true,
                        Code = "",
                        Message = $"registered device: { newDevice.DeviceId } to company: { newDevice.CompanyName }"
                    });
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
                    // get and remove the device
                    ctx.Devices.Remove(ctx.Devices.Where(d => d.Id == new Guid(id)).FirstOrDefault());
                    return (ctx.SaveChanges() == 1) ? true : false;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public class UpdateDevice
        {
            public string DeviceId { get; set; }
            public string CompanyId { get; set; }
        }

        public class NewDevice
        {
            public string DeviceId { get; set; }
            public string DeviceName { get; set; }
            public string CompanyName { get; set; }
            public string CompanyId { get; set; }
        }

        public class ExistingDevice
        {
            public string Name { get; set; }
            public long Time { get; set; } = 0;
            public string Company { get; set; }
            public string Company_Name { get; set; }
            public string Company_Id { get; set; }
            public bool Enabled { get; set; } = true;
            public string Device_Id { get; set; }
        }

        public class ExistingCompany : Company
        {
            //public Guid Id { get; set; }
            //public string Name { get; set; }
            //public string Contact { get; set; }
            //public string Address { get; set; }
            //public string City { get; set; }
            //public string State { get; set; }
            //public string Zip { get; set; }
            //public string Phone { get; set; }
            //public string Email { get; set; }
        }

        public class DeviceRegistrationStatus
        {
            public bool Success { get; set; }
            public string Code { get; set; }
            public string Message { get; set; }
        }
    }
}