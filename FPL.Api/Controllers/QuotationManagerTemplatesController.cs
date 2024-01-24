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
    public class QuotationManagerTemplatesController : ApiController

    {

        private CustomerManagerEntities db = new CustomerManagerEntities();



        [HttpGet]
        public async Task<IHttpActionResult> GetAllRefNo()
        {
            try
            {
                var refID = db.Table_Quotationtemplate.ToList();
                return Ok(refID);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpGet]
        public async Task<IHttpActionResult> Getbillingaddress([FromUri(Name = "id")] int id)
        {

            try
            {
                var billingaddress = db.Table_CustomerRegistartion.Where(c => c.CustomerID == id).ToList();
                return Ok(billingaddress);
            }
            catch (Exception e)
            {
                throw e;
            }

        }



        [HttpPost]
        public async Task<IHttpActionResult> Savequotationtemplate(Quotationtemplatedetails data)
        {
            try
            {
                Table_Quotationtemplate abc = new Table_Quotationtemplate()
                {
                    RefID = data.RefID,

                    BillingAddress = data.BillingAddress,
                };

                await Task.Run(() => db.Table_Quotationtemplate.Add(abc));
                await db.SaveChangesAsync();

                return Ok("success");
            }
            catch (Exception e)
            {

                throw e;
            }
        }



        public partial class Quotationtemplatedetails
        {
            public int RefID { get; set; }
            public string YourEnquiry { get; set; }
            public string KindAttention { get; set; }
            public string BillingAddress { get; set; }
            public string TelephoneNo { get; set; }
            public string Pincode { get; set; }
            public Nullable<System.DateTime> CreatedOn { get; set; }
            public string CreatedBy { get; set; }
        }

    }
}
