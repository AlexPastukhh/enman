using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnergyManagement.Server.Migrations
{
    /// <inheritdoc />
    public partial class normalpasswordwithbackingfield : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "_password_isPasswordSet",
                table: "Clients");

            migrationBuilder.AlterColumn<string>(
                name: "PasswordHash",
                table: "Clients",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PasswordHash",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "_password_isPasswordSet",
                table: "Clients",
                type: "bit",
                nullable: true,
                computedColumnSql: "CAST(CASE WHEN [PasswordHash] IS NULL THEN 0 ELSE 1 END AS BIT)");
        }
    }
}
