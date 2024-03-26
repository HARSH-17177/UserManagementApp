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
    public class RoleCollection : IRepository<Role, int>
    {
        TableCreationDbContext dbcontext = new TableCreationDbContext();    
        public Role FindById(int id)
        {try
            {
                return dbcontext.Roles.Where(c => c.IsActive == true).FirstOrDefault(c => c.RoleId == id);
              

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public IEnumerable<Role> GetAll()
        {
            try
            {

            return dbcontext.Roles.Where(c => c.IsActive == true).ToList();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public IEnumerable<Role> GetByCriteria(string filterCriteria)
        {
            return null;
        }

        public void RemoveById(int id)
        {
            try
            {

            var emp = dbcontext.Roles.Find(id);
            if(emp is not null)
            {
               emp.IsActive = false;
                Upsert(emp);
            }
            else
            {
                Console.WriteLine("This id doesn't Exist");
            }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void Upsert(Role entity)
        {
            try
            {
                var emp = FindById(entity.RoleId);
                if (emp is null)
                {
                    dbcontext.Roles.Add(entity);
                    dbcontext.SaveChanges();
                }
                else
                {
                    dbcontext.Roles.Where(
                        c => c.RoleId == entity.RoleId)
                        .ExecuteUpdate(setters => setters.SetProperty(p => p.RoleDescription, entity.RoleDescription));
                    dbcontext.SaveChanges();

                }
            }
           catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
