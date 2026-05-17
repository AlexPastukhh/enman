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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
