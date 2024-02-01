using FPL.Dal.DataModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace FPL.Api.Controllers
{
    //[RoutePrefix("api/quotation2015")]
    public class quotation2015Controller : ApiController
    {
        private CustomerManagerEntities db = new CustomerManagerEntities();

        [HttpGet]
        public IHttpActionResult GetAllRefNo()
        {
            try
            {
                var refID = db.Table_Common_RefID_Template.ToList();
                return Ok(refID);
            }
            catch (Exception e)
            {
                return InternalServerError(e);//gggasassda
             
            }
        }

        [HttpGet]
        public IHttpActionResult GetBillingAddress([FromUri(Name = "id")] int id)
        {
            try
            {
                var billingAddress = db.Table_CustomerRegistartion.Where(c => c.CustomerID == id).ToList();
                return Ok(billingAddress);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }
        [HttpGet]
        public async Task<IHttpActionResult> gettemplatedetails2015([FromUri(Name = "id")] int id)
        {
            try
            {
                var result = db.Table_quotation2015.Where(c => c.RefID == id).FirstOrDefault();

                return Ok(result);
            }
            catch (Exception e)
            {

                throw e;
            }

        }
        [HttpGet]
        public async Task<IHttpActionResult> gettemplatedetails2015HT([FromUri(Name = "id")] int id)
        {
            try
            {
                var result = db.Table_quotation2015HT.Where(c => c.RefID == id).FirstOrDefault();
                return Ok(result);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpGet]
        public async Task<IHttpActionResult> Savequotationtemplate2015Z25([FromUri(Name = "id")] int id)
        {
            try
            {
                var result = db.Table_quotation2015Z25.Where(c => c.RefID == id).FirstOrDefault();

                return Ok(result);
            }
            catch (Exception e)
            {

                throw e;
            }

        }
        [HttpGet]
        public async Task<IHttpActionResult> Savequotationtemplate4020INDOLLOR([FromUri(Name = "id")] int id)
        {
            try
            {
                var result = db.Table_QUOTATION40202inDollor.Where(c => c.RefID == id).FirstOrDefault();

                return Ok(result);
            }
            catch (Exception e)
            {

                throw e;
            }

        }
        [HttpGet]
        public async Task<IHttpActionResult> Savequotationtemplate4030([FromUri(Name = "id")] int id)
        {
            try
            {
                var result = db.Table_Quotation4030.Where(c => c.RefID == id).FirstOrDefault();

                return Ok(result);
            }
            catch (Exception e)
            {

                throw e;
            }

        }

        [HttpGet]
        public async Task<IHttpActionResult> Savequotationtemplate4030INDOLLOR([FromUri(Name = "id")] int id)
        {
            try
            {
                var result = db.Table_4030InDollor.Where(c => c.RefID == id).FirstOrDefault();

                return Ok(result);
            }
            catch (Exception e)
            {

                throw e;
            }

        }
        [HttpGet]
        public async Task<IHttpActionResult> Savequotationtemplate4020z25([FromUri(Name = "id")] int id)
        {
            try
            {
                var result = db.Table_4020Z25.Where(c => c.RefID == id).FirstOrDefault();

                return Ok(result);
            }
            catch (Exception e)
            {

                throw e;
            }

        }

        [HttpGet]
        public async Task<IHttpActionResult> Savequotationtemplate4030Z25([FromUri(Name = "id")] int id)
        {
            try
            {
                var result = db.Table_Quotation4030z25.Where(c => c.RefID == id).FirstOrDefault();

                return Ok(result);
            }
            catch (Exception e)
            {

                throw e;
            }

        }
        [HttpGet]
        public async Task<IHttpActionResult> Savequotationtemplate5030([FromUri(Name = "id")] int id)
        {
            try
            {
                var result = db.Table_Quotation5030.Where(c => c.RefID == id).FirstOrDefault();

                return Ok(result);
            }
            catch (Exception e)
            {

                throw e;
            }

        }
        [HttpGet]
        public async Task<IHttpActionResult> Savequotationtemplate5030Z25ZLX([FromUri(Name = "id")] int id)
        {
            try
            {
                var result = db.Table_Quotation5030Z25ZLX_.Where(c => c.RefID == id).FirstOrDefault();

                return Ok(result);
            }
            catch (Exception e)
            {

                throw e;
            }

        }


        [HttpPost]
        public async Task<IHttpActionResult> Savequotationtemplate2015(QuotationTemplateDetails data)
        {
            try
            {
                Table_quotation2015 abc = new Table_quotation2015()
                {
                    RefID = data.RefID,
                    BillingAddress = data.BillingAddress,
                    YourEnquiry = data.YourEnquiry,
                    KindAttention = data.KindAttention,
                    Price = data.Price,
                    Quantity = data.Quantity,
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
                    CustomerID = data.CustomerID,
                    CustomerName = data.CustomerName,
                    TemplateID = data.TemplateID,
                    TemplateName = data.TemplateName,
                    CreatedOn = DateTime.Now,
                    CreatedBy = data.CreatedBy,
                    TotalAmount=data.TotalAmount,
                };

                db.Table_quotation2015.Add(abc);
                await db.SaveChangesAsync();

                var result = db.Table_quotation2015.OrderByDescending(c => c.ID).FirstOrDefault();

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
                return InternalServerError(e);
            }
        }

      

        [HttpPost]
        public async Task<IHttpActionResult>
            SavequotationtemplateRapidI5APRIL2015(QuotationTemplateRapidI5APRIL2015Details data)
        {
            try
            {
                Table_Rapid_I_4020_4030J_LX_ACSC_05_Apr_2021 abc = new Table_Rapid_I_4020_4030J_LX_ACSC_05_Apr_2021()
                {
                    RefID = data.RefID,
                    BillingAddress = data.BillingAddress,
                    YourEnquiry = data.YourEnquiry,
                    KindAttention = data.KindAttention,
            
                    CustomerID = data.CustomerID,
                    CustomerName = data.CustomerName,
                    TemplateID = data.TemplateID,
                    TemplateName = data.TemplateName,
                    CreatedOn = DateTime.Now,
                    CreatedBy = data.CreatedBy,

                };
                db.Table_Rapid_I_4020_4030J_LX_ACSC_05_Apr_2021.Add(abc);
                await db.SaveChangesAsync();

                var result = db.Table_Rapid_I_4020_4030J_LX_ACSC_05_Apr_2021.OrderByDescending(c => c.ID).FirstOrDefault();
           
               

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
                return InternalServerError(e);
            }
        }

        
        [HttpPost]
        public async Task<IHttpActionResult> SavequotationtemplateRapidIAMC(QuotationTemplateRapidIAMCDetails data)
        {
            try
            {
                Table_Rapid_I_AMC_OFFER abc = new Table_Rapid_I_AMC_OFFER()
                {
                    RefID = data.RefID,
                    BillingAddress = data.BillingAddress,
                    YourEnquiry = data.YourEnquiry,
                    KindAttention = data.KindAttention,
                    CustomerID = data.CustomerID,
                    CustomerName = data.CustomerName,
                    TemplateID = data.TemplateID,
                    TemplateName = data.TemplateName,
                    CreatedOn = DateTime.Now,
                    CreatedBy = data.CreatedBy,
                };

                
                db.Table_Rapid_I_AMC_OFFER.Add(abc);
                await db.SaveChangesAsync();

                var result = db.Table_Rapid_I_AMC_OFFER
                    .OrderByDescending(c => c.ID)
                    .FirstOrDefault();
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
                return InternalServerError(e);
            }
        }
       

        [HttpPost]
        public async Task<IHttpActionResult> Savequotationtemplate4030Z25(QuotationTemplateDetails4030Z25 data)
        {
            try
            {
                Table_Quotation4030z25 abc = new Table_Quotation4030z25()
                {
                    RefID = data.RefID,
                    BillingAddress = data.BillingAddress,
                    YourEnquiry = data.YourEnquiry,
                    KindAttention = data.KindAttention,
                    //Price = data.Price,
                    //Quantity = data.Quantity,
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
                    CustomerID = data.CustomerID,
                    CustomerName = data.CustomerName,
                    TemplateID = data.TemplateID,
                    TemplateName = data.TemplateName,
                    CreatedOn = DateTime.Now,
                    CreatedBy = data.CreatedBy,
                    TotalAmount = data.TotalAmount,


                };


                db.Table_Quotation4030z25.Add(abc);
                await db.SaveChangesAsync();

                var result = db.Table_Quotation4030z25.OrderByDescending(c => c.ID).FirstOrDefault();

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
                return InternalServerError(e);
            }
        }



        [HttpPost]
        public async Task<IHttpActionResult> Savequotationtemplate2015HT(QuotationTemplateDetails2015HT data)
        {
            try
            {
                Table_quotation2015HT abc = new Table_quotation2015HT()
                {
                    RefID = data.RefID,
                    BillingAddress = data.BillingAddress,
                    YourEnquiry = data.YourEnquiry,

                    Price = data.Price,
                    Quantity = data.Quantity,
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

                    CustomerID = data.CustomerID,
                    CustomerName = data.CustomerName,
                    TemplateID = data.TemplateID,
                    TemplateName = data.TemplateName,
                    CreatedOn = DateTime.Now,
                    CreatedBy = data.CreatedBy,
                    TotalAmount = data.TotalAmount,
                };
                db.Table_quotation2015HT.Add(abc);
                await db.SaveChangesAsync();
            
                await db.SaveChangesAsync();
                var result = db.Table_quotation2015
                                .OrderByDescending(c => c.ID)
                                .FirstOrDefault();
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
                return InternalServerError(e);
            }
        }
        [HttpPost]
        public async Task<IHttpActionResult> Savequotationtemplate4020INDOLLOR(QuotationTemplateDetails4020INDOLLOR data)
        {
            try
            {
                Table_QUOTATION40202inDollor abc = new Table_QUOTATION40202inDollor()
                {
                    RefID = data.RefID,
                    BillingAddress = data.BillingAddress,
                    YourEnquiry = data.YourEnquiry,
                    KindAttention = data.KindAttention,
                    Price = data.Price,
                    Quantity = data.Quantity,
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
                    CustomerID = data.CustomerID,
                    CustomerName = data.CustomerName,
                    TemplateID = data.TemplateID,
                    TemplateName = data.TemplateName,
                    CreatedOn = DateTime.Now,
                    CreatedBy = data.CreatedBy,
                    TotalAmount = data.TotalAmount,
                };


                db.Table_QUOTATION40202inDollor.Add(abc);
                await db.SaveChangesAsync();

                await db.SaveChangesAsync();
                var result = db.Table_QUOTATION40202inDollor
                                .OrderByDescending(c => c.ID)
                                .FirstOrDefault();
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
                return InternalServerError(e);
            }
        }

        [HttpPost]
        public async Task<IHttpActionResult> Savequotationtemplate4030(QuotationTemplateDetails4030 data)
        {
            try
            {
                Table_Quotation4030 abc = new Table_Quotation4030()
                {
                    RefID = data.RefID,
                    BillingAddress = data.BillingAddress,
                    YourEnquiry = data.YourEnquiry,
                    KindAttention = data.KindAttention,
                    Price = data.Price,
                    Quantity = data.Quantity,
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
                    CustomerID = data.CustomerID,
                    CustomerName = data.CustomerName,
                    TemplateID = data.TemplateID,
                    TemplateName = data.TemplateName,
                    CreatedOn = DateTime.Now,
                    CreatedBy = data.CreatedBy,
                    TotalAmount = data.TotalAmount,
                };

                db.Table_Quotation4030.Add(abc);
                await db.SaveChangesAsync();

                await db.SaveChangesAsync();
                var result = db.Table_Quotation4030
                                .OrderByDescending(c => c.ID)
                                .FirstOrDefault();
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
                return InternalServerError(e);
            }
        }
        [HttpPost]
        public async Task<IHttpActionResult> Savequotationtemplate4020Z25(QuotationTemplate4020z25Details data)
        {
            try
            {
                Table_4020Z25 abc = new Table_4020Z25()
                {
                    RefID = data.RefID,
                    BillingAddress = data.BillingAddress,
                    YourEnquiry = data.YourEnquiry,
                    KindAttention = data.KindAttention,
                    Price = data.Price,
                    Quantity = data.Quantity,
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
                    CustomerID = data.CustomerID,
                    CustomerName = data.CustomerName,
                    TemplateID = data.TemplateID,
                    TemplateName = data.TemplateName,
                    CreatedOn = DateTime.Now,
                    CreatedBy = data.CreatedBy,
                    TotalAmount = data.TotalAmount,

                };

                db.Table_4020Z25.Add(abc);
                await db.SaveChangesAsync();

                await db.SaveChangesAsync();
                var result = db.Table_4020Z25
                                .OrderByDescending(c => c.ID)
                                .FirstOrDefault();
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
                return InternalServerError(e);
            }
        }

        [HttpPost]

        public async Task<IHttpActionResult> Savequotationtemplate5030Z25ZLX(QuotationTemplateDetails5030Z25ZLX data)
        {
            try
            {
                Table_Quotation5030Z25ZLX_ abc = new Table_Quotation5030Z25ZLX_()
                {
                    RefID = data.RefID,
                    BillingAddress = data.BillingAddress,
                    YourEnquiry = data.YourEnquiry,
                    KindAttention = data.KindAttention,
                    Price = data.Price,
                    Quantity = data.Quantity,
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
                    CustomerID = data.CustomerID,
                    CustomerName = data.CustomerName,
                    TemplateID = data.TemplateID,
                    TemplateName = data.TemplateName,
                    CreatedOn = DateTime.Now,
                    CreatedBy = data.CreatedBy,
                    TotalAmount = data.TotalAmount,
                };

                db.Table_Quotation5030Z25ZLX_.Add(abc);
                await db.SaveChangesAsync();

                await db.SaveChangesAsync();
                var result = db.Table_Quotation5030Z25ZLX_
                                .OrderByDescending(c => c.ID)
                                .FirstOrDefault();
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
                return InternalServerError(e);
            }
        }


        [HttpPost]
       //[Route("Savequotationtemplate4030INDOLLOR")]
        public async Task<IHttpActionResult> Savequotationtemplate4030INDOLLOR(QuotationTemplateDetails4030INDOLLOR data)
        {
            try
            {
                Table_4030InDollor abc = new Table_4030InDollor()
                {
                    RefID = data.RefID,
                    BillingAddress = data.BillingAddress,
                    YourEnquiry = data.YourEnquiry,
                    KindAttention = data.KindAttention,
                    Price = data.Price,
                    Quantity = data.Quantity,
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
                    CustomerID = data.CustomerID,
                    CustomerName = data.CustomerName,
                    TemplateID = data.TemplateID,
                    TemplateName = data.TemplateName,
                    CreatedOn = DateTime.Now,
                    CreatedBy = data.CreatedBy,
                    TotalAmount = data.TotalAmount,
                };
                db.Table_4030InDollor.Add(abc);
                await db.SaveChangesAsync();

                await db.SaveChangesAsync();
                var result = db.Table_4030InDollor
                                .OrderByDescending(c => c.ID)
                                .FirstOrDefault();
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
                return InternalServerError(e);
            }
        }


        [HttpPost]

        public async Task<IHttpActionResult> Savequotationtemplate5030(QuotationTemplateDetails5030 data)
        {
            try
            {
                Table_Quotation5030 abc = new Table_Quotation5030()
                {
                    RefID = data.RefID,
                    BillingAddress = data.BillingAddress,
                    YourEnquiry = data.YourEnquiry,
                    KindAttention = data.KindAttention,
                    Price = data.Price,
                    Quantity = data.Quantity,
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
                    CustomerID = data.CustomerID,
                    CustomerName = data.CustomerName,
                    TemplateID = data.TemplateID,
                    TemplateName = data.TemplateName,
                    CreatedOn = DateTime.Now,
                    CreatedBy = data.CreatedBy,
                    TotalAmount = data.TotalAmount,
                };

                db.Table_Quotation5030.Add(abc);
                await db.SaveChangesAsync();

                await db.SaveChangesAsync();
                var result = db.Table_Quotation5030
                                .OrderByDescending(c => c.ID)
                                .FirstOrDefault();
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
                return InternalServerError(e);
            }
        }


        [HttpPost]

        public async Task<IHttpActionResult> Savequotationtemplate2015Z25(QuotationTemplate2015z25Details data)
        {
            try
            {
                Table_quotation2015Z25 abc = new Table_quotation2015Z25()
                {
                    RefID = data.RefID,
                    BillingAddress = data.BillingAddress,
                    YourEnquiry = data.YourEnquiry,
                    KindAttention = data.KindAttention,
                    Price = data.Price,
                    Quantity = data.Quantity,
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
                    CustomerID = data.CustomerID,
                    CustomerName = data.CustomerName,
                    TemplateID = data.TemplateID,
                    TemplateName = data.TemplateName,
                    CreatedOn = DateTime.Now,
                    CreatedBy = data.CreatedBy,
                    TotalAmount = data.TotalAmount,
                };

                db.Table_quotation2015Z25.Add(abc);
                await db.SaveChangesAsync();

                await db.SaveChangesAsync();
                var result = db.Table_quotation2015Z25
                                .OrderByDescending(c => c.ID)
                                .FirstOrDefault();
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
                return InternalServerError(e);
            }
        }


        public partial class QuotationTemplatecommonDetails
        {
            public int ID { get; set; } 
            public Nullable<int> RefID { get; set; }
            public string TemplateName { get; set; }
            public string CreatedBy { get; set; }
            public Nullable<System.DateTime> CreatedOn { get; set; }
        }
        public partial class QuotationTemplateRapidIAMCDetails
        {
            public int ID { get; set; }
            public Nullable<int> RefID { get; set; }
            public string YourEnquiry { get; set; }
            public string KindAttention { get; set; }
            public string BillingAddress { get; set; }
            public int CustomerID { get; set; }
            public string CustomerName { get; set; }
            public Nullable<int> TemplateID { get; set; }
            public string TemplateName { get; set; }
            public Nullable<System.DateTime> CreatedOn { get; set; }
            public string CreatedBy { get; set; }
            public Nullable<int> TotalAmount { get; set; }
        }
        public partial class QuotationTemplate4020z25Details
        {
            public int ID { get; set; }
            public Nullable<int> RefID { get; set; }
            public string YourEnquiry { get; set; }
            public string KindAttention { get; set; }
            public string BillingAddress { get; set; }
            public string Price { get; set; }
            public string Quantity { get; set; }
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
            public Nullable<System.DateTime> CreatedOn { get; set; }
            public string CreatedBy { get; set; }
            public string TemplateName { get; set; }
            public Nullable<int> TotalAmount { get; set; }
        }
        public partial class QuotationTemplate2015z25Details
        {
            public int ID { get; set; }
            public Nullable<int> RefID { get; set; }
            public string YourEnquiry { get; set; }
            public string BillingAddress { get; set; }
            public string Price { get; set; }
            public string Quantity { get; set; }
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
            public string OptionalPriceE { get; set; }
            public string OptionalQtyF { get; set; }
            public string OptionalPriceF { get; set; }
            public string OptionalQtyG { get; set; }
            public string OptionalPriceG { get; set; }
            public string OptionalQtyH { get; set; }
            public string OptionalPriceH { get; set; }
            public int CustomerID { get; set; }
            public string CustomerName { get; set; }
            public Nullable<int> TemplateID { get; set; }
            public string TemplateName { get; set; }
            public Nullable<System.DateTime> CreatedOn { get; set; }
            public string CreatedBy { get; set; }
            public string KindAttention { get; set; }
            public string OptionalQtyE { get; set; }
            public Nullable<int> TotalAmount { get; set; }
        }
        public partial class QuotationTemplateRapidI5APRIL2015Details
        {
            public int ID { get; set; }
            public Nullable<int> RefID { get; set; }
            public string YourEnquiry { get; set; }
            public string KindAttention { get; set; }
            public string BillingAddress { get; set; }
            public Nullable<int> CustomerID { get; set; }
            public string CustomerName { get; set; }
            public Nullable<int> TemplateID { get; set; }
            public string TemplateName { get; set; }
            public Nullable<System.DateTime> CreatedOn { get; set; }
            public string CreatedBy { get; set; }
            public Nullable<int> TotalAmount { get; set; }
        }
        public partial class QuotationTemplateDetails4030Z25
        {
            public int ID { get; set; }
            public Nullable<int> RefID { get; set; }
            public string YourEnquiry { get; set; }
            public string KindAttention { get; set; }
            public string BillingAddress { get; set; }
            public string Price { get; set; }
            public string Quantity { get; set; }
            public string BasicSystemQty { get; set; }
            public string BasicSystemPrice { get; set; }
            public string OptionalQtyA { get; set; }
            public string OptionalPriceA { get; set; }
            public string OptionalQtyB { get; set; }
            public string OptionalPriceB { get; set; }
            public string OptionalQtyC { get; set; }
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
            public Nullable<System.DateTime> CreatedOn { get; set; }
            public string CreatedBy { get; set; }
            public string OptionalPriceC { get; set; }
            public string TemplateName { get; set; }
            public Nullable<int> TotalAmount { get; set; }
        }
        public class QuotationTemplateDetails
        {
            public int RefID { get; set; }
            public string YourEnquiry { get; set; }
            public string KindAttention { get; set; }
            public string BillingAddress { get; set; }
            public string Price { get; set; }
            public string Quantity { get; set; }
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
            public string CustomerID { get; set; }
            public string CustomerName { get; set; }
            public Nullable<int> TemplateID { get; set; }
            public string TemplateName { get; set; }
            public int ID { get; set; }
            public Nullable<System.DateTime> CreatedOn { get; set; }
            public string CreatedBy { get; set; }
            public Nullable<int> TotalAmount { get; set; }
        }

        public partial class QuotationTemplateDetails2015HT

        {
            public int ID { get; set; }
            public Nullable<int> RefID { get; set; }
            public string YourEnquiry { get; set; }
            public string BillingAddress { get; set; }
            public string Price { get; set; }
            public string Quantity { get; set; }
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
            public string CustomerID { get; set; }
            public string CustomerName { get; set; }
            public int TemplateID { get; set; }
            public string TemplateName { get; set; }
            public Nullable<System.DateTime> CreatedOn { get; set; }
            public string CreatedBy { get; set; }
            public Nullable<int> TotalAmount { get; set; }
        }
        public partial class QuotationTemplateDetails4030INDOLLOR
        {
            public int ID { get; set; }
            public Nullable<int> RefID { get; set; }
            public string YourEnquiry { get; set; }
            public string KindAttention { get; set; }
            public string BillingAddress { get; set; }
            public string Price { get; set; }
            public string Quantity { get; set; }
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
            public int CustomerID { get; set; }
            public string CustomerName { get; set; }
            public Nullable<int> TemplateID { get; set; }
            public Nullable<System.DateTime> CreatedOn { get; set; }
            public string CreatedBy { get; set; }
            public string TemplateName { get; set; }
            public Nullable<int> TotalAmount { get; set; }
        }
        public partial class QuotationTemplateDetails4020INDOLLOR
        {
            public int ID { get; set; }
            public Nullable<int> RefID { get; set; }                                                         
            public string YourEnquiry { get; set; }
            public string BillingAddress { get; set; }
            public string Price { get; set; }
            public string Quantity { get; set; }
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
            public string OptionalPriceE { get; set; }
            public string OptionalQtyF { get; set; }
            public string OptionalPriceF { get; set; }
            public string OptionalQtyG { get; set; }
            public string OptionalPriceG { get; set; }
            public string OptionalQtyH { get; set; }
            public string OptionalPriceH { get; set; }
            public Nullable<int> CustomerID { get; set; }
            public string CustomerName { get; set; }
            public int TemplateID { get; set; }
            public string TemplateName { get; set; }
            public Nullable<System.DateTime> CreatedOn { get; set; }
            public string CreatedBy { get; set; }
            public string OptionalQtyE { get; set; }
            public string KindAttention { get; set; }
            public Nullable<int> TotalAmount { get; set; }
        }
            public partial class QuotationTemplateDetails4030
        {
            public int ID { get; set; }
            public Nullable <int> RefID { get; set; }
            public string YourEnquiry { get; set; }
            public string KindAttention { get; set; }
            public string BillingAddress { get; set; }
            public string Price { get; set; }
            public string Quantity { get; set; }
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
            public System.DateTime CreatedOn { get; set; }
            public string CreatedBy { get; set; }
            public string TemplateName { get; set; }
            public Nullable<int> TotalAmount { get; set; }
        }
        public partial class QuotationTemplateDetails5030Z25ZLX
        {
            public int ID { get; set; }
            public Nullable<int> RefID { get; set; }
            public string YourEnquiry { get; set; }
            public string KindAttention { get; set; }
            public string BillingAddress { get; set; }
            public string Price { get; set; }
            public string Quantity { get; set; }
            public string BasicSystemQty { get; set; }
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
            public Nullable<System.DateTime> CreatedOn { get; set; }
            public string CreatedBy { get; set; }
            public string BasicSystemPrice { get; set; }

            public Nullable<int> TotalAmount { get; set; }
        }
        public partial class QuotationTemplateDetails5030
        {
            public int ID { get; set; }
            public Nullable<int> RefID { get; set; }
            public string YourEnquiry { get; set; }
            public string KindAttention { get; set; }
            public string BillingAddress { get; set; }
            public string Price { get; set; }
            public string Quantity { get; set; }
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
            public string OptionalQtyH { get; set; }
            public string OptionalPriceH { get; set; }
            public Nullable<int> CustomerID { get; set; }
            public Nullable<int> TemplateID { get; set; }
            public string TemplateName { get; set; }
            public Nullable<System.DateTime> CreatedOn { get; set; }
            public string CreatedBy { get; set; }
            public string CustomerName { get; set; }
            public string OptionalPriceG { get; set; }
            public string BasicSystemQty { get; set; }
            public Nullable<int> TotalAmount { get; set; }
        }



    }
}