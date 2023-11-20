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
    public class ConsumablesController : ApiController
    {
        private CustomerManagerEntities db = new CustomerManagerEntities();
        [HttpGet]
        public async Task<IHttpActionResult> GetAllConsumables()
        {
            var result = await Task.Run(() => db.Table_Consumables.ToList());
            return Ok(result);
        }
  
             public async Task<IHttpActionResult> PostSaveConsumables(ConsumablesMasterDetails data1)
        {

            try
            {
                Table_Consumables data = new Table_Consumables()
                {
                    ConsumablesId = 0,
                    ConsumablesName = data1.ConsumablesName,
                    CreatedOn = DateTime.Now,
                    CreatedBy = data1.CreatedBy
                };

                await Task.Run(() => db.Table_Consumables.Add(data));
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
        public async Task<IHttpActionResult> PostSaveUpdateConsumables(ConsumablesMasterDetails data1)
        {

            try
            {
                int consumablesID = Convert.ToInt32(data1.ConsumablesId);
                var data = db.Table_Consumables.Where(c => c.ConsumablesId == consumablesID).FirstOrDefault();
                data.ConsumablesName = data1.ConsumablesName;
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
        public async Task<IHttpActionResult> DeleteConsumablesData([FromUri(Name = "id")] int id)
        {
            var data = await Task.Run(() => db.Table_Consumables.FindAsync(id));

            await Task.Run(() => db.Table_Consumables.Remove(data));
            await db.SaveChangesAsync();

            return Ok("success");
        }

        public partial class ConsumablesMasterDetails
        {
            public int ConsumablesId { get; set; }
            public string ConsumablesName { get; set; }
            public string CreatedBy { get; set; }
            public Nullable<System.DateTime> CreatedOn { get; set; }
        }
    }
}
