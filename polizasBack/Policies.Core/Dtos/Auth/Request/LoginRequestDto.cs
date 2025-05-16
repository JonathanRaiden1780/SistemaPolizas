namespace Policies.Core.Dtos.Auth.Request
{   
    public class LoginRequestDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class ChangePasswordRequestDto
    {
        public int idUser { get; set; }
        public string NewPassword { get; set; }
        public string OldPassword { get; set; }
    }

    public class CreateUser
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}