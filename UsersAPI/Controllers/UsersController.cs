using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserManagementProcess;
using System.Collections.Generic;
using UserManagement.TableCreation;
using ProductsApiService.Infrastructure;

namespace UsersAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [CustomAuthorization]
    public class UsersController : ControllerBase
    {
        private readonly UserProcess _userProcess;

        public UsersController(UserProcess userProcess)
        {
            _userProcess = userProcess;
        }

        // GET: /api/users/list
        [HttpGet("list")]
        public ActionResult<IEnumerable<User>> GetUsers()
        {
            return Ok(_userProcess.GetUsers());
        }

        // GET: /api/users/details/{id}
        [HttpGet("details/{id}")]
        public ActionResult<User> GetUserById(int id)
        {
            var user = _userProcess.FindUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        // POST: /api/users/createnew
        [HttpPost("createnew")]
        public ActionResult<User> CreateNewUser([FromBody] User user)
        {
            var newUser = _userProcess.InsertUser(user.UserName, user.Password, user.FirstName, user.LastName);
            if (newUser == null)
            {
                return BadRequest("Invalid user data");
            }
            return CreatedAtAction(nameof(GetUserById), new { id = newUser.UserId }, newUser);
        }

        // PUT: /api/users/update/{id}
        [HttpPut("update/{id}")]
        public IActionResult UpdateUser(int id, [FromBody] User user)
        {
            var isUpdated = _userProcess.UpdateUser(id, user.FirstName, user.LastName);
            if (!isUpdated)
            {
                return NotFound();
            }
            var model = _userProcess.FindUserById(id);
            return Ok(model);
        }

        // DELETE: /api/users/delete/{id}
        [HttpDelete("delete/{id}")]
        public IActionResult DeleteUser(int id)
        {
            var user = _userProcess.FindUserById(id);
            var isDeleted = _userProcess.RemoveUser(id);
            if (!isDeleted)
            {
                return NotFound();
            }
            return Ok(user);
        }

        // POST: /api/users/mapusertoroles
        [HttpPost("mapusertoroles")]
        public IActionResult MapUserToRoles(int userId,int roleid)
        {
            // Implementation depends on how UserRolesEntity is defined and how roles are managed in your system.
            // Assuming there's a method in UserProcess to handle this:
            var isMapped = _userProcess.UpdateRole(userId,roleid);
            if (!isMapped)
            {
                return NotFound();
            }
            return Ok("Mapped Successfully");
        }
    }
}
