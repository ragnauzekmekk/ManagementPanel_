using ManagementPanel_.Model.Models;
using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;

namespace ManagementPanel_.Data.EntityFramework
{
    public class ManagementPanelContext : DbContext
    {
        // Your context has been configured to use a 'ManagementPanelContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'ManagementPanel_.Data.EntityFramework.ManagementPanelContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'ManagementPanelContext' 
        // connection string in the application configuration file.ManagementPanelContext
        public ManagementPanelContext()
            : base("name=ManagementPanelContext")
        {
        }


        //Tablonun sonuna gelen s takýsýný kadýrmaya yarýyor..
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }


        public virtual DbSet<User> Users { get; set; }
    }

}