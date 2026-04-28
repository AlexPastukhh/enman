using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnergyManagement.Server.Migrations
{
    /// <inheritdoc />
    public partial class computedonupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "_password_isPasswordSet",
                table: "Clients",
                type: "bit",
                nullable: true,
                computedColumnSql: "CAST(CASE WHEN [PasswordHash] IS NULL THEN 0 ELSE 1 END AS BIT)",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true,
                oldComputedColumnSql: "CASE WHEN [PasswordHash] IS NULL THEN 0 ELSE 1 END");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "_password_isPasswordSet",
                table: "Clients",
                type: "bit",
                nullable: true,
                computedColumnSql: "CASE WHEN [PasswordHash] IS NULL THEN 0 ELSE 1 END",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true,
                oldComputedColumnSql: "CAST(CASE WHEN [PasswordHash] IS NULL THEN 0 ELSE 1 END AS BIT)");
        }
    }
}
