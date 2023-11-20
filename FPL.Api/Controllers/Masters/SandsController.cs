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
    public class SandsController : ApiController
    {
        private CustomerManagerEntities db = new CustomerManagerEntities();

        [HttpGet]
        public async Task<IHttpActionResult> GetAllSands()
        {
            var result = await Task.Run(() => db.Table_Sands.ToList());
            return Ok(result);
        }

        public async Task<IHttpActionResult> PostSaveSands(SandsMasterDetails data1)
        {

            try
            {
                Table_Sands data = new Table_Sands()
                {
                    SandsId = 0,
                    SandsName = data1.SandsName,
                    CreatedOn = DateTime.Now,
                    CreatedBy = data1.CreatedBy
                };

                await Task.Run(() => db.Table_Sands.Add(data));
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
        public async Task<IHttpActionResult> PostSaveSandsUpdate(SandsMasterDetails data1)
        {

            try
            {
                int sandsID = data1.SandsId;
                var data = db.Table_Sands.Where(c => c.SandsId == sandsID).FirstOrDefault();
                data.SandsName = data1.SandsName;
                data.CreatedBy = data1.CreatedBy;
               // data.CreatedOn = data1.CreatedOn;

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
        public async Task<IHttpActionResult> DeleteSandsData([FromUri(Name = "id")] int id)
        {
            var data = await Task.Run(() => db.Table_Sands.FindAsync(id));

            await Task.Run(() => db.Table_Sands.Remove(data));
            await db.SaveChangesAsync();

            return Ok("success");
        }
        public partial class SandsMasterDetails
        {
            public int SandsId { get; set; }
            public string SandsName { get; set; }
            public string CreatedBy { get; set; }
            public Nullable<System.DateTime> CreatedOn { get; set; }
        }
    }
}
