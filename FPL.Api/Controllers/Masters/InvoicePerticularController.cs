using FPL.Dal.DataModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace FPL.Api.Controllers.Masters
{
    public class InvoicePerticularController : ApiController
    {
        private CustomerManagerEntities db = new CustomerManagerEntities();

        [HttpGet]
        public async Task<IHttpActionResult> GetAllInvoicePerticular()
        {
            var result = await Task.Run(() => db.Table_InvoicePerticular.ToList());
            return Ok(result);
        }

        public async Task<IHttpActionResult> PostSaveInvoicePerticular(InvoicePerticularMasterDetails data1)
        {

            try
            {
                Table_InvoicePerticular data = new Table_InvoicePerticular()
                {
                    InvoicePerticularId = 0,
                    InvoicePerticularName = data1.InvoicePerticularName,
                    CreatedOn = DateTime.Now,
                    CreatedBy = data1.CreatedBy
                };

                await Task.Run(() => db.Table_InvoicePerticular.Add(data));
                await db.SaveChangesAsync();

                return Ok("success");
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
            }
        }
        [HttpPost]
        public async Task<IHttpActionResult> PostSaveInvoiceUpdatePerticular(InvoicePerticularMasterDetails data1)
        {

            try
            {
                int invoiceID = Convert.ToInt32(data1.InvoicePerticularId);
                var data = db.Table_InvoicePerticular.Where(c => c.InvoicePerticularId == invoiceID).FirstOrDefault();
                data.InvoicePerticularName = data1.InvoicePerticularName;
                data.CreatedBy = data1.CreatedBy;
                //data.CreatedOn = data1.CreatedOn;

                await Task.Run(() => db.Entry(data).State = EntityState.Modified);
                await db.SaveChangesAsync();

                return Ok("success");
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
            }
        }

        [HttpGet]
        public async Task<IHttpActionResult> DeleteInvoicePerticularData([FromUri(Name = "id")] int id)
        {
            var data = await Task.Run(() => db.Table_InvoicePerticular.FindAsync(id));

            await Task.Run(() => db.Table_InvoicePerticular.Remove(data));
            await db.SaveChangesAsync();

            return Ok("success");
        }
        public partial class InvoicePerticularMasterDetails
        {
            public int InvoicePerticularId { get; set; }
            public string InvoicePerticularName { get; set; }
            public string CreatedBy { get; set; }
            public Nullable<System.DateTime> CreatedOn { get; set; }
        }
    }
}
