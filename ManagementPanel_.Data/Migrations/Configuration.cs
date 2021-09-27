namespace ManagementPanel_.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ManagementPanel_.Data.EntityFramework.ManagementPanelContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ManagementPanel_.Data.EntityFramework.ManagementPanelContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            //context.Users.AddOrUpdate(x => x.ID,
            // new Model.Models.User() { ID = 1, Name = "Admin", Surname = "Admin", Username = "Admin", Email = "admin@mail.com", Phone = "53849788740", Admin = true, Date = Convert.ToDateTime("11.03.1997"), Password = "C8qiotBAbGg=" });
            //context.SaveChanges();
        }
    }
}
