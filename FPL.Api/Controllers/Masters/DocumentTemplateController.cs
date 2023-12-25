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
    public class DocumentTemplateController : ApiController
    {
        private CustomerManagerEntities db = new CustomerManagerEntities();

        [HttpGet]
        public async Task<IHttpActionResult> getTemplate()
        {
         var result = await Task.Run(() => db.Table_DocumentTemplate.ToList());
            return Ok(result);
        }

        public async Task<IHttpActionResult> PostsaveTemplate(DocumentTemplateDetails data1)
        {
            try
            {
                Table_DocumentTemplate data = new Table_DocumentTemplate()
                {
                    DocumentID = Convert.ToInt32(data1.documentID),
                     DocumentTemplateID = 0,
                     TemplateName = data1.TemplateName,
                     Createdon = DateTime.Now,
                     CreatedBy = data1.CreatedBy,
                    
                 

            };

           

                await Task.Run(() => db.Table_DocumentTemplate.Add(data));
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
        public async Task<IHttpActionResult> PostSaveUpdateTemplate(DocumentTemplateDetails data1)
        {

            try
            {
                int documentTemplateID = Convert.ToInt32(data1.documentID);
                var data = db.Table_DocumentTemplate.Where(c => c.DocumentTemplateID == documentTemplateID).FirstOrDefault();
                data.TemplateName = data1.TemplateName;



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
        public async Task<IHttpActionResult> deleteTemplate([FromUri(Name = "id")] int id)
        {
            var data = await Task.Run(() => db.Table_DocumentTemplate.FindAsync(id));

            await Task.Run(() => db.Table_DocumentTemplate.Remove(data));
            await db.SaveChangesAsync();

            return Ok("success");
        }
        public partial class DocumentTemplateDetails
        {
            public int DocumentTemplateID { get; set; }
            public string TemplateName { get; set; }
            public Nullable<System.DateTime> Createdon { get; set; }

            public string CreatedBy { get; set; }

               public int documentID { get; set; }
        }
    }
}
    