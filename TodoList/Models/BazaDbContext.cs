using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using MySql.Data.EntityFramework;

namespace TodoList.Models
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class BazaDbContext : DbContext
    {
        public DbSet<TodoList> TodoLists { get; set; }
        public DbSet<Task> Tasks { get; set; }
       
        public DbSet<Korisnik> PopisKorisnika { get; set; } 
        public DbSet<Ovlast> PopisOvlasti { get; set; }
   
    }
}
