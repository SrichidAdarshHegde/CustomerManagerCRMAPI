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
    public class PurposeEstJobTimeController : ApiController
    {
        private CustomerManagerEntities db = new CustomerManagerEntities();

       [HttpPost]
        public async Task<IHttpActionResult> PostSavePurposeEstJobTime(PurposeMasterDetails data1)
        {

            try
            {
                Table_PurposeEstJobTime data = new Table_PurposeEstJobTime()
                {
                    Id = 0,
                    PurposeName = data1.PurposeName,
                    JobTime = data1.JobTime,
                    CreatedOn = DateTime.Now,
                    CreatedBy = data1.CreatedBy
                };

                await Task.Run(() => db.Table_PurposeEstJobTime.Add(data));
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
        public async Task<IHttpActionResult> PostUpdatePurpose(PurposeMasterDetails data1)
        {
            try
            {
                int purposeId  = Convert.ToInt32(data1.Id);
                var dat = await Task.Run(() => db.Table_PurposeEstJobTime.Where(c => c.Id == purposeId).FirstOrDefault());
                if (dat != null)
                {
                    dat.PurposeName = data1.PurposeName;
                    dat.JobTime = data1.JobTime;
                    dat.CreatedBy = data1.CreatedBy;

                    await Task.Run(() => db.Entry(dat).State = EntityState.Modified);
                    await db.SaveChangesAsync();
                }

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
        public async Task<IHttpActionResult> GetAllPurposeJobTime()
        {
            var result = await Task.Run(() => db.Table_PurposeEstJobTime.ToList());
            return Ok(result);
        }

        [HttpGet]
        public async Task<IHttpActionResult> DeletePurposeJobTime([FromUri(Name = "id")] int id)
        {
            var data = await Task.Run(() => db.Table_PurposeEstJobTime.FindAsync(id));

            await Task.Run(() => db.Table_PurposeEstJobTime.Remove(data));
            await db.SaveChangesAsync();

            return Ok("success");
        }

        public  partial class PurposeMasterDetails
        {
            public int Id { get; set; }
            public string PurposeName { get; set; }
            public Nullable<System.TimeSpan> JobTime { get; set; }
            public string CreatedBy { get; set; }
            public Nullable<System.DateTime> CreatedOn { get; set; }
        }

    }
}
