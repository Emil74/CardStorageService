using CardStorageService.Models;
using CardStorageService.Models.Requests;
using CardStorageService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;
using System.Net.Http.Headers;

namespace CardStorageService.Controllers
{
    [Authorize]
    [Route("api/auth")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        #region Services
        private readonly IAuthenticateService _authenticateService;
        #endregion

        #region Constructors
        public AuthenticateController(IAuthenticateService authenticateService)
        {
            _authenticateService = authenticateService;
        }
        #endregion
        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody] AuthenticationRequest authenticationRequest)
        {
            AuthenticationResponse authenticationResponse = _authenticateService.Login(authenticationRequest);
            if (authenticationResponse.Status == Models.AuthenticationStatus.Success)
            {
                Response.Headers.Add("X-Session-Token", authenticationResponse.SessionInfo.SessionToken);
            }
            return Ok(authenticationResponse);
        }
        [HttpGet("session")]
        public IActionResult GetSessionInfo()
        {
            var authorization = Request.Headers[HeaderNames.Authorization];
            if (AuthenticationHeaderValue.TryParse(authorization, out var headerValue))
            {
                var scheme = headerValue.Scheme;//"Bearer"
                var sessionToken = headerValue.Parameter; //Token
                if (string.IsNullOrEmpty(sessionToken))
                    return Unauthorized();

                SessionInfo sessionInfo = _authenticateService.GetSessionInfo(sessionToken);
                if (sessionInfo == null)
                    return Unauthorized();


                return Ok(sessionInfo);
            }

            return Unauthorized();

        }


    }
}
