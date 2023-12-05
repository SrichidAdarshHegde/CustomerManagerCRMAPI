using FPL.Dal.DataModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace FPL.Api.Controllers
{
    public class FollowUpController : ApiController
    {
        private CustomerManagerEntities db = new CustomerManagerEntities();
        [HttpGet]
        public async Task<IHttpActionResult> GetAllListtoFollowUp()
        {
            var result = await Task.Run(() => db.Table_InvoiceDocuments.ToList());
            return Ok(result);
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetPerticularFollowupforCustomer([FromUri(Name = "id")] int id)
        {
            var result = await Task.Run(() => db.Table_InvoiceDocuments.Where(c => c.CustomerId == id ).ToList());
            return Ok(result);
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetPerticularFollowupComplete([FromUri(Name = "id")] string date)
        {
            var dateArray = date.Split(',');
            DateTime fromDate = DateTime.Parse(dateArray[0], CultureInfo.CreateSpecificCulture("en-IN"));
            DateTime toDate = DateTime.Parse(dateArray[1], CultureInfo.CreateSpecificCulture("en-IN"));

            var result = await Task.Run(() => db.Table_InvoiceDocuments.Where(c => c.IsPaid == true && c.CreatedOn >= fromDate && c.CreatedOn <= toDate).ToList());
            return Ok(result);
        }


        [HttpGet]
        public async Task<IHttpActionResult> GetPerticularFollowupPending()
        {
            var result = await Task.Run(() => db.Table_InvoiceDocuments.Where(c => c.IsPaid == false).ToList());
            return Ok(result);
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetFollowupList([FromUri(Name = "id")] string date)
        {
            var dateArray = date.Split(',');
            DateTime fromDate = DateTime.Parse(dateArray[0], CultureInfo.CreateSpecificCulture("en-IN"));
            DateTime toDate = DateTime.Parse(dateArray[1], CultureInfo.CreateSpecificCulture("en-IN"));

            var result = await Task.Run(() => db.Table_FollowUpDetails.Where(c => c.CreatedOn >= fromDate && c.CreatedOn <= toDate).ToList());
            return Ok(result);
        }


        [HttpGet]
        public async Task<IHttpActionResult> GetFollowupListsforCustomer([FromUri(Name = "id")] int id)
        {
            var result = await Task.Run(() => db.Table_FollowUpDetails.Where(c=>c.CustomerID== id).ToList());
            return Ok(result);
        }


        [HttpGet]
        public async Task<IHttpActionResult> GetPerticularFollowupforCustomerComplete([FromUri(Name = "id")] int id)
        {
            var result = await Task.Run(() => db.Table_InvoiceDocuments.Where(c => c.CustomerId == id && c.IsPaid==true).ToList());
            return Ok(result);
        }



        [HttpGet]
        public async Task<IHttpActionResult> GetPerticularFollowupforCustomerPending([FromUri(Name = "id")] int id)
        {
            var result = await Task.Run(() => db.Table_InvoiceDocuments.Where(c => c.CustomerId == id && c.IsPaid == false).ToList());
            return Ok(result);
        }
        public async Task<IHttpActionResult> PostSaveFollowup(Table_FollowUpDetailsVM data1)
        {

            try
            {

                var followdate = db.Table_FollowUpDetails.Where(c => c.MachineNumber == data1.MachineNumber && c.CustomerID == data1.CustomerID && c.ModelID == data1.ModelID).Select(c => c).FirstOrDefault();
                var invdate = db.Table_InvoiceDocuments.Where(c => c.Id == data1.InvoiceID).Select(c => c).FirstOrDefault();


                //var vc = await Task.Run(() => db.Table_FollowUpDetails.Where(c => c.DirectSaleBillNo.ToString() == item.DirectSaleBillNo).Select(c => c.VendorCode).FirstOrDefault());
                if (followdate != null)
                {
                    var penlist = followdate.PaymentsList.ToString();
                    var paylists = penlist+ ", " + data1.followupupdatedamount+ "/- Rs. ";

                    followdate.MachineNumber = data1.MachineNumber;
                    followdate.CustomerID = data1.CustomerID;
                    followdate.ModelName = data1.ModelName;
                    followdate.CustomerName = data1.CustomerName;
                    followdate.ModelID = data1.ModelID;
                    followdate.InvoiceNumber = data1.InvoiceNumber;
                    followdate.InvoiceAmount = data1.InvoiceAmount;
                    followdate.DueAmount = data1.DueAmount - data1.followupupdatedamount;
                    followdate.InvoiceID = data1.InvoiceID;
                    followdate.Date = data1.Date;
                    followdate.Time = data1.Time;
                    followdate.ContactPerson = data1.ContactPerson;
                    followdate.Remarks = data1.Remarks;
                    followdate.OurPerson = data1.OurPerson;
                    followdate.PaymentsList = paylists;
                    invdate.DueAmount = followdate.DueAmount;
                    //dat.PhotoStatus = 3;              
                    await Task.Run(() => db.Entry(followdate).State = EntityState.Modified);

                    if (followdate.DueAmount==0)
                    {
                        invdate.IsPaid = true;
                        await Task.Run(() => db.Entry(invdate).State = EntityState.Modified);
                    }
                   
                        await Task.Run(() => db.Entry(invdate).State = EntityState.Modified);

                

                    await db.SaveChangesAsync();
                    return Ok("success");
                }
                else
                {
                    Table_FollowUpDetails data = new Table_FollowUpDetails()
                    {
                        ID = 0,
                        MachineNumber = data1.MachineNumber,
                        CustomerID = data1.CustomerID,
                        ModelName = data1.ModelName,
                        CustomerName = data1.CustomerName,
                        ModelID = data1.ModelID,
                        InvoiceNumber = data1.InvoiceNumber,
                        InvoiceAmount = data1.InvoiceAmount,
                        DueAmount = data1.DueAmount - data1.followupupdatedamount,
                        InvoiceID = data1.InvoiceID,
                        Date = data1.Date,
                        Time = data1.Time,
                        ContactPerson = data1.ContactPerson,
                        Remarks = data1.Remarks,
                        OurPerson = data1.OurPerson,
                        CreatedOn = DateTime.Now,
                        PaymentsList = data1.DueAmount.ToString()+ "/- Rs.",

                };
                    invdate.DueAmount = data.DueAmount;
                    if (data.DueAmount == 0)
                    {
                        invdate.IsPaid = true;
                    }
                    await Task.Run(() => db.Entry(invdate).State = EntityState.Modified);
                    await Task.Run(() => db.Table_FollowUpDetails.Add(data));
                    await db.SaveChangesAsync();

                    return Ok("success");
                }
               
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
            }
        }

    }
    public partial class Table_FollowUpDetailsVM
    {
        public int ID { get; set; }
        public Nullable<int> InvoiceID { get; set; }
        public Nullable<int> MachineNumber { get; set; }
        public Nullable<int> CustomerID { get; set; }
        public string CustomerName { get; set; }
        public Nullable<int> ModelID { get; set; }
        public string ModelName { get; set; }
        public string InvoiceNumber { get; set; }
        public Nullable<decimal> InvoiceAmount { get; set; }
        public Nullable<decimal> DueAmount { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string ContactPerson { get; set; }
        public string Remarks { get; set; }
        public string OurPerson { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string PaymentsList { get; set; }
        public decimal? followupupdatedamount { get; set; }
    }
}
