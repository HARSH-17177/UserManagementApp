using Microsoft.Extensions.Options;
using UserManagement.TableCreation;
using UserRoleManagementFrontend.Infrastructure;

namespace UserRoleManagementFrontend.Services
{
    public class RolesApiRepository : IRepositoryAsync<Role>
    {
        private readonly ApiConfigurations _apiConfig;
        private readonly User user;
        private readonly string token;
        public RolesApiRepository(
            IHttpContextAccessor contextAccessor,
            IOptions<ApiConfigurations> options)
        {
            _apiConfig = options.Value;
            token = contextAccessor.HttpContext.Session.GetString("Token")!;

            if(!string.IsNullOrEmpty(token)) {
                var userString = contextAccessor.HttpContext.Session.GetString("User")!;
                user = ConvertData.JsonStringToObject<User>(userString)!;
            }
          
        }



      


        public async Task<IEnumerable<Role>> GetAll()
        {
            var result = await ApiHelper.ExecuteHttpGet<List<Role>>(
                url: _apiConfig.RoleUrl + "/list",
                token: token,
                baseUrl: _apiConfig.RoleBaseUrl!);
            return result;
        }

        public async Task<Role> GetById(int id)
        {
            var result = await ApiHelper.ExecuteHttpGet<Role>(
                url: _apiConfig.RoleUrl+"/details/"+id,
                token: token,
                baseUrl: _apiConfig.RoleBaseUrl!);
            return result;
        }

        public async Task<Role> Remove(int id)
        {
            var result = await ApiHelper.ExecuteHttpDelete<Role>(
                url: $"{_apiConfig.RoleUrl}/delete/{id}",
                token: token,
                baseUrl: _apiConfig.RoleBaseUrl!);

            return result;
        }

        public async Task<Role> Update(Role entity)
        {
            var result = await ApiHelper.ExecuteHttpPut<Role>(
                url: $"{_apiConfig.RoleUrl}/update/{entity.RoleId}", 
                token: token,
                baseUrl: _apiConfig.RoleBaseUrl!,
                data: entity); 

            return result;
        }

        public async Task<Role> CreateNew(Role entity)
        {
            var result = await ApiHelper.ExecuteHttpPost<Role>(
          url: $"{_apiConfig.RoleUrl}/createnew",
          token: token,
          baseUrl: _apiConfig.RoleBaseUrl!,
          data: entity);

            return result;
        }
    }

}
