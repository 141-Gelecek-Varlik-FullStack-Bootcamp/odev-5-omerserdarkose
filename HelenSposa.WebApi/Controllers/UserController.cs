using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelenSposa.Business.Abstract;
using HelenSposa.Entities.Dtos.User;


namespace HelenSposa.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userManager;

        public UserController(IUserService userService)
        {
            _userManager = userService;
        }

        // GET: api/<UserController>
        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _userManager.GetAll();
            return Ok(result.Data);
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UserController>
        [HttpPost]
        public IActionResult Add([FromBody] UserAddDto userAddDto)
        {
            var userNotExists = _userManager.UserNotExists(userAddDto.Email);

            if (!userNotExists.Success)
            {
                return BadRequest(userNotExists.Message);
            }

            var newUser = _userManager.Add(userAddDto);

            return Ok(newUser.Message);
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
