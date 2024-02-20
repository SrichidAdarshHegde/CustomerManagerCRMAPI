using FPL.Dal.DataModel;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;




namespace FPL.Api.Controllers
{
    public class RequestAndInteractionsController : ApiController
    {
        private CustomerManagerEntities db = new CustomerManagerEntities();
        List<ContactData> contact = new List<ContactData>();
        [HttpPost]
        public async Task<IHttpActionResult> PostSaveRequestForm()
        {
            List<Table_MachineFeatureDetails> MachineDetails = new List<Table_MachineFeatureDetails>();
            try
            {
                var httpRequest = System.Web.HttpContext.Current.Request;
                var mn = httpRequest["MachineNumber"];
                if (mn == "undefined" || mn == "null" || mn == "")
                {
                    //var httpRequest = HttpContext.Current.Request;
                    var customerId = httpRequest["CustomerId"];
                    var ticketNo = httpRequest["TokenNo"];
                    var contactId = httpRequest["ContactId"];
                    var startTime = httpRequest["StartTime"];
                    var endTime = httpRequest["EndTime"];
                    var callFrom = httpRequest["CallFrom"];
                    var requestfor = httpRequest["RequestFor"];
                    List<RequestItem> selectedRequestFor = JsonConvert.DeserializeObject<List<RequestItem>>(requestfor);
                   
                    var customername = httpRequest["CustomerName"];
                    var remarks = httpRequest["Remarks"];
                    var resolution = httpRequest["Resolution"];
                    int contId = Convert.ToInt32(contactId);
                    var createdBy = httpRequest["CreatedBy"];
                    var createdOn = httpRequest["CreatedOn"];
                    var contactData = db.Table_Contactdetails.Where(c => c.Id == contId).Select(c => c).FirstOrDefault();

                    for (int i = 0; i < selectedRequestFor.Count; i++)
                    {
                        Table_MachineCustomerRequestsDetails data2 = new Table_MachineCustomerRequestsDetails()
                        {
                            TokenNo = Convert.ToInt32(ticketNo),
                            CustomerId = Convert.ToInt32(customerId),
                            CustomerName = customername,
                            RequestFor = selectedRequestFor[i].label,
                            RequestForId = selectedRequestFor[i].value,
                            Remarks = remarks,
                            Resolution = resolution,
                            CreatedBy = createdBy,
                            CreatedOn = Convert.ToDateTime(createdOn),
                            ContactId = contId,
                            ContactName = contactData.ContactName,
                            Salute = contactData.Salute,
                            Designation = contactData.Designation,
                            Email = contactData.Email,
                            Mobile = contactData.Mobile,
                            StartTime = Convert.ToDateTime(startTime),
                            EndTime = Convert.ToDateTime(endTime),
                            CallFrom = callFrom,

                        };
                        if (data2 != null)
                        {
                            await Task.Run(() => db.Table_MachineCustomerRequestsDetails.Add(data2));
                            int status = await db.SaveChangesAsync();
                        }
                        else
                        {
                            continue;
                        }
                    }

                    int ticketId = Convert.ToInt32(ticketNo);
                    var ticketData = db.Table_RequestsFormData.Where(c => c.TokenID == ticketId).FirstOrDefault();
                    if (ticketData != null && ticketData.TokenID == ticketId)
                    {
                        ticketData.TokenID = ticketId;
                        ticketData.RequestFor = selectedRequestFor[0].label;
                        ticketData.RequestForId = selectedRequestFor[0].value;
                        ticketData.Remarks = remarks;
                        ticketData.Resolution = resolution;
                        ticketData.ContactId = contId;
                        ticketData.Salute = contactData.Salute;
                        ticketData.ContactName = contactData.ContactName;
                        ticketData.Designation = contactData.Designation;
                        ticketData.Email = contactData.Designation;
                        ticketData.Mobile = contactData.Mobile;
                        ticketData.IsDone = false;
                        ticketData.CreatedOn = Convert.ToDateTime(createdOn);
                        ticketData.CreatedBy = createdBy;

                        await Task.Run(() => db.Entry(ticketData).State = EntityState.Modified);
                        await db.SaveChangesAsync();

                        return Ok("success");
                    }
                    else
                    {

                        Table_RequestsFormData data = new Table_RequestsFormData()
                        {
                            CustomerId = Convert.ToInt32(customerId),
                            CustomerName = customername,
                            TokenID = Convert.ToInt32(ticketNo),
                            IsDone = false,
                            Remarks = remarks,
                            Resolution = resolution,
                            RequestForId = selectedRequestFor[0].value,
                            CreatedOn = Convert.ToDateTime(createdOn),
                            CreatedBy = createdBy,
                            ContactId = contId,
                            ContactName = contactData.ContactName,
                            Salute = contactData.Salute,
                            Designation = contactData.Designation,
                            Email = contactData.Email,
                            Mobile = contactData.Mobile,

                        };

                        await Task.Run(() => db.Table_RequestsFormData.Add(data));
                        await db.SaveChangesAsync();

                    }
                    return Ok("success");
                }
                else
                {
                    //var httpRequest = HttpContext.Current.Request;
                    var machineNumber = httpRequest["MachineNumber"];

                    var customerId = httpRequest["CustomerId"];
                    var ticketNo = httpRequest["TokenNo"];
                    var contactId = httpRequest["ContactId"];
                    var startTime = httpRequest["StartTime"];
                    var endTime = httpRequest["EndTime"];
                    var callFrom = httpRequest["CallFrom"];
                    var requestfor = httpRequest["RequestFor"];
                    //JArray selectedFeatures = JArray.Parse(features);
                    List<RequestItem> selectedRequestFor = JsonConvert.DeserializeObject<List<RequestItem>>(requestfor);

                    var customername = httpRequest["CustomerName"];
                    var remarks = httpRequest["Remarks"];
                    var resolution = httpRequest["Resolution"];

                    var createdBy = httpRequest["CreatedBy"];
                    var createdOn = httpRequest["CreatedOn"];
                    int contId = Convert.ToInt32(contactId);
                    int machineID = Convert.ToInt32(machineNumber);

                    var macid = db.Table_MachineRegistration.Where(c => c.MachineNumber == machineID).Select(c => c.Id).FirstOrDefault();

                    var contactData = db.Table_Contactdetails.Where(c => c.Id == contId).Select(c => c).FirstOrDefault();

                    for (int i = 0; i < selectedRequestFor.Count; i++)
                    {
                        Table_MachineCustomerRequestsDetails data2 = new Table_MachineCustomerRequestsDetails()
                        {
                            MachineNumber = Convert.ToInt32(machineNumber),
                            TokenNo = Convert.ToInt32(ticketNo),
                            MachineId = macid,
                            CustomerId = Convert.ToInt32(customerId),
                            CustomerName = customername,
                            RequestFor = selectedRequestFor[i].label,
                            RequestForId = selectedRequestFor[i].value,
                            Remarks = remarks,
                            Resolution = resolution,
                            CreatedBy = createdBy,
                            UniqueID = string.Concat(machineNumber.Concat(remarks).Concat(customerId)),
                            CreatedOn = Convert.ToDateTime(createdOn),
                            ContactId = contId,
                            ContactName = contactData.ContactName,
                            Salute = contactData.Salute,
                            Designation = contactData.Designation,
                            Email = contactData.Email,
                            Mobile = contactData.Mobile,
                            StartTime = Convert.ToDateTime(startTime),
                            EndTime = Convert.ToDateTime(endTime),
                            CallFrom = callFrom,

                        };
                        if (data2 != null)
                        {
                            await Task.Run(() => db.Table_MachineCustomerRequestsDetails.Add(data2));
                            int status = await db.SaveChangesAsync();

                        }
                        else
                        {
                            continue;
                        }
                    }

                    int ticketId = Convert.ToInt32(ticketNo);
                    var ticketData = db.Table_RequestsFormData.Where(c => c.TokenID == ticketId).FirstOrDefault();
                    if (ticketData != null && ticketData.TokenID == ticketId)
                    {
                        ticketData.TokenID = ticketId;
                        ticketData.RequestFor = selectedRequestFor[0].label;
                        ticketData.RequestForId = selectedRequestFor[0].value;
                        ticketData.Remarks = remarks;
                        ticketData.Resolution = resolution;
                        ticketData.ContactId = contId;
                        ticketData.Salute = contactData.Salute;
                        ticketData.ContactName = contactData.ContactName;
                        ticketData.Designation = contactData.Designation;
                        ticketData.Email = contactData.Designation;
                        ticketData.Mobile = contactData.Mobile;
                        ticketData.IsDone = false;
                        ticketData.CreatedOn = Convert.ToDateTime(createdOn);
                        ticketData.CreatedBy = createdBy;

                        await Task.Run(() => db.Entry(ticketData).State = EntityState.Modified);
                        await db.SaveChangesAsync();

                        return Ok("success");
                    }
                    else
                    {
                        Table_RequestsFormData data = new Table_RequestsFormData()
                        {
                            CustomerId = Convert.ToInt32(customerId),
                            CustomerName = customername,
                            TokenID = Convert.ToInt32(ticketNo),
                            IsDone = false,
                            MachineId = macid,
                            MachineNumber = machineID,
                            Remarks = remarks,
                            Resolution = resolution,
                            RequestFor = selectedRequestFor[0].label,
                            RequestForId = selectedRequestFor[0].value,
                            SandS = string.Concat(machineNumber.Concat(remarks).Concat(customerId)),
                            CreatedOn = Convert.ToDateTime(createdOn),
                            CreatedBy = createdBy,
                            ContactId = contId,
                            ContactName = contactData.ContactName,
                            Salute = contactData.Salute,
                            Designation = contactData.Designation,
                            Email = contactData.Email,
                            Mobile = contactData.Mobile,
                        };

                        await Task.Run(() => db.Table_RequestsFormData.Add(data));
                        await db.SaveChangesAsync();

                    }
                    return Ok("success");

                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public class RequestVM
        {

            public List<RequestItem> RequestFor { get; set; }
        }
        public class RequestItem
        {
            public string label { get; set; }
            public int value { get; set; }
        }


        public class SandSVM
        {

            public List<SandSItem> SandS { get; set; }
        }
        public class SandSItem
        {
            public string label { get; set; }
            public int value { get; set; }
        }
        [HttpPost]
        public async Task<IHttpActionResult> PostSaveNewInteraction(Table_InteractionsData_DataModel data1)
        {
            try
            {
                var UserData = db.Table_UserRegistration.Where(c => c.Id == data1.AttendedByUserId).Select(c => c).FirstOrDefault();
                var attenddata = db.Table_AttendType.Where(c => c.AttendTypeId == data1.AttendedHowId).Select(c => c).FirstOrDefault();
                Table_InteractionsData data = new Table_InteractionsData()
                {
                    AttendedByUserId = data1.AttendedByUserId,
                    CreatedBy = data1.CreatedBy,
                    CutomerId = data1.CutomerId,
                    CutomerName = data1.CutomerName,
                    MachineNumber = data1.MachineNumber,
                    TicketNo = data1.TicketNo,
                    ModelId = data1.ModelId,
                    ModelName = data1.ModelName,
                    RegionId = data1.RegionId,
                    RegionName = data1.RegionName,
                    ZoneName = data1.ZoneName,
                    AttendedByUserName = UserData.UserName,
                    RequestId = data1.RequestId,
                    DateOfInteraction = data1.DateOfInteraction,
                    AttendedHowId = data1.AttendedHowId,
                    AttendedHowName = attenddata.AttendTypeName,
                    Remarks = data1.Remarks,
                    CreatedOn = DateTime.Now,
                };


                await Task.Run(() => db.Table_InteractionsData.Add(data));
                await db.SaveChangesAsync();
                var MachineData = db.Table_RequestsFormData.Where(c => c.CustomerId == data1.CutomerId && c.RequestForId == data1.RequestId && c.TokenID == data1.TicketNo).Select(c => c).FirstOrDefault();

                if (MachineData != null)
                {
                    MachineData.IsDone = true;


                    db.Entry(MachineData).State = EntityState.Modified;
                    await db.SaveChangesAsync();

                }


                return Ok("success");
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public class Table_InteractionsData_DataModel
        {
            public int Id { get; set; }
            public Nullable<int> CutomerId { get; set; }
            public string CutomerName { get; set; }
            public Nullable<int> MachineId { get; set; }
            public Nullable<int> MachineNumber { get; set; }
            public Nullable<int> ModelId { get; set; }
            public string ModelName { get; set; }
            public Nullable<int> RegionId { get; set; }

            public Nullable<int> RequestId { get; set; }
            public string RegionName { get; set; }
            public Nullable<int> ZoneId { get; set; }
            public string ZoneName { get; set; }
            public string Remarks { get; set; }
            public Nullable<int> AttendedByUserId { get; set; }
            public string AttendedByUserName { get; set; }
            public Nullable<int> AttendedHowId { get; set; }
            public string AttendedHowName { get; set; }

            public string CreatedBy { get; set; }
            public Nullable<System.DateTime> CreatedOn { get; set; }
            public DateTime DateOfInteraction { get; set; }
            public Nullable<int> TicketNo { get; set; }
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetAllDetails([FromUri(Name = "id")] int id)
        {

            var result = await Task.Run(() => db.Table_LocationDetails.Where(c => c.MachineNumber == id).ToList());
            return Ok(result);
        }


        [HttpGet]
        public async Task<IHttpActionResult> Getparticularcustomerdetails([FromUri(Name = "id")] string id)
        {
            int idd = Convert.ToInt32(id);
            var result = await Task.Run(() => db.Table_LocationDetails.Where(c => c.CustomerID == idd).ToList());
            return Ok(result);
        }

        [HttpGet]
        public async Task<IHttpActionResult> getClusterdetails([FromUri(Name = "id")] string id)
        {
            int idd = Convert.ToInt32(id);
            var result = await Task.Run(() => db.Table_LocationDetails.Where(c => c.ClusterId == idd).ToList());
            return Ok(result);
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetAllCluster()
        {
            var result = await Task.Run(() => db.Table_Cluster.ToList());
            return Ok(result);
        }
        [HttpGet]
        public async Task<IHttpActionResult> GetPendingRequestsfordashboard()
        {
            try
            {
                string requestforresult = "";
                string requestsandsresult = "";
                var Requests = db.Table_RequestsFormData.Where(c => c.IsDone == false && c.IsMachineDeleted != true).ToList();
                return Ok(Requests);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

            [HttpGet]
        public async Task<IHttpActionResult> GetPendingRequests()
        {
            try
            {
                var requests = db.Table_RequestsFormData .Where(c => c.IsDone == false && c.IsMachineDeleted != true).ToList();

                var dataList = new List<allrequestdatamodel>();

                foreach (var request in requests)
                {
                    var machineData = await db.Table_MachineRegistration.Where(c => c.MachineNumber == request.MachineNumber).FirstOrDefaultAsync();

                    var companyData = await db.Table_CustomerRegistartion.Where(c => c.CustomerID == request.CustomerId).FirstOrDefaultAsync();

                    var contactData = await db.Table_Contactdetails.Where(c => c.MachineId == request.MachineNumber) .Select(c => new ContactData
                        {
                            ContactName = c.ContactName,
                            Designation = c.Designation,
                            Email = c.Email,
                            Mobile = c.Mobile,
                            Salute = c.Salute
                        })
                        .ToListAsync();

                    var requestForData = await db.Table_MachineCustomerRequestsDetails .Where(c => c.UniqueID == request.RequestFor) .Select(c => c.RequestFor) .ToListAsync();

                    var sandSData = await db.Table_MachineCustomerSansSDetails .Where(c => c.UniqueID == request.SandS).Select(c => c.SandS) .ToListAsync();

                    var requestForResult = string.Join(" , ", requestForData);
                    var sandSResult = string.Join(" , ", sandSData);

                    var data = new allrequestdatamodel
                    {
                        CompanyName = companyData.CompanyName,
                        CustomerID = companyData.CustomerID,
                        MachineNumber = machineData.MachineNumber,
                        ModelName = machineData.ModelName,
                        ModelId = machineData.ModelId,
                        Remarks = request.Remarks,
                        Region = companyData.Region,
                        Zone = companyData.Zone,
                        ContactData = contactData,
                        CreatedBy = request.CreatedBy,
                        CreatedOn = request.CreatedOn,
                        IsDone = request.IsDone,
                        RequestId = request.id,
                        TokenID = request.TokenID,
                        RequestForId = request.RequestForId,
                        RequestFor = requestForResult,
                        SandS = sandSResult
                    };

                    dataList.Add(data);
                }

                return Ok(dataList);
            }
            catch (Exception e)
            {
              
                return InternalServerError();
            }
        }

        [HttpGet]


     
        public async Task<IHttpActionResult> GetDatewiserequest([FromUri(Name = "id")]string dates)
        {
            try
            {
                var dateArray = dates.Split(',');
                DateTime fromDate = DateTime.Parse(dateArray[0], CultureInfo.CreateSpecificCulture("en-IN"));
                DateTime toDate = DateTime.Parse(dateArray[1], CultureInfo.CreateSpecificCulture("en-IN"));
                var requestData = await Task.Run(() => db.Table_RequestsFormData.Where(c => c.CreatedOn >= fromDate && c.CreatedOn <= toDate).ToList());

                return Ok(requestData);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }
        [HttpGet]
        public async Task<IHttpActionResult> GetDatewiserequestinvoice([FromUri(Name = "id")] string dates)
        {
            try
            {
                var dateArray = dates.Split(',');
                DateTime fromDate = DateTime.Parse(dateArray[0], CultureInfo.CreateSpecificCulture("en-IN"));
                DateTime toDate = DateTime.Parse(dateArray[1], CultureInfo.CreateSpecificCulture("en-IN"));
                var requestData = await Task.Run(() => db.Table_InvoicePerticular.Where(c => c.CreatedOn >= fromDate && c.CreatedOn <= toDate).ToList());

                return Ok(requestData);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        public async Task<IHttpActionResult> GetDatewiserequestInetraction([FromUri(Name = "id")] string dates)
        {
            try
            {
                var dateArray = dates.Split(',');
                DateTime fromDate = DateTime.Parse(dateArray[0], CultureInfo.CreateSpecificCulture("en-IN"));
                DateTime toDate = DateTime.Parse(dateArray[1], CultureInfo.CreateSpecificCulture("en-IN"));
                var requestData = await Task.Run(() => db.Table_RequestsFormData.Where(c => c.CreatedOn >= fromDate && c.CreatedOn <= toDate).ToList());

                return Ok(requestData);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }
        public async Task<IHttpActionResult> GetDatewiserequestfollowupDate([FromUri(Name = "id")] string dates)
        {
            try
            {
                var dateArray = dates.Split(',');
                DateTime fromDate = DateTime.Parse(dateArray[0], CultureInfo.CreateSpecificCulture("en-IN"));
                DateTime toDate = DateTime.Parse(dateArray[1], CultureInfo.CreateSpecificCulture("en-IN"));
                var requestData = await Task.Run(() => db.Table_FollowUpDetails.Where(c => c.CreatedOn >= fromDate && c.CreatedOn <= toDate).ToList());

                return Ok(requestData);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }
        [HttpGet]
        public async Task<IHttpActionResult> GetPendingRequestsforWorkFront()
        {
            try
            {
                string output = "";
                string requestforresult = "";
                string requestsandsresult = "";
                var Requests = db.Table_RequestsFormData.Where(c => c.IsDone == false).ToList();
                var datalist = new List<allrequestdatamodel>();



                for (var i = 0; i < Requests.Count; i++)
                {
                    var mn = Requests[i].MachineNumber;
                    var cid = Requests[i].CustomerId;

                    var uniquerequestfor = Requests[i].RequestFor;
                    var uniquesands = Requests[i].SandS;

                    var MachineData = await Task.Run(() => db.Table_MachineRegistration.Where(c => c.MachineNumber == mn).Select(c => c).FirstOrDefault());

                    var CompanyData = await Task.Run(() => db.Table_CustomerRegistartion.Where(c => c.CustomerID == cid).Select(c => c).FirstOrDefault());

                    var requestfordata = await Task.Run(() => db.Table_MachineCustomerRequestsDetails.Where(c => c.UniqueID == uniquerequestfor).Select(c => c.RequestFor).ToList());

                    var sanddsdata = await Task.Run(() => db.Table_MachineCustomerSansSDetails.Where(c => c.UniqueID == uniquesands).Select(c => c.SandS).ToList());

                    for (int r = 0; r < requestfordata.Count; r++)

                    {
                        var requestNamee = requestfordata[r];
                        var requestfordataa = await Task.Run(() => db.Table_Requests.Where(c => c.RequestsName == requestNamee).Select(c => c.Priority).FirstOrDefault());

                        if (r == 0)
                        {
                            requestforresult = requestfordataa.ToString();
                        }
                        else
                        {
                            var requestNameee = requestfordata[r];
                            var requestfordataaa = await Task.Run(() => db.Table_Requests.Where(c => c.RequestsName == requestNamee).Select(c => c.Priority).FirstOrDefault());
                            requestforresult = requestforresult.ToString() + " , " + requestfordataaa.ToString();
                        }

                    }

                    for (int s = 0; s < sanddsdata.Count; s++)
                    {
                        if (s == 0)
                        {
                            requestsandsresult = sanddsdata[s].ToString();
                        }
                        else
                        {
                            requestsandsresult = requestsandsresult + " / " + requestfordata[s];
                        }

                    }
                    string input = "2023-06-26T11:57:05.467";
                    string cleanedInput = Requests[i].CreatedOn.ToString().Trim('{', '}');
                    DateTime dateTime = DateTime.ParseExact(cleanedInput, "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                    string outputt = dateTime.ToString("yyyy-MM-dd'T'HH:mm:ss.fff");
                    DateTime dateTimeee;
                    if (DateTime.TryParseExact(outputt, "yyyy-MM-dd'T'HH:mm:ss.fff", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTimeee))
                    {
                        output = dateTimeee.ToString("dd-MMM-yy", CultureInfo.InvariantCulture);
                        Console.WriteLine(output);  // Output: 26-Jun-23
                    }
                    allrequestdatamodel data = new allrequestdatamodel()
                    {
                        CompanyName = CompanyData.CompanyName,
                        CustomerID = CompanyData.CustomerID,
                        MachineNumber = MachineData.MachineNumber,
                        ModelName = MachineData.ModelName,
                        ModelId = MachineData.ModelId,
                        Remarks = Requests[i].Remarks,
                        Region = CompanyData.Region,
                        Zone = CompanyData.Zone,
                        Cluster = CompanyData.Cluster,
                        ContactData = contact,
                        CreatedBy = Requests[i].CreatedBy,
                        DateString = output,
                        IsDone = Requests[i].IsDone,
                        RouteId = CompanyData.RouteId,
                        RequestId = Requests[i].id,
                        RequestFor = requestforresult,
                        SandS = requestsandsresult

                    };
                    datalist.Add(data);
                }

                return Ok(datalist);
            }
            catch (Exception e)
            {

                throw e;
            }
        }



        //[HttpGet]
        //public async Task<IHttpActionResult> GetPendingRequestsforWorkFrontZonewise([FromUri(Name = "id")] int id)
        //{
        //    try
        //    {

        //        string output = "";
        //        string requestforresult = "";
        //        string requestsandsresult = "";
        //        var Requests = db.Table_RequestsFormData.Where(c => c.IsDone == false).ToList();
        //        var datalist = new List<allrequestdatamodel>();

        //        var CompanyDataa = await Task.Run(() => db.Table_CustomerRegistartion.Where(c => c.ZoneId == id).Select(c => c).FirstOrDefault());

        //        if (CompanyDataa != null)
        //        {
        //            for (var i = 0; i < Requests.Count; i++)
        //            {

        //                var MachineDataa = await Task.Run(() => db.Table_MachineRegistration.Where(c => c.CustomerId == CompanyDataa.CustomerID).Select(c => c).FirstOrDefault());
        //                var mn = Requests[i].MachineNumber;
        //                var cid = Requests[i].CustomerId;

        //                var uniquerequestfor = Requests[i].RequestFor;
        //                var uniquesands = Requests[i].SandS;

        //                var MachineData = await Task.Run(() => db.Table_MachineRegistration.Where(c => c.MachineNumber == mn && c.MachineNumber == MachineDataa.MachineNumber).Select(c => c).FirstOrDefault());

        //                var CompanyData = await Task.Run(() => db.Table_CustomerRegistartion.Where(c => c.CustomerID == cid && c.CustomerID == CompanyDataa.CustomerID).Select(c => c).FirstOrDefault());

        //                var requestfordata = await Task.Run(() => db.Table_MachineCustomerRequestsDetails.Where(c => c.MachineNumber == MachineData.MachineNumber && c.CustomerId == CompanyData.CustomerID && c.CustomerName == CompanyData.CompanyName).Select(c => c.RequestFor).ToList());

        //                var sanddsdata = await Task.Run(() => db.Table_MachineCustomerSansSDetails.Where(c => c.MachineNumber == MachineData.MachineNumber && c.CustomerId == CompanyData.CustomerID && c.CustomerName == CompanyData.CompanyName).Select(c => c.SandS).ToList());

        //                for (int r = 0; r < requestfordata.Count; r++)

        //                {
        //                    var requestNamee = requestfordata[r];
        //                    var requestfordataa = await Task.Run(() => db.Table_Requests.Where(c => c.RequestsName == requestNamee).Select(c => c.Priority).FirstOrDefault());

        //                    if (r == 0)
        //                    {
        //                        requestforresult = requestfordataa.ToString();
        //                    }
        //                    else
        //                    {
        //                        var requestNameee = requestfordata[r];
        //                        var requestfordataaa = await Task.Run(() => db.Table_Requests.Where(c => c.RequestsName == requestNamee).Select(c => c.Priority).FirstOrDefault());
        //                        requestforresult = requestforresult.ToString() + " , " + requestfordataaa.ToString();
        //                    }

        //                }

        //                for (int s = 0; s < sanddsdata.Count; s++)
        //                {
        //                    if (s == 0)
        //                    {
        //                        requestsandsresult = sanddsdata[s].ToString();
        //                    }
        //                    else
        //                    {
        //                        requestsandsresult = requestsandsresult + " / " + requestfordata[s];
        //                    }

        //                }
        //                string input = "2023-06-26T11:57:05.467";
        //                string cleanedInput = Requests[i].CreatedOn.ToString().Trim('{', '}');
        //                DateTime dateTime = DateTime.ParseExact(cleanedInput, "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture);
        //                string outputt = dateTime.ToString("yyyy-MM-dd'T'HH:mm:ss.fff");
        //                DateTime dateTimeee;
        //                if (DateTime.TryParseExact(outputt, "yyyy-MM-dd'T'HH:mm:ss.fff", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTimeee))
        //                {
        //                    output = dateTimeee.ToString("dd-MMM-yy", CultureInfo.InvariantCulture);
        //                    Console.WriteLine(output);  // Output: 26-Jun-23
        //                }
        //                allrequestdatamodel data = new allrequestdatamodel()
        //                {
        //                    CompanyName = CompanyData.CompanyName,
        //                    CustomerID = CompanyData.CustomerID,
        //                    MachineNumber = MachineData.MachineNumber,
        //                    ModelName = MachineData.ModelName,
        //                    ModelId = MachineData.ModelId,
        //                    Remarks = Requests[i].Remarks,
        //                    Region = CompanyData.Region,
        //                    Zone = CompanyData.Zone,
        //                    Cluster = CompanyData.Cluster,
        //                    ContactData = contact,
        //                    CreatedBy = Requests[i].CreatedBy,
        //                    DateString = output,
        //                    IsDone = Requests[i].IsDone,
        //                    RouteId = CompanyData.RouteId,
        //                    RequestId = Requests[i].id,
        //                    RequestFor = requestforresult,
        //                    SandS = requestsandsresult

        //                };
        //                datalist.Add(data);
        //            }

        //            return Ok(datalist);
        //        }
        //        else
        //        {
        //            allrequestdatamodel data = new allrequestdatamodel()
        //            {
        //                CompanyName = null,
        //                CustomerID = 0,
        //                MachineNumber = null,
        //                ModelName = null,
        //                ModelId = null,
        //                Remarks = null,
        //                Region = null,
        //                Zone = null,
        //                Cluster = null,
        //                ContactData = null,
        //                CreatedBy = null,
        //                DateString = null,
        //                IsDone = null,
        //                RouteId = null,
        //                RequestId = 0,
        //                RequestFor = null,
        //                SandS = null

        //            };
        //            datalist.Add(data);

        //            return Ok(new List<allrequestdatamodel>());
        //        }
        //    }
        //    catch (Exception e)
        //    {

        //        throw e;
        //    }
        //}

        //[HttpGet]
        //public async Task<IHttpActionResult> GetPendingRequestsforWorkFrontZonewise([FromUri(Name = "id")] int id)
        //{
        //    try
        //    {

        //        string output = "";
        //        string requestforresult = "";
        //        string requestsandsresult = "";
        //        var Requests = db.Table_RequestsFormData.Where(c => c.IsDone == false).ToList();
        //        var datalist = new List<allrequestdatamodel>();



        //        if (Requests != null)
        //        {
        //            for (var i = 0; i < Requests.Count; i++)
        //            {
        //                var custid = Requests[i].CustomerId;
        //                var CompanyDataa = await Task.Run(() => db.Table_CustomerRegistartion.Where(c => c.CustomerID == custid && c.ZoneId == id).Select(c => c).FirstOrDefault());

        //                if (CompanyDataa != null)
        //                {
        //                    var MachineDataa = await Task.Run(() => db.Table_MachineRegistration.Where(c => c.CustomerId == CompanyDataa.CustomerID).Select(c => c).FirstOrDefault());
        //                    var mn = Requests[i].MachineNumber;
        //                    var cid = Requests[i].CustomerId;

        //                    var uniquerequestfor = Requests[i].RequestFor;
        //                    var uniquesands = Requests[i].SandS;

        //                    var MachineData = await Task.Run(() => db.Table_MachineRegistration.Where(c => c.MachineNumber == mn && c.MachineNumber == MachineDataa.MachineNumber).Select(c => c).FirstOrDefault());

        //                    var CompanyData = await Task.Run(() => db.Table_CustomerRegistartion.Where(c => c.CustomerID == cid && c.CustomerID == CompanyDataa.CustomerID).Select(c => c).FirstOrDefault());

        //                    var requestfordata = await Task.Run(() => db.Table_MachineCustomerRequestsDetails.Where(c => c.MachineNumber == MachineData.MachineNumber && c.CustomerId == CompanyData.CustomerID && c.CustomerName == CompanyData.CompanyName).Select(c => c.RequestFor).ToList());

        //                    var sanddsdata = await Task.Run(() => db.Table_MachineCustomerSansSDetails.Where(c => c.MachineNumber == MachineData.MachineNumber && c.CustomerId == CompanyData.CustomerID && c.CustomerName == CompanyData.CompanyName).Select(c => c.SandS).ToList());

        //                    for (int r = 0; r < requestfordata.Count; r++)

        //                    {
        //                        var requestNamee = requestfordata[r];
        //                        var requestfordataa = await Task.Run(() => db.Table_Requests.Where(c => c.RequestsName == requestNamee).Select(c => c.Priority).FirstOrDefault());

        //                        if (r == 0)
        //                        {
        //                            requestforresult = requestfordataa.ToString();
        //                        }
        //                        else
        //                        {
        //                            var requestNameee = requestfordata[r];
        //                            var requestfordataaa = await Task.Run(() => db.Table_Requests.Where(c => c.RequestsName == requestNamee).Select(c => c.Priority).FirstOrDefault());
        //                            requestforresult = requestfordataaa.ToString();
        //                        }

        //                    }

        //                    for (int s = 0; s < sanddsdata.Count; s++)
        //                    {
        //                        if (s == 0)
        //                        {
        //                            requestsandsresult = sanddsdata[s].ToString();
        //                        }
        //                        else
        //                        {
        //                            requestsandsresult = requestsandsresult + " / " + requestfordata[s];
        //                        }

        //                    }

        //                    allrequestdatamodel data = new allrequestdatamodel()
        //                    {
        //                        CompanyName = CompanyData.CompanyName,
        //                        CustomerID = CompanyData.CustomerID,
        //                        MachineNumber = MachineData.MachineNumber,
        //                        ModelName = MachineData.ModelName,
        //                        ModelId = MachineData.ModelId,
        //                        Remarks = Requests[i].Remarks,
        //                        Region = CompanyData.Region,
        //                        Zone = CompanyData.Zone,
        //                        Cluster = CompanyData.Cluster,
        //                        ContactData = contact,
        //                        CreatedBy = Requests[i].CreatedBy,
        //                        DateString = output,
        //                        IsDone = Requests[i].IsDone,
        //                        RouteId = CompanyData.RouteId,
        //                        RequestId = Requests[i].id,
        //                        RequestFor = requestforresult,
        //                        SandS = requestsandsresult

        //                    };
        //                    datalist.Add(data);
        //                }
                        
                      
        //            }

        //            return Ok(datalist);
        //        }
        //        else
        //        {
        //            allrequestdatamodel data = new allrequestdatamodel()
        //            {
        //                CompanyName = null,
        //                CustomerID = 0,
        //                MachineNumber = null,
        //                ModelName = null,
        //                ModelId = null,
        //                Remarks = null,
        //                Region = null,
        //                Zone = null,
        //                Cluster = null,
        //                ContactData = null,
        //                CreatedBy = null,
        //                DateString = null,
        //                IsDone = null,
        //                RouteId = null,
        //                RequestId = 0,
        //                RequestFor = null,
        //                SandS = null

        //            };
        //            datalist.Add(data);

        //            return Ok(new List<allrequestdatamodel>());
        //        }
        //    }
        //    catch (Exception e)
        //    {

        //        throw e;
        //    }
        //}


        [HttpGet]
        public async Task<IHttpActionResult> GetPendingRequestsforWorkFrontZonewise([FromUri(Name = "id")] int id)
        {
            try
            {
                var datalist = new List<allrequestdatamodel>();
                var Requests = db.Table_RequestsFormData.Where(c => c.IsDone == false).ToList();
                if (Requests != null)
                {
                    for (var i = 0; i < Requests.Count; i++)
                    {
                        var customerId = Requests[i].CustomerId;
                        var tokenId = Requests[i].TokenID;
                        var machineNumber = Convert.ToString(Requests[i].MachineNumber);
                        if (machineNumber == "undefined" || machineNumber == null || machineNumber == "")
                        {
                            var reqForId = Requests[i].RequestForId;
                            var customerData = db.Table_CustomerRegistartion.Where(c => c.CustomerID == customerId).FirstOrDefault();
                            //var machineData = db.Table_MachineRegistration.Where(c => c.MachineNumber == machineNumber && c.CustomerId == customerId).FirstOrDefault();
                            var requestData = db.Table_Requests.Where(c => c.RequestsId == reqForId).FirstOrDefault();
                            var contactData = db.Table_Contactdetails.Where(c => c.CustomerId == customerId).FirstOrDefault();
                            var requestDatafull = db.Table_MachineCustomerRequestsDetails.Where(c => c.CustomerId == customerId && c.RequestForId == reqForId && c.TokenNo == tokenId).OrderByDescending(c => c.TokenNo).FirstOrDefault();
                            if (customerData.ZoneId == id)
                            {
                                allrequestdatamodel data = new allrequestdatamodel()
                                {
                                    CompanyName = customerData.CompanyName,
                                    CustomerID = customerData.CustomerID,
                                    //MachineNumber = machineData.MachineNumber,
                                    //ModelName = machineData.ModelName,
                                    //ModelId = machineData.ModelId,
                                    Remarks = Requests[i].Remarks,
                                    Region = customerData.Region,
                                    Zone = customerData.Zone,
                                    Cluster = customerData.Cluster,
                                    //ContactDatastring = contactData.Salute + " " + contactData.ContactName + "(" + contactData.Mobile + "," + contactData.Email + ")",
                                    CreatedBy = Requests[i].CreatedBy,
                                    date = Requests[i].CreatedOn,
                                    IsDone = Requests[i].IsDone,
                                    RouteId = customerData.RouteId,
                                    RequestId = Requests[i].id,
                                    RequestForId = Requests[i].RequestForId,
                                    RequestFor = requestData.RequestsName,
                                    Priority = requestData.Priority,
                                    SandS = null,
                                    TokenID = requestDatafull.TokenNo,
                                };
                                datalist.Add(data);
                            }

                        }
                        else
                        {
                            var machineNum = Convert.ToInt32(Requests[i].MachineNumber);
                            var reqForId = Requests[i].RequestForId;
                            var customerData = db.Table_CustomerRegistartion.Where(c => c.CustomerID == customerId).FirstOrDefault();
                            var machineData = db.Table_MachineRegistration.Where(c => c.MachineNumber == machineNum && c.CustomerId == customerId).FirstOrDefault();
                            var requestData = db.Table_Requests.Where(c => c.RequestsId == reqForId).FirstOrDefault();
                            var contactData = db.Table_Contactdetails.Where(c => c.CustomerId == customerId).FirstOrDefault();
                            var requestDatafull = db.Table_MachineCustomerRequestsDetails.Where(c => c.MachineNumber == machineNum && c.CustomerId == customerId && c.RequestForId == reqForId && c.TokenNo == tokenId).OrderByDescending(c => c.TokenNo).FirstOrDefault();
                            if (customerData.ZoneId == id)
                            {
                                allrequestdatamodel data = new allrequestdatamodel()
                                {
                                    CompanyName = customerData.CompanyName,
                                    CustomerID = customerData.CustomerID,
                                    MachineNumber = machineData.MachineNumber,
                                    ModelName = machineData.ModelName,
                                    ModelId = machineData.ModelId,
                                    Remarks = Requests[i].Remarks,
                                    Region = customerData.Region,
                                    Zone = customerData.Zone,
                                    Cluster = customerData.Cluster,
                                    ContactDatastring = contactData.Salute + " " + contactData.ContactName + "(" + contactData.Mobile + "," + contactData.Email + ")",
                                    CreatedBy = Requests[i].CreatedBy,
                                    date = Requests[i].CreatedOn,
                                    IsDone = Requests[i].IsDone,
                                    RouteId = customerData.RouteId,
                                    RequestId = Requests[i].id,
                                    RequestForId = Requests[i].RequestForId,
                                    RequestFor = requestData.RequestsName,
                                    Priority = requestData.Priority,
                                    SandS = null,
                                    TokenID = requestDatafull.TokenNo,
                                };
                                datalist.Add(data);
                            }

                        }

                    }
                }
                return Ok(datalist);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

            [HttpGet]
        public async Task<IHttpActionResult> GetAllInteractions()
        {
            var result = await Task.Run(() => db.Table_InteractionsData.ToList());
            return Ok(result);
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetAllRequests()
        {
            try
            {
                string requestforresult = "";
                string requestsandsresult = "";
                List<ContactData> cccc = new List<ContactData>();

                var Requests = db.Table_RequestsFormData.Where(c => c.IsMachineDeleted != true).ToList();
                var datalist = new List<allrequestdatamodel>();

                for (var i = 0; i < Requests.Count; i++)
                {
                    if (i == 0)
                    {
                        int memnerID = Convert.ToInt32(Requests[i].MachineNumber);
                        GetContactList(memnerID);
                    }
                    var mn = Requests[i].MachineNumber;

                    var cid = Requests[i].CustomerId;
                    var uniquerequestfor = Requests[i].RequestFor;
                    var uniquesands = Requests[i].SandS;

                    var MachineData = await Task.Run(() => db.Table_MachineRegistration.Where(c => c.MachineNumber == mn).Select(c => c).FirstOrDefault());
                    var CompanyData = await Task.Run(() => db.Table_CustomerRegistartion.Where(c => c.CustomerID == cid).Select(c => c).FirstOrDefault());
                    var CuntactDataData = await Task.Run(() => db.Table_Contactdetails.Where(c => c.MachineId == mn).Select(c => c).ToList());
                    var requestfordata = await Task.Run(() => db.Table_MachineCustomerRequestsDetails.Where(c => c.UniqueID == uniquerequestfor).Select(c => c.RequestFor).ToList());

                    var sanddsdata = await Task.Run(() => db.Table_MachineCustomerSansSDetails.Where(c => c.UniqueID == uniquesands).Select(c => c.SandS).ToList());

                    for (int r = 0; r < requestfordata.Count; r++)

                    {
                        if (r == 0)
                        {
                            requestforresult = requestfordata[r];
                        }
                        else
                        {
                            requestforresult = requestforresult + " , " + requestfordata[r];
                        }

                    }

                    for (int s = 0; s < sanddsdata.Count; s++)
                    {
                        if (s == 0)
                        {
                            requestsandsresult = requestfordata[s];
                        }
                        else
                        {
                            requestsandsresult = requestsandsresult + " , " + requestfordata[s];
                        }

                    }

                    allrequestdatamodel data = new allrequestdatamodel()
                    {
                        CompanyName = CompanyData.CompanyName,
                        MachineNumber = MachineData.MachineNumber,
                        ModelName = MachineData.ModelName,
                        Remarks = Requests[i].Remarks,
                        Region = CompanyData.Region,
                        Zone = CompanyData.Zone,
                        // ContactData = contact,
                        CreatedBy = Requests[i].CreatedBy,
                        CreatedOn = Requests[i].CreatedOn,
                        IsDone = Requests[i].IsDone,
                        RequestId = Requests[i].id,
                        RequestFor = requestforresult,
                        SandS = requestsandsresult

                    };
                    datalist.Add(data);
                }

                MyResponse response = new MyResponse
                {
                    Array1 = datalist,
                    Array2 = contact
                };

                return Ok(response);
                // return Ok(datalist);
            }
            catch (Exception e)
            {

                throw e;
            }

        }


        [HttpGet]
        public async Task<IHttpActionResult> GetMachineRequestsFromMachineNumber([FromUri(Name = "id")] int id)
        {
            try
            {
                string requestforresult = "";
                string requestsandsresult = "";
                List<ContactData> cccc = new List<ContactData>();
                int memberIDD = Convert.ToInt32(id);
                var Requests = db.Table_RequestsFormData.Where(c => c.MachineNumber == memberIDD).ToList();
                var datalist = new List<allrequestdatamodel>();
                GetContactList(memberIDD);
                for (var i = 0; i < Requests.Count; i++)
                {

                    var mn = memberIDD;

                    var cid = Requests[i].CustomerId;

                    var uniquerequestfor = Requests[i].RequestFor;
                    var uniquesands = Requests[i].SandS;

                    var MachineData = await Task.Run(() => db.Table_MachineRegistration.Where(c => c.MachineNumber == mn).Select(c => c).FirstOrDefault());
                    var CompanyData = await Task.Run(() => db.Table_CustomerRegistartion.Where(c => c.CustomerID == cid).Select(c => c).FirstOrDefault());
                    var CuntactDataData = await Task.Run(() => db.Table_Contactdetails.Where(c => c.MachineId == mn).Select(c => c).ToList());

                    var requestfordata = await Task.Run(() => db.Table_MachineCustomerRequestsDetails.Where(c => c.UniqueID == uniquerequestfor).Select(c => c).ToList());

                    var sanddsdata = await Task.Run(() => db.Table_MachineCustomerSansSDetails.Where(c => c.UniqueID == uniquesands).Select(c => c).ToList());

                    for (int r = 0; r < requestfordata.Count; r++)

                    {
                        if (r == 0)
                        {
                            requestforresult = requestfordata[r].RequestFor.ToString();
                        }
                        else
                        {
                            requestforresult = requestforresult + " , " + requestfordata[r].RequestFor;
                        }

                    }

                    for (int s = 0; s < sanddsdata.Count; s++)
                    {
                        if (s == 0)
                        {
                            requestsandsresult = sanddsdata[s].SandS.ToString();
                        }
                        else
                        {
                            requestsandsresult = requestsandsresult + " , " + sanddsdata[s].SandS.ToString();
                        }

                    }

                    allrequestdatamodel data = new allrequestdatamodel()
                    {
                        CompanyName = CompanyData.CompanyName,
                        MachineNumber = MachineData.MachineNumber,
                        ModelName = MachineData.ModelName,
                        Remarks = Requests[i].Remarks,
                        Region = CompanyData.Region,
                        Zone = CompanyData.Zone,
                        // ContactData = contact,
                        CreatedBy = Requests[i].CreatedBy,
                        CreatedOn = Requests[i].CreatedOn,
                        IsDone = Requests[i].IsDone,
                        RequestId = Requests[i].id,
                        RequestFor = requestforresult,
                        SandS = requestsandsresult
                    };
                    datalist.Add(data);
                }

                MyResponse response = new MyResponse
                {
                    Array1 = datalist,
                    Array2 = contact
                };

                return Ok(response);
                // return Ok(datalist);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetMachineRequestsFromCustomer([FromUri(Name = "id")] int id)
        {
            try
            {
                string requestforresult = "";
                string requestsandsresult = "";
                List<ContactData> cccc = new List<ContactData>();
                int memberIDD = Convert.ToInt32(id);
                var Requests = db.Table_RequestsFormData.Where(c => c.CustomerId == memberIDD).ToList();
                var datalist = new List<allrequestdatamodel>();

                for (var i = 0; i < Requests.Count; i++)
                {
                    if (i == 0)
                    {
                        int memnerID = Convert.ToInt32(Requests[i].MachineNumber);
                        GetContactList(memnerID);
                    }
                    var mn = Requests[i].MachineNumber;

                    var uniquerequestfor = Requests[i].RequestFor;
                    var uniquesands = Requests[i].SandS;

                    //var cid = Requests[i].CustomerId;

                    var MachineData = await Task.Run(() => db.Table_MachineRegistration.Where(c => c.MachineNumber == mn).Select(c => c).FirstOrDefault());
                    var CompanyData = await Task.Run(() => db.Table_CustomerRegistartion.Where(c => c.CustomerID == memberIDD).Select(c => c).FirstOrDefault());
                    var CuntactDataData = await Task.Run(() => db.Table_Contactdetails.Where(c => c.MachineId == mn).Select(c => c).ToList());
                    var requestfordata = await Task.Run(() => db.Table_MachineCustomerRequestsDetails.Where(c => c.UniqueID == uniquerequestfor).Select(c => c).ToList());

                    var sanddsdata = await Task.Run(() => db.Table_MachineCustomerSansSDetails.Where(c => c.UniqueID == uniquesands).Select(c => c).ToList());

                    for (int r = 0; r < requestfordata.Count; r++)

                    {
                        if (r == 0)
                        {
                            requestforresult = requestfordata[r].RequestFor.ToString();
                        }
                        else
                        {
                            requestforresult = requestforresult + " , " + requestfordata[r].RequestFor;
                        }

                    }

                    for (int s = 0; s < sanddsdata.Count; s++)
                    {
                        if (s == 0)
                        {
                            requestsandsresult = sanddsdata[s].SandS.ToString();
                        }
                        else
                        {
                            requestsandsresult = requestsandsresult + " , " + sanddsdata[s].SandS;
                        }

                    }
                    allrequestdatamodel data = new allrequestdatamodel()
                    {
                        CompanyName = CompanyData.CompanyName,
                        MachineNumber = MachineData.MachineNumber,
                        ModelName = MachineData.ModelName,
                        Remarks = Requests[i].Remarks,
                        Region = CompanyData.Region,
                        Zone = CompanyData.Zone,
                        // ContactData = contact,
                        CreatedBy = Requests[i].CreatedBy,
                        CreatedOn = Requests[i].CreatedOn,
                        IsDone = Requests[i].IsDone,
                        RequestId = Requests[i].id,
                        RequestFor = requestforresult,
                        SandS = requestsandsresult
                    };
                    datalist.Add(data);
                }

                MyResponse response = new MyResponse
                {
                    Array1 = datalist,
                    Array2 = contact
                };

                return Ok(response);
                // return Ok(datalist);
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        public class MyResponse
        {
            public List<allrequestdatamodel> Array1 { get; set; }
            public List<ContactData> Array2 { get; set; }
        }
        public class ContactData
        {
            public string Salute { get; set; }
            public string ContactName { get; set; }
            public string Designation { get; set; }
            public string Email { get; set; }
            public string Mobile { get; set; }
        }
        public class allrequestdatamodel
        {
            public string Priority { get; set; }
            public DateTime? date { get; set; }
            public int RequestId { get; set; }
            public string ContactDatastring { get; set; }
            public List<ContactData> ContactData { get; set; }

            public Nullable<int> MachineNumber { get; set; }
            public Nullable<int> MachineId { get; set; }
            public Nullable<int> CustomerId { get; set; }
            public string CreatedBy { get; set; }

            public string DateString { get; set; }
            public Nullable<System.DateTime> CreatedOn { get; set; }
            public Nullable<int> ModelId { get; set; }
            public string ModelName { get; set; }
            public string CustomerName { get; set; }
            public string Features { get; set; }
            public Nullable<int> FeaturesId { get; set; }
            public string InvoiceNumber { get; set; }
            public Nullable<System.DateTime> InvoiceDate { get; set; }
            public Nullable<decimal> InvoiceAmount { get; set; }
            public string InvoiceFileBlob { get; set; }
            public Nullable<int> InvoicePerticularId { get; set; }
            public string InvoicePerticular { get; set; }
            public Nullable<System.DateTime> WarrantyFrom { get; set; }
            public Nullable<System.DateTime> WarrantyTill { get; set; }
            public int CustomerID { get; set; }
            public string CompanyName { get; set; }
            public string Unit { get; set; }
            public string AddressOne { get; set; }
            public string AddressTwo { get; set; }
            public string AddressThree { get; set; }
            public string Pincode { get; set; }
            public string City { get; set; }
            public string State { get; set; }
            public string Country { get; set; }
            public string GSTIN { get; set; }
            public string Cluster { get; set; }
            public string RouteNumber { get; set; }
            public Nullable<int> RouteId { get; set; }
            public Nullable<int> ClusterId { get; set; }
            public string Region { get; set; }
            public Nullable<int> RegionId { get; set; }
            public string Zone { get; set; }
            public Nullable<int> ZoneId { get; set; }
            public string WeeklyOff { get; set; }
            public string WorkingStart { get; set; }
            public string WorkingEnd { get; set; }
            public string SecurityFormalities { get; set; }

            public string RequestFor { get; set; }
            public Nullable<int> RequestForId { get; set; }
            public string SandS { get; set; }
            public Nullable<int> SandSId { get; set; }
            public string Remarks { get; set; }
            public Nullable<bool> IsDone { get; set; }
            public Nullable<int> TokenID { get; set; }

        }

        [HttpGet]
        public async Task<IHttpActionResult> GetTicketDetailsFromTicket([FromUri(Name = "id")] int id)
        {
            var data = await Task.Run(() => db.Table_MachineCustomerRequestsDetails.Where(c => c.TokenNo == id).ToList());
            return Ok(data);
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetMachineFromMachineNumber([FromUri(Name = "id")] int id)
        {
            var datalist = new List<machineAndCustomerDataVM>();
            var MachineData = await Task.Run(() => db.Table_MachineRegistration.Where(c => c.MachineNumber == id).FirstOrDefault());
            var CustomerData = await Task.Run(() => db.Table_CustomerRegistartion.Where(c => c.CustomerID == MachineData.CustomerId).FirstOrDefault());
            machineAndCustomerDataVM data = new machineAndCustomerDataVM()
            {
                AddressOne = CustomerData.AddressOne,
                AddressTwo = CustomerData.AddressTwo,
                AddressThree = CustomerData.AddressThree,
                City = CustomerData.City,
                CustomerId = CustomerData.CustomerID.ToString(),
                ClusterId = CustomerData.ClusterId,
                Cluster = CustomerData.Cluster,
                Country = CustomerData.Country,
                CompanyName = CustomerData.CompanyName,
                CreatedBy = CustomerData.CreatedBy,
                CreatedOn = CustomerData.CreatedOn,
                GSTIN = CustomerData.GSTIN,
                ModelName = MachineData.ModelName,
                Unit = CustomerData.Unit,
                Zone = CustomerData.Zone,
                InvoiceAmount = MachineData.InvoiceAmount,
                InvoiceDate = MachineData.InvoiceDate,
                InvoiceFileBlob = MachineData.InvoiceFileBlob,
                InvoiceNumber = MachineData.InvoiceNumber,
                InvoicePerticular = MachineData.InvoicePerticular,
                Region = CustomerData.Region,
                Pincode = CustomerData.Pincode,
                RouteNumber = CustomerData.RouteNumber,
                SecurityFormalities = CustomerData.SecurityFormalities,
                State = CustomerData.State,
                WarrantyFrom = MachineData.WarrantyFrom,
                WarrantyTill = MachineData.WarrantyTill,
                WeeklyOff = CustomerData.WeeklyOff,
                WorkingEnd = CustomerData.WorkingEnd,
                WorkingStart = CustomerData.WorkingStart,
            };
            datalist.Add(data);
            return Ok(datalist);
        }

        public class Table_RequestsFormDataDataModel
        {
            public int id { get; set; }
            public Nullable<int> MachineNumber { get; set; }
            public Nullable<int> MachineId { get; set; }
            public Nullable<int> CustomerId { get; set; }
            public string CustomerName { get; set; }
            public string RequestFor { get; set; }
            public Nullable<int> RequestForId { get; set; }
            public string SandS { get; set; }
            public Nullable<int> SandSId { get; set; }
            public string Remarks { get; set; }
            public Nullable<bool> IsDone { get; set; }
            public string CreatedBy { get; set; }
            public Nullable<System.DateTime> CreatedOn { get; set; }
        }

        [ResponseType(typeof(List<ContactData>))]
        public async Task<IHttpActionResult> GetContactList(int id)
        {

            var CuntactDataData = db.Table_Contactdetails.Where(c => c.MachineId == id).Select(c => c).FirstOrDefault();

            ContactData cdata = new ContactData()
            {
                ContactName = CuntactDataData.ContactName,
                Designation = CuntactDataData.Designation,
                Email = CuntactDataData.Email,
                Mobile = CuntactDataData.Mobile,
                Salute = CuntactDataData.Salute
            };
            contact.Add(cdata);

            return this.Ok(contact);
        }



        public partial class machineAndCustomerDataVM
        {
            public int Id { get; set; }
            public Nullable<int> MachineNumber { get; set; }
            public Nullable<int> ModelId { get; set; }
            public string ModelName { get; set; }
            public string CustomerId { get; set; }
            public string CustomerName { get; set; }

            private string companyName;

            public string GetCompanyName()
            {
                return companyName;
            }

            public void SetCompanyName(string value)
            {
                companyName = value;
            }



            public string Features { get; set; }
            public Nullable<int> FeaturesId { get; set; }
            public string InvoiceNumber { get; set; }
            public Nullable<System.DateTime> InvoiceDate { get; set; }
            public Nullable<decimal> InvoiceAmount { get; set; }
            public string InvoiceFileBlob { get; set; }
            public Nullable<int> InvoicePerticularId { get; set; }
            public string InvoicePerticular { get; set; }
            public Nullable<System.DateTime> WarrantyFrom { get; set; }
            public Nullable<System.DateTime> WarrantyTill { get; set; }
            public Nullable<System.DateTime> CreatedOn { get; set; }
            public string CreatedBy { get; set; }
            public int CustomerID { get; set; }
            public string CompanyName { get; set; }
            public string Unit { get; set; }
            public string AddressOne { get; set; }
            public string AddressTwo { get; set; }
            public string AddressThree { get; set; }
            public string Pincode { get; set; }
            public string City { get; set; }
            public string State { get; set; }
            public string Country { get; set; }
            public string GSTIN { get; set; }
            public string Cluster { get; set; }
            public string RouteNumber { get; set; }
            public Nullable<int> RouteId { get; set; }
            public Nullable<int> ClusterId { get; set; }
            public string Region { get; set; }
            public Nullable<int> RegionId { get; set; }
            public string Zone { get; set; }
            public Nullable<int> ZoneId { get; set; }
            public string WeeklyOff { get; set; }
            public string WorkingStart { get; set; }
            public string WorkingEnd { get; set; }
            public string SecurityFormalities { get; set; }
        }

    }
}
