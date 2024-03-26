using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UserManagement.TableCreation;

namespace UserManagementProcess
{
    internal class UserRolesCollection 
    {
        TableCreationDbContext dbcontext = new TableCreationDbContext();
   

        public IEnumerable<UserRole> GetAll()
        {
            return dbcontext.UserRoles.ToList();
        }

        public IEnumerable<UserRole> GetByCriteria(string filterCriteria)
        {
            return null;
        }

    }
}
