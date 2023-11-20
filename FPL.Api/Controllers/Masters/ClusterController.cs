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
    public class ClusterController : ApiController
    {
        private CustomerManagerEntities db = new CustomerManagerEntities();

        [HttpGet]
        public async Task<IHttpActionResult> GetAllCluster()
        {
            var result = await Task.Run(() => db.Table_Cluster.ToList());
            return Ok(result);
        }

        public async Task<IHttpActionResult> PostSaveCluster(ClusterMasterDetails data1)
        {

            try
            {
                Table_Cluster data = new Table_Cluster()
                {
                    ClusterId = 0,
                    ClusterName = data1.ClusterName,
                    CreatedOn = DateTime.Now,
                    CreatedBy = data1.CreatedBy
                };

                await Task.Run(() => db.Table_Cluster.Add(data));
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
        public async Task<IHttpActionResult> PostSaveUpdateCluster(ClusterMasterDetails data1)
        {

            try
            {
                int clusterid = Convert.ToInt32(data1.ClusterId);
                var data = db.Table_Cluster.Where(c => c.ClusterId == clusterid).FirstOrDefault();
                data.ClusterName = data1.ClusterName;
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
        public async Task<IHttpActionResult> DeleteClusterData([FromUri(Name = "id")] int id)
        {
            var data = await Task.Run(() => db.Table_Cluster.FindAsync(id));

            await Task.Run(() => db.Table_Cluster.Remove(data));
            await db.SaveChangesAsync();

            return Ok("success");
        }
        public partial class ClusterMasterDetails
        {
            public int ClusterId { get; set; }
            public string ClusterName { get; set; }
            public string CreatedBy { get; set; }
            public Nullable<System.DateTime> CreatedOn { get; set; }
        }
    }
}
