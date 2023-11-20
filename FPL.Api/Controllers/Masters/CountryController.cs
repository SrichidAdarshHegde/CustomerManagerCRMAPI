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
    public class CountryController : ApiController
    {
        private CustomerManagerEntities db = new CustomerManagerEntities();

        [HttpGet]
        public async Task<IHttpActionResult> GetAllCountry()
        {
            var result = await Task.Run(() => db.Table_Country.ToList());
            return Ok(result);
        }

        public async Task<IHttpActionResult> PostSaveCountry(CountryMasterDetails data1)
        {

            try
            {
                Table_Country data = new Table_Country()
                {
                    CountryId = 0,
                    CountryName = data1.CountryName,
                    CreatedOn = DateTime.Now,
                    CreatedBy = data1.CreatedBy
                };

                await Task.Run(() => db.Table_Country.Add(data));
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
        public async Task<IHttpActionResult> PutCountry(CountryMasterDetails data1)
        {

            try
            {
                var data = db.Table_Country.Where(c => c.CountryId == data1.CountryId).FirstOrDefault();
                data.CountryName = data1.CountryName;
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

        [HttpGet]
        public async Task<IHttpActionResult> DeleteCountryData([FromUri(Name = "id")] int id)
        {
            var data = await Task.Run(() => db.Table_Country.FindAsync(id));

            await Task.Run(() => db.Table_Country.Remove(data));
            await db.SaveChangesAsync();

            return Ok("success");
        }
        public partial class CountryMasterDetails
        {
            public int CountryId { get; set; }
            public string CountryName { get; set; }
            public string CreatedBy { get; set; }
            public Nullable<System.DateTime> CreatedOn { get; set; }
        }
    }
}
