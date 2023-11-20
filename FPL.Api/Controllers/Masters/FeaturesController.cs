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
    public class FeaturesController : ApiController
    {
        private CustomerManagerEntities db = new CustomerManagerEntities();

        [HttpGet]
        public async Task<IHttpActionResult> GetAllFeatures()
        {
            var result = await Task.Run(() => db.Table_Features.ToList());
            return Ok(result);
        }

        public async Task<IHttpActionResult> PostSaveFeatures(FeaturesMasterDetails data1)
        {

            try
            {
                Table_Features data = new Table_Features()
                {
                    FeaturesId = 0,
                    FeaturesName = data1.FeaturesName,
                    CreatedOn = DateTime.Now,
                    CreatedBy = data1.CreatedBy
                };

                await Task.Run(() => db.Table_Features.Add(data));
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
        public async Task<IHttpActionResult> PostSaveUpdateFeatures(FeaturesMasterDetails data1)
        {

            try
            {
                int featureID = Convert.ToInt32(data1.FeaturesId);
                var data = db.Table_Features.Where(c => c.FeaturesId == featureID).FirstOrDefault();
                data.FeaturesName = data1.FeaturesName;
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
        public async Task<IHttpActionResult> DeleteFeaturesData([FromUri(Name = "id")] int id)
        {
            var data = await Task.Run(() => db.Table_Features.FindAsync(id));

            await Task.Run(() => db.Table_Features.Remove(data));
            await db.SaveChangesAsync();

            return Ok("success");
        }
        public partial class FeaturesMasterDetails
        {
            public int FeaturesId { get; set; }
            public string FeaturesName { get; set; }
            public string CreatedBy { get; set; }
            public Nullable<System.DateTime> CreatedOn { get; set; }
        }
    }
}
