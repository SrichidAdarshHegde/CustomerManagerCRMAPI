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
    public class FaultsController : ApiController
    {
        private CustomerManagerEntities db = new CustomerManagerEntities();
        [HttpGet]
        public async Task<IHttpActionResult> GetAllFaults()
        {
            var result = await Task.Run(() => db.Table_Faults.ToList());
            return Ok(result);
        }

        public async Task<IHttpActionResult> PostSaveFaults(FaultsMasterDetails data1)
        {

            try
            {
                Table_Faults data = new Table_Faults()
                {
                    FaultsId = 0,
                    FaultsName = data1.FaultsName,
                    CreatedOn = DateTime.Now,
                    CreatedBy = data1.CreatedBy
                };

                await Task.Run(() => db.Table_Faults.Add(data));
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
        public async Task<IHttpActionResult> PostSaveUpdateFaults(FaultsMasterDetails data1)
        {

            try
            {
                int faultsID = Convert.ToInt32(data1.FaultsId);
                var data = db.Table_Faults.Where(c => c.FaultsId == faultsID).FirstOrDefault();
                data.FaultsName = data1.FaultsName;
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
        public async Task<IHttpActionResult> DeleteFaultsData([FromUri(Name = "id")] int id)
        {
            var data = await Task.Run(() => db.Table_Faults.FindAsync(id));

            await Task.Run(() => db.Table_Faults.Remove(data));
            await db.SaveChangesAsync();

            return Ok("success");
        }


        public partial class FaultsMasterDetails
        {
            public int FaultsId { get; set; }
            public string FaultsName { get; set; }
            public string CreatedBy { get; set; }
            public Nullable<System.DateTime> CreatedOn { get; set; }
        }
    }
}
