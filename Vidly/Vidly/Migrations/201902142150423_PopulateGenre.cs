namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateGenre : DbMigration
    {
        public override void Up()
        {
            Sql("SET IDENTITY_INSERT Genres ON");
            Sql("Insert into Genres (Id,Name) values (1,'Action')");
            Sql("Insert into Genres (Id,Name) values (2,'Drama')");
            Sql("Insert into Genres (Id,Name) values (3,'Comedy')");
            Sql("Insert into Genres (Id,Name) values (4,'Si-Fi')");
            Sql("Insert into Genres (Id,Name) values (5,'Romantic')");
            
        }
        
        public override void Down()
        {
        }
    }
}
