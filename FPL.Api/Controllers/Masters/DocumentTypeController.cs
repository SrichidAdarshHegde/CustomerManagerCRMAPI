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
    public class DocumentTypeController : ApiController
    {
        private CustomerManagerEntities db = new CustomerManagerEntities();
        private int documentID;

        [HttpGet]
        public async Task<IHttpActionResult> GetDocumentType()
        {
            var result = await Task.Run(() => db.Table_DocumentType.ToList());


            return Ok(result);
        }
        [HttpGet]
        public async Task<IHttpActionResult> GetPerticularDocumentType([FromUri(Name = "id")] int id)
        {
            var result = await Task.Run(() => db.Table_DocumentTemplate.Where(c => c.DocumentID == id).ToList());
            return Ok(result);
        }

        public async Task<IHttpActionResult> PostsaveDocumentType(DocumentTypeDetails data1)
        {
          
            try
            {
                Table_DocumentType data = new Table_DocumentType()
                {
                    DocumentID = 0,
                    DocumentName = data1.DocumentName,
                    CreatedOn = DateTime.Now,
                    CreatedBy= data1.CreatedBy

                };

                await Task.Run(() => db.Table_DocumentType.Add(data));
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
        public async Task<IHttpActionResult> PostSaveUpdateDocumentType(DocumentTypeDetails data1)
        {

            try
            {
                int documentID = Convert.ToInt32(data1.DocumentID);
                var data = db.Table_DocumentType.Where(c => c.DocumentID == documentID).FirstOrDefault();
                data.DocumentName = data1.DocumentName;



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
        public async Task<IHttpActionResult> deleteDocumentTypeData([FromUri(Name = "id")] int id)
        {
            var data = await Task.Run(() => db.Table_DocumentType.FindAsync(id));

            await Task.Run(() => db.Table_DocumentType.Remove(data));
            await db.SaveChangesAsync();

            return Ok("success");
        }
        public partial class DocumentTypeDetails
        {
            public int DocumentID { get; set; }
            public string DocumentName { get; set; }
         
            public Nullable<System.DateTime> CreatedOn { get; set; }

            public string CreatedBy { get; set; }
        }
    }
}