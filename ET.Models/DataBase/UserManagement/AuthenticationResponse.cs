namespace ET.Models.DataBase.UserManagement
{
    public class AuthenticationResponse
    {
        public string UserId { get; set; }
        public string JwtToken { get; set; }
        public int ExpiresIn { get; set; }
    }
}
