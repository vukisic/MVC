namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SetBirtdateToCustomer : DbMigration
    {
        public override void Up()
        {
            Sql("Update Customers SET Birthdate=CAST('1997-10-26' as DATETIME) Where Id=1");
            Sql("Update Customers SET Birthdate=CAST('1997-01-21' as DATETIME) Where Id=2");
        }
        
        public override void Down()
        {
        }
    }
}
