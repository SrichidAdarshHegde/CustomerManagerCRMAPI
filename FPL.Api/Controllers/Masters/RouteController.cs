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
    public class RouteController : ApiController
    {
        private CustomerManagerEntities db = new CustomerManagerEntities();

        [HttpGet]
        public async Task<IHttpActionResult> GetAllRoute()
        {
            var result = await Task.Run(() => db.Table_Route.ToList());
            return Ok(result);
        }

        public async Task<IHttpActionResult> PostSaveRoute(RouteMasterDetails data1)
        {

            try
            {
                Table_Route data = new Table_Route()
                {
                    RouteId = 0,
                    RouteName = data1.RouteName,
                    CreatedOn = DateTime.Now,
                    CreatedBy = data1.CreatedBy
                };

                await Task.Run(() => db.Table_Route.Add(data));
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
        public async Task<IHttpActionResult> PostSaveUpdateRoute(RouteMasterDetails data1)
        {

            try
            {
                int routteID = Convert.ToInt32(data1.RouteId);
                var data = db.Table_Route.Where(c => c.RouteId == routteID).FirstOrDefault();
                data.RouteName = data1.RouteName;
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
        public async Task<IHttpActionResult> DeleteRouteData([FromUri(Name = "id")] int id)
        {
            var data = await Task.Run(() => db.Table_Route.FindAsync(id));

            await Task.Run(() => db.Table_Route.Remove(data));
            await db.SaveChangesAsync();

            return Ok("success");
        }
        public partial class RouteMasterDetails
        {
            public int RouteId { get; set; }
            public string RouteName { get; set; }
            public string CreatedBy { get; set; }
            public Nullable<System.DateTime> CreatedOn { get; set; }
        }
    }
}
