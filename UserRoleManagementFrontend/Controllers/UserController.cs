using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using UserManagement.TableCreation;
using UserRoleManagementFrontend.Infrastructure;
using UserRoleManagementFrontend.Models;

namespace UserRoleManagementFrontend.Controllers
{
    public class UsersController : Controller
    {
        private readonly IRepositoryAsync<User> _repositoryAsync;
        private readonly ApiConfigurations _apiConfig;
        public UsersController(
            IRepositoryAsync<User> repositoryAsync,
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
            var user = await _repositoryAsync.GetById(id);
            return View(user);
        }

        [TokenCheck, HttpPost]
        public async Task<IActionResult> Update(User user)
        {

            var model = await _repositoryAsync.Update(user);
            return RedirectToAction("List");
        }


        [TokenCheck]
        public async Task<IActionResult> Create()
        {

            return View();
        }
        [TokenCheck, HttpPost]
        public async Task<IActionResult> Create(User data)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            await _repositoryAsync.CreateNew(data);
            return RedirectToAction("List");
        }

    }
}
