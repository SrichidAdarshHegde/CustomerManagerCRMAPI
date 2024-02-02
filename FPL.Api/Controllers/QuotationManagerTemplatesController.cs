using FPL.Dal.DataModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using static FPL.Api.Controllers.MachineRegistrationController;

namespace FPL.Api.Controllers
{
    public class QuotationManagerTemplatesController : ApiController

    {

        private CustomerManagerEntities db = new CustomerManagerEntities();




        [HttpPost]
        public async Task<IHttpActionResult> postcontactdetailsqm(Table_ContactdetailsDatamodel data1)
        {
            try
            {
                var machineID = data1.MachineNumber;
                var modelID = data1.ModelID;
                var modelname = db.Table_Contactdetails.Where(c => c.MachineNumber == machineID && c.ModelID == modelID).Select(c => c.ContactDetailsID).FirstOrDefault();
                //var modelnamee = db.Table_Contactdetails.Where(c => c.MachineNumber == machineID && c.ModelID == modelID).Select(c => c.Id).FirstOrDefault();

                if (modelname == null)
                {
                    var univaue = db.Table_Contactdetails.OrderByDescending(c => c.Id).Select(c => c.Id).FirstOrDefault();

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
              
                    Mobile = data1.Mobile,
                    Salute = data1.Salute,
          
               
                };

                await Task.Run(() => db.Table_Contactdetails.Add(data));
                await db.SaveChangesAsync();


                return Ok(db.Table_Contactdetails.Where(c => c.CustomerId == data1.CustomerId).ToList());
            }
            catch (Exception e)
            {
                throw e;
            }
        }









        [HttpGet]
        public async Task<IHttpActionResult> GetAllRefNo()
        {
            try
            {
                var refID = db.Table_Common_RefID_Template.ToList();
               return Ok(refID);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpGet]
        public async Task<IHttpActionResult> Getbillingaddress([FromUri(Name = "id")] int id)
        {

            try
            {
                var billingaddress = db.Table_CustomerRegistartion.Where(c => c.CustomerID == id).ToList();
                return Ok(billingaddress);
            }
            catch (Exception e)
            {
                throw e;
            }

        }






        [HttpGet]
        public async Task<IHttpActionResult> GetQ4020details([FromUri(Name = "id")] int id)
        {
            try
            {
                var result = db.Table_Quotation4020template.Where(c => c.RefID==id).FirstOrDefault();
               


                return Ok(result);
            }
            catch (Exception e)
            {

                throw e;
            }

        }

        [HttpGet]
        public async Task<IHttpActionResult> GetQ4020HTdetails([FromUri(Name = "id")] int id)
        {
            try
            {
                var result = db.Table_Quotation4020_HT.Where(c => c.RefID == id).FirstOrDefault();



                return Ok(result);
            }
            catch (Exception e)
            {

                throw e;
            }

        }

        [HttpGet]
        public async Task<IHttpActionResult> Get2015indollordetails([FromUri(Name = "id")] int id)
        {
            try
            {
                var result = db.Table_Quotation2015_in_dollar.Where(c => c.RefID == id).FirstOrDefault();



                return Ok(result);
            }
            catch (Exception e)
            {

                throw e;
            }

        }

        [HttpGet]
        public async Task<IHttpActionResult> GetRapidisparesdetails([FromUri(Name = "id")] int id)
        {
            try
            {
                var result = db.Table_Rapid_I_Spares.Where(c => c.RefID == id).FirstOrDefault();



                return Ok(result);
            }
            catch (Exception e)
            {

                throw e;
            }

        }

        [HttpGet]
        public async Task<IHttpActionResult> GetRapid64details([FromUri(Name = "id")] int id)
        {
            try
            {
                var result = db.Table_Rapid_I_64_Upgrade.Where(c => c.RefID == id).FirstOrDefault();



                return Ok(result);
            }
            catch (Exception e)
            {

                throw e;
            }

        }

        [HttpGet]
        public async Task<IHttpActionResult> GetRapid64CAMdetails([FromUri(Name = "id")] int id)
        {
            try
            {
                var result = db.Table_Rapid64CAM.Where(c => c.RefID == id).FirstOrDefault();



                return Ok(result);
            }
            catch (Exception e)
            {

                throw e;
            }

        }


        [HttpGet]
        public async Task<IHttpActionResult> GetRapid2015JLXdetails([FromUri(Name = "id")] int id)
        {
            try
            {
                var result = db.Table_Rapidi2015JLX.Where(c => c.RefID == id).FirstOrDefault();



                return Ok(result);
            }
            catch (Exception e)
            {

                throw e;
            }

        }

        [HttpGet]
        public async Task<IHttpActionResult> GetRapiditabledetails([FromUri(Name = "id")] int id)
        {
            try
            {
                var result = db.Table_rapiditable.Where(c => c.RefID == id).FirstOrDefault();



                return Ok(result);
            }
            catch (Exception e)
            {

                throw e;
            }

        }

        [HttpGet]
        public async Task<IHttpActionResult> GetV4020details([FromUri(Name = "id")] int id)
        {
            try
            {
                var result = db.Table_V4020.Where(c => c.RefID == id).FirstOrDefault();



                return Ok(result);
            }
            catch (Exception e)
            {

                throw e;
            }

        }

        [HttpGet]
        public async Task<IHttpActionResult> GetV4030etails([FromUri(Name = "id")] int id)
        {
            try
            {
                var result = db.Table_V4030.Where(c => c.RefID == id).FirstOrDefault();



                return Ok(result);
            }
            catch (Exception e)
            {

                throw e;
            }

        }

        [HttpGet]
        public async Task<IHttpActionResult> GetV20152axesetails([FromUri(Name = "id")] int id)
        {
            try
            {
                var result = db.Table_V20152AXES.Where(c => c.RefID == id).FirstOrDefault();



                return Ok(result);
            }
            catch (Exception e)
            {

                throw e;
            }

        }



        [HttpGet]
        public async Task<IHttpActionResult> GetVMCdetails([FromUri(Name = "id")] int id)
        {
            try
            {
                var result = db.Table_rapiditable.Where(c => c.RefID == id).FirstOrDefault();



                return Ok(result);
            }
            catch (Exception e)
            {

                throw e;
            }

        }



        [HttpPost]
        public async Task<IHttpActionResult> Savequotationtemplate(Quotationtemplatedetails data)
        {
            try
            {


                Table_Quotation4020template abc = new Table_Quotation4020template()

                {

                 RefID = data.RefID,

                 BillingAddress = data.BillingAddress,

                 BasicSystemQty=data.BasicSystemQty,
                 BasicSystemPrice= data.BasicSystemPrice,

                 OptionalQtyA= data.OptionalQtyA,
                 OptionalPriceA= data.OptionalPriceA,

                 OptionalQtyB= data.OptionalQtyB,
                 OptionalPriceB=data.OptionalPriceB,

                 OptionalQtyC= data.OptionalQtyC,
                 OptionalPriceC=data.OptionalPriceC,

                 OptionalQtyD= data.OptionalQtyD,
                 OptionalPriceD= data.OptionalPriceD,

                 OptionalQtyE= data.OptionalQtyE,
                 OptionalPriceE= data.OptionalPriceE,

                 OptionalQtyF= data.OptionalQtyF,
                 OptionalPriceF=data.OptionalPriceF, 

                OptionalQtyG=data.OptionalQtyG,
                OptionalPriceG= data.OptionalPriceG,

                OptionalQtyH=data.OptionalQtyH,
                OptionalPriceH= data.OptionalPriceH,

                TemplateID=data.TemplateID,
                TemplateName = data.TemplateName,
                CustomerName=data.CustomerName,
                CreatedBy = data.CreatedBy,
                CreatedOn = DateTime.Now,
                CustomerID = data.CustomerID,
                KindAttention=data.KindAttention,
                TotalAmount = data.TotalAmount,
               
                };

               db.Table_Quotation4020template.Add(abc);
                await db.SaveChangesAsync();

               var result= db.Table_Quotation4020template.OrderByDescending(c => c.ID).FirstOrDefault();

                Table_Common_RefID_Template ab = new Table_Common_RefID_Template()

                {
                    RefID = data.RefID,
                    TemplateName = data.TemplateName,
                    CreatedBy = data.CreatedBy,
                    CreatedOn = DateTime.Now,
                };

                await Task.Run(() => db.Table_Common_RefID_Template.Add(ab));
                await db.SaveChangesAsync();

                return Ok("success");


            }

            catch (Exception e)
            {

                throw e;
            }
        }


        [HttpPost]
        public async Task<IHttpActionResult> SaveRapidIVMC(RapidIVMCdetails data)
        {
            try
            {


                Table_Rapid_i_VMS abc = new Table_Rapid_i_VMS()

                {

                    RefID = data.RefID,

                    BillingAddress = data.BillingAddress,

                    BasicQty = data.BasicQty,
                    BasicPrice = data.BasicPrice,

                    BasicQtyA = data.BasicQtyA,
                    BasicPriceA = data.BasicPriceA,

                    BasicQtyB = data.BasicQtyB,
                    BasicPriceB = data.BasicPriceB,

                    BasicQtyC = data.BasicQtyC,
                    BasicPriceC = data.BasicPriceC,


                    OptionalQtyA = data.OptionalQtyA,
                    OptionalPriceA = data.OptionalPriceA,

                    OptionalQtyB = data.OptionalQtyB,
                    OptionalPriceB = data.OptionalPriceB,

                    OptionalQtyC = data.OptionalQtyC,
                    OptionalPriceC = data.OptionalPriceC,

                    OptionalQtyD = data.OptionalQtyD,
                    OptionalPriceD = data.OptionalPriceD,

                    OptionalQtyE = data.OptionalQtyE,
                    OptionalPriceE = data.OptionalPriceE,

                    OptionalQtyF = data.OptionalQtyF,
                    OptionalPriceF = data.OptionalPriceF,

                    OptionalQtyG = data.OptionalQtyG,
                    OptionalPriceG = data.OptionalPriceG,

                    OptionalQtyH = data.OptionalQtyH,
                    OptionalPriceH = data.OptionalPriceH,

                    TemplateID = data.TemplateID,
                    TemplateName = data.TemplateName,
                    CustomerName = data.CustomerName,
                    CreatedBy = data.CreatedBy,
                    CreatedOn = DateTime.Now,
                    CustomerID = data.CustomerID,
                    KindAttention = data.KindAttention,
                    TotalAmount = data.TotalAmount,

                };

                db.Table_Rapid_i_VMS.Add(abc);
                await db.SaveChangesAsync();

                var result = db.Table_Rapid_i_VMS.OrderByDescending(c => c.ID).FirstOrDefault();

                Table_Common_RefID_Template ab = new Table_Common_RefID_Template()

                {
                    RefID = data.RefID,
                    TemplateName = data.TemplateName,
                    CreatedBy = data.CreatedBy,
                    CreatedOn = DateTime.Now,
                };

                await Task.Run(() => db.Table_Common_RefID_Template.Add(ab));
                await db.SaveChangesAsync();

                return Ok("success");


            }

            catch (Exception e)
            {

                throw e;
            }
        }




        [HttpPost]
        public async Task<IHttpActionResult> SaveRapid2015JLX(Rapidi2015JLX data)
        {
            try
            {


                Table_Rapidi2015JLX abc = new Table_Rapidi2015JLX()
                {
                    RefID = data.RefID,

                    BillingAddress = data.BillingAddress,

                    BasicSystemQty = data.BasicSystemQty,
                    BasicSystemPrice = data.BasicSystemPrice,

                    OptionalQtyA = data.OptionalQtyA,
                    OptionalPriceA = data.OptionalPriceA,

                    OptionalQtyB = data.OptionalQtyB,
                    OptionalPriceB = data.OptionalPriceB,

                    OptionalQtyC = data.OptionalQtyC,
                    OptionalPriceC = data.OptionalPriceC,

                    OptionalQtyD = data.OptionalQtyD,
                    OptionalPriceD = data.OptionalPriceD,

                    OptionalQtyE = data.OptionalQtyE,
                    OptionalPriceE = data.OptionalPriceE,

                    OptionalQtyF = data.OptionalQtyF,
                    OptionalPriceF = data.OptionalPriceF,

                    OptionalQtyG = data.OptionalQtyG,
                    OptionalPriceG = data.OptionalPriceG,

                    OptionalQtyH = data.OptionalQtyH,
                    OptionalPriceH = data.OptionalPriceH,

                    TemplateID = data.TemplateID,
                    TemplateName = data.TemplateName,
                    CustomerName = data.CustomerName,
                    CreatedBy = data.CreatedBy,
                    CreatedOn = DateTime.Now,
                    CustomerID = data.CustomerID,
                    KindAttention = data.KindAttention,
                    TotalAmount = data.TotalAmount,
                };

                db.Table_Rapidi2015JLX.Add(abc);
                await db.SaveChangesAsync();

                var result = db.Table_Rapidi2015JLX.OrderByDescending(c => c.ID).FirstOrDefault();

                Table_Common_RefID_Template ab = new Table_Common_RefID_Template()

                {
                    RefID = data.RefID,
                    TemplateName = data.TemplateName,
                    CreatedBy = data.CreatedBy,
                    CreatedOn = DateTime.Now,
                };

                await Task.Run(() => db.Table_Common_RefID_Template.Add(ab));
                await db.SaveChangesAsync();

                return Ok("success");

            }

            catch (Exception e)
            {

                throw e;
            }
        }





        [HttpPost]
        public async Task<IHttpActionResult> Savequotation4020HT(Quotation4020HTtemplatedetails data)
        {
            try
            {


                Table_Quotation4020_HT abc = new Table_Quotation4020_HT()
                {
                    RefID = data.RefID,

                    BillingAddress = data.BillingAddress,

                    BasicSystemQty = data.BasicSystemQty,
                    BasicSystemPrice = data.BasicSystemPrice,

                    OptionalQtyA = data.OptionalQtyA,
                    OptionalPriceA = data.OptionalPriceA,

                    OptionalQtyB = data.OptionalQtyB,
                    OptionalPriceB = data.OptionalPriceB,

                    OptionalQtyC = data.OptionalQtyC,
                    OptionalPriceC = data.OptionalPriceC,

                    OptionalQtyD = data.OptionalQtyD,
                    OptionalPriceD = data.OptionalPriceD,

                  

                    TemplateID = data.TemplateID,
                    TemplateName = data.TemplateName,
                    CustomerName = data.CustomerName,
                    CreatedBy = data.CreatedBy,
                    CreatedOn = DateTime.Now,
                    CustomerID = data.CustomerID,
                    KindAttention=data.KindAttention,
                    TotalAmount = data.TotalAmount,
                };

                db.Table_Quotation4020_HT.Add(abc);
                await db.SaveChangesAsync();

                var result = db.Table_Quotation4020_HT.OrderByDescending(c => c.ID).FirstOrDefault();

                Table_Common_RefID_Template ab = new Table_Common_RefID_Template()

                {
                    RefID = data.RefID,
                    TemplateName = data.TemplateName,
                    CreatedBy = data.CreatedBy,
                    CreatedOn = DateTime.Now,
                };

                await Task.Run(() => db.Table_Common_RefID_Template.Add(ab));
                await db.SaveChangesAsync();

                return Ok("success");

            }

            catch (Exception e)
            {

                throw e;
            }
        }






        [HttpPost]
        public async Task<IHttpActionResult> Savequotation2015indollar(Quotation2015indollartemplatedetails data)
        {
            try
            {


                Table_Quotation2015_in_dollar xyz = new Table_Quotation2015_in_dollar()
                {
                    RefID = data.RefID,

                    BillingAddress = data.BillingAddress,

                    BasicSystemQty = data.BasicSystemQty,
                    BasicSystemPrice = data.BasicSystemPrice,

                    OptionalQtyA = data.OptionalQtyA,
                    OptionalPriceA = data.OptionalPriceA,

                    OptionalQtyB = data.OptionalQtyB,
                    OptionalPriceB = data.OptionalPriceB,

                    OptionalQtyC = data.OptionalQtyC,
                    OptionalPriceC = data.OptionalPriceC,

                    OptionalQtyD = data.OptionalQtyD,
                    OptionalPriceD = data.OptionalPriceD,

                    OptionalQtyE = data.OptionalQtyE,
                    OptionalPriceE = data.OptionalPriceE,

                    OptionalQtyF = data.OptionalQtyF,
                    OptionalPriceF = data.OptionalPriceF,

                    OptionalQtyG = data.OptionalQtyG,
                    OptionalPriceG = data.OptionalPriceG,

                    OptionalQtyH = data.OptionalQtyH,
                    OptionalPriceH = data.OptionalPriceH,

                    TemplateID = data.TemplateID,
                    TemplateName = data.TemplateName,
                    CustomerName = data.CustomerName,
                    CreatedBy = data.CreatedBy,
                    CreatedOn = DateTime.Now,
                    CustomerID = data.CustomerID,
                    KindAttention = data.KindAttention,
                    TotalAmount = data.TotalAmount,
                };
                db.Table_Quotation2015_in_dollar.Add(xyz);
              
                await db.SaveChangesAsync();

                var result = db.Table_Quotation2015_in_dollar.OrderByDescending(c => c.ID).FirstOrDefault();

                Table_Common_RefID_Template ab = new Table_Common_RefID_Template()

                {
                    RefID = data.RefID,
                    TemplateName = data.TemplateName,
                    CreatedBy = data.CreatedBy,
                    CreatedOn = DateTime.Now,
                };

                await Task.Run(() => db.Table_Common_RefID_Template.Add(ab));
                await db.SaveChangesAsync();

                return Ok("success");

            }

            catch (Exception e)
            {

                throw e;
            }
        }




        [HttpPost]
        public async Task<IHttpActionResult> Saverapidspares(Rapidsparesdetails data)
        {
            try
            {


                Table_Rapid_I_Spares abc = new Table_Rapid_I_Spares()
                {
                    RefID = data.RefID,

                    BillingAddress = data.BillingAddress,

                    VisionQtyA = data.VisionQtyA,
                    VisionPriceA = data.VisionPriceA,

                    VisionQtyB = data.VisionQtyB,
                    VisionPriceB = data.VisionPriceB,

                    KindAttention = data.KindAttention,

                    TemplateID = data.TemplateID,
                    TemplateName = data.TemplateName,
                    CustomerName = data.CustomerName,
                    CreatedBy = data.CreatedBy,
                    CreatedOn = DateTime.Now,
                    CustomerID = data.CustomerID,
                    TotalAmount = data.TotalAmount,

                };

                db.Table_Rapid_I_Spares.Add(abc);
                await db.SaveChangesAsync();

                var result = db.Table_Rapid_I_Spares.OrderByDescending(c => c.ID).FirstOrDefault();
                Table_Common_RefID_Template ab = new Table_Common_RefID_Template()

                {
                    RefID = data.RefID,
                    TemplateName = data.TemplateName,
                    CreatedBy = data.CreatedBy,
                    CreatedOn = DateTime.Now,
                };

                await Task.Run(() => db.Table_Common_RefID_Template.Add(ab));
                await db.SaveChangesAsync();

                return Ok("success");

            }


            catch (Exception e)
            {

                throw e;
            }
        }


        [HttpPost]
        public async Task<IHttpActionResult> SaveV2015(V2015details data)
        {
            try
            {


                Table_V20152AXES abc = new Table_V20152AXES()
                {
                    RefID = data.RefID,

                    BillingAddress = data.BillingAddress,
                    KindAttention = data.KindAttention,
                    AxesQty = data.AxesQty,
                    AxesPrice = data.AxesPrice,
                    TemplateID = data.TemplateID,
                    TemplateName = data.TemplateName,
                    CustomerName = data.CustomerName,
                    CreatedBy = data.CreatedBy,
                    CreatedOn = DateTime.Now,
                    CustomerID = data.CustomerID,
                    TotalAmount = data.TotalAmount,

                };

                db.Table_V20152AXES.Add(abc);
                await db.SaveChangesAsync();

                var result = db.Table_V20152AXES.OrderByDescending(c => c.ID).FirstOrDefault();

                Table_Common_RefID_Template ab = new Table_Common_RefID_Template()

                {
                    RefID = data.RefID,
                    TemplateName = data.TemplateName,
                    CreatedBy = data.CreatedBy,
                    CreatedOn = DateTime.Now,
                };

                await Task.Run(() => db.Table_Common_RefID_Template.Add(ab));
                await db.SaveChangesAsync();

                return Ok("success");

            }

            catch (Exception e)
            {

                throw e;
            }
        }




        [HttpPost]
        public async Task<IHttpActionResult> SaveV4020(V4020details data)
        {
            try
            {


                Table_V4020 abc = new Table_V4020()
                {
                    RefID = data.RefID,
                    KindAttention = data.KindAttention,
                    BillingAddress = data.BillingAddress,
                    BasicQty = data.BasicQty,
                    BasicPrice = data.BasicPrice,
                    DesQty = data.DesQty,
                    DesPrice = data.DesPrice,
                    TemplateID = data.TemplateID,
                    TemplateName = data.TemplateName,
                    CustomerName = data.CustomerName,
                    CreatedBy = data.CreatedBy,
                    CreatedOn = DateTime.Now,
                    CustomerID = data.CustomerID,
                    TotalAmount = data.TotalAmount,
               
                };

                db.Table_V4020.Add(abc);
                await db.SaveChangesAsync();

                var result = db.Table_V4020.OrderByDescending(c => c.ID).FirstOrDefault();

                Table_Common_RefID_Template ab = new Table_Common_RefID_Template()

                {
                    RefID = data.RefID,
                    TemplateName = data.TemplateName,
                    CreatedBy = data.CreatedBy,
                    CreatedOn = DateTime.Now,
                };

                await Task.Run(() => db.Table_Common_RefID_Template.Add(ab));
                await db.SaveChangesAsync();

                return Ok("success");

            }

            catch (Exception e)
            {

                throw e;
            }
        }



        [HttpPost]
        public async Task<IHttpActionResult> Saverapid64(Rapid64details data)
        {
            try
            {


                Table_Rapid_I_64_Upgrade abc = new Table_Rapid_I_64_Upgrade()
                {
                    RefID = data.RefID,

                    BillingAddress = data.BillingAddress,
                    KindAttention = data.KindAttention,
                    UpgradeQtyA = data.UpgradeQtyA,
                    UpgradeQtyB = data.UpgradeQtyB,

                    UpgradeQtyC = data.UpgradeQtyC,
                    UpgradePrice = data.UpgradePrice,



                    TemplateID = data.TemplateID,
                    TemplateName = data.TemplateName,
                    CustomerName = data.CustomerName,
                    CreatedBy = data.CreatedBy,
                    CreatedOn = DateTime.Now,
                    CustomerID = data.CustomerID,
                    TotalAmount = data.TotalAmount,

                };

                db.Table_Rapid_I_64_Upgrade.Add(abc);
                await db.SaveChangesAsync();

                var result = db.Table_Rapid_I_64_Upgrade.OrderByDescending(c => c.ID).FirstOrDefault();

                Table_Common_RefID_Template ab = new Table_Common_RefID_Template()

                {
                    RefID = data.RefID,
                    TemplateName = data.TemplateName,
                    CreatedBy = data.CreatedBy,
                    CreatedOn = DateTime.Now,
                };

                await Task.Run(() => db.Table_Common_RefID_Template.Add(ab));
                await db.SaveChangesAsync();

                return Ok("success");

            }

            catch (Exception e)
            {

                throw e;
            }
        }



        [HttpPost]
        public async Task<IHttpActionResult> SaveV4030(V4030details data)
        {
            try
            {


                Table_V4030 abc = new Table_V4030()
                {
                    RefID = data.RefID,

                    BillingAddress = data.BillingAddress,
                    KindAttention = data.KindAttention,
                    BasicQty = data.BasicQty,
                    BasicPrice = data.BasicPrice,

                    AuxQty = data.AuxQty,
                    AuxPrice = data.AuxPrice,



                    TemplateID = data.TemplateID,
                    TemplateName = data.TemplateName,
                    CustomerName = data.CustomerName,
                    CreatedBy = data.CreatedBy,
                    CreatedOn = DateTime.Now,
                    CustomerID = data.CustomerID,
                    TotalAmount = data.TotalAmount,

                };

                db.Table_V4030.Add(abc);
                await db.SaveChangesAsync();

                var result = db.Table_V4030.OrderByDescending(c => c.ID).FirstOrDefault();

                Table_Common_RefID_Template ab = new Table_Common_RefID_Template()

                {
                    RefID = data.RefID,
                    TemplateName = data.TemplateName,
                    CreatedBy = data.CreatedBy,
                    CreatedOn = DateTime.Now,
                };

                await Task.Run(() => db.Table_Common_RefID_Template.Add(ab));
                await db.SaveChangesAsync();

                return Ok("success");

            }

            catch (Exception e)
            {

                throw e;
            }
        }


        [HttpPost]
        public async Task<IHttpActionResult> SaveRapidi64cam(Rapid64CAMdetails data)
        {
            try
            {


                Table_Rapid64CAM abc = new Table_Rapid64CAM()
                {
                    RefID = data.RefID,
                    KindAttention = data.KindAttention,
                    BillingAddress = data.BillingAddress,
                    TemplateID = data.TemplateID,
                    TemplateName = data.TemplateName,
                    CustomerName = data.CustomerName,
                    CreatedBy = data.CreatedBy,
                    CreatedOn = DateTime.Now,
                    CustomerID = data.CustomerID,
                    DesQty= data.DesQty,
                    DesPrice = data. DesPrice,
                    ACSCQty = data.ACSCQty,
                    ACSCPrice = data.ACSCPrice,
                    TotalAmount = data.TotalAmount,
                };

                db.Table_Rapid64CAM.Add(abc);
                await db.SaveChangesAsync();

                var result = db.Table_Rapid64CAM.OrderByDescending(c => c.ID).FirstOrDefault();
                Table_Common_RefID_Template ab = new Table_Common_RefID_Template()

                {
                    RefID = data.RefID,
                    TemplateName = data.TemplateName,
                    CreatedBy = data.CreatedBy,
                    CreatedOn = DateTime.Now,
                };

                await Task.Run(() => db.Table_Common_RefID_Template.Add(ab));
                await db.SaveChangesAsync();

                return Ok("success");

            }

            

            catch (Exception e)
            {

                throw e;
            }
        }



        [HttpPost]
        public async Task<IHttpActionResult> SaveRapidtable(Rapiditabledetails data)
        {
            try
            {


                Table_rapiditable abc = new Table_rapiditable()
                {
                    RefID = data.RefID,
                    KindAttention = data.KindAttention,
                    BillingAddress = data.BillingAddress,

                    VisionQty = data.VisionQty,
                    VisionPrice = data.VisionPrice,
                    TemplateID = data.TemplateID,
                    TemplateName = data.TemplateName,
                    CustomerName = data.CustomerName,
                    CreatedBy = data.CreatedBy,
                    CreatedOn = DateTime.Now,
                    CustomerID = data.CustomerID,
                    TotalAmount = data.TotalAmount,

                };

                db.Table_rapiditable.Add(abc);
                await db.SaveChangesAsync();

                var result = db.Table_rapiditable.OrderByDescending(c => c.ID).FirstOrDefault();

                Table_Common_RefID_Template ab = new Table_Common_RefID_Template()

                {
                    RefID = data.RefID,
                    TemplateName = data.TemplateName,
                    CreatedBy = data.CreatedBy,
                    CreatedOn = DateTime.Now,
                };

                await Task.Run(() => db.Table_Common_RefID_Template.Add(ab));
                await db.SaveChangesAsync();

                return Ok("success");

            }

            catch (Exception e)
            {

                throw e;
            }
        }


        [HttpPost]
        public async Task<IHttpActionResult> SaveRapidtrainingcharges(Rapidtrainingchargesdetails data)
        {
            try
            {





                Table_RapidiTrainingcharges abc = new Table_RapidiTrainingcharges()
                {
                    RefID = data.RefID,
                    KindAttention = data.KindAttention,
                    BillingAddress = data.BillingAddress,
                    TemplateID = data.TemplateID,
                    TemplateName = data.TemplateName,
                    CustomerName = data.CustomerName,
                    CreatedBy = data.CreatedBy,
                    CreatedOn = DateTime.Now,
                    CustomerID = data.CustomerID,
                    TotalAmount = data.TotalAmount,

                };

                db.Table_RapidiTrainingcharges.Add(abc);
                await db.SaveChangesAsync();

                var result = db.Table_RapidiTrainingcharges.OrderByDescending(c => c.ID).FirstOrDefault();

                Table_Common_RefID_Template ab = new Table_Common_RefID_Template()

                {
                    RefID = data.RefID,
                    TemplateName = data.TemplateName,
                    CreatedBy = data.CreatedBy,
                    CreatedOn = DateTime.Now,
                };

                await Task.Run(() => db.Table_Common_RefID_Template.Add(ab));
                await db.SaveChangesAsync();

                return Ok("success");

            }

            catch (Exception e)
            {

                throw e;
            }
        }


    

        public partial class Rapidtrainingchargesdetails
        {
            public int ID { get; set; }
            public Nullable<int> RefID { get; set; }
            public string CreatedBy { get; set; }
            public Nullable<System.DateTime> CreatedOn { get; set; }
            public Nullable<int> TemplateID { get; set; }
            public string TemplateName { get; set; }
            public Nullable<int> CustomerID { get; set; }
            public string CustomerName { get; set; }
            public string YourEnquiry { get; set; }
            public string KindAttention { get; set; }
            public string BillingAddress { get; set; }
            public string ACSCQty { get; set; }
            public string ACSCPrice { get; set; }
            public Nullable<int> TotalAmount { get; set; }
        }
        public partial class Quotation2015indollartemplatedetails
        {
            public int RefID { get; set; }
            public string YourEnquiry { get; set; }
            public string KindAttention { get; set; }
            public string BillingAddress { get; set; }
            public Nullable<System.DateTime> CreatedOn { get; set; }
            public string CreatedBy { get; set; }
            public string BasicSystemQty { get; set; }
            public string BasicSystemPrice { get; set; }
            public string OptionalQtyA { get; set; }
            public string OptionalPriceA { get; set; }
            public string OptionalQtyB { get; set; }
            public string OptionalPriceB { get; set; }
            public string OptionalQtyC { get; set; }
            public string OptionalPriceC { get; set; }
            public string OptionalQtyD { get; set; }
            public string OptionalPriceD { get; set; }
            public string OptionalQtyE { get; set; }
            public string OptionalPriceE { get; set; }
            public string OptionalQtyF { get; set; }
            public string OptionalPriceF { get; set; }
            public string OptionalQtyG { get; set; }
            public string OptionalPriceG { get; set; }
            public string OptionalQtyH { get; set; }
            public string OptionalPriceH { get; set; }
            public Nullable<int> CustomerID { get; set; }
            public string CustomerName { get; set; }
            public Nullable<int> TemplateID { get; set; }
            public string TemplateName { get; set; }
            public int ID { get; set; }
            public Nullable<int> TotalAmount { get; set; }
        }

        public partial class Rapiditabledetails
        {
            public int ID { get; set; }
            public Nullable<int> RefID { get; set; }
            public string CreatedBy { get; set; }
            public Nullable<System.DateTime> CreatedOn { get; set; }
            public string VisionQty { get; set; }
            public string VisionPrice { get; set; }
            public Nullable<int> TemplateID { get; set; }
            public string TemplateName { get; set; }
            public Nullable<int> CustomerID { get; set; }
            public string CustomerName { get; set; }
            public string YourEnquiry { get; set; }
            public string KindAttention { get; set; }
            public string BillingAddress { get; set; }
            public Nullable<int> TotalAmount { get; set; }
        }
       
        public partial class Quotation4020HTtemplatedetails  
        {
            public int ID { get; set; }
            public Nullable<int> RefID { get; set; }
            public string YourEnquiry { get; set; }
            public string KindAttention { get; set; }
            public string BillingAddress { get; set; }
            public Nullable<System.DateTime> CreatedOn { get; set; }
            public string CreatedBy { get; set; }
            public string BasicSystemQty { get; set; }
            public string BasicSystemPrice { get; set; }
            public string OptionalQtyA { get; set; }
            public string OptionalPriceA { get; set; }
            public string OptionalQtyB { get; set; }
            public string OptionalPriceB { get; set; }
            public string OptionalQtyC { get; set; }
            public string OptionalPriceC { get; set; }
            public string OptionalQtyD { get; set; }
            public string OptionalPriceD { get; set; }
            public string OptionalQtyE { get; set; }
            public string OptionalPriceE { get; set; }
            public string OptionalQtyF { get; set; }
            public string OptionalPriceF { get; set; }
            public string OptionalQtyG { get; set; }
            public string OptionalPriceG { get; set; }
            public string OptionalQtyH { get; set; }
            public string OptionalPriceH { get; set; }
            public Nullable<int> CustomerID { get; set; }
            public string CustomerName { get; set; }
            public Nullable<int> TemplateID { get; set; }
            public string TemplateName { get; set; }
            public Nullable<int> TotalAmount { get; set; }
        }
        public partial class Rapid64CAMdetails
        {
            public int ID { get; set; }
            public Nullable<int> RefID { get; set; }
            public string CreatedBy { get; set; }
            public Nullable<System.DateTime> CreatedOn { get; set; }
            public Nullable<int> TemplateID { get; set; }
            public string TemplateName { get; set; }
            public Nullable<int> CustomerID { get; set; }
            public string CustomerName { get; set; }
            public string YourEnquiry { get; set; }
            public string KindAttention { get; set; }
            public string ACSCQty { get; set; }
            public string ACSCPrice { get; set; }
            public string BillingAddress { get; set; }
            public string DesQty { get; set; }
            public string DesPrice { get; set; }
            public Nullable<int> TotalAmount { get; set; }
        }

        public partial class V4030details
        {
            public int ID { get; set; }
            public Nullable<int> RefID { get; set; }
            public string CreatedBy { get; set; }
            public Nullable<System.DateTime> CreatedOn { get; set; }
            public string BasicQty { get; set; }
            public string BasicPrice { get; set; }
            public string AuxQty { get; set; }
            public string AuxPrice { get; set; }
            public Nullable<int> TemplateID { get; set; }
            public string TemplateName { get; set; }
            public Nullable<int> CustomerID { get; set; }
            public string CustomerName { get; set; }
            public string YourEnquiry { get; set; }
            public string KindAttention { get; set; }
            public string BillingAddress { get; set; }
            public Nullable<int> TotalAmount { get; set; }
        }

        public partial class V4020details
        {
            public int ID { get; set; }
            public Nullable<int> RefID { get; set; }
            public string CreatedBy { get; set; }
            public Nullable<System.DateTime> CreatedOn { get; set; }
            public string DesQty { get; set; }
            public string DesPrice { get; set; }
            public Nullable<int> TemplateID { get; set; }
            public string TemplateName { get; set; }
            public Nullable<int> CustomerID { get; set; }
            public string CustomerName { get; set; }
            public string YourEnquiry { get; set; }
            public string KindAttention { get; set; }
            public string BillingAddress { get; set; }
            public string BasicQty { get; set; }
            public string BasicPrice { get; set; }
            public Nullable<int> TotalAmount { get; set; }
        }

        public partial class V2015details
        {
            public int ID { get; set; }
            public Nullable<int> RefID { get; set; }
            public string CreatedBy { get; set; }
            public Nullable<System.DateTime> CreatedOn { get; set; }
            public string AxesQty { get; set; }
            public string AxesPrice { get; set; }
            public Nullable<int> TemplateID { get; set; }
            public string TemplateName { get; set; }
            public Nullable<int> CustomerID { get; set; }
            public string CustomerName { get; set; }
            public string YourEnquiry { get; set; }
            public string KindAttention { get; set; }
            public string BillingAddress { get; set; }
            public Nullable<int> TotalAmount { get; set; }
        }

        public partial class Rapidi2015JLX
        {
            public int ID { get; set; }
            public Nullable<int> RefID { get; set; }
            public string YourEnquiry { get; set; }
            public string KindAttention { get; set; }
            public string BillingAddress { get; set; }
            public Nullable<System.DateTime> CreatedOn { get; set; }
            public string CreatedBy { get; set; }
            public string BasicSystemQty { get; set; }
            public string BasicSystemPrice { get; set; }
            public string OptionalQtyA { get; set; }
            public string OptionalPriceA { get; set; }
            public string OptionalQtyB { get; set; }
            public string OptionalPriceB { get; set; }
            public string OptionalQtyC { get; set; }
            public string OptionalPriceC { get; set; }
            public string OptionalQtyD { get; set; }
            public string OptionalPriceD { get; set; }
            public string OptionalQtyE { get; set; }
            public string OptionalPriceE { get; set; }
            public string OptionalQtyF { get; set; }
            public string OptionalPriceF { get; set; }
            public string OptionalQtyG { get; set; }
            public string OptionalPriceG { get; set; }
            public string OptionalQtyH { get; set; }
            public string OptionalPriceH { get; set; }
            public Nullable<int> CustomerID { get; set; }
            public string CustomerName { get; set; }
            public Nullable<int> TemplateID { get; set; }
            public string TemplateName { get; set; }
            public Nullable<int> TotalAmount { get; set; }
        }


        public partial class Rapid64details
        {
            public int ID { get; set; }
            public Nullable<int> RefID { get; set; }
            public string CreatedBy { get; set; }
            public Nullable<System.DateTime> CreatedOn { get; set; }
            public string UpgradeQtyA { get; set; }
            public string UpgradePrice { get; set; }
            public string UpgradeQtyB { get; set; }
            public string UpgradeQtyC { get; set; }
            public Nullable<int> CustomerID { get; set; }
            public string CustomerName { get; set; }
            public Nullable<int> TemplateID { get; set; }
            public string TemplateName { get; set; }
            public string YourEnquiry { get; set; }
            public string KindAttention { get; set; }
            public string BillingAddress { get; set; }
            public Nullable<int> TotalAmount { get; set; }
        }

        public partial class Rapidsparesdetails
        {
            public int ID { get; set; }
            public Nullable<int> RefID { get; set; }
            public string CreatedBy { get; set; }
            public Nullable<System.DateTime> CreatedOn { get; set; }
            public string VisionQtyA { get; set; }
            public string VisionPriceA { get; set; }
            public string VisionQtyB { get; set; }
            public string VisionPriceB { get; set; }
            public Nullable<int> TemplateID { get; set; }
            public string TemplateName { get; set; }
            public Nullable<int> CustomerID { get; set; }
            public string CustomerName { get; set; }
            public string YourEnquiry { get; set; }
            public string KindAttention { get; set; }
            public string BillingAddress { get; set; }
            public Nullable<int> TotalAmount { get; set; }
        }


        public partial class Quotationtemplatedetails
        {
            public int RefID { get; set; }
            public string YourEnquiry { get; set; }
            public string KindAttention { get; set; }
            public string BillingAddress { get; set; }
            public Nullable<System.DateTime> CreatedOn { get; set; }
            public string CreatedBy { get; set; }
            public string BasicSystemQty { get; set; }
            public string BasicSystemPrice { get; set; }
            public string OptionalQtyA { get; set; }
            public string OptionalPriceA { get; set; }
            public string OptionalQtyB { get; set; }
            public string OptionalPriceB { get; set; }
            public string OptionalQtyC { get; set; }
            public string OptionalPriceC { get; set; }
            public string OptionalQtyD { get; set; }
            public string OptionalPriceD { get; set; }
            public string OptionalQtyE { get; set; }
            public string OptionalPriceE { get; set; }
            public string OptionalQtyF { get; set; }
            public string OptionalPriceF { get; set; }
            public string OptionalQtyG { get; set; }
            public string OptionalPriceG { get; set; }
            public string OptionalQtyH { get; set; }
            public string OptionalPriceH { get; set; }
            public Nullable<int> CustomerID { get; set; }
            public string CustomerName { get; set; }
            public Nullable<int> TemplateID { get; set; }
            public string TemplateName { get; set; }
            public int ID { get; set; }
            public int? TotalAmount { get; set; }
        }

        public partial class RapidIVMCdetails
        {
            public int ID { get; set; }
            public Nullable<int> RefID { get; set; }
            public string YourEnquiry { get; set; }
            public string KindAttention { get; set; }
            public string BillingAddress { get; set; }
            public Nullable<System.DateTime> CreatedOn { get; set; }
            public string CreatedBy { get; set; }
            public string BasicQty { get; set; }
            public string BasicPrice { get; set; }
            public string OptionalQtyA { get; set; }
            public string OptionalPriceA { get; set; }
            public string OptionalQtyB { get; set; }
            public string OptionalPriceB { get; set; }
            public string OptionalQtyC { get; set; }
            public string OptionalPriceC { get; set; }
            public string OptionalQtyD { get; set; }
            public string OptionalPriceD { get; set; }
            public string OptionalQtyE { get; set; }
            public string OptionalPriceE { get; set; }
            public string OptionalQtyF { get; set; }
            public string OptionalPriceF { get; set; }
            public string OptionalQtyG { get; set; }
            public string OptionalPriceG { get; set; }
            public string OptionalQtyH { get; set; }
            public string OptionalPriceH { get; set; }
            public Nullable<int> CustomerID { get; set; }
            public string CustomerName { get; set; }
            public Nullable<int> TemplateID { get; set; }
            public string TemplateName { get; set; }
            public Nullable<int> TotalAmount { get; set; }
            public string BasicQtyA { get; set; }
            public string BasicPriceA { get; set; }
            public string BasicQtyB { get; set; }
            public string BasicPriceB { get; set; }
            public string BasicQtyC { get; set; }
            public string BasicPriceC { get; set; }
        }

    }
}
