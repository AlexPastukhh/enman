using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnergyManagement.Server.Migrations.L1Db
{
    /// <inheritdoc />
    public partial class ModelEmployeeAsAccountSubtype : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EmployeeFullName_FirstName",
                table: "L1Accounts",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmployeeFullName_LastName",
                table: "L1Accounts",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmployeeFullName_MiddleName",
                table: "L1Accounts",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WindowsLogin",
                table: "L1Accounts",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_L1Accounts_WindowsLogin",
                table: "L1Accounts",
                column: "WindowsLogin",
                unique: true,
                filter: "[WindowsLogin] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_L1Accounts_WindowsLogin",
                table: "L1Accounts");

            migrationBuilder.DropColumn(
                name: "WindowsLogin",
                table: "L1Accounts");

            migrationBuilder.DropColumn(
                name: "EmployeeFullName_FirstName",
                table: "L1Accounts");

            migrationBuilder.DropColumn(
                name: "EmployeeFullName_LastName",
                table: "L1Accounts");

            migrationBuilder.DropColumn(
                name: "EmployeeFullName_MiddleName",
                table: "L1Accounts");
        }
    }
}
