using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using UserManagement.Repository;
using UserManagement.TableCreation;
using UserManagementProcess;

namespace UserManagementProcess.Tests
{
    [TestClass]
    public class UserProcessTests
    {
       
        // User test cases
        IRepository<User, int> test = new UserCollection();



        [TestMethod]
        public void InsertUser()
        {
            int expected_Count = test.GetAll().Count() + 1;
            User user =new User();
            user.UserName= "Testing";
            user.FirstName = "Testing";
            user.LastName = "Testing";
            user.Password = "TestingPassword";
            user.IsActive = true;
            test.Upsert(user);
            int current_value = test.GetAll().Count();
            Assert.AreEqual(expected_Count, current_value);
        }   
        
        [TestMethod]
        public void InsertDuplicateUser()
        {
            int expected_Count = test.GetAll().Count();
            User user =new User();
            user.UserName= "Testing";
            user.FirstName = "Testing";
            user.LastName = "Testing";
            user.Password = "TestingPassword";
            user.IsActive = true;
          
            test.Upsert(user);

         
            int current_value = test.GetAll().Count();
            Assert.AreEqual(expected_Count, current_value);
           
        }

        [TestMethod]
        public void INsertInvalidUser()
        {
            int expected_Count = test.GetAll().Count();
            User user = new User();
            user.UserName = "Testing";
            user.FirstName = "Testing";
            user.LastName = "Testing";
        //    user.Password = "TestingPassword";
            user.IsActive = true;
          

            test.Upsert(user);
          
         
            int current_value = test.GetAll().Count();
            Assert.AreEqual(expected_Count, current_value);

           
        }

        [TestMethod]
        public void FindUserById()
        {
            User user = new User();
            user.UserName = "Testing3";
            user.FirstName = "Testtesting2";
            user.LastName = "Testting2";
            user.Password = "TestingPassword2";
            user.IsActive = true;
            test.Upsert(user);
            var result = test.FindById(user.UserId);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.UserId, user.UserId);
        }

        [TestMethod]
        public void FindByInvalidUser()
        {
     
            var result = test.FindById(-9888);
     
            Assert.AreEqual(result, null);
        }

        [TestMethod]
public void getAll()
        {
            var allusers = test.GetAll();
            Assert.IsNotNull(allusers);
            Assert.IsTrue(allusers.Any());

        }

        [TestMethod]
        public void RemoveById()
        {
            int initialcount = test.GetAll().Count();
            var user = new User();
            user.UserName = "Testin4g5";
            user.FirstName = "Testt765esting5";
            user.LastName = "Testtin8g5";
            user.Password = "Testin8gPassword5";
            user.IsActive = true;
            test.Upsert(user);
            test.RemoveById(user.UserId);
            Assert.AreEqual(initialcount, test.GetAll().Count());


        }

        [TestMethod]
        public void RemoveInvalidUserById()
        {
            int initialcount = test.GetAll().Count();
         

            test.RemoveById(-987);
            Assert.AreEqual(initialcount, test.GetAll().Count());
          
       



        }





        [TestMethod]
        public void UpdateById()
        {
            var user = new User();
            user.UserName = "saaregamapadha";
            user.FirstName = "saaregamapadha";
            user.LastName = "saaregamapadha";
            user.Password = "saaregamapadha";
            user.IsActive = true;
            test.Upsert(user);
            var item = test.FindById(user.UserId);
            string oldname = user.UserName;
            user.UserName = "saareUpdated";
            user.FirstName = "updated";
            user.LastName = "updated";
            user.Password = "updated";
            user.IsActive = true;
            test.Upsert(user);
            Assert.AreNotEqual(user.UserName, oldname);


        }

        [TestMethod]
        public void UpdateInvalidColumnById()
        {
            var user = new User();
            user.UserName = "saaregamapa";
            user.FirstName = "saaregamapa";
            user.LastName = "saaregamapa";
            user.Password = "saaregamapa";
            user.IsActive = true;
            test.Upsert(user);
            var item = test.FindById(user.UserId);
            string oldname = user.UserName;
           

            user.UserId = 79;
            test.Upsert(user);
        
                Assert.AreEqual(user.UserName, oldname);
            
            


        }


        // Roles TEst Cases

        IRepository<Role, int> rep = new RoleCollection();


        [TestMethod]
        public void InsertRole()
        {
            int expected_Count = rep.GetAll().Count() + 1;
            Role role = new Role();
            role.RoleName = "Devops";
            role.RoleDescription = "site reliable Engineer";
            rep.Upsert(role);
            int current_value = rep.GetAll().Count();
            Assert.AreEqual(expected_Count, current_value);
        }

        [TestMethod]
        public void InsertDuplicateRole()
        {
            int expected_Count = rep.GetAll().Count() ;
            Role role = new Role();
            role.RoleName = "Devops";
            role.RoleDescription = "site reliable Engineer";
            rep.Upsert(role);


            int current_value = rep.GetAll().Count();
            Assert.AreEqual(expected_Count, current_value);

        }

        [TestMethod]
        public void INsertInvalidRole()
        {
            int expected_Count = rep.GetAll().Count() ;
            Role role = new Role();
         //   role.RoleName = "Devops";
            role.RoleDescription = "Managing";
            rep.Upsert(role);





            int current_value = rep.GetAll().Count();
            Assert.AreEqual(expected_Count, current_value);


        }

        [TestMethod]
        public void FindRoleById()
        {
          
            Role role = new Role();
            role.RoleName = "Fluttter dev";
            role.RoleDescription = "developer";
            rep.Upsert(role);
            var result = rep.FindById(role.RoleId);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.RoleId, role.RoleId);
        }

        [TestMethod]
        public void FindByInvalidRole()
        {

            var result =rep.FindById(-9888);

            Assert.AreEqual(result, null);
        }

        [TestMethod]
        public void getAllRole()
        {
            var allusers = rep.GetAll();
            Assert.IsNotNull(allusers);
            Assert.IsTrue(allusers.Any());

        }

        [TestMethod]
        public void RemoveRoleById()
        {
            int initialCount = rep.GetAll().Count() ;
            Role role = new Role();
            role.RoleName = "Manager";
            role.RoleDescription = "Managing Apps";
            rep.Upsert(role);
            test.RemoveById(role.RoleId);
            Assert.AreEqual(initialCount, rep.GetAll().Count());


        }

        [TestMethod]
        public void RemoveInvalidRoleById()
        {
            int initialcount = rep.GetAll().Count();


            test.RemoveById(-987);
            Assert.AreEqual(initialcount, rep.GetAll().Count());





        }





        [TestMethod]
        public void UpdateRoleById()
        {
           
            Role role = new Role();
            role.RoleName = "Devops2";
            role.RoleDescription = "site reliable Engineer2";
            rep.Upsert(role);
            var item = rep.FindById(role.RoleId);
            string oldname = role.RoleName;
            role.RoleName = "Devops3";

            rep.Upsert(role);
            Assert.AreNotEqual(role.RoleName, oldname);


        }

       

    }
}
