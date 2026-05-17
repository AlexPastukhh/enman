using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnergyManagement.Server.Migrations.L1Db
{
    /// <inheritdoc />
    public partial class AddAgreementExchangeClientAccountId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ClientAccountId",
                table: "L1AgreementProposalExchanges",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_L1AgreementProposalExchanges_ClientAccountId",
                table: "L1AgreementProposalExchanges",
                column: "ClientAccountId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_L1AgreementProposalExchanges_ClientAccountId",
                table: "L1AgreementProposalExchanges");

            migrationBuilder.DropColumn(
                name: "ClientAccountId",
                table: "L1AgreementProposalExchanges");
        }
    }
}
