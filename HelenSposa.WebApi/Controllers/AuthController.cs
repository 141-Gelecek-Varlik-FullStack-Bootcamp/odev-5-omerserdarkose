using HelenSposa.BackgroundJob;
using HelenSposa.Business.Abstract;
using HelenSposa.Entities.Dtos.User;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HelenSposa.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthService _authManager;

        public AuthController(IAuthService authManager)
        {
            _authManager = authManager;
        }


        [HttpPost("login")]
        public ActionResult Login([FromBody] UserLoginDto userLoginDto)
        {
            var userToLogin = _authManager.Login(userLoginDto);
            if (!userToLogin.Success)
            {
                return BadRequest(userToLogin.Message);
            }

            var tokenResult=_authManager.CreateAccessToken(userToLogin.Data);
            if (!tokenResult.Success)
            {
                return BadRequest(tokenResult.Message);
            }

            return Ok(tokenResult.Data);
        }

        [HttpPost("register")]
        public ActionResult Register([FromBody] UserRegisterDto userRegisterDto)
        {
            var userNotExists = _authManager.UserNotExists(userRegisterDto.Email);

            if (!userNotExists.Success)
            {
                return BadRequest(userNotExists.Message);
            }

            var userToRegister = _authManager.Register(userRegisterDto);

            var tokenResult = _authManager.CreateAccessToken(userToRegister.Data);

            if (!tokenResult.Success)
            {
                return BadRequest(tokenResult.Message);
            }

            //register olan kullanici icin mail gonderme islemine kayit yapiliyor
            DelayedJobs.SendMailWelcomeJobs(userToRegister.Data.Id);

            return Ok(tokenResult.Data);
        }

    }
}
