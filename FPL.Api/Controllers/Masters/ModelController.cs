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
    public class ModelController : ApiController
    {
        private CustomerManagerEntities db = new CustomerManagerEntities();

        [HttpGet]
        public async Task<IHttpActionResult> GetAllModel()
        {
            var result = await Task.Run(() => db.Table_Model.ToList());
            return Ok(result);
        }

        public async Task<IHttpActionResult> PostSaveModel(ModelMasterDetails data1)
        {

            try
            {
                Table_Model data = new Table_Model()
                {
                    ModelId = 0,
                    ModelName = data1.ModelName,
                    CreatedOn = DateTime.Now,
                    CreatedBy = data1.CreatedBy
                };

                await Task.Run(() => db.Table_Model.Add(data));
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
        public async Task<IHttpActionResult> PostSaveUpdateModel(ModelMasterDetails data1)
        {

            try
            {
                int modelID = data1.ModelId;
                var data = db.Table_Model.Where(c => c.ModelId == modelID).FirstOrDefault();
                data.ModelName = data1.ModelName;
                data.CreatedBy = data1.CreatedBy;
                //data.CreatedOn = data1.CreatedOn;

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
        public async Task<IHttpActionResult> DeleteModelData([FromUri(Name = "id")] int id)
        {
            var data = await Task.Run(() => db.Table_Model.FindAsync(id));

            await Task.Run(() => db.Table_Model.Remove(data));
            await db.SaveChangesAsync();

            return Ok("success");
        }
        public partial class ModelMasterDetails
        {
            public int ModelId { get; set; }
            public string ModelName { get; set; }
            public string CreatedBy { get; set; }
            public Nullable<System.DateTime> CreatedOn { get; set; }
        }
    }
}
