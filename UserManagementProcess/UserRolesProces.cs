using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Repository;
using UserManagement.TableCreation;

namespace UserManagementProcess
{
    public class UserRolesProces
    {

        IRepository<User, int> repository = new UserCollection();
        IUserRepository<User, int> userRepository = new UserCollection();
        IRepository<Role, int> roleRepository = new RoleCollection();
        public Role GetRoleForUser(User u)
        {
            int roleID = userRepository.GetRoleIdForUser(u.UserId);
            if(roleID == 0) { return null;}

            return roleRepository.FindById(roleID);

        }
        public UserRole UpdateRole(string rollName, int userId)
        {
            UserRole userRole = new UserRole();
            if (repository.FindById(userId) == null) throw new Exception("UserId not available");
            else
            {

            userRole.UserId = userId;
            Role role = roleRepository.GetAll().Where(c => c.RoleName == rollName).FirstOrDefault();
            if (role is null) return null;

            userRole.RoleId = role.RoleId;
            userRole.IsActive = true;
            return userRepository.UpdateUserRole(userRole);
            }


        }
    }
}
