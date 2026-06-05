using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnergyManagement.Server.Migrations.L1Db
{
    /// <inheritdoc />
    public partial class RenameL1DatabaseTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "L1Accounts",
                newName: "Accounts");

            migrationBuilder.RenameTable(
                name: "L1ApplicantParties",
                newName: "ApplicantParties");

            migrationBuilder.RenameTable(
                name: "L1ClientRequests",
                newName: "ClientRequests");

            migrationBuilder.RenameTable(
                name: "L1RequestReviews",
                newName: "RequestReviews");

            migrationBuilder.RenameTable(
                name: "L1AgreementProposalExchanges",
                newName: "AgreementProposalExchanges");

            migrationBuilder.RenameTable(
                name: "L1AgreementProposals",
                newName: "AgreementProposals");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "AgreementProposals",
                newName: "L1AgreementProposals");

            migrationBuilder.RenameTable(
                name: "AgreementProposalExchanges",
                newName: "L1AgreementProposalExchanges");

            migrationBuilder.RenameTable(
                name: "RequestReviews",
                newName: "L1RequestReviews");

            migrationBuilder.RenameTable(
                name: "ClientRequests",
                newName: "L1ClientRequests");

            migrationBuilder.RenameTable(
                name: "ApplicantParties",
                newName: "L1ApplicantParties");

            migrationBuilder.RenameTable(
                name: "Accounts",
                newName: "L1Accounts");
        }
    }
}
