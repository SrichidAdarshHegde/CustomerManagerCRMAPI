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
    public class AttendTypeController : ApiController
    {
        private CustomerManagerEntities db = new CustomerManagerEntities();

        [HttpGet]
        public async Task<IHttpActionResult> GetAllAttendType()
        {
            var result = await Task.Run(() => db.Table_AttendType.ToList());
            return Ok(result);
        }

        public async Task<IHttpActionResult> PostSaveAttendType(AttendTypeMasterDetails data1)
        {

            try
            {
                Table_AttendType data = new Table_AttendType()
                {
                    AttendTypeId = 0,
                    AttendTypeName = data1.AttendTypeName,
                    CreatedOn = DateTime.Now,
                    CreatedBy = data1.CreatedBy
                };

                await Task.Run(() => db.Table_AttendType.Add(data));
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
        public async Task<IHttpActionResult> PostSaveAttendTypeUpdate(AttendTypeMasterDetails data1)
        {

            try
            {
                int attendtypeID = Convert.ToInt32(data1.AttendTypeId);
                var data = db.Table_AttendType.Where(c => c.AttendTypeId == attendtypeID).FirstOrDefault();
                data.AttendTypeName = data1.AttendTypeName;
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
        public async Task<IHttpActionResult> DeleteAttendTypeData([FromUri(Name = "id")] int id)
        {
            var data = await Task.Run(() => db.Table_AttendType.FindAsync(id));

            await Task.Run(() => db.Table_AttendType.Remove(data));
            await db.SaveChangesAsync();

            return Ok("success");
        }
        public partial class AttendTypeMasterDetails
        {
            public int AttendTypeId { get; set; }
            public string AttendTypeName { get; set; }
            public string CreatedBy { get; set; }
            public Nullable<System.DateTime> CreatedOn { get; set; }
        }
    }
}
