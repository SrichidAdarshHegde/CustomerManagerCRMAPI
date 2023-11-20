namespace Application.Security
{
    public class RegisterUserModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string MiddleName { get; set; }
        public bool IsActive { get; set; }
        public int UserRole { get; set; }
        public bool IsPhoneNumberConfirmed { get; set; }
        public int ProvinceId { get; set; }
        public int PlanId { get; set; }
        public object PresentPlan { get; set; }
        public string AccessToken { get; set; }

    }

    public class UserModel
    {
        public string UserName { get; set; }
    }
}
