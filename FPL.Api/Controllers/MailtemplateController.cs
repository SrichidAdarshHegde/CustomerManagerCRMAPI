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
    public class MailtemplateController : ApiController
    {


        private CustomerManagerEntities db = new CustomerManagerEntities();
        [HttpGet]
        public async Task<IHttpActionResult> GetAllMail()
        {
            var result = await Task.Run(() => db.table_mailtemplate.ToList());
            return Ok(result);
        }

        public async Task<IHttpActionResult> PostSaveMail(MailMasterDetails data1)
        {

            try
            {
                table_mailtemplate data = new table_mailtemplate()
                {
                    MailID = 0,
                    MailName = data1.MailName,
                    CreatedBy = data1.CreatedBy,
                    CreatedOn = DateTime.Now,
                };

                await Task.Run(() => db.table_mailtemplate.Add(data));
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
        public async Task<IHttpActionResult> PostSaveUpdateMail(MailMasterDetails data1)
        {

            try
            {
                int templateID = Convert.ToInt32(data1.MailID);
                var data = db.table_mailtemplate.Where(c => c.MailID == templateID).FirstOrDefault();
                data.MailName = data1.MailName;
                data.CreatedBy = data1.CreatedBy;


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
        public async Task<IHttpActionResult> DeleteMailData([FromUri(Name = "id")] int id)
        {
            var data = await Task.Run(() => db.table_mailtemplate.FindAsync(id));

            await Task.Run(() => db.table_mailtemplate.Remove(data));
            await db.SaveChangesAsync();

            return Ok("success");
        }

        public partial class MailMasterDetails
        {
            public int MailID { get; set; }
            public string MailName { get; set; }
            public string CreatedBy { get; set; }
            public string CreatedOn { get; set; }
        }
    }
}
