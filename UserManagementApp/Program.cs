


using UserManagement.Repository;
using UserManagement.TableCreation;
using UserManagementProcess;

namespace UserManagementApp
{
    public class Program
    {
        static UserProcess userProcess = new UserProcess();
        static RoleProcess roleProcess = new RoleProcess();
        static UserRolesProces UserRolesProces = new UserRolesProces();
        static void showAllUsers()
            {
            userProcess.GetUsers().ToList().ForEach(x => Console.WriteLine($"UserId : {x.UserId}\nUserName : {x.UserName}\nFirstName : {x.FirstName}\nLastName : {x.LastName}\nPassword : {x.Password}\n-------------------------------"));
        }
        static void findUserById()
        {
            Console.Write("Enter UserId : ");
            var x = userProcess.FindUserById(int.Parse(Console.ReadLine()));
            //   Console.WriteLine($"{x.UserId} | {x.UserName} | {x.FirstName} | {x.LastName} | {x.Password} | {x.IsActive}");
            Console.WriteLine($"UserId : {x.UserId}\nUserName : {x.UserName}\nFirstName : {x.FirstName}\nLastName : {x.LastName}\nPassword : {x.Password}");
        }
        static void insertNewUser()
        {
            Console.Write("Enter UserName : ");
            string username = Console.ReadLine();
            Console.Write("Enter Password : ");
            string password = Console.ReadLine();
            Console.Write("Enter FirstName : ");
            string FirstName = Console.ReadLine();
            Console.Write("Enter LastName : ");
            string LastName = Console.ReadLine();
          var  x = userProcess.InsertUser(username, password, FirstName, LastName);
            Console.WriteLine("user Inserted");
            Console.WriteLine($"UserId : {x.UserId}\nUserName : {x.UserName}\nFirstName : {x.FirstName}\nLastName : {x.LastName}\nPassword : {x.Password}");
        }
        static void updateUser()
        {

            Console.Write("Enter UserId to Update : ");
            int id = int.Parse(Console.ReadLine());
            Console.Write("Enter New FirstName : ");
            string FirstName = Console.ReadLine();
            Console.Write("Enter New LastName : ");
            string LastName = Console.ReadLine();
           var x = userProcess.UpdateUser(id, FirstName, LastName);
            if(x)
            {

            Console.WriteLine("user Updated");
            }
            else
            {
                Console.WriteLine("Enter valid id");
            }
           
        }
        static void removeUser()
        {
            Console.Write("Enter UserId to Remove : ");
            int id =int.Parse(Console.ReadLine());
           var x = userProcess.RemoveUser(id);
            if(x)
            {
                Console.WriteLine("User deleted");
            }
            else
            {
                Console.WriteLine("Enter Correct id");
            }
        }


        //----------------------------------------------------------------------------------------





        static void showAllRoles()
        {
            roleProcess.GetRoles().ToList().ForEach(x => Console.WriteLine($"RoleId : {x.RoleId}\nRoleName : {x.RoleName}\nRoleDescription : {x.RoleDescription}\n--------------------------------------------"));
        }
        static void findRoleById()
        {
            Console.Write("Enter RoleId : ");
            var x = roleProcess.FindRolebyId(int.Parse(Console.ReadLine()));
            //   Console.WriteLine($"{x.UserId} | {x.UserName} | {x.FirstName} | {x.LastName} | {x.Password} | {x.IsActive}");
            Console.WriteLine($"RoleName : {x.RoleName}\nRoleDescription : {x.RoleDescription}\n");
        }
        static void insertNewRole()
        {
            Console.Write("Enter RoleName : ");
            string rolename = Console.ReadLine();
            Console.Write("Enter RoleDescription : ");
            string roleDescription = Console.ReadLine();
          
            var x = roleProcess.InsertRole(rolename, roleDescription);
            Console.WriteLine("Role Inserted");
            Console.WriteLine($"RoleName : {x.RoleName}\nRoleDescription : {x.RoleDescription}");
        }
        static void updateRole()
        {
            Console.Write("Enter Role Id : )");
            int id =int.Parse(Console.ReadLine());
            Console.Write("Enter new RoleDescription : ");
            string roleDescription = Console.ReadLine();
     var x = roleProcess.UpdateRole(id,roleDescription);
            if (x)
            {

                Console.WriteLine("Roles Updated");
            }
            else
            {
                Console.WriteLine("Enter valid id");
            }

        }
        static void removeRole()
        {
            Console.Write("Enter RoleId to Remove : ");
            int id = int.Parse(Console.ReadLine());
            var x = roleProcess.RemoveRole(id);
            if (x)
            {
                Console.WriteLine("Roles deleted");
            }
            else
            {
                Console.WriteLine("Enter Correct id");
            }
        }

        static void UpdateUserRole()
        {
           
            Console.Write("Enter your UserId : ");
            int userid =int.Parse(Console.ReadLine());
            var roles = roleProcess.GetRoles();
            foreach ( var role in roles )
            {
                Console.Write($"{role.RoleName} |");
            }

            Console.Write("Enter Role Name: ");
            string roleName = Console.ReadLine();


            Console.Write("Save this mapping? Y/n: ");
            char saveChoice = Console.ReadKey().KeyChar;

            if (char.ToUpper(saveChoice) == 'Y')
            {
               var item= UserRolesProces.UpdateRole(roleName, userid);
              if(item is not null)
                {
                Console.WriteLine("\nMapping saved successfully!");

                }
              else
                {
                    Console.WriteLine("\nMapping failed");
                }
            }
            else
            {
                Console.WriteLine("\nMapping discarded.");
            }

        }
        static void Main(string[] args)
        {
            int choice;
            do
            {
                Console.Clear();
                Console.WriteLine("************** Login Management System ***************");
                Console.WriteLine("********* 1. Manage Users");
                Console.WriteLine("********* 2. Manage Roles");
                Console.WriteLine("********* 3. Manage User Roles");
                Console.WriteLine("********* 0. Quit");
                Console.WriteLine("*****************************************************");
                Console.Write("Your Choice: ");

                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Invalid input. Please enter a valid choice.");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        ManageUsersScreen();
                        break;
                    case 2:
                        ManageRolesScreen();
                        break;
                    case 3:
                        ManageUserRolesScreen();
                        break;
                    case 0:
                        Console.WriteLine("Quitting application...");
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please select a valid option.");
                        break;
                }

                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            } while (choice != 0);
        }

        static void ManageUsersScreen()
        {
            int choice;
            do
            {
                Console.Clear();
                Console.WriteLine("************** Login Management System ***************");
                Console.WriteLine("************** Manage Users ***************");
                Console.WriteLine("********* 1. List all Users");
                Console.WriteLine("********* 2. Find User By Id");
                Console.WriteLine("********* 3. Add new User");
                Console.WriteLine("********* 4. Update User Details");
                Console.WriteLine("********* 5. Remove User");
                Console.WriteLine("********* 0. Back to Main Menu");
                Console.WriteLine("*****************************************************");
                Console.Write("Enter Choice: ");

                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Invalid input. Please enter a valid choice.");
                    continue;
                }
             
                switch (choice)
                {
                    case 1:
                        showAllUsers();
                        break;
                    case 2:
                        findUserById();
                        break;
                    case 3:
                        insertNewUser();
                        break;
                    case 4:

                       updateUser();
                        break;
                    case 5:
                        removeUser();
                        break;
                    case 0:
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please select a valid option.");
                        break;
                }

                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            } while (choice != 0);
        }

        static void ManageRolesScreen()
        {
            int choice;
            do
            {
                Console.Clear();
                Console.WriteLine("************** Login Management System ***************");
                Console.WriteLine("************** Manage Roles ***************");
                Console.WriteLine("********* 1. List all Roles");
                Console.WriteLine("********* 2. Find Role By Id");
                Console.WriteLine("********* 3. Add new Role");
                Console.WriteLine("********* 4. Update Role Details");
                Console.WriteLine("********* 5. Remove Role");
                Console.WriteLine("********* 0. Back to Main Menu");
                Console.WriteLine("*****************************************************");
                Console.Write("Enter Choice: ");

                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Invalid input. Please enter a valid choice.");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        showAllRoles();
                        break;
                    case 2:
                        findRoleById();
                        break;
                    case 3:
                        insertNewRole();
                        break;
                    case 4:

                        updateRole();
                        break;
                    case 5:
                        removeRole();
                        break;
                    case 0:
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please select a valid option.");
                        break;
                }

                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            } while (choice != 0);
        }

        static void ManageUserRolesScreen()
        {
            Console.Clear();
            Console.WriteLine("************** Login Management System ***************");
            Console.WriteLine("************** Manage User Roles ***************");

            UpdateUserRole();



 
        }
    }


}
