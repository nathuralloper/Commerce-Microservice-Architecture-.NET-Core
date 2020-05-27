using Microsoft.EntityFrameworkCore.Migrations;

namespace Customer.Persistence.Database.Migrations
{
    public partial class Initialize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Client");

            migrationBuilder.CreateTable(
                name: "Clients",
                schema: "Client",
                columns: table => new
                {
                    ClientId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.ClientId);
                });

            migrationBuilder.InsertData(
                schema: "Client",
                table: "Clients",
                columns: new[] { "ClientId", "Name" },
                values: new object[,]
                {
                    { 1, "Juan Jose Gonzales Mejias 1" },
                    { 2, "Juan Jose Gonzales Mejias 2" },
                    { 3, "Juan Jose Gonzales Mejias 3" },
                    { 4, "Juan Jose Gonzales Mejias 4" },
                    { 5, "Juan Jose Gonzales Mejias 5" },
                    { 6, "Juan Jose Gonzales Mejias 6" },
                    { 7, "Juan Jose Gonzales Mejias 7" },
                    { 8, "Juan Jose Gonzales Mejias 8" },
                    { 9, "Juan Jose Gonzales Mejias 9" },
                    { 10, "Juan Jose Gonzales Mejias 10" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clients_ClientId",
                schema: "Client",
                table: "Clients",
                column: "ClientId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clients",
                schema: "Client");
        }
    }
}
