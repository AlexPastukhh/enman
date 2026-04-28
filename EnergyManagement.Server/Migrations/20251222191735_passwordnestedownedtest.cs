using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnergyManagement.Server.Migrations
{
    /// <inheritdoc />
    public partial class passwordnestedownedtest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Clients");

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "_password_isPasswordSet",
                table: "Clients",
                type: "bit",
                nullable: true,
                computedColumnSql: "CASE WHEN [PasswordHash] IS NULL THEN 0 ELSE 1 END");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "_password_isPasswordSet",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Clients");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Clients",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);
        }
    }
}
