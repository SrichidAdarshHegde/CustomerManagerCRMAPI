using FPL.Dal.DataModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace FPL.Api.Controllers
{
    public class CustomerRegistrationController : ApiController
    {
        private CustomerManagerEntities db = new CustomerManagerEntities();


        [HttpPost]
        public async Task<IHttpActionResult> PostSaveCustomerRegistration(Table_CustomerRegistartionDataModel data1)
        {
            int clusterID = Convert.ToInt32(data1.Cluster);
            int routeID = Convert.ToInt32(data1.RouteNumber);
            int regionID = Convert.ToInt32(data1.Region);
            int zoneID = Convert.ToInt32(data1.Zone);
            var clustername = db.Table_Cluster.Where(c=>c.ClusterId== clusterID).Select(c=>c.ClusterName).FirstOrDefault();
            var routename = db.Table_Route.Where(c => c.RouteId == routeID).Select(c => c.RouteName).FirstOrDefault();
            var regionname = db.Table_Region.Where(c => c.RegionId == regionID).Select(c => c.RegionName).FirstOrDefault();
            var zonename = db.Table_Zone.Where(c => c.ZoneId == zoneID).Select(c => c.ZoneName).FirstOrDefault();

            try
            {
                    Table_CustomerRegistartion data = new Table_CustomerRegistartion()
                    {
                      AddressOne = data1.AddressOne,
                      AddressThree = data1.AddressThree,
                      AddressTwo = data1.AddressTwo,
                      City = data1.City,
                      Cluster = clustername,
                      ClusterId= clusterID,
                      RouteId= routeID,
                      ZoneId= zoneID,
                      RegionId= regionID,
                      CompanyName = data1.CompanyName,
                      CompanyOldName = data1.CompanyOldName,
                     // Country = data1.Country,
                      CreatedBy = data1.CreatedBy,
                      CreatedOn = DateTime.Now,
                      CustomerID = 0,
                      GSTIN = data1.GSTIN,
                      Pincode = data1.Pincode,
                      Region = regionname,
                      RouteNumber = routename,
                      SecurityFormalities = data1.SecurityFormalities,
                      State = data1.State,
                      Unit = data1.Unit,
                      WeeklyOff = data1.WeeklyOff,
                      WorkingEnd = data1.WorkingEnd,
                      WorkingStart = data1.WorkingStart,
                      Zone = zonename,
                      
                    };

                    await Task.Run(() => db.Table_CustomerRegistartion.Add(data));
                    await db.SaveChangesAsync();

                    return Ok("success");
                }
                catch (Exception e)
                {

                    throw e;
                }
                                                       
    }



        [HttpPost]
        public async Task<IHttpActionResult> PostSaveUpdateCustomerRegistration(Table_CustomerRegistartionDataModel data1)
        {

            int customerIDD = Convert.ToInt32(data1.CustomerID);
            int clusterID = Convert.ToInt32(data1.Cluster);
            int routeID = Convert.ToInt32(data1.RouteNumber);
            int regionID = Convert.ToInt32(data1.Region);
            int zoneID = Convert.ToInt32(data1.Zone);
            var clustername = db.Table_Cluster.Where(c => c.ClusterId == clusterID).Select(c => c.ClusterName).FirstOrDefault();
            var routename = db.Table_Route.Where(c => c.RouteId == routeID).Select(c => c.RouteName).FirstOrDefault();
            var regionname = db.Table_Region.Where(c => c.RegionId == regionID).Select(c => c.RegionName).FirstOrDefault();
            var zonename = db.Table_Zone.Where(c => c.ZoneId == zoneID).Select(c => c.ZoneName).FirstOrDefault();

            try
            {
                var dat = await Task.Run(() => db.Table_CustomerRegistartion.Where(c => c.CustomerID == customerIDD).FirstOrDefault());
                if (dat != null)
                {
                    dat.CustomerID = customerIDD;
                    dat.CompanyName =data1.CompanyName;
                    dat.Unit = data1.Unit;
                    dat.AddressOne = data1.AddressOne;
                    dat.AddressTwo = data1.AddressTwo;
                    dat.AddressThree = data1.AddressThree;
                    dat.GSTIN = data1.GSTIN;
                    dat.Cluster = clustername;
                    dat.ClusterId = clusterID;
                    dat.RouteNumber = routename;
                    dat.RouteId = routeID;
                    dat.Region = regionname;
                    dat.RegionId = regionID;
                    dat.Zone = zonename;
                    dat.ZoneId = zoneID;
                    dat.WeeklyOff = data1.WeeklyOff;
                    dat.WorkingStart = data1.WorkingStart;
                    dat.WorkingEnd = data1.WorkingEnd;
                    dat.SecurityFormalities=data1.SecurityFormalities;
                    dat.Pincode = data1.Pincode;
                    dat.City = data1.City;
                    dat.State = data1.State;
                   // dat.Country = data1.Country;
                    //dat.PhotoStatus = 3;              
                    await Task.Run(() => db.Entry(dat).State = EntityState.Modified);
                    await db.SaveChangesAsync();
                }
                return Ok("success");
            }
            catch (Exception e)
            {

                throw e;
            }

        }

        [HttpGet]
        public async Task<IHttpActionResult> GetAllCustomer()
        {
            var result = await Task.Run(() => db.Table_CustomerRegistartion.ToList());
            return Ok(result);
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetParticularCustomer([FromUri(Name = "id")] string id)
        {
            int idd = Convert.ToInt32(id);
            var result = await Task.Run(() => db.Table_CustomerRegistartion.Where(c=>c.CustomerID== idd).FirstOrDefault());
            return Ok(result);
        }


        [HttpGet]
        public async Task<IHttpActionResult> DeleteCustomerData([FromUri(Name = "id")] int id)
        {
            var data = await Task.Run(() => db.Table_CustomerRegistartion.FindAsync(id));

            await Task.Run(() => db.Table_CustomerRegistartion.Remove(data));
            await db.SaveChangesAsync();

            return Ok("success");
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetPerticularCustomer([FromUri(Name = "id")] int id)
        {
            var result = await Task.Run(() => db.Table_CustomerRegistartion.Where(c => c.CustomerID == id).Select(c => c).ToList());
            return Ok(result);
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetPerticularCustomerInvoice([FromUri(Name = "id")] int id)
        {
            var result = await Task.Run(() => db.Table_InvoiceDocuments.Where(c => c.CustomerId == id).Select(c => c).ToList());
            return Ok(result);
        }









        public class Table_CustomerRegistartionDataModel
        {
            public int CustomerID { get; set; }
            public string CompanyName { get; set; }
            public string CompanyOldName { get; set; }
            public string Unit { get; set; }
            public string AddressOne { get; set; }
            public string AddressTwo { get; set; }
            public string AddressThree { get; set; }
            public string Pincode { get; set; }
            public string City { get; set; }
            public string State { get; set; }
            public string Country { get; set; }
            public string GSTIN { get; set; }
            public string Cluster { get; set; }
            public string RouteNumber { get; set; }
            public Nullable<int> RouteId { get; set; }
            public Nullable<int> ClusterId { get; set; }
            public string Region { get; set; }
            public Nullable<int> RegionId { get; set; }
            public string Zone { get; set; }
            public Nullable<int> ZoneId { get; set; }
            public string WeeklyOff { get; set; }
            public string WorkingStart { get; set; }
            public string WorkingEnd { get; set; }
            public string SecurityFormalities { get; set; }
            public string CreatedBy { get; set; }
            public Nullable<System.DateTime> CreatedOn { get; set; }
        }
    }
}
