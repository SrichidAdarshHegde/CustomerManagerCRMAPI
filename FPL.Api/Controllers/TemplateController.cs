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
    public class TemplateController : ApiController
    {


        private CustomerManagerEntities db = new CustomerManagerEntities();
        [HttpGet]
        public async Task<IHttpActionResult> GetAllTemplate()
        {
            var result = await Task.Run(() => db.table_template.ToList());
            return Ok(result);
        }

        public async Task<IHttpActionResult> PostSaveTemplate(TemplateMasterDetails data1)
        {

            try
            {
                table_template data = new table_template()
                {
                    TemplateID = 0,
                    TemplateName = data1.TemplateName,
                    CreatedOn = DateTime.Now,
                    CreatedBy = data1.CreatedBy
                };

                await Task.Run(() => db.table_template.Add(data));
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
        public async Task<IHttpActionResult> PostSaveUpdateTemplate(TemplateMasterDetails data1)
        {

            try
            {
                int templateID = Convert.ToInt32(data1.TemplateID);
                var data = db.table_template.Where(c => c.TemplateID == templateID).FirstOrDefault();
                data.TemplateName = data1.TemplateName;
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
        public async Task<IHttpActionResult> DeleteTemplateData([FromUri(Name = "id")] int id)
        {
            var data = await Task.Run(() => db.table_template.FindAsync(id));

            await Task.Run(() => db.table_template.Remove(data));
            await db.SaveChangesAsync();

            return Ok("success");
        }


        public partial class TemplateMasterDetails
        {
            public int TemplateID { get; set; }
            public string TemplateName { get; set; }
            public string CreatedBy { get; set; }
            public Nullable<System.DateTime> CreatedOn { get; set; }
        }
    }

}