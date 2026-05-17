using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnergyManagement.Server.Migrations.L1Db
{
    /// <inheritdoc />
    public partial class AddClientAccountIdToL1ClientRequests : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ClientAccountId",
                table: "L1ClientRequests",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.Sql("""
                UPDATE requests
                SET ClientAccountId = applicantParties.ClientAccountId
                FROM L1ClientRequests AS requests
                INNER JOIN L1ApplicantParties AS applicantParties
                    ON requests.ApplicantPartyId = applicantParties.Id
                """);

            migrationBuilder.CreateIndex(
                name: "IX_L1ClientRequests_ClientAccountId",
                table: "L1ClientRequests",
                column: "ClientAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_L1ClientRequests_L1Accounts_ClientAccountId",
                table: "L1ClientRequests",
                column: "ClientAccountId",
                principalTable: "L1Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_L1ClientRequests_L1Accounts_ClientAccountId",
                table: "L1ClientRequests");

            migrationBuilder.DropIndex(
                name: "IX_L1ClientRequests_ClientAccountId",
                table: "L1ClientRequests");

            migrationBuilder.DropColumn(
                name: "ClientAccountId",
                table: "L1ClientRequests");
        }
    }
}
