using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnergyManagement.Server.Migrations.L1Db
{
    /// <inheritdoc />
    public partial class AddApplicantPartySubtypeRequisites : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ApplicantPartyDiscriminator",
                table: "L1ApplicantParties",
                type: "nvarchar(34)",
                maxLength: 34,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(21)",
                oldMaxLength: 21);

            migrationBuilder.AddColumn<string>(
                name: "IndividualEntrepreneurFullName_FirstName",
                table: "L1ApplicantParties",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IndividualEntrepreneurFullName_LastName",
                table: "L1ApplicantParties",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IndividualEntrepreneurFullName_MiddleName",
                table: "L1ApplicantParties",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IndividualEntrepreneur_Inn",
                table: "L1ApplicantParties",
                type: "nvarchar(12)",
                maxLength: 12,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IndividualEntrepreneur_Ogrnip",
                table: "L1ApplicantParties",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LegalEntity_Inn",
                table: "L1ApplicantParties",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LegalEntity_Kpp",
                table: "L1ApplicantParties",
                type: "nvarchar(9)",
                maxLength: 9,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LegalEntity_Ogrn",
                table: "L1ApplicantParties",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LegalEntity_OrganizationName",
                table: "L1ApplicantParties",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IndividualEntrepreneurFullName_FirstName",
                table: "L1ApplicantParties");

            migrationBuilder.DropColumn(
                name: "IndividualEntrepreneurFullName_LastName",
                table: "L1ApplicantParties");

            migrationBuilder.DropColumn(
                name: "IndividualEntrepreneurFullName_MiddleName",
                table: "L1ApplicantParties");

            migrationBuilder.DropColumn(
                name: "IndividualEntrepreneur_Inn",
                table: "L1ApplicantParties");

            migrationBuilder.DropColumn(
                name: "IndividualEntrepreneur_Ogrnip",
                table: "L1ApplicantParties");

            migrationBuilder.DropColumn(
                name: "LegalEntity_Inn",
                table: "L1ApplicantParties");

            migrationBuilder.DropColumn(
                name: "LegalEntity_Kpp",
                table: "L1ApplicantParties");

            migrationBuilder.DropColumn(
                name: "LegalEntity_Ogrn",
                table: "L1ApplicantParties");

            migrationBuilder.DropColumn(
                name: "LegalEntity_OrganizationName",
                table: "L1ApplicantParties");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicantPartyDiscriminator",
                table: "L1ApplicantParties",
                type: "nvarchar(21)",
                maxLength: 21,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(34)",
                oldMaxLength: 34);
        }
    }
}
