
using DocumentFormat.OpenXml.Wordprocessing;
using FPL.Dal.DataModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace FactorsPrivateLimited.Controllers.Masters
{
    public class roleController : ApiController
    {
        private CustomerManagerEntities db = new CustomerManagerEntities();
      
       [HttpGet]
        public async Task<IHttpActionResult> GetAllRoles()
        {
            var result = await Task.Run(() => db.Table_RoleMaster.ToList());
            return Ok(result);
        }

        public async Task<IHttpActionResult> PostSaveRole(RoleMasterDetails data1)
        {
            var timer = Stopwatch.StartNew();

            try
            {
                Table_RoleMaster data = new Table_RoleMaster()
                {
                    Id = 0,
                    RoleName = data1.RoleName,
                    CreatedOn = DateTime.Now,
                    CreatedBy = data1.CreatedBy
                };

                await Task.Run(() => db.Table_RoleMaster.Add(data));
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
        public async Task<IHttpActionResult> PutRole(RoleMasterDetails data1)
        {
            var timer = Stopwatch.StartNew();

            try
            {
                var data = db.Table_RoleMaster.Where(c => c.Id == data1.Id).FirstOrDefault();
                data.RoleName = data1.RoleName;
                data.CreatedBy = data1.CreatedBy;
                data.CreatedOn = data1.CreatedOn;

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


        [HttpPost]
        public async Task<IHttpActionResult> PostSaveUpdateRole(RoleMasterDetails data1)
        {
            var timer = Stopwatch.StartNew();

            int roleid = Convert.ToInt32(data1.Id);

            try
            {
                var data = db.Table_RoleMaster.Where(c => c.Id == roleid).FirstOrDefault();
                data.RoleName = data1.RoleName;
                data.CreatedBy = data1.CreatedBy;
              //  data.CreatedOn = data1.CreatedOn;

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
        public async Task<IHttpActionResult> DeleteRoleData([FromUri(Name = "id")] int id)
        {
            var data = await Task.Run(() => db.Table_RoleMaster.FindAsync(id));

            await Task.Run(() => db.Table_RoleMaster.Remove(data));
            await db.SaveChangesAsync();

            return Ok("success");
        }
        public partial class RoleMasterDetails
        {
            public int Id { get; set; }
            public string RoleName { get; set; }
            public string UserId { get; set; }
            public Nullable<System.DateTime> CreatedOn { get; set; }
            public string CreatedBy { get; set; }
            public Nullable<System.DateTime> ModifiedOn { get; set; }
            public string ModifiedBy { get; set; }
        }
    }
}
