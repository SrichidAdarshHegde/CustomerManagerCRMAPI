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
    public class ZoneController : ApiController
    {
        private CustomerManagerEntities db = new CustomerManagerEntities();

        [HttpGet]
        public async Task<IHttpActionResult> GetAllZone()
        {
            var result = await Task.Run(() => db.Table_Zone.ToList());
            return Ok(result);
        }

        public async Task<IHttpActionResult> PostSaveZone(ZoneMasterDetails data1)
        {

            try
            {
                Table_Zone data = new Table_Zone()
                {
                    ZoneId = 0,
                    ZoneName = data1.ZoneName,
                    CreatedOn = DateTime.Now,
                    CreatedBy = data1.CreatedBy
                };

                await Task.Run(() => db.Table_Zone.Add(data));
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
        public async Task<IHttpActionResult> PostSaveUpdateZone(ZoneMasterDetails data1)
        {

            try
            {
                int zoneID = Convert.ToInt32(data1.ZoneId);
                var data = db.Table_Zone.Where(c => c.ZoneId == zoneID).FirstOrDefault();
                data.ZoneName = data1.ZoneName;
                data.CreatedBy = data1.CreatedBy;
             //   data.CreatedOn = data1.CreatedOn;

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
        public async Task<IHttpActionResult> DeleteZoneData([FromUri(Name = "id")] int id)
        {
            var data = await Task.Run(() => db.Table_Zone.FindAsync(id));

            await Task.Run(() => db.Table_Zone.Remove(data));
            await db.SaveChangesAsync();

            return Ok("success");
        }
        public partial class ZoneMasterDetails
        {
            public int ZoneId { get; set; }
            public string ZoneName { get; set; }
            public string CreatedBy { get; set; }
            public Nullable<System.DateTime> CreatedOn { get; set; }
        }
    }
}
