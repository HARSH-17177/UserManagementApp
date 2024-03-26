using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Repository;
using UserManagement.TableCreation;

namespace UserManagementProcess
{
    public class UserCollection : IRepository<User, int>, IUserRepository<UserRole, int>
    {
        TableCreationDbContext dbContext = new TableCreationDbContext();

        public User FindById(int id)
        {
            try
            {
            return dbContext.Users.Where(c=>c.IsActive==true).FirstOrDefault(c=>c.UserId==id);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public IEnumerable<User> GetAll()
        {
            try
            {

            return dbContext.Users.Where(c=>c.IsActive==true).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public IEnumerable<User> GetByCriteria(string filterCriteria)
        {
            throw new NotImplementedException();
        }

        public int GetRoleIdForUser(int userid)
        {
            try
            {
                return dbContext.UserRoles.Where(c => c.IsActive == true).FirstOrDefault(c => c.UserId == userid).RoleId;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return -1;
            }
        }

        public void RemoveById(int id)
        {
            try
            {
                var emp = dbContext.Users.Find(id);
                if (emp is not null)
                {
                    emp.IsActive = false;
                    Upsert(emp);
                }
                else
                {
                    Console.WriteLine("This id doesn't Exist");
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
          
        }



        public UserRole UpdateUserRole(UserRole userRole)
        {
            var item = dbContext.UserRoles.Find(userRole);
            if(item is not null && item.IsActive==false)
            {
                dbContext.UserRoles.Where(
            c => c.UserId == userRole.UserId)
            .ExecuteUpdate(setters => setters.SetProperty(p => p.IsActive, true));
                dbContext.SaveChanges();
                return item;
            }
            else
            {
                dbContext.UserRoles.Add(userRole);
                dbContext.SaveChanges();
            }
            return dbContext.UserRoles.FirstOrDefault(c => c.UserId == userRole.UserId);
        }

        public void Upsert(User entity)
        {
            try
            {
                var emp = FindById(entity.UserId);
                if (emp is null)
                {
                    dbContext.Users.Add(entity);
                    dbContext.SaveChanges();
                }
                else
                {
                    dbContext.Users.Where(
                        c => c.UserId == entity.UserId)
                        .ExecuteUpdate(setters => setters.SetProperty(p => p.FirstName, entity.FirstName)
                        .SetProperty(s => s.LastName, entity.LastName));
                    dbContext.SaveChanges();

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
           
        }

        UserRole IUserRepository<UserRole, int>.GetRoleIdForUser(int id)
        {
            throw new NotImplementedException();
        }
    }
}
