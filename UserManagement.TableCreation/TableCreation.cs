using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.TableCreation
{
    /*
     
The information required to be persisted is in the below format: 
Users 
UserId (auto-generated), UserName, Password, Firstname, Lastname, IsActive 
Roles 
RoleId (auto-generated), RoleName, RoleDescription 
UserRoles 
UserId, RoleId - [ PK on UserId and RoleId ] 

     */

    [Table("Users")]
    [Index("UserName", IsUnique = true)]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [Required]
        [StringLength(100)]
        [MinLength(5)]
        public string UserName { get; set; }

        [Required]
        [MinLength(8)]
        public string Password { get; set; }

        [Required]
        [StringLength(100)] 
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)] 
        public string LastName { get; set; }

        public bool IsActive { get; set; } = true;
    }

    [Table("Roles")]
    [Index("RoleName", IsUnique = true)]
    public class Role
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RoleId { get; set; }

        [Required]
        [StringLength(100)]
        [MinLength(5)]
        public string RoleName { get; set; }

        [StringLength(500)] 
        public string RoleDescription { get; set; }

        public bool IsActive { get; set; } = true;
    }

    [Table("UserRoles")]
    [PrimaryKey("UserId", "RoleId")]
    public class UserRole
    {

        public int UserId { get; set; }

   
        public int RoleId { get; set; }

        public bool IsActive { get; set; } = true;

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }
    }

    public class TableCreationDbContext: DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        //Microsodt.EntityFrameworkCore.SqlServer   --->nuget install
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseSqlServer(
             connectionString: @"Server=(local);database=UserDB;integrated security=sspi;trustservercertificate=true;
                        multipleactiveresultsets=true"
      );

        }
    }
}
