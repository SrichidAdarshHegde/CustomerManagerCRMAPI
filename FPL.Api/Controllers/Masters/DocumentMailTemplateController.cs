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
    public class DocumentMailTemplateController : ApiController
    {
        private CustomerManagerEntities db = new CustomerManagerEntities();

        [HttpGet]
        public async Task<IHttpActionResult> getMailTemplate()
        {
            var result = await Task.Run(() => db.Table_DocumentMailTemplate.ToList());
            return Ok(result);
        }

        public async Task<IHttpActionResult> PostsaveMailtemplate(DocumentMailTemplateDetails data1)
        {
            try
            {
                Table_DocumentMailTemplate data = new Table_DocumentMailTemplate()
                {
                    MailTemplateID = 0,
                    MailTemplateName = data1.MailTemplateName,
                    CreatedOn = DateTime.Now,
                    CreatedBy = data1.CreatedBy
                };

       

                await Task.Run(() => db.Table_DocumentMailTemplate.Add(data));
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
        public async Task<IHttpActionResult> PostUpdateMailTemplate(DocumentMailTemplateDetails data1)
        {

            try
            {
                int mailTemplateID = Convert.ToInt32(data1.MailTemplateID);
                var data = db.Table_DocumentMailTemplate.Where(c => c.MailTemplateID == mailTemplateID).FirstOrDefault();
                data.MailTemplateName = data1.MailTemplateName;



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
        public async Task<IHttpActionResult> deleteMailtemplate([FromUri(Name = "id")] int id)
        {
            var data = await Task.Run(() => db.Table_DocumentMailTemplate.FindAsync(id));

            await Task.Run(() => db.Table_DocumentMailTemplate.Remove(data));
            await db.SaveChangesAsync();

            return Ok("success");
        }
        public partial class DocumentMailTemplateDetails
        {
            public int MailTemplateID { get; set; }
        public string MailTemplateName { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
            public string CreatedBy { get; set; }
        }
}
}
