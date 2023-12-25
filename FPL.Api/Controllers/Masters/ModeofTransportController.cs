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
    public class ModeofTransportController : ApiController
    {

        private CustomerManagerEntities db = new CustomerManagerEntities();


        [HttpGet]
        public async Task<IHttpActionResult> GetModeofTransport()
        {
            var result = await Task.Run(() => db.Table_ModeofTransport.ToList());
            return Ok(result);
        }
        public async Task<IHttpActionResult> PostSaveModeofTransport(ModeofTransportDetails data1)
        {

            try
                
            {
                Table_ModeofTransport data = new Table_ModeofTransport()
                {
                    ModeofTransportID = 0,
                    TransportName = data1.TransportName,
                    CreatedOn = DateTime.Now,
                    CreatedBy= data1.CreatedBy
                };

                await Task.Run(() => db.Table_ModeofTransport.Add(data));
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
        public async Task<IHttpActionResult> PostSaveUpdateModeofTransport(ModeofTransportDetails data1)
        {

            try
            {
                int modeofTransportID = Convert.ToInt32(data1.ModeofTransportID);
                var data = db.Table_ModeofTransport.Where(c => c.ModeofTransportID == modeofTransportID).FirstOrDefault();
                data.TransportName = data1.TransportName;



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
        public async Task<IHttpActionResult> deleteModeofTransport([FromUri(Name = "id")] int id)
        {
            var data = await Task.Run(() => db.Table_ModeofTransport.FindAsync(id));

            await Task.Run(() => db.Table_ModeofTransport.Remove(data));
            await db.SaveChangesAsync();

            return Ok("success");
        }
        public partial class ModeofTransportDetails
        {
            public int ModeofTransportID { get; set; }
            public string TransportName { get; set; }
            public Nullable<System.DateTime> CreatedOn { get; set; }

            public string CreatedBy { get; set; }
        }
    }
}
 