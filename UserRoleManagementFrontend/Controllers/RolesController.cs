using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using UserManagement.TableCreation;
using UserRoleManagementFrontend.Infrastructure;
using UserRoleManagementFrontend.Models;

namespace UserRoleManagementFrontend.Controllers
{
    public class RolesController : Controller
    {
        private readonly IRepositoryAsync<Role> _repositoryAsync;
        private readonly ApiConfigurations _apiConfig;
        public RolesController(
            IRepositoryAsync<Role> repositoryAsync,
            IOptions<ApiConfigurations> options)
        {
            _repositoryAsync = repositoryAsync;
            _apiConfig = options.Value;
        }

        [TokenCheck]
        public async Task<IActionResult> List()
        {
       

            var model = await _repositoryAsync.GetAll();
            return View(model);
        }

        [TokenCheck]
        public async Task<IActionResult> Details(int id)
        {
         

            var model = await _repositoryAsync.GetById(id);
            return View(model);
        }

        [TokenCheck]
        public async Task<IActionResult> Delete(int id)
        {
  var model = await _repositoryAsync.Remove(id);
            return RedirectToAction("List");
        }

        [TokenCheck]
        public async Task<IActionResult> Update(int id)
        {
            var role = await _repositoryAsync.GetById(id);
            return View(role);
        }   
        
        [TokenCheck,HttpPost]
        public async Task<IActionResult> Update(Role role)
        {
          
             var model = await _repositoryAsync.Update(role);
            return RedirectToAction("List");
        }


        [TokenCheck]
        public async Task<IActionResult> Create()
        {
        
            return View();
        }
        [TokenCheck,HttpPost]
        public async Task<IActionResult> Create(Role data)
        {

            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
            await _repositoryAsync.CreateNew(data);
            return RedirectToAction("List");
        }

    }
}
