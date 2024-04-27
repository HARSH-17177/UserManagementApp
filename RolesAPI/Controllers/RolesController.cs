using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductsApiService.Infrastructure;
using UserManagement.TableCreation;
using UserManagementProcess;

namespace RolesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [CustomAuthorization]
    public class RolesController : ControllerBase
    {
        private readonly RoleProcess _rolesProcess;
        public RolesController(RoleProcess rolesProcess)
        {
            _rolesProcess = rolesProcess;
        }

        [HttpGet("list")]
        public ActionResult<IEnumerable<Role>> GetRoles() {
        return Ok(_rolesProcess.GetRoles());
        
        }

        [HttpGet("details/{id}")]
        public ActionResult<Role> GetRoleById(int id)
        {
            var role = _rolesProcess.FindRolebyId(id);
            if (role == null) return NotFound();
            return Ok(role);
        }

        [HttpPost("createnew")]
        public ActionResult<Role> CreateNewRole([FromBody] Role role)
        {
            var newRole = _rolesProcess.InsertRole(role.RoleName, role.RoleDescription);
            if (newRole == null) return BadRequest("Invalid role data");
            return CreatedAtAction(nameof(GetRoleById), new { id = newRole.RoleId }, newRole);
        }

        [HttpPut("update/{id}")]
        public IActionResult UpdateRole(int id, [FromBody] Role role)
        {
            var isupdated = _rolesProcess.UpdateRole(id,role.RoleDescription);
            if(!isupdated)
            {
                return NotFound();
            }
            var updated = _rolesProcess.FindRolebyId(id) ;
            return Ok(updated);
        }

        [HttpDelete("delete/{id}")]
        public IActionResult DeleteRole(int id) {
            var role = _rolesProcess.FindRolebyId(id);
        var isDeleted = _rolesProcess.RemoveRole(id);
            if(!isDeleted) return NotFound();
            return Ok(role);

        }
    }


}
