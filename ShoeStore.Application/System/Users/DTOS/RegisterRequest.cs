namespace ShoeStore.Application.System.Users.DTOS
{
    public class RegisterRequest
    {

        public string firstName { get; set; }
        public string lastName { get; set; }
        public DateTime Dob { get; set; }
        public string email { get; set; }

        public string phoneNumber { get; set; }

        public string userName { get; set; }

        public string passWord { get; set; }

        public string confirmPassword { get; set; }
    }
}