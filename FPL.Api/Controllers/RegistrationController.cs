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
    public class RegistrationController : ApiController
    {
        private CustomerManagerEntities db = new CustomerManagerEntities();



        public async Task<IHttpActionResult> PostSaveUserRegistration(Table_UserRegistrationModel data1)
        {


            var username = db.Table_UserRegistration.Where(c => c.UserName == data1.UserName).Select(c => c.UserName).FirstOrDefault();
            if (username == null)
            {
                try
                {
                    var roleName = db.Table_RoleMaster.Where(c => c.Id == data1.RoleId).Select(c => c.RoleName).FirstOrDefault();
                    Table_UserRegistration data = new Table_UserRegistration()
                    {
                        Id = 0,
                        RoleName = roleName,
                        CreatedOn = DateTime.Now,
                        ConfirmPassword = data1.ConfirmPassword,
                        Email = data1.Email,
                        Password = data1.Password,
                        RoleId = data1.RoleId,
                        UserName = data1.UserName
                    };

                    await Task.Run(() => db.Table_UserRegistration.Add(data));
                    await db.SaveChangesAsync();

                    return Ok("success");
                }
                catch (Exception e)
                {

                    throw e;
                }

            }
            else
            {
                return Ok("fail");
            }

        }



        [HttpPost]
        public async Task<IHttpActionResult> PostSaveUpdateUserRegistration(Table_UserRegistrationModel data1)
        {

            int userID = Convert.ToInt32(data1.Id);
            int roleID = Convert.ToInt32(data1.RoleId);

            var rolename = db.Table_RoleMaster.Where(c => c.Id == roleID).Select(c => c.RoleName).FirstOrDefault();


            try
            {
                var dat = await Task.Run(() => db.Table_UserRegistration.Where(c => c.Id == userID).FirstOrDefault());
                if (dat != null)
                {
                    dat.UserName = data1.UserName;
                    dat.Email = data1.Email;
                    dat.RoleName = data1.RoleName;
                    dat.RoleId = roleID;


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


        [HttpGet]
        public async Task<IHttpActionResult> GetAllUsers()
        {
            var result = await Task.Run(() => db.Table_UserRegistration.ToList());
            return Ok(result);
        }

        [HttpGet]
        public async Task<IHttpActionResult> DeleteUserData([FromUri(Name = "id")] int id)
        {
            var data = await Task.Run(() => db.Table_UserRegistration.FindAsync(id));

            await Task.Run(() => db.Table_UserRegistration.Remove(data));
            await db.SaveChangesAsync();

            return Ok("success");
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetMachineId()
        {
            try
            {
                var machineID = db.Table_RequestsFormData.ToList();
                return Ok(machineID);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }



    public partial class Table_UserRegistrationModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
    }
}
