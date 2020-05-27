using Microsoft.EntityFrameworkCore.Migrations;

namespace Customer.Persistence.Database.Migrations
{
    public partial class dev_001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Customer");

            migrationBuilder.RenameTable(
                name: "Clients",
                schema: "Client",
                newName: "Clients",
                newSchema: "Customer");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Client");

            migrationBuilder.RenameTable(
                name: "Clients",
                schema: "Customer",
                newName: "Clients",
                newSchema: "Client");
        }
    }
}
