using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.TableCreation;

namespace UserManagement.Repository
{
    public interface IUserRepository<Tentity, TIdentity> : IRepository<Tentity,TIdentity>
    {
        int GetRoleIdForUser(int id);
        UserRole UpdateUserRole(UserRole userRole);
    }
   
}
