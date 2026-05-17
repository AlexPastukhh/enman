using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnergyManagement.Server.Migrations
{
    /// <inheritdoc />
    public partial class L2RequestReviewAndAgreementProposalPersistence : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "L1Accounts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Role = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    AccountType = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_L1Accounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "L1ApplicantParties",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientAccountId = table.Column<long>(type: "bigint", nullable: false),
                    ApplicantPartyType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    VerificationStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsCurrentActiveVersion = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ApplicantPartyDiscriminator = table.Column<string>(type: "nvarchar(21)", maxLength: 21, nullable: false),
                    FullName_FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    FullName_MiddleName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    FullName_LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_L1ApplicantParties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_L1ApplicantParties_L1Accounts_ClientAccountId",
                        column: x => x.ClientAccountId,
                        principalTable: "L1Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "L1ClientRequests",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicantPartyId = table.Column<long>(type: "bigint", nullable: false),
                    RequestType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Details = table.Column<string>(type: "nvarchar(3000)", maxLength: 3000, nullable: false),
                    ObjectAddress_PostalCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ObjectAddress_Region = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ObjectAddress_City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ObjectAddress_Street = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ObjectAddress_House = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ObjectAddress_Apartment = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ObjectAddress_Building = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ClientRequestDiscriminator = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_L1ClientRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_L1ClientRequests_L1ApplicantParties_ApplicantPartyId",
                        column: x => x.ApplicantPartyId,
                        principalTable: "L1ApplicantParties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "L1AgreementProposalExchanges",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestId = table.Column<long>(type: "bigint", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ActiveProposalVersion = table.Column<int>(type: "int", nullable: false),
                    FinalRefusedByEmployeeId = table.Column<long>(type: "bigint", nullable: true),
                    FinalRefusedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    FinalRefusalReason = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_L1AgreementProposalExchanges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_L1AgreementProposalExchanges_L1ClientRequests_RequestId",
                        column: x => x.RequestId,
                        principalTable: "L1ClientRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "L1RequestReviews",
                columns: table => new
                {
                    RequestId = table.Column<long>(type: "bigint", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    StartedByEmployeeId = table.Column<long>(type: "bigint", nullable: false),
                    StartedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CompletedByEmployeeId = table.Column<long>(type: "bigint", nullable: true),
                    CompletedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    RejectionReason = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_L1RequestReviews", x => x.RequestId);
                    table.ForeignKey(
                        name: "FK_L1RequestReviews_L1ClientRequests_RequestId",
                        column: x => x.RequestId,
                        principalTable: "L1ClientRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "L1AgreementProposals",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AgreementProposalExchangeId = table.Column<long>(type: "bigint", nullable: false),
                    Version = table.Column<int>(type: "int", nullable: false),
                    Sender = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SenderId = table.Column<long>(type: "bigint", nullable: false),
                    State = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DocumentStorageKey = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    DocumentOriginalFileName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    DocumentContentType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DocumentSizeBytes = table.Column<long>(type: "bigint", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_L1AgreementProposals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_L1AgreementProposals_L1AgreementProposalExchanges_AgreementProposalExchangeId",
                        column: x => x.AgreementProposalExchangeId,
                        principalTable: "L1AgreementProposalExchanges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_L1AgreementProposalExchanges_RequestId",
                table: "L1AgreementProposalExchanges",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_L1AgreementProposals_AgreementProposalExchangeId",
                table: "L1AgreementProposals",
                column: "AgreementProposalExchangeId");

            migrationBuilder.CreateIndex(
                name: "IX_L1ApplicantParties_ClientAccountId",
                table: "L1ApplicantParties",
                column: "ClientAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_L1ClientRequests_ApplicantPartyId",
                table: "L1ClientRequests",
                column: "ApplicantPartyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "L1AgreementProposals");

            migrationBuilder.DropTable(
                name: "L1RequestReviews");

            migrationBuilder.DropTable(
                name: "L1AgreementProposalExchanges");

            migrationBuilder.DropTable(
                name: "L1ClientRequests");

            migrationBuilder.DropTable(
                name: "L1ApplicantParties");

            migrationBuilder.DropTable(
                name: "L1Accounts");
        }
    }
}
