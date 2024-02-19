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
    public class RequestsController : ApiController
    {
        private CustomerManagerEntities db = new CustomerManagerEntities();

        [HttpGet]
        public async Task<IHttpActionResult> GetAllRequests()
        {
            var result = await Task.Run(() => db.Table_Requests.ToList());
            return Ok(result);
        }

      
        [HttpGet]
        public async Task<IHttpActionResult> getPerticularPriority([FromUri(Name = "id")] int id)
        {
            var result = await Task.Run(() => db.Table_Requests.Where(c => c.RequestsId == id).Select(c => c.Priority).ToList());
            return Ok(result);
        }


        [HttpGet]
        public async Task<IHttpActionResult> GetAllPriorities()
        {
            var result = await Task.Run(() => db.Table_Priority.ToList());
            return Ok(result);
        }

        public async Task<IHttpActionResult> PostSaveRequests(RequestsMasterDetails data1)
        {

            try
            {
                Table_Requests data = new Table_Requests()
                {
                    RequestsId = 0,
                    RequestsName = data1.RequestsName,
                    CreatedOn = DateTime.Now,
                    CreatedBy = data1.CreatedBy
                };

                await Task.Run(() => db.Table_Requests.Add(data));
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



        public async Task<IHttpActionResult> PostUpdateRequest(RequestsMasterDetails data1)
        {

            try
            {
                int requestID = Convert.ToInt32(data1.RequestsId);
                var dat = await Task.Run(() => db.Table_Requests.Where(c => c.RequestsId == requestID).FirstOrDefault());
                if (dat != null)
                {
                    dat.RequestsName = data1.RequestsName;
                    dat.Priority = data1.Priority;

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

        public async Task<IHttpActionResult> PostUpdateRequestPriority(RequestsMasterDetails data1)
        {

            try
            {
                var dat = await Task.Run(() => db.Table_Requests.Where(c => c.RequestsId == data1.RequestsId).FirstOrDefault());
                var priid = Convert.ToInt32(data1.Priority);
                var priData = await Task.Run(() => db.Table_Priority.Where(c => c.ID == priid).Select(c => c.PriorityID).FirstOrDefault());
                if (dat != null)
                {
                    dat.Priority = priData;
                             
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


        [HttpPost]
        public async Task<IHttpActionResult> PutRequests(RequestsMasterDetails data1)
        {

            try
            {
                var data = db.Table_Requests.Where(c => c.RequestsId == data1.RequestsId).FirstOrDefault();
                data.RequestsName = data1.RequestsName;
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
        public async Task<IHttpActionResult> DeleteRequests([FromUri(Name = "id")] int id)
        {
            var data = await Task.Run(() => db.Table_Requests.FindAsync(id));

            await Task.Run(() => db.Table_Requests.Remove(data));
            await db.SaveChangesAsync();

            return Ok("success");
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetRequestForById([FromUri(Name = "id")] int id)
        {
            var requestForName = await Task.Run(() => db.Table_Requests.Where(c => c.RequestsId == id).Select(c => c.RequestsName).FirstOrDefault());
            return Ok(requestForName);
        }
        public partial class RequestsMasterDetails
        {
            public int RequestsId { get; set; }
            public string RequestsName { get; set; }
            public string CreatedBy { get; set; }
            public Nullable<System.DateTime> CreatedOn { get; set; }

            public string Priority { get; set; }
        }
    }
}
