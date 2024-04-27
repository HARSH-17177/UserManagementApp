using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.TableCreation;

namespace AuthenticationAPI.Models
{
    public interface IUserServiceAsync
    {
        Task<User> AuthenticateAsync(AuthenticationRequest model);
        Task<User> GetUserDetails(int userId);
    }
    public class UserService : IUserServiceAsync
    {

        private readonly TableCreationDbContext _dbContext;
        public UserService(TableCreationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<User> AuthenticateAsync(AuthenticationRequest model)
        {
            var user = _dbContext.Users.FirstOrDefault(c => c.UserName == model.UserName && c.Password == model.password);
            return Task.Run(() => user);
        }
        public Task<User> GetUserDetails(int userId)
        {
            var user = _dbContext.Users.FirstOrDefault(c => c.UserId == userId);
            return Task.Run(() => user);
        }
    }
}
