namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'42119b76-cfec-406e-a7d6-6a2504ac6bf5', N'admin@vidly.com', 0, N'AH7GU1VQLp/HZC6zW0fvjt4t4d3KCyMmDPN945b9N3aVacOjrGrTzsaOkMNQNkOrMg==', N'0003c91a-12e8-4894-8b57-b45cbf24f8c5', NULL, 0, 0, NULL, 1, 0, N'admin@vidly.com')
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'acdd396f-67aa-467c-9ad4-ee7549a340dd', N'guest@vidly.com', 0, N'AFmWXmn5xrAHJ+Gxxg6SOnu63mu9BtqtXsRO6ZvfqYE3O3BtCduZ4TZ2iovzBWFBIw==', N'8c950d26-fbec-46ae-886a-1c4f217723af', NULL, 0, 0, NULL, 1, 0, N'guest@vidly.com')
INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'2993ee93-f6cf-45ef-b473-f67925267cf9', N'CanManage')
INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'42119b76-cfec-406e-a7d6-6a2504ac6bf5', N'2993ee93-f6cf-45ef-b473-f67925267cf9')
");

        }
        
        public override void Down()
        {
        }
    }
}
