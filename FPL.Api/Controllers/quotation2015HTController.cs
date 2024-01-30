//using FPL.Dal.DataModel;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net;
//using System.Net.Http;
//using System.Threading.Tasks;
//using System.Web.Http;

//namespace FPL.Api.Controllers
//{
//    public class quotation2015HTController : ApiController
//    {
//        private CustomerManagerEntities db = new CustomerManagerEntities();
//        [HttpPost]
//        public async Task<IHttpActionResult> Savequotationtemplate(Quotationtemplatedetails data)
//        {
//            try
//            {

//                Table_quotation2015HT abc = new Table_quotation2015HT()
//                {
//                    RefID = data.RefID,

//                    BillingAddress = data.BillingAddress,

//                    BasicSystemQty = data.BasicSystemQty,
//                    BasicSystemPrice = data.BasicSystemPrice,

//                    OptionalQtyA = data.OptionalQtyA,
//                    OptionalPriceA = data.OptionalPriceA,

//                    OptionalQtyB = data.OptionalQtyB,
//                    OptionalPriceB = data.OptionalPriceB,

//                    OptionalQtyC = data.OptionalQtyC,
//                    OptionalPriceC = data.OptionalPriceC,

//                    OptionalQtyD = data.OptionalQtyD,
//                    OptionalPriceD = data.OptionalPriceD,


//                    TemplateID = (int)data.TemplateID,
//                    TemplateName = data.TemplateName,
//                    CustomerName = data.CustomerName,
//                    CreatedBy = data.CreatedBy,
//                    CreatedOn = DateTime.Now,
//                    CustomerID = data.CustomerID,


//                };

//                await Task.Run(() => db.Table_quotation2015HT.Add(abc));
//                await db.SaveChangesAsync();

//                return Ok("success");
//            }
//            catch (Exception e)
//            {

//                throw e;
//            }
//        }

//    }
//}
//public partial class Quotationtemplatedetails
//{
//    public int ID { get; set; }
//    public int RefID { get; set; }
//    public string YourEnquiry { get; set; }
//    public string BillingAddress { get; set; }
//    public string Price { get; set; }
//    public string Quantity { get; set; }
//    public string BasicSystemQty { get; set; }
//    public string BasicSystemPrice { get; set; }
//    public string OptionalQtyA { get; set; }
//    public string OptionalPriceA { get; set; }
//    public string OptionalQtyB { get; set; }
//    public string OptionalPriceB { get; set; }
//    public string OptionalQtyC { get; set; }
//    public string OptionalPriceC { get; set; }
//    public string OptionalQtyD { get; set; }
//    public string OptionalPriceD { get; set; }
//    public string CustomerID { get; set; }
//    public string CustomerName { get; set; }
//    public int TemplateID { get; set; }
//    public string TemplateName { get; set; }
//    public string CreatedOn { get; set; }
//    public string CreatedBy { get; set; }

//}

