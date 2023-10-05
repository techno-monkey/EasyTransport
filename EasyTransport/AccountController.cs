using ET.JwtAuthManager;
using ET.Models.DataBase.UserManagement;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EasyTransport
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly JwtTokenHandler _jwtTokenHandler;
        public AccountController(JwtTokenHandler jwtTokenHandler)
        {
            _jwtTokenHandler = jwtTokenHandler;
        }

        [HttpPost]
        public ActionResult<AuthenticationResponse> Authenticate(AuthenticationRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            else
            {
                var authResponse = _jwtTokenHandler.GenrerateJwtToken(request);
                if (authResponse == null)
                {
                    return Unauthorized();
                }
                else
                    return authResponse;
            }

        }
    }
}
