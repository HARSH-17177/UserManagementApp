using Microsoft.Extensions.Options;
using UserManagement.TableCreation;
using UserRoleManagementFrontend.Infrastructure;

namespace UserRoleManagementFrontend.Services
{
    public class UsersApiRepository : IRepositoryAsync<User>
    {

        private readonly ApiConfigurations _apiConfig;
        private readonly User user;
        private readonly string token;
        public UsersApiRepository(
            IHttpContextAccessor contextAccessor,
            IOptions<ApiConfigurations> options)
        {
            _apiConfig = options.Value;
            token = contextAccessor.HttpContext.Session.GetString("Token")!;
            if (!string.IsNullOrEmpty(token))
            {
                var userString = contextAccessor.HttpContext.Session.GetString("User")!;
                user = ConvertData.JsonStringToObject<User>(userString)!;
            }
        }







        public async Task<IEnumerable<User>> GetAll()
        {
            var result = await ApiHelper.ExecuteHttpGet<List<User>>(
                url: _apiConfig.UserUrl + "/list",
                token: token,
                baseUrl: _apiConfig.UserBaseUrl!);
            return result;
        }

        public async Task<User> GetById(int id)
        {
            var result = await ApiHelper.ExecuteHttpGet<User>(
                url: _apiConfig.UserUrl + "/details/" + id,
                token: token,
                baseUrl: _apiConfig.UserBaseUrl!);
            return result;
        }

        public async Task<User> CreateNew(User entity)
        {
            var result = await ApiHelper.ExecuteHttpPost<User>(
          url: $"{_apiConfig.UserUrl}/createnew",
          token: token,
          baseUrl: _apiConfig.UserBaseUrl!,
          data: entity);

            return result;
        }

        public async Task<User> Remove(int id)
        {
            var result = await ApiHelper.ExecuteHttpDelete<User>(
                url: $"{_apiConfig.UserUrl}/delete/{id}",
                token: token,
                baseUrl: _apiConfig.UserBaseUrl!);

            return result;
        }



        public async Task<User> Update(User entity)
        {
            var result = await ApiHelper.ExecuteHttpPut<User>(
                url: $"{_apiConfig.UserUrl}/update/{entity.UserId}",
                token: token,
                baseUrl: _apiConfig.UserBaseUrl!,
                data: entity);

            return result;
        }
    }
}
