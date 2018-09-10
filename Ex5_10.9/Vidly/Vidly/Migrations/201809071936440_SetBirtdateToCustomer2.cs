namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SetBirtdateToCustomer2 : DbMigration
    {
        public override void Up()
        {
            Sql("Update Customers SET Birthdate=CAST('1997-10-26' as DATE) Where Id=1");
            Sql("Update Customers SET Birthdate=CAST('1997-01-21' as DATE) Where Id=2");
        }
        
        public override void Down()
        {
        }
    }
}
