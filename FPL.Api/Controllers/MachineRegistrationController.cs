using FPL.Api.Controllers.Blob;
using FPL.Dal.DataModel;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using HttpContext = System.Web.HttpContext;

namespace FPL.Api.Controllers
{
    public class MachineRegistrationController : ApiController
    {
        private CustomerManagerEntities db = new CustomerManagerEntities();

        [HttpPost]
        public async Task<IHttpActionResult> PostMachineRegistration()
        {
            List<Table_MachineFeatureDetails> MachineDetails = new List<Table_MachineFeatureDetails>();

            try
            {
                var httpRequest = System.Web.HttpContext.Current.Request;
                var machineNumber = httpRequest["MachineNumber"];
                var modelid = httpRequest["ModelId"];
                var customerId = httpRequest["CustomerId"];

                var features = httpRequest["Features"];
                //JArray selectedFeatures = JArray.Parse(features);
                List<FeatureItem> selectedFeaturess = JsonConvert.DeserializeObject<List<FeatureItem>>(features);

                //    var featuresdd = JsonConvert.DeserializeObject<FeaturesVM>(features);
                var invoiceNumber = httpRequest["InvoiceNumber"];
                var invoiceDate = httpRequest["InvoiceDate"];
                var invoiceamount = httpRequest["InvoiceAmount"];
                var dueamount = httpRequest["DueAmount"];
                var invoiceparticulars = httpRequest["InvoicePerticularId"];
                var warrantyr = httpRequest["WarrantyFrom"];
                var warrantytill = httpRequest["WarrantyTill"];
                var createdBy = httpRequest["CreatedBy"];
                if (dueamount == null || dueamount == "" || dueamount == "undefined" || invoiceNumber == null || invoiceNumber == "" || invoiceNumber == "undefined" || invoiceamount == null || invoiceamount == "" || invoiceamount == "undefined" || invoiceDate == null || invoiceDate == "" || invoiceDate == "undefined")
                {
                    int custID = Convert.ToInt32(customerId);
                    int machineID = Convert.ToInt32(machineNumber);
                    int modellID = Convert.ToInt32(modelid);
                    //int invoiceparticuls = Convert.ToInt32(invoiceparticulars);
                    string uniqueID = "";
                    var customerData = db.Table_CustomerRegistartion.Where(c => c.CustomerID == custID).Select(c => c).FirstOrDefault();
                   // var invoiceFileData = db.Table_InvoiceDocuments.Where(c => c.CustomerId == custID && c.MachineNumber == machineID).Select(c => c).FirstOrDefault();
                    //var invoiceData = db.Table_InvoicePerticular.Where(c => c.InvoicePerticularId == invoiceparticuls).Select(c => c).FirstOrDefault();
                    var modelData = db.Table_Model.Where(c => c.ModelId == modellID).Select(c => c).FirstOrDefault();

                    for (int i = 0; i < selectedFeaturess.Count; i++)
                    {
                        uniqueID = string.Concat(machineNumber.Concat(customerId).Concat(modellID.ToString()));
                        Table_MachineFeatureDetails data2 = new Table_MachineFeatureDetails()
                        {

                            FeatureID = selectedFeaturess[i].value,
                            FeatureName = selectedFeaturess[i].label,
                            MachineID = machineID,
                            MachineNumber = machineID,
                            CustomerID = custID,
                            CustomerName = customerData.CompanyName,
                            //InvoiceID = invoiceFileData.Id,
                            WarrantyFrom = Convert.ToDateTime(warrantyr),
                            WarrantyTill = Convert.ToDateTime(warrantytill),
                            ModelID = modellID,
                            ModelName = modelData.ModelName,
                            CreatedBy = createdBy,
                            CreatedOn = DateTime.Now,
                            FeatureDataID = uniqueID

                        };
                        if (data2 != null)
                        {
                            await Task.Run(() => db.Table_MachineFeatureDetails.Add(data2));
                            int status = await db.SaveChangesAsync();

                        }
                        else
                        {
                            continue;
                        }
                    }

                    //int mybalance = 0;
                    //bool ispaid = false;
                    //if (dueamountint == mybalance)
                    //{
                    //    ispaid = true;
                    //}
                    //else
                    //{
                    //    ispaid = ispaid;
                    //}
                    Table_MachineRegistration data = new Table_MachineRegistration()
                    {

                        MachineNumber = Convert.ToInt32(machineNumber),
                        CreatedBy = createdBy,
                        CreatedOn = DateTime.Now,
                        CustomerId = customerData.CustomerID,
                        CustomerName = customerData.CompanyName,
                        //InvoiceAmount = Convert.ToDecimal(invoiceamount),
                        //InvoiceDate = Convert.ToDateTime(invoiceDate),
                        //InvoiceFileBlob = invoiceFileData.BlobLink,
                        //InvoiceNumber = invoiceNumber,
                        //InvoicePerticular = invoiceData.InvoicePerticularName,
                        //InvoicePerticularId = invoiceData.InvoicePerticularId,
                        ModelId = modelData.ModelId,
                        ModelName = modelData.ModelName,
                        WarrantyFrom = Convert.ToDateTime(warrantyr),
                        WarrantyTill = Convert.ToDateTime(warrantytill),
                        IsMachineDeleted = false,
                        FeatureDetailsID = uniqueID,
                        //DueAmount = Convert.ToDecimal(dueamount),
                        //IsPaid = ispaid
                    };

                    await Task.Run(() => db.Table_MachineRegistration.Add(data));
                    await db.SaveChangesAsync();

                    return Ok("success");

                }
                else
                {

                    int dueamountint = Convert.ToInt32(dueamount);
                    int custID = Convert.ToInt32(customerId);

                    int invoiceparticuls = Convert.ToInt32(invoiceparticulars);

                    int machineID = Convert.ToInt32(machineNumber);

                    int modellID = Convert.ToInt32(modelid);


                    string uniqueID = "";
                    var customerData = db.Table_CustomerRegistartion.Where(c => c.CustomerID == custID).Select(c => c).FirstOrDefault();
                    var invoiceFileData = db.Table_InvoiceDocuments.Where(c => c.CustomerId == custID && c.MachineNumber == machineID).Select(c => c).FirstOrDefault();
                    var invoiceData = db.Table_InvoicePerticular.Where(c => c.InvoicePerticularId == invoiceparticuls).Select(c => c).FirstOrDefault();
                    var modelData = db.Table_Model.Where(c => c.ModelId == modellID).Select(c => c).FirstOrDefault();

                    for (int i = 0; i < selectedFeaturess.Count; i++)
                    {
                        uniqueID = string.Concat(machineNumber.Concat(customerId).Concat(modellID.ToString()));
                        Table_MachineFeatureDetails data2 = new Table_MachineFeatureDetails()
                        {

                            FeatureID = selectedFeaturess[i].value,
                            FeatureName = selectedFeaturess[i].label,
                            MachineID = machineID,
                            MachineNumber = machineID,
                            CustomerID = custID,
                            CustomerName = customerData.CompanyName,
                            InvoiceID = invoiceFileData.Id,
                            InvoiceParticulars = invoiceData.InvoicePerticularName,
                            WarrantyFrom = Convert.ToDateTime(warrantyr),
                            WarrantyTill = Convert.ToDateTime(warrantytill),
                            ModelID = modellID,
                            ModelName = modelData.ModelName,
                            CreatedBy = createdBy,
                            CreatedOn = DateTime.Now,
                            FeatureDataID = uniqueID

                        };
                        if (data2 != null)
                        {
                            await Task.Run(() => db.Table_MachineFeatureDetails.Add(data2));
                            int status = await db.SaveChangesAsync();

                        }
                        else
                        {
                            continue;
                        }
                    }

                    int mybalance = 0;
                    bool ispaid = false;
                    if (dueamountint == mybalance)
                    {
                        ispaid = true;
                    }
                    else
                    {
                        ispaid = ispaid;
                    }
                    Table_MachineRegistration data = new Table_MachineRegistration()
                    {

                        MachineNumber = Convert.ToInt32(machineNumber),
                        CreatedBy = createdBy,
                        CreatedOn = DateTime.Now,
                        CustomerId = customerData.CustomerID,
                        CustomerName = customerData.CompanyName,
                        InvoiceAmount = Convert.ToDecimal(invoiceamount),
                        InvoiceDate = Convert.ToDateTime(invoiceDate),
                        InvoiceFileBlob = invoiceFileData.BlobLink,
                        InvoiceNumber = invoiceNumber,
                        InvoicePerticular = invoiceData.InvoicePerticularName,
                        InvoicePerticularId = invoiceData.InvoicePerticularId,
                        ModelId = modelData.ModelId,
                        ModelName = modelData.ModelName,
                        WarrantyFrom = Convert.ToDateTime(warrantyr),
                        WarrantyTill = Convert.ToDateTime(warrantytill),
                        IsMachineDeleted = false,
                        FeatureDetailsID = uniqueID,
                        DueAmount = Convert.ToDecimal(dueamount),
                        IsPaid = ispaid
                    };

                    await Task.Run(() => db.Table_MachineRegistration.Add(data));
                    await db.SaveChangesAsync();

                    return Ok("success");
                }
            }
            catch (Exception e)
            {
                return Ok("fail");
            }
            //try
            //{
            //    var customerData = db.Table_CustomerRegistartion.Where(c => c.CustomerID == data1.CustomerId).Select(c => c).FirstOrDefault();
            //    var invoiceFileData = db.Table_InvoiceDocuments.Where(c => c.CustomerId == data1.CustomerId && c.MachineNumber == data1.MachineNumber).Select(c => c).FirstOrDefault();
            //    var invoiceData = db.Table_InvoicePerticular.Where(c => c.InvoicePerticularId == data1.InvoicePerticularId).Select(c => c).FirstOrDefault();
            //    var modelData = db.Table_Model.Where(c => c.ModelId == data1.ModelId).Select(c => c).FirstOrDefault();
            //    Table_MachineRegistration data = new Table_MachineRegistration()
            //    {
            //        MachineNumber = data1.MachineNumber,
            //        CreatedBy = data1.CreatedBy,
            //        CreatedOn = DateTime.Now,
            //        CustomerId = customerData.CustomerID,
            //        CustomerName = customerData.CompanyName,
            //        InvoiceAmount = data1.InvoiceAmount,
            //        InvoiceDate = data1.InvoiceDate,
            //        InvoiceFileBlob = invoiceFileData.BlobLink,
            //        InvoiceNumber = data1.InvoiceNumber,
            //        InvoicePerticular = invoiceData.InvoicePerticularName,
            //        InvoicePerticularId = invoiceData.InvoicePerticularId,
            //        ModelId = modelData.ModelId,
            //        ModelName = modelData.ModelName,
            //        WarrantyFrom = data1.WarrantyFrom,
            //        WarrantyTill = data1.WarrantyTill
            //    };

            //    await Task.Run(() => db.Table_MachineRegistration.Add(data));
            //    await db.SaveChangesAsync();

            //    return Ok("success");
            //}
            //catch (Exception e)
            //{
            //    throw e;
            //}
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetMachineInLocation([FromUri(Name = "id")] string id)
        {
           // int idd = Convert.ToInt32(id);
            var result = await Task.Run(() => db.Table_MachineRegistration.Where(c => c.CustomerName == id && c.IsMachineDeleted != true).ToList());
            return Ok(result);
        }

        [HttpPost]
        public async Task<IHttpActionResult> LocationDetails()
        {
            List<Table_LocationDetails> locationDetails = new List<Table_LocationDetails>();
            try
            {
                var httpRequest = System.Web.HttpContext.Current.Request;
                var machineNumber = httpRequest["MachineNumber"];
                var customerId = httpRequest["CustomerId"];
                var companyName = httpRequest["CustomerName"];
                var ClusterId = httpRequest["clusterId"];
                var RoadCondition = httpRequest["RoadCondition"];
                var InsideCityOutsideCity = httpRequest["InsideCityOutsideCity"];
                var NearbyLodge = httpRequest["NearbyLodge"];
                var NearbyPetrolBunk = httpRequest["NearbyPetrolBunk"];
                var HotelType = httpRequest["HotelType"];
                var BestHotels = httpRequest["BestHotels"];
                var NearbyMedicalShop = httpRequest["NearbyMedicalShop"];
                var NearbyHospital = httpRequest["NearbyHospital"];
                var NearbyMechanicShops = httpRequest["NearbyMechanicShops"];
                var whatToCarry = httpRequest["whatToCarry"];
                var textarea = httpRequest["textarea"];
                var createdBy = httpRequest["CreatedBy"];

                int? clustid = null;

                if (!string.IsNullOrEmpty(ClusterId) && ClusterId.ToLower() != "null")
                {
                    clustid = Convert.ToInt32(ClusterId);
                }

                int machineID = Convert.ToInt32(machineNumber);
                int custID = Convert.ToInt32(customerId);

                var customerData = db.Table_CustomerRegistartion.Where(c => c.CustomerID == custID).Select(c => c).FirstOrDefault();

                Table_LocationDetails data = new Table_LocationDetails()
                {
                    MachineNumber = machineID,
                    CustomerID = custID,
                    CustomerName = companyName,
                    ClusterId = clustid,
                    RoadCondition = RoadCondition,
                    InsideCityOutsideCity = InsideCityOutsideCity,
                    NearbyLodge = NearbyLodge,
                    NearbyPetrolBunk = NearbyPetrolBunk,
                    HotelType = HotelType,
                    BestHotels = BestHotels,
                    NearbyMedicalShop = NearbyMedicalShop,
                    NearbyHospital = NearbyHospital,
                    NearbyMechanicShops = NearbyMechanicShops,
                    WhatToCarry = whatToCarry,
                    TextArea = textarea,
                    UserName = createdBy,
                    CreatedOn = DateTime.Now
                };

                await Task.Run(() => db.Table_LocationDetails.Add(data));
                await db.SaveChangesAsync();

                return Ok("success");
            }
            catch (Exception e)
            {
                return Ok("fail");
            }
        }



        [HttpPost]
        public async Task<IHttpActionResult> DeleteMachineData(Table_MachineRegistration  data1)
        {
            int machinenumber = Convert.ToInt32(data1.MachineNumber);
            int modelID = Convert.ToInt32(data1.ModelId);
            int customerID = Convert.ToInt32(data1.CustomerId);
            var customerName = data1.CustomerName;
            var featureDetailsID = data1.FeatureDetailsID;

            var modelname = db.Table_Model.Where(c => c.ModelId == modelID).Select(c => c.ModelName).FirstOrDefault();


            try
            {
                var dat = await Task.Run(() => db.Table_MachineRegistration.Where(c => c.MachineNumber == machinenumber && c.CustomerId==customerID).FirstOrDefault());
                if (dat != null)
                {
                    dat.IsMachineDeleted = true;
                    


                             
                    await Task.Run(() => db.Entry(dat).State = EntityState.Modified);
                    await db.SaveChangesAsync();
                }

                var machineID = await Task.Run(() => db.Table_MachineRegistration.Where(c => c.MachineNumber == machinenumber && c.CustomerId == customerID).Select(c=>c.Id).FirstOrDefault());
                var contactdetails = await Task.Run(() => db.Table_Contactdetails.Where(c => c.MachineNumber == machinenumber && c.CustomerId == customerID ).FirstOrDefault());

                if (contactdetails != null)
                {
                    contactdetails.IsMachineDeleted = true;




                    await Task.Run(() => db.Entry(contactdetails).State = EntityState.Modified);
                    await db.SaveChangesAsync();
                }

                var invoicedetails = await Task.Run(() => db.Table_InvoiceDocuments.Where(c => c.MachineNumber == machinenumber && c.CustomerId == customerID).FirstOrDefault());

                if (invoicedetails != null)
                {
                    invoicedetails.IsMachineDeleted = true;




                    await Task.Run(() => db.Entry(invoicedetails).State = EntityState.Modified);
                    await db.SaveChangesAsync();
                }

                var featuredetails = await Task.Run(() => db.Table_MachineFeatureDetails.Where(c => c.MachineNumber == machinenumber && c.MachineID == machinenumber && c.CustomerID==customerID && c.CustomerName== customerName).FirstOrDefault());

                if (featuredetails != null)
                {
                    featuredetails.IsMachineDeleted = true;




                    await Task.Run(() => db.Entry(featuredetails).State = EntityState.Modified);
                    await db.SaveChangesAsync();
                }

                var requestdetails = await Task.Run(() => db.Table_MachineCustomerRequestsDetails.Where(c => c.MachineNumber == machinenumber && c.MachineId == machineID && c.CustomerId == customerID && c.CustomerName == customerName).ToList());
                for(int i = 0; i < requestdetails.Count; i++)
                {
                    if (requestdetails[i] != null)
                    {
                        requestdetails[i].IsMachineDeleted = true;




                        await Task.Run(() => db.Entry(requestdetails[i]).State = EntityState.Modified);
                        await db.SaveChangesAsync();
                    }
                }


                var requestsandsdetails = await Task.Run(() => db.Table_MachineCustomerSansSDetails.Where(c => c.MachineNumber == machinenumber && c.MachineId == machineID && c.CustomerId == customerID).ToList());
                for (int j = 0; j < requestsandsdetails.Count; j++)
                {
                    if (requestsandsdetails[j] != null)
                    {
                        requestsandsdetails[j].IsMachineDeleted = true;




                        await Task.Run(() => db.Entry(requestsandsdetails[j]).State = EntityState.Modified);
                        await db.SaveChangesAsync();
                    }
                }

                var requesttable = await Task.Run(() => db.Table_RequestsFormData.Where(c => c.MachineNumber == machinenumber && c.MachineId == machineID && c.CustomerId == customerID).FirstOrDefault());
              
                    if (requesttable != null)
                    {
                    requesttable.IsMachineDeleted = true;




                        await Task.Run(() => db.Entry(requesttable).State = EntityState.Modified);
                        await db.SaveChangesAsync();
                    }
                


                return Ok("success");
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public class FeaturesVM
        {

            public List<FeatureItem> Features { get; set; }
        }
        public class FeatureItem
        {
            public string label { get; set; }
            public int value { get; set; }
        }
        [HttpPost]
        public async Task<IHttpActionResult> Postcontactdetails(Table_ContactdetailsDatamodel data1)
        {
            try
            {
                var machineID = data1.MachineNumber;
                var modelID = data1.ModelID;
                var modelname = db.Table_Contactdetails.Where(c => c.MachineNumber == machineID && c.ModelID== modelID).Select(c => c.ContactDetailsID).FirstOrDefault();
                //var modelnamee = db.Table_Contactdetails.Where(c => c.MachineNumber == machineID && c.ModelID == modelID).Select(c => c.Id).FirstOrDefault();

                if (modelname == null)
                {
                    var univaue = db.Table_Contactdetails.OrderByDescending(c=>c.Id).Select(c => c.Id).FirstOrDefault();
                   
                    int idd = univaue + 1;
                    modelname = string.Concat("MODContact".Concat(idd.ToString()));
                }
                else
                {
                    var univaue = db.Table_Contactdetails.Where(c => c.ContactDetailsID == modelname).Select(c => c.Id).FirstOrDefault();

                    modelname = string.Concat("MODContact".Concat(univaue.ToString()));
                }
               

               
                Table_Contactdetails data = new Table_Contactdetails()
                {
                    ContactName = data1.ContactName,
                    CreatedBy = data1.CreatedBy,
                    CreatedOn = DateTime.Now,
                    CustomerId = data1.CustomerId,
                    Designation = data1.Designation,
                    Email = data1.Email,
                    MachineId = data1.MachineId,
                    MachineNumber = data1.MachineNumber,
                    Mobile = data1.Mobile,
                    Salute = data1.Salute,
                    ContactDetailsID= modelname,
                    ModelID= modelID
                };

                await Task.Run(() => db.Table_Contactdetails.Add(data));
                await db.SaveChangesAsync();
              

                return Ok(db.Table_Contactdetails.Where(c => c.MachineId == data1.MachineId).ToList());
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetCustomerContactDetails(int id)
        {
            var result = db.Table_Contactdetails.Where(c => c.CustomerId == id).ToList();
            return Ok(result);
        }

        [HttpGet]
        public async Task<IHttpActionResult> getCustomerContactDetailsForQM(int id)
        {
            var result = db.Table_Contactdetails.Where(c => c.CustomerId == id).ToList();
            return Ok(result);
        }


        [HttpGet]
        public async Task<IHttpActionResult> DeleteContactDetails([FromUri(Name = "id")] int id)
        {
            var data = await Task.Run(() => db.Table_Contactdetails.FindAsync(id));

            await Task.Run(() => db.Table_Contactdetails.Remove(data));
            await db.SaveChangesAsync();

            return Ok("success");
        }
        [HttpGet]
        public async Task<IHttpActionResult> GetCustomerContactDetailss(int id)
        {
            var result = db.Table_Contactdetails.Where(c => c.CustomerId == id).ToList();
            return Ok(result);
        }


        [HttpPost]
        public async Task<IHttpActionResult> UpdateContactDetails(Table_ContactdetailsDatamodel data1)
        {

            try
            {
                int ID = Convert.ToInt32(data1.Id);
                var data = db.Table_Contactdetails.Where(c => c.Id == ID).FirstOrDefault();
                data.ContactName = data1.ContactName;
                data.Salute = data1.Salute;
                data.Designation = data1.Designation;
                data.Email = data1.Email;
                data.Mobile = data1.Mobile;
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


        [HttpPost]
        public async Task<IHttpActionResult> PostSaveUpdateMachinerRegistration(Table_MachineRegistration data1)
        {

            int machineID = Convert.ToInt32(data1.Id);
            int modelID = Convert.ToInt32(data1.ModelId);
           
            var modelname = db.Table_Model.Where(c => c.ModelId == modelID).Select(c => c.ModelName).FirstOrDefault();
        

            try
            {
                var dat = await Task.Run(() => db.Table_MachineRegistration.Where(c => c.Id == machineID).FirstOrDefault());
                if (dat != null)
                {
                    dat.ModelName = modelname;
                    dat.MachineNumber = data1.MachineNumber;
                    dat.CustomerName = data1.CustomerName;
                  

                    //dat.PhotoStatus = 3;              
                    await Task.Run(() => db.Entry(dat).State = EntityState.Modified);
                    await db.SaveChangesAsync();
                }
                return Ok("success");
            }
            catch (Exception e)
            {

                throw e;
            }

        }

        //[HttpGet]
        //public async Task<IHttpActionResult> GetAllMachines()
        //{
        //    var result = await Task.Run(() => db.Table_MachineRegistration.Where(c => c.IsMachineDeleted != true).ToList());
        //    return Ok(result);
        //}
        [HttpGet]
        public async Task<IHttpActionResult> GetPerticularMachines([FromUri(Name = "id")] int id)
        {
            try
            {
                var result = await Task.Run(() => db.Table_MachineRegistration.Where(c => c.CustomerId == id && c.IsMachineDeleted != true).ToList());
                var datalist = new List<AllCustomerMachineDetails>();
                for (var i = 0; i < result.Count; i++)
                {
                    var cid = result[i].CustomerId;
                    var mn = result[i].MachineNumber;

                    var MachineData = await Task.Run(() => db.Table_MachineRegistration.Where(c => c.MachineNumber == mn).Select(c => c).FirstOrDefault());

                    var customerData = await Task.Run(() => db.Table_CustomerRegistartion.Where(c => c.CustomerID == cid).Select(c => c).FirstOrDefault());

                    AllCustomerMachineDetails data = new AllCustomerMachineDetails()
                    {
                        MachineNumber = MachineData.MachineNumber,
                        ModelId = MachineData.ModelId,
                        ModelName = MachineData.ModelName,
                        CustomerID = customerData.CustomerID,
                        CustomerName = customerData.CompanyName,
                        Unit = customerData.Unit,
                        AddressOne = customerData.AddressOne,
                        AddressTwo = customerData.AddressTwo,
                        AddressThree = customerData.AddressThree,
                        City = customerData.City,
                        State = customerData.State,
                        Pincode = customerData.Pincode,
                        GSTIN = customerData.GSTIN,
                        Cluster = customerData.Cluster,
                        RouteNumber = customerData.RouteNumber,
                        Region = customerData.Region,
                        Zone = customerData.Zone,
                        CreatedBy = result[i].CreatedBy,
                        CreatedOn = result[i].CreatedOn,
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

        [HttpGet]
        public async Task<IHttpActionResult> GetCustomerTickets([FromUri(Name = "id")] int id)
        {
            try
            {
                var result = db.Table_RequestsFormData.Where(c => c.CustomerId == id && c.IsDone != true && c.IsMachineDeleted != true).ToList();
                var datalist = new List<AllCustomerTicketDetails>();
                for (var i = 0; i < result.Count; i++)
                {
                    var requestId = result[i].RequestForId;

                    var custId = result[i].CustomerId;

                    var priorityData = await Task.Run(() => db.Table_Requests.Where(c => c.RequestsId == requestId).Select(c => c).FirstOrDefault());

                    var requestData = await Task.Run(() => db.Table_RequestsFormData.Where(c => c.CustomerId == id).Select(c => c).FirstOrDefault());

                    AllCustomerTicketDetails data = new AllCustomerTicketDetails()
                    {
                        id = result[i].id,
                        MachineId = result[i].MachineId,
                        MachineNumber = result[i].MachineNumber,
                        CustomerId = result[i].CustomerId,
                        CustomerName = result[i].CustomerName,
                        RequestForId = result[i].RequestForId,
                        RequestsName = priorityData.RequestsName,
                        Priority = priorityData.Priority,
                        Remarks = result[i].Remarks,
                        Resolution = result[i].Resolution,
                        CreatedBy = result[i].CreatedBy,
                        CreatedOn = result[i].CreatedOn,
                        TokenID = result[i].TokenID,
                        ContactId = result[i].ContactId,
                        ContactName = result[i].ContactName,
                        Salute = result[i].Salute,
                        Designation = result[i].Designation,
                        Email = result[i].Email,
                        Mobile = result[i].Mobile,
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
        //public async Task<IHttpActionResult> GetCustomerTickets([FromUri(Name = "id")] int id)
        //{
        //    var result = db.Table_RequestsFormData.Where(c => c.CustomerId == id && c.IsDone != true).ToList();
        //    return Ok(result);
        //}

        [HttpGet]
        public async Task<IHttpActionResult> GetMachineCustomerDetails()
        {
            try
            {
                var result = await Task.Run(() => db.Table_MachineRegistration.Where(c => c.IsMachineDeleted != true).ToList());
                var datalist = new List<AllCustomerMachineDetails>();

                for (var i = 0; i < result.Count; i++)
                {
                    var cid = result[i].CustomerId;
                    var mn = result[i].MachineNumber;

                    var MachineData = await Task.Run(() => db.Table_MachineRegistration.Where(c => c.MachineNumber == mn).Select(c => c).FirstOrDefault());

                    var customerData = await Task.Run(() => db.Table_CustomerRegistartion.Where(c => c.CustomerID == cid).Select(c => c).FirstOrDefault());

                    AllCustomerMachineDetails data = new AllCustomerMachineDetails();

                    if (MachineData != null)
                    {
                        data.Id = MachineData.Id;
                        data.MachineNumber = MachineData.MachineNumber;
                        data.ModelId = MachineData.ModelId;
                        data.ModelName = MachineData.ModelName;
                        data.CreatedBy = result[i].CreatedBy;
                        data.CreatedOn = result[i].CreatedOn;
                    }

                    if (customerData != null)
                    {
                        data.CustomerID = customerData.CustomerID;
                        data.CustomerName = customerData.CompanyName;
                        data.Unit = customerData.Unit;
                        data.AddressOne = customerData.AddressOne;
                        data.AddressTwo = customerData.AddressTwo;
                        data.AddressThree = customerData.AddressThree;
                        data.City = customerData.City;
                        data.State = customerData.State;
                        data.Pincode = customerData.Pincode;
                        data.GSTIN = customerData.GSTIN;
                        data.Cluster = customerData.Cluster;
                        data.RouteNumber = customerData.RouteNumber;
                        data.Region = customerData.Region;
                        data.Zone = customerData.Zone;
                    }

                    datalist.Add(data);
                }

                return Ok(datalist);
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        [HttpPost]
        //[Route("api/FileUpload/PostUploadSpecimensignature/{id}")]
        public async Task<IHttpActionResult> UploadInvoice()
        {
            HttpPostedFile hpf;
            var httpRequest = HttpContext.Current.Request;
            var machineNumber = httpRequest["MachineNumber"];
            var customerId= httpRequest["CustomerId"];
            var createdBy= httpRequest["CreatedBy"];
            var invoiceNumber= httpRequest["InvoiceNumber"];
            var invoiceamount = httpRequest["InvoiceAmount"];
            var dueamount = httpRequest["DueAmount"];
            var modelid = httpRequest["ModelId"];
            int dueamountint = Convert.ToInt32(dueamount);
            byte[] fileData = null;
            var doclink = "";
            string sPath = "";
            sPath = System.Web.Hosting.HostingEnvironment.MapPath("~/locker/");

            HttpFileCollection hfc = HttpContext.Current.Request.Files;

            for (int iCnt = 0; iCnt <= hfc.Count - 1; iCnt++)
            {
                hpf = hfc[iCnt];
                doclink = BlobDocuments.GetDocumentorfileUri(hpf);
                using (var binaryReader = new BinaryReader(hpf.InputStream))
                {
                    fileData = binaryReader.ReadBytes(hpf.ContentLength);
                }
                try
                {
                    var cid = Convert.ToInt32(customerId);
                    int custID = Convert.ToInt32(customerId);
                    int modellID = Convert.ToInt32(modelid);
                    var customerData = db.Table_CustomerRegistartion.Where(c => c.CustomerID == custID).Select(c => c).FirstOrDefault();

                    var modelData = db.Table_Model.Where(c => c.ModelId == modellID).Select(c => c).FirstOrDefault();
                    int mybalance = 0;
                    bool ispaid = false;
                    if (dueamountint == mybalance)
                    {
                        ispaid = true;
                    }
                    else
                    {
                        ispaid = ispaid;
                    }
                    Table_InvoiceDocuments a = new Table_InvoiceDocuments()
                    {


                  
                    BlobLink = doclink,
                        InvoiceName = hpf.FileName,
                        InvoiceNumber = invoiceNumber,
                        CreatedOn = DateTime.Now,
                        Id = 0,
                        CreatedBy = createdBy,
                        CustomerId = cid,
                        MachineNumber = Convert.ToInt32(machineNumber),
                        InvoiceAmount = Convert.ToDecimal(invoiceamount),
                        DueAmount = Convert.ToDecimal(dueamount),
                        IsPaid = ispaid,
                        ModelId= modellID,
                        ModelName=modelData.ModelName,
                        CustomerName=customerData.CompanyName
                    };
            
                    db.Table_InvoiceDocuments.Add(a);
                }
                catch (Exception e)
                {

                    throw e;
                }
                await db.SaveChangesAsync();

            }
            return Ok("success");
        }

        public partial class AllCustomerTicketDetails
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
            public string Resolution { get; set; }
            public Nullable<bool> IsDone { get; set; }
            public string CreatedBy { get; set; }
            public Nullable<System.DateTime> CreatedOn { get; set; }
            public Nullable<bool> IsMachineDeleted { get; set; }
            public Nullable<int> TokenID { get; set; }
            public Nullable<int> ContactId { get; set; }
            public string Salute { get; set; }
            public string ContactName { get; set; }
            public string Designation { get; set; }
            public string Email { get; set; }
            public string Mobile { get; set; }
            public int RequestsId { get; set; }
            public string RequestsName { get; set; }
            public string Priority { get; set; }
        }

        public partial class AllCustomerMachineDetails
        {
            public int Id { get; set; }
            public Nullable<int> MachineNumber { get; set; }
            public Nullable<int> ModelId { get; set; }
            public string ModelName { get; set; }
            public Nullable<int> CustomerId { get; set; }
            public string CustomerName { get; set; }
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
            public string CreatedBy { get; set; }
            public Nullable<System.DateTime> CreatedOn { get; set; }
            public string CompanyOldName { get; set; }

        }
        public partial class Table_MachineRegistrationDataModel
        {
            public int Id { get; set; }
            public Nullable<int> MachineNumber { get; set; }
            public Nullable<int> ModelId { get; set; }
            public string ModelName { get; set; }
            public Nullable<int> CustomerId { get; set; }
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
            public Nullable<System.DateTime> CreatedOn { get; set; }
            public string CreatedBy { get; set; }
        }

        public partial class Table_ContactdetailsDatamodel
        {
            public int Id { get; set; }

            public Nullable<int> ModelID { get; set; }
            public string Salute { get; set; }
            public string ContactName { get; set; }
            public string Designation { get; set; }
            public string Email { get; set; }
            public string Mobile { get; set; }
            public Nullable<int> MachineNumber { get; set; }
            public Nullable<int> MachineId { get; set; }
            public Nullable<int> CustomerId { get; set; }
            public string CreatedBy { get; set; }
            public Nullable<System.DateTime> CreatedOn { get; set; }
        }
    }
}
