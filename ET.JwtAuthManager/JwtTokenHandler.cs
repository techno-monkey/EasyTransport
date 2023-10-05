using ET.Models.DataBase.UserManagement;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ET.JwtAuthManager
{
    public class JwtTokenHandler
    {
        public const string JWT_SECURITY_KEY = "sampleKeyForPOC-purushottam-jha-1982";
        private const int JWT_TOKEN_VALIDITY_MINS = 20;
        private readonly List<User> _users;
        public JwtTokenHandler()
        {
            _users = new List<User>()
            {
                new User{ UserId ="User1", Name="User1", Password="User1", Email ="user1@demo.com", Role ="Admin"},
                new User{ UserId ="User2", Name="User2", Password="User2", Email ="user2@demo.com", Role ="User"}
            };
        }

        public AuthenticationResponse? GenrerateJwtToken(AuthenticationRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.UserId) || string.IsNullOrWhiteSpace(request.Password))
                return null;
            var userAccount = _users.Where(c => c.UserId == request.UserId && c.Password == request.Password).FirstOrDefault();
            if (userAccount == null)
                return null;
            var tokenExpiryTimeStamp = DateTime.UtcNow.AddMinutes(JWT_TOKEN_VALIDITY_MINS);
            var tokenKey = Encoding.ASCII.GetBytes(JWT_SECURITY_KEY);
            var claimIdentity = new ClaimsIdentity(new List<Claim> {
             new Claim(JwtRegisteredClaimNames.Name , userAccount.UserId),
             new Claim(ClaimTypes.Role, userAccount.Role),
             new Claim("Email", userAccount.Email)
            });
            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey),
                SecurityAlgorithms.HmacSha256Signature);

            var securityTokenDesc = new SecurityTokenDescriptor
            {
                Subject = claimIdentity,
                Expires = tokenExpiryTimeStamp,
                SigningCredentials = signingCredentials
            };

            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var sequrityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDesc);
            var token = jwtSecurityTokenHandler.WriteToken(sequrityToken);
            return new AuthenticationResponse
            {
                UserId = userAccount.UserId,
                JwtToken = token,
                ExpiresIn = (int)tokenExpiryTimeStamp.Subtract(DateTime.UtcNow).TotalSeconds
            };

        }
    }
}
