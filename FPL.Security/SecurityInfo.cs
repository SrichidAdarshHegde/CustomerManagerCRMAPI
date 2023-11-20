//using System;
//using System.Data.Entity.Infrastructure;
//using System.Linq;
//using System.Net.Http;
//using System.Net.Http.Headers;
//using System.Security.Cryptography;
//using System.Text;
//using Application.Bal.TransactionIdGenerator;
//using Application.Dal.DataModel;
//using Application.Security.PasswordHashing;
//using FPL.Dal.DataModel;

//namespace Application.Security
//{
//    public class SecurityInfo
//    {
//        public SecurityInfo()
//        {
//        }
//        public bool IsSucceed { get; set; }
//        internal SecurityInfo CreateUserAsync(RegisterUserModel model, factosprivatelimitedEntities db, SecurityInfo info)
//        {
//            var uid = "";
//            var oldid = db.UserRegistrations.OrderByDescending(c => c.UserId).Select(c => c.UserId).FirstOrDefault();
//            if (oldid != null)
//            {
//                uid = TransactionIdGenerator.GenerateId(oldid, "CGD");

//            }
//            else
//            {
//                uid = TransactionIdGenerator.GenerateId(null, "CGD");

//            }

//            var f = HashGenerator.Get16BitHash2(uid);
//            UserRegistration user = new UserRegistration
//            {
//                //LastName = model.LastName,
//                //FirstName = model.FirstName,
//                //ConfirmPassword = model.ConfirmPassword,
//                EmailId = model.Email,
//                //MiddleName = model.MiddleName,
//                //PhoneNumber = model.PhoneNumber,
//                Password = SecurePasswordHasher.Hash(model.Password),
//                FirstName = model.UserName,
//                // Role = model.UserRole,
//                // IsPhoneNumberConfirmed = model.IsPhoneNumberConfirmed,
//                //  IsActive = model.IsActive,
//                //ProvinceId = model.ProvinceId,
//                UserId = uid,
//                // AccountNo = f.ToString()
//            };

//            try
//            {
//                db.UserRegistrations.Add(user);
//                int status = db.SaveChanges();
//                if (status == 1)
//                { info.IsSucceed = true; }
//            }
//            catch (DbUpdateConcurrencyException ex)
//            {
//                var msg = ex.Message;
//                throw;
//            }
//            return info;
//        }

//        //internal SecurityInfo FindByEmailAsync(ForgotPasswordViewModel model, APIManagementEntities _db, SecurityInfo _info)
//        //{
//        //    throw new NotImplementedException();
//        //}

//        public string generateID()
//        {
//            return Guid.NewGuid().ToString("N");
//        }
//        public void Dispose()
//        {
//            throw new NotImplementedException();
//        }



//        //internal RegistrationDetail FinduserByuseridandpassword(string uname, string pwd, APIManagementEntities Db)
//        //{
//        //    RegistrationDetail users = Db.RegistrationDetails.FirstOrDefault(c => c.UserName == uname && c.Password == pwd);
//        //    return users;
//        //}
//    }
//}