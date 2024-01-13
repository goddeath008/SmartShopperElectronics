using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyProWeb.Migrations
{
    /// <inheritdoc />
    public partial class SeedUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            for (int i = 1;i<=100;i++)
            {
                migrationBuilder.InsertData(
                    "AspNetUsers",
                    columns: new[] {
                        "Id",
                        "UserName",
                        "Email",
                        "SecurityStamp",
                        "EmailConfirmed",
                        "PhoneNumberConfirmed",
                        "TwoFactorEnabled",
                        "LockoutEnabled",
                        "AccessFailedCount"
                    },
                    values: new object[] {
                        Guid.NewGuid().ToString(),
                        "User-"+i.ToString("D3"),
                        $"email{i.ToString("D3")}@example.com",
                         Guid.NewGuid().ToString(),
                         true,
                         false,
                         false,
                         false,
                         0
                    }
                    );
            }
        }
        
        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
