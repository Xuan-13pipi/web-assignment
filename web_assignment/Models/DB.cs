using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace web_assignment.Models;

public class DB : DbContext
{
    public DB(DbContextOptions options) : base(options) { }
    
    //DbSets
    public DbSet<User> Users { get; set; } // Represents the Users table in the database
    public DbSet<Admin> Admins { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Staff> Staffs { get; set; }

}


//Entity classes can be defined here---------------------

#nullable disable warnings //Disable warnings for nullable properties

public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int id { get; set; }
    [MaxLength(100)]
    
    public string Email { get; set; }
    [MaxLength(200)]
    public string PasswordHash { get; set; }
    [MaxLength(100)]
    public string Username { get; set; }
    [MaxLength(12)]
    public string Phone { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public string Role => GetType().Name;//Only field with get and set will be added to the database, so this will not be added to the database 


}

public class Admin : User
{
}

public class Customer : User
{

}

public class Staff : User
{

}



