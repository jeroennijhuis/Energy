using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using Energy.API.Controllers.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Energy.API.Controllers
{
    [Route("auth")]
    public class AuthenticationController : LoggingController<AuthenticationController>
    {
        private const string ClientId = "34ciupnqfed4l7gga4r0mq9sd6";
        private const string UserPoolId = "eu-central-1_7wrt2SCUs";

        private readonly IAmazonCognitoIdentityProvider _client;

        public AuthenticationController(IAmazonCognitoIdentityProvider cognitoClient, ILogger<AuthenticationController> logger) : base(logger)
        {
            _client = cognitoClient;
        }

        /// <summary>
        /// Authenticate your 
        /// </summary>
        /// <param name="username">Username (Email-address / Phone number)</param>
        /// <param name="password">Password</param>
        /// d
        [HttpPost]
        [Route("signin")]
        public async Task<ActionResult<string>> SignIn([FromQuery, Required] string username, [FromQuery, Required] string password)
        {
            var request = new AdminInitiateAuthRequest
            {
                UserPoolId = UserPoolId,
                ClientId = ClientId,
                AuthFlow = AuthFlowType.ADMIN_NO_SRP_AUTH
            };

            request.AuthParameters.Add("USERNAME", username);
            request.AuthParameters.Add("PASSWORD", password);

            var response = await _client.AdminInitiateAuthAsync(request);

            return Ok(response.AuthenticationResult.IdToken);
        }
    }
}