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
    public class BrochureController : ApiController
    {

        private CustomerManagerEntities db = new CustomerManagerEntities();
        [HttpGet]
        public async Task<IHttpActionResult> GetAllBrouchure()
        {
            var result = await Task.Run(() => db.table_brochure.ToList());
            return Ok(result);
        }

        public async Task<IHttpActionResult> PostSaveBrouchure(BrochureMasterDetails data1)
        {

            try
            {
                table_brochure data = new table_brochure()
                {
                    BrochureID = 0,
                    BrochureName = data1.BrochureName,
                    CreatedOn = DateTime.Now,
                    CreatedBy = data1.CreatedBy
                };

                await Task.Run(() => db.table_brochure.Add(data));
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
        public async Task<IHttpActionResult> PostSaveUpdateBrouchure(BrochureMasterDetails data1)
        {

            try
            {
                int templateID = Convert.ToInt32(data1.BrochureID);
                var data = db.table_brochure.Where(c => c.BrochureID == templateID).FirstOrDefault();
                data.BrochureName = data1.BrochureName;
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
        public async Task<IHttpActionResult> DeleteBrouchureData([FromUri(Name = "id")] int id)
        {
            var data = await Task.Run(() => db.table_brochure.FindAsync(id));

            await Task.Run(() => db.table_brochure.Remove(data));
            await db.SaveChangesAsync();

            return Ok("success");
        }

        public partial class BrochureMasterDetails
        {
            public int BrochureID { get; set; }
            public string BrochureName { get; set; }
            public string CreatedBy { get; set; }
            public Nullable<System.DateTime> CreatedOn { get; set; }
        }
    }
}
