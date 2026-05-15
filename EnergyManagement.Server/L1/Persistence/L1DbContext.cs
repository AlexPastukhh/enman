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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_connectionString);
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ConfigureAccounts(modelBuilder);
        ConfigureApplicantParties(modelBuilder);
        ConfigureClientRequests(modelBuilder);
    }

    private static void ConfigureAccounts(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(account =>
        {
            account.ToTable("L1Accounts");
            account.HasKey(x => x.Id);

            account.HasDiscriminator<string>("AccountType")
                .HasValue<ClientAccount>("Client");

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

            clientRequest.HasOne<ApplicantParty>()
                .WithMany()
                .HasForeignKey(x => x.ApplicantPartyId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}
