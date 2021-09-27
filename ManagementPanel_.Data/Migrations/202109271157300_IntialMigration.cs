namespace ManagementPanel_.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class IntialMigration : DbMigration
    {
        public override void Up()
        {
            //CreateTable(
            //    "dbo.User",
            //    c => new
            //    {
            //        ID = c.Int(nullable: false, identity: true),
            //        Username = c.String(nullable: false, maxLength: 5),
            //        Name = c.String(nullable: false, maxLength: 50),
            //        Surname = c.String(nullable: false, maxLength: 50),
            //        Password = c.String(nullable: false, maxLength: 50),
            //        Email = c.String(nullable: false, maxLength: 50),
            //        Phone = c.String(nullable: false, maxLength: 10),
            //        Date = c.DateTime(nullable: false),
            //        Admin = c.Boolean(nullable: false),
            //    })
            //    .PrimaryKey(t => t.ID);

            Sql(@"INSERT INTO [dbo].[User]
                   ([Name]
                   ,[Surname]
                   ,[Username]
                   ,[Password]
                   ,[Email]
                   ,[Phone]
                   ,[Date]
                   ,[Admin])
             VALUES
                   ('Admin'
                   ,'Admin'
                   ,'admin'
                   ,'C8qiotBAbGg='
                   ,'admin@mail.com'
                   ,'5387459889'
                   ,'11.03.1997'
                   ,1)");
        }

        public override void Down()
        {
            DropTable("dbo.User");
        }
    }
}
