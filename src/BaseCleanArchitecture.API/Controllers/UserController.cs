using System.Linq;
using System.Threading.Tasks;
using BaseCleanArchitecture.API.ApiModels;
using BaseCleanArchitecture.Core.Entities;
using BaseCleanArchitecture.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BaseCleanArchitecture.API.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUser _user;

        public UserController(IUser user)
        {
            _user = user;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> Get()
        {
            var users = await _user.List();
            return Ok(users);
        }

        [HttpGet("get-user/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var user = await _user.GetById(id);
            if (user == null) return NotFound();

            return Ok(user);
        }

        [HttpPost("create-user")]
        public async Task<IActionResult> Post(UserDTO registerUser)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.Values.SelectMany(e => e.Errors));
            if (registerUser == null) return NoContent();

            var user = new User
            {
                Name = registerUser.Name,
                Email = registerUser.Email
            };

            var createdUser = await _user.Create(user);

            return Ok(createdUser);
        }

        [HttpPut("edit-user")]
        public async Task<IActionResult> Put(int id, UserDTO registerUser)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.Values.SelectMany(e => e.Errors));
            if (registerUser == null) return NoContent();

            var userToUpdate = _user.GetById(id).Result;
            if (userToUpdate == null) return NotFound();
            else
            {
                userToUpdate.Name = registerUser.Name;
                userToUpdate.Email = registerUser.Email;
            }

            var updatedUser = await _user.Update(userToUpdate);

            return Ok(updatedUser);
        }

        [HttpPut("delete-user")]
        public IActionResult Delete(int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.Values.SelectMany(e => e.Errors));

            var userToDelete = _user.GetById(id).Result;
            if (userToDelete == null) return NotFound();

            _user.Delete(userToDelete);

            return Ok();
        }
    }
}