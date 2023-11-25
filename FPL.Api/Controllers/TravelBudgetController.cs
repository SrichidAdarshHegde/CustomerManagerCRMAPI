using FPL.Dal.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace FPL.Api.Controllers
{
    public class TravelBudgetController : ApiController
    {
        private CustomerManagerEntities db = new CustomerManagerEntities();
     [HttpPost]
     public async Task<IHttpActionResult> saveTravelBudget(TravelBudgetDetails data)
        {
            try
            {
                Table_TravelBudget abc = new Table_TravelBudget()
                {           
                    MachineNumber = data.MachineNumber,
                    CustomerName = data.CustomerName,
                    Purpose = data.Purpose,
                    ClusterLocation = data.ClusterLocation,
                    CNG = data.CNG,
                    Mileage = data.Mileage,
                    EstInterDistance = data.EstInterDistance,
                    EstCompanyDistance = data.EstCompanyDistance,
                    ActualODOReading = data.ActualODOReading,
                    EstTravelTime = data.EstTravelTime,
                    EstTimeForJob = data.EstTimeForJob,
                    SchdETD = data.SchdETD,
                    Totaltime= data.Totaltime,
                    ActualTime = data.ActualTime,
                    CNGFilledPreviously = data.CNGFilledPreviously,
                    UserName = data.UserName,
                    CreatedOn = DateTime.Now,
                    TripId = data.TripId,
                    TravelId = data.TravelId,
                    UserId = data.UserId,
                };

                await Task.Run(() => db.Table_TravelBudget.Add(abc));
                await db.SaveChangesAsync();

                return Ok("success");
            }
            catch (Exception e)
            {

                throw e;
            }
        }
         
        [HttpGet]
        public async Task<IHttpActionResult> GetAllTravelBudget()
        {
            var result = await Task.Run(() => db.Table_TravelBudget.ToList());
            return Ok(result);
        }
        [HttpGet]
        public async Task<IHttpActionResult> GetTravelBudgetbyUser([FromUri(Name = "id")] int id)
        {
       
            var result = await Task.Run(() => db.Table_TravelBudget.Where(c => c.UserId == id).ToList());
            return Ok(result);
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetTravelBudgetbyTravelId([FromUri(Name = "id")] int id)
        {

            var result = await Task.Run(() => db.Table_TravelBudget.Where(c => c.TravelId == id).ToList());
            return Ok(result);
        }
    }

    public partial class TravelBudgetDetails
    {
        public int id { get; set; }
        public string MachineNumber { get; set; }
        public string CustomerName { get; set; }
        public string Purpose { get; set; }
        public string ClusterLocation { get; set; }
        public string CNG { get; set; }
        public string Mileage { get; set; }
        public string EstInterDistance { get; set; }
        public string EstCompanyDistance { get; set; }
        public string ActualODOReading { get; set; }
        public Nullable<System.DateTime> EstTravelTime { get; set; }
        public Nullable<System.DateTime> EstTimeForJob { get; set; }
        public Nullable<System.DateTime> SchdETD { get; set; }
        public Nullable<System.DateTime> ActualTime { get; set; }
        public Nullable<System.DateTime> Totaltime { get; set; }



        public string CNGFilledPreviously { get; set; }
        public string UserName { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string TripId { get; set; }
        public Nullable<int> TravelId { get; set; }
        public Nullable<int> UserId { get; set; }
    }
}
