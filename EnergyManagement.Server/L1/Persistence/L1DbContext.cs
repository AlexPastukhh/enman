using Domain.EnergyManagement.L1;
using Microsoft.EntityFrameworkCore;
using PasswordHash = Domain.EnergyManagement.DocumentManaging.PasswordHash;

namespace EnergyManagement.Server.L1.Persistence;

public class L1DbContext : DbContext
{
    private readonly string _connectionString;

    public L1DbContext(string connectionString)
    {
        _connectionString = connectionString;
    }

    public DbSet<Account> Accounts => Set<Account>();
    public DbSet<ApplicantParty> ApplicantParties => Set<ApplicantParty>();
    public DbSet<ClientRequest> ClientRequests => Set<ClientRequest>();
    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<AgreementProposalExchange> AgreementProposalExchanges => Set<AgreementProposalExchange>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_connectionString);
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ConfigureAccounts(modelBuilder);
        ConfigureEmployees(modelBuilder);
        ConfigureApplicantParties(modelBuilder);
        ConfigureClientRequests(modelBuilder);
        ConfigureAgreementProposalExchanges(modelBuilder);
    }

    private static void ConfigureAccounts(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(account =>
        {
            account.ToTable("L1Accounts");
            account.HasKey(x => x.Id);

            account.HasDiscriminator<string>("AccountType")
                .HasValue<ClientAccount>("Client")
                .HasValue<Employee>("Employee");

            account.OwnsOne(x => x.Email, email =>
            {
                email.Property(x => x.Value)
                    .HasColumnName("Email")
                    .HasMaxLength(100)
                    .IsRequired();
            });

            account.Property(x => x.PasswordHash)
                .HasColumnName("PasswordHash")
                .HasConversion(
                    passwordHash => passwordHash.Value,
                    value => PasswordHash.ConvertFromString(value))
                .HasMaxLength(200)
                .IsRequired();

            account.Property(x => x.Role)
                .HasColumnName("Role")
                .HasConversion<string>()
                .HasMaxLength(50)
                .IsRequired();

            account.Property(x => x.IsActive)
                .HasColumnName("IsActive")
                .IsRequired();

            account.Property(x => x.CreatedAt)
                .HasColumnName("CreatedAt")
                .IsRequired();
        });
    }

    private static void ConfigureApplicantParties(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ApplicantParty>(applicantParty =>
        {
            applicantParty.ToTable("L1ApplicantParties");
            applicantParty.HasKey(x => x.Id);

            applicantParty.HasDiscriminator<string>("ApplicantPartyDiscriminator")
                .HasValue<IndividualApplicantParty>("Individual");

            applicantParty.Property(x => x.ClientAccountId)
                .HasColumnName("ClientAccountId")
                .IsRequired();

            applicantParty.Property(x => x.ApplicantPartyType)
                .HasColumnName("ApplicantPartyType")
                .HasConversion<string>()
                .HasMaxLength(50)
                .IsRequired();

            applicantParty.Property(x => x.VerificationStatus)
                .HasField("_verificationStatus")
                .HasColumnName("VerificationStatus")
                .HasConversion<string>()
                .HasMaxLength(50)
                .IsRequired();

            applicantParty.Property(x => x.IsCurrentActiveVersion)
                .HasField("_isCurrentActiveVersion")
                .HasColumnName("IsCurrentActiveVersion")
                .IsRequired();

            applicantParty.OwnsOne(x => x.Email, email =>
            {
                email.Property(x => x.Value)
                    .HasColumnName("Email")
                    .HasMaxLength(100)
                    .IsRequired();
            });

            applicantParty.OwnsOne(x => x.PhoneNumber, phoneNumber =>
            {
                phoneNumber.Property(x => x.Value)
                    .HasColumnName("PhoneNumber")
                    .HasMaxLength(50)
                    .IsRequired();
            });

            applicantParty.Property(x => x.CreatedAt)
                .HasColumnName("CreatedAt")
                .IsRequired();

            applicantParty.HasOne<ClientAccount>()
                .WithMany()
                .HasForeignKey(x => x.ClientAccountId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<IndividualApplicantParty>(individual =>
        {
            individual.OwnsOne(x => x.FullName, fullName =>
            {
                fullName.Property(x => x.FirstName)
                    .HasColumnName("FullName_FirstName")
                    .HasMaxLength(100)
                    .IsRequired();

                fullName.Property(x => x.MiddleName)
                    .HasColumnName("FullName_MiddleName")
                    .HasMaxLength(100)
                    .IsRequired();

                fullName.Property(x => x.LastName)
                    .HasColumnName("FullName_LastName")
                    .HasMaxLength(100)
                    .IsRequired();
            });
        });
    }

    private static void ConfigureClientRequests(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ClientRequest>(clientRequest =>
        {
            clientRequest.ToTable("L1ClientRequests");
            clientRequest.HasKey(x => x.Id);

            clientRequest.HasDiscriminator<string>("ClientRequestDiscriminator")
                .HasValue<ConnectionRequest>("Connection");

            clientRequest.Property(x => x.ApplicantPartyId)
                .HasColumnName("ApplicantPartyId")
                .IsRequired();

            clientRequest.Property(x => x.ClientAccountId)
                .HasColumnName("ClientAccountId")
                .IsRequired();

            clientRequest.HasIndex(x => x.ClientAccountId);

            clientRequest.Property(x => x.RequestType)
                .HasColumnName("RequestType")
                .HasConversion<string>()
                .HasMaxLength(50)
                .IsRequired();

            clientRequest.Property(x => x.Status)
                .HasColumnName("Status")
                .HasConversion<string>()
                .HasMaxLength(50)
                .IsRequired();

            clientRequest.Property(x => x.Details)
                .HasColumnName("Details")
                .HasMaxLength(3000)
                .IsRequired();

            clientRequest.OwnsOne(x => x.ObjectAddress, address =>
            {
                address.Property(x => x.PostalCode)
                    .HasColumnName("ObjectAddress_PostalCode")
                    .HasMaxLength(50)
                    .IsRequired();

                address.Property(x => x.Region)
                    .HasColumnName("ObjectAddress_Region")
                    .HasMaxLength(50)
                    .IsRequired();

                address.Property(x => x.City)
                    .HasColumnName("ObjectAddress_City")
                    .HasMaxLength(50)
                    .IsRequired();

                address.Property(x => x.Street)
                    .HasColumnName("ObjectAddress_Street")
                    .HasMaxLength(50)
                    .IsRequired();

                address.Property(x => x.House)
                    .HasColumnName("ObjectAddress_House")
                    .HasMaxLength(50)
                    .IsRequired();

                address.Property<string?>("_building")
                    .HasColumnName("ObjectAddress_Building")
                    .HasMaxLength(50)
                    .IsRequired(false);

                address.Property<string?>("_apartment")
                    .HasColumnName("ObjectAddress_Apartment")
                    .HasMaxLength(50)
                    .IsRequired(false);
            });

            clientRequest.Property(x => x.CreatedAt)
                .HasColumnName("CreatedAt")
                .IsRequired();

            clientRequest.HasOne<ClientAccount>()
                .WithMany()
                .HasForeignKey(x => x.ClientAccountId)
                .OnDelete(DeleteBehavior.Restrict);

            clientRequest.HasOne<ApplicantParty>()
                .WithMany()
                .HasForeignKey(x => x.ApplicantPartyId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<ConnectionRequest>(connectionRequest =>
        {
            connectionRequest.OwnsOne(x => x.Review, review =>
            {
                review.ToTable("L1RequestReviews");
                review.WithOwner().HasForeignKey(x => x.RequestId);
                review.HasKey(x => x.RequestId);

                review.Property(x => x.RequestId)
                    .HasColumnName("RequestId")
                    .ValueGeneratedNever();

                review.Property(x => x.Status)
                    .HasColumnName("Status")
                    .HasConversion<string>()
                    .HasMaxLength(50)
                    .IsRequired();

                review.Property(x => x.StartedByEmployeeId)
                    .HasColumnName("StartedByEmployeeId")
                    .IsRequired();

                review.Property(x => x.StartedAt)
                    .HasColumnName("StartedAt")
                    .IsRequired();

                review.Property(x => x.CompletedByEmployeeId)
                    .HasColumnName("CompletedByEmployeeId")
                    .IsRequired(false);

                review.Property(x => x.CompletedAt)
                    .HasColumnName("CompletedAt")
                    .IsRequired(false);

                review.OwnsOne(x => x.RejectionFeedback, feedback =>
                {
                    feedback.Property(x => x.Value)
                        .HasColumnName("RejectionReason")
                        .HasMaxLength(RejectionFeedback.MaxLength)
                        .IsRequired(false);
                });
            });
        });
    }


    private static void ConfigureEmployees(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>(employee =>
        {
            employee.Property(x => x.WindowsLogin)
                .HasColumnName("WindowsLogin")
                .HasMaxLength(256)
                .IsRequired(false);

            employee.HasIndex(x => x.WindowsLogin)
                .IsUnique()
                .HasFilter("[WindowsLogin] IS NOT NULL");

            employee.OwnsOne(x => x.FullName, fullName =>
            {
                fullName.Property(x => x.FirstName)
                    .HasColumnName("EmployeeFullName_FirstName")
                    .HasMaxLength(100)
                    .IsRequired(false);

                fullName.Property(x => x.MiddleName)
                    .HasColumnName("EmployeeFullName_MiddleName")
                    .HasMaxLength(100)
                    .IsRequired(false);

                fullName.Property(x => x.LastName)
                    .HasColumnName("EmployeeFullName_LastName")
                    .HasMaxLength(100)
                    .IsRequired(false);
            });
        });
    }

    private static void ConfigureAgreementProposalExchanges(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AgreementProposalExchange>(exchange =>
        {
            exchange.ToTable("L1AgreementProposalExchanges");
            exchange.HasKey(x => x.Id);

            exchange.Property(x => x.RequestId)
                .HasColumnName("RequestId")
                .IsRequired();

            exchange.Property(x => x.ClientAccountId)
                .HasColumnName("ClientAccountId")
                .IsRequired();

            exchange.HasIndex(x => x.ClientAccountId);

            exchange.Property(x => x.Status)
                .HasColumnName("Status")
                .HasConversion<string>()
                .HasMaxLength(50)
                .IsRequired();

            exchange.Property(x => x.ActiveProposalVersion)
                .HasColumnName("ActiveProposalVersion")
                .HasConversion(
                    version => version.Value,
                    value => new AgreementProposalVersion(value))
                .IsRequired();

            exchange.Property(x => x.FinalRefusedByEmployeeId)
                .HasColumnName("FinalRefusedByEmployeeId")
                .IsRequired(false);

            exchange.Property(x => x.FinalRefusedAt)
                .HasColumnName("FinalRefusedAt")
                .IsRequired(false);

            exchange.OwnsOne(x => x.FinalRefusalReason, reason =>
            {
                reason.Property(x => x.Value)
                    .HasColumnName("FinalRefusalReason")
                    .HasMaxLength(FinalRefusalReason.MaxLength)
                    .IsRequired(false);
            });

            exchange.Property(x => x.CreatedAt)
                .HasColumnName("CreatedAt")
                .IsRequired();

            exchange.HasOne<ClientRequest>()
                .WithMany()
                .HasForeignKey(x => x.RequestId)
                .OnDelete(DeleteBehavior.Restrict);

            exchange.OwnsMany(x => x.Proposals, proposal =>
            {
                proposal.ToTable("L1AgreementProposals");
                proposal.WithOwner().HasForeignKey(x => x.AgreementProposalExchangeId);
                proposal.HasKey(x => x.Id);

                proposal.Property(x => x.Id)
                    .HasColumnName("Id")
                    .ValueGeneratedOnAdd();

                proposal.Property(x => x.AgreementProposalExchangeId)
                    .HasColumnName("AgreementProposalExchangeId")
                    .IsRequired();

                proposal.Property(x => x.Version)
                    .HasColumnName("Version")
                    .HasConversion(
                        version => version.Value,
                        value => new AgreementProposalVersion(value))
                    .IsRequired();

                proposal.OwnsOne(x => x.Author, author =>
                {
                    author.Property(x => x.Sender)
                        .HasColumnName("Sender")
                        .HasConversion<string>()
                        .HasMaxLength(50)
                        .IsRequired();

                    author.Property(x => x.SenderId)
                        .HasColumnName("SenderId")
                        .IsRequired();
                });

                proposal.Property(x => x.State)
                    .HasColumnName("State")
                    .HasConversion<string>()
                    .HasMaxLength(50)
                    .IsRequired();

                proposal.OwnsOne(x => x.Document, document =>
                {
                    document.Property(x => x.StorageKey)
                        .HasColumnName("DocumentStorageKey")
                        .HasMaxLength(500)
                        .IsRequired();

                    document.Property(x => x.OriginalFileName)
                        .HasColumnName("DocumentOriginalFileName")
                        .HasMaxLength(255)
                        .IsRequired();

                    document.Property(x => x.ContentType)
                        .HasColumnName("DocumentContentType")
                        .HasMaxLength(100)
                        .IsRequired();

                    document.Property(x => x.SizeBytes)
                        .HasColumnName("DocumentSizeBytes")
                        .IsRequired();
                });

                proposal.OwnsOne(x => x.Comment, comment =>
                {
                    comment.Property(x => x.Value)
                        .HasColumnName("Comment")
                        .HasMaxLength(ProposalComment.MaxLength)
                        .IsRequired(false);
                });

                proposal.Property(x => x.CreatedAt)
                    .HasColumnName("CreatedAt")
                    .IsRequired();
            });

            exchange.Navigation(x => x.Proposals)
                .HasField("_proposals")
                .UsePropertyAccessMode(PropertyAccessMode.Field);
        });
    }
}
