using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.TableCreation;

namespace UserManagementProcess
{
    public class UserProcess
    {
        UserCollection coll = new UserCollection();
        public User InsertUser(string username,string password,string firstname,string lastname)
        {
          
            User user = new User();
            user.UserName = username;
          
            user.Password = password;
            user.FirstName = firstname;
            if(firstname.Length == 0) throw new Exception("FirstName Length should be greater than 0");
            user.LastName = lastname;
            if (lastname.Length == 0) throw new Exception("LastName Length should be greater than 0");
            user.IsActive = true;
  
            bool isDigit = false;
            foreach(char c in password)
            {
                if(c>='0' && c<='9')
                {
                    isDigit = true;
                }
            }
         
                if (isDigit && password.Length >= 8 && username.Length >= 5 && firstname.Length > 0 && lastname.Length > 0)
                {

                    coll.Upsert(user);
                    return user;
                }
                else
                {
                    return null;
                }
       
           
        }
        public User FindUserById(int id)
        {
            User user = coll.FindById(id);
            if (user is not null)
            {
                return user;
            }
            else
                return null;
        }

        public IEnumerable<User> GetUsers()
        {

            return coll.GetAll().ToList();
           
        }
        public bool RemoveUser(int id)
        {
            User usr = coll.FindById(id);
            if(usr is not null)
            {
                coll.RemoveById(id);
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool UpdateUser(int id,string firstname,string lastname) {
            User usr = coll.FindById(id);
            if (firstname.Length > 0)
            {
                usr.FirstName = firstname;

            }
            else throw new Exception("FirstName Length should be greater than 0");
            if (lastname.Length > 0)
            {
                 usr.LastName =lastname;
            }
            else throw new Exception("LastName Length should be greater than 0");
            if (usr is not null)
            {
                coll.Upsert(usr);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool UpdateRole(int userId, int roleId)
        {
            try
            {
                UserRole ur = new UserRole();
                ur.UserId = userId;
                ur.RoleId = roleId;
                ur.IsActive = true;

            var data=    coll.UpdateUserRole(ur);
                if(data != null) { return true; }
                return false;
            }
            catch
            {
                throw;
            }



        }
    }
}
