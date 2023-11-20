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
    public class RegionController : ApiController
    {
        private CustomerManagerEntities db = new CustomerManagerEntities();

        [HttpGet]
        public async Task<IHttpActionResult> GetAllRegion()
        {
            var result = await Task.Run(() => db.Table_Region.ToList());
            return Ok(result);
        }

        public async Task<IHttpActionResult> PostSaveRegion(RegionMasterDetails data1)
        {

            try
            {
                Table_Region data = new Table_Region()
                {
                    RegionId = 0,
                    RegionName = data1.RegionName,
                    CreatedOn = DateTime.Now,
                    CreatedBy = data1.CreatedBy
                };

                await Task.Run(() => db.Table_Region.Add(data));
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
        public async Task<IHttpActionResult> PostSaveUpdateRegion(RegionMasterDetails data1)
        {

            try
            {
                int regionID = Convert.ToInt32(data1.RegionId);
                var data = db.Table_Region.Where(c => c.RegionId == regionID).FirstOrDefault();
                data.RegionName = data1.RegionName;
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
        public async Task<IHttpActionResult> DeleteRegionData([FromUri(Name = "id")] int id)
        {
            var data = await Task.Run(() => db.Table_Region.FindAsync(id));

            await Task.Run(() => db.Table_Region.Remove(data));
            await db.SaveChangesAsync();

            return Ok("success");
        }
        public partial class RegionMasterDetails
        {
            public int RegionId { get; set; }
            public string RegionName { get; set; }
            public string CreatedBy { get; set; }
            public Nullable<System.DateTime> CreatedOn { get; set; }
        }
    }
}
