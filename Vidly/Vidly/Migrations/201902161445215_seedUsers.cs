namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class seedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'487f99a1-016c-4eb4-ad31-3d0236208d6f', N'admin@vidly.com', 0, N'AM/CPPCLoyz5gUf17KYnz7wV3j0lfSfiJasEk0FHaJtlg38DUksH8gesCOas1Fd9Hw==', N'51020007-f67b-4672-ba2f-fa12d4b7058e', NULL, 0, 0, NULL, 1, 0, N'admin@vidly.com')
                INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'59bfbcbd-e9f1-4dcf-b892-8007309b05e8', N'guest@vidly.com', 0, N'AInIg8zDjAPhoB+qqH32+ShM7z/a6WtT7YAGC4mcQmU+ztv3tWlo7cBoMHqan0Tuyg==', N'349b2144-8a8d-4aff-8866-9a8316b4b9e5', NULL, 0, 0, NULL, 1, 0, N'guest@vidly.com')"
            );

            Sql(@"INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'a3d8a0f1-c383-4cd3-b24b-42eaf83475ad', N'CanManageMovies')");

            Sql(@"INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'487f99a1-016c-4eb4-ad31-3d0236208d6f', N'a3d8a0f1-c383-4cd3-b24b-42eaf83475ad')");
        }
        
        public override void Down()
        {
        }
    }
}
