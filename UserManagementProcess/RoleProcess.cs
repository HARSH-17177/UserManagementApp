using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.TableCreation;

namespace UserManagementProcess
{
    public class RoleProcess
    {
        RoleCollection coll = new RoleCollection();
        public Role InsertRole(string rolename, string roledescription)
        {

            Role role = new Role();
         role.RoleName= rolename;
            role.RoleDescription= roledescription;
            role.IsActive = true;

          
            if (rolename.Length >= 5 &&  roledescription.Length>0)
            {
                coll.Upsert(role);
                return role;
            }
            else
            {
                return null;
            }
        }

        public Role FindRolebyId(int id)
        {
            Role role = coll.FindById(id);
            if (role is not null)
            {
                return role; 
            }
            else
            {
                return null;
            }    
        }
        public IEnumerable<Role> GetRoles()
        {

            return coll.GetAll().ToList();

        }
        public bool RemoveRole(int id)
        {
            Role role = coll.FindById(id);
            if (role is not null)
            {
                coll.RemoveById(id);
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool UpdateRole(int id,string description)
        {
            Role role = coll.FindById(id);if (role is null) throw new Exception("Role Not Found");
            if (description.Length > 0)
            {
               role.RoleDescription = description;
            }
            if (role is not null)
            {
                coll.Upsert(role);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
