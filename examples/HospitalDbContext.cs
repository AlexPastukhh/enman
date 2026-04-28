using CSharpFunctionalExtensions;
using Hospital.proj.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Hospital.proj.Server
{
    public class HospitalDbContext:DbContext
    {
        private readonly string ConnString;
        public HospitalDbContext(string connectionString)
        {
            ConnString = connectionString;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnString);
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(ub =>
            {
                ub.ToTable("Users").HasKey(u => u.Id);

                ub.Property(u => u.Id).HasColumnName("Users Ids");

                ub.Property(u => u.Email)
                    .HasConversion(
                        e => e.Value,
                        str => Email.Create(str).Value)
                    .HasMaxLength(100);

                ub.OwnsOne(u => u.Password, pb =>
                {
                    pb.Property(p => p.Hash)
                        .HasConversion(
                            ph => ph.Value,
                            str => PasswordHash.ConvertFromString(str))
                        .HasMaxLength(100)
                        .HasColumnName("PasswordHash");

                    pb.OwnsOne<ChangePassword>("_currentPasswordChange", chb =>
                    {
                        chb.Property(ch => ch.Secret)
                            .HasColumnName("ChangePasswordSecret");

                        chb.Property(ch => ch.SubmitExpirationTime)
                            .HasConversion(
                                maybe => maybe.GetValueOrDefault(),
                                value => Maybe.From(value)
                             )
                            .HasColumnName("ChangePasswordSubmitExpires");

                        chb.Property(ch => ch.SecretExpirationTime)
                            .HasColumnName("ChangePasswordCodeExpires");
                    });
                });
                    
                ub.OwnsOne(u => u.FullName, fnb =>
                {
                    fnb.Property(fn => fn.FirstName)
                        .HasMaxLength(50);
                    fnb.Property(fn => fn.MiddleName)
                        .HasMaxLength(50);
                    fnb.Property(fn => fn.LastName)
                        .HasMaxLength(50);

                });

                ub.OwnsOne(u => u.AccountActivation, aab =>
                {
                    aab.WithOwner(a => a.User);

                    aab.Property(a => a.ActivatedAt)
                        .HasConversion(
                            maybe => maybe.GetValueOrDefault(),
                            value => Maybe.From(value));
                });

                

                ub.Property<byte[]>("Version")
                    .IsRowVersion();

            });

            //modelBuilder.Entity<Activation>(ab =>
            //{
            //    ab.ToTable("AccountActivations").HasKey(a => a.Id);

            //    ab.Property(a => a.Id).HasColumnName("ActivationsIds");

            //    ab.Property<long>("UserId");

            //    ab.Property(a => a.ActivatedAt)
            //        .HasConversion(
            //            maybe => maybe.GetValueOrDefault(),
            //            value => Maybe.From(value)
            //        );


            //    ab.Property<byte[]>("Version")
            //        .IsRowVersion();


            //});

            
            

            base.OnModelCreating(modelBuilder);
        }
        public DbSet<User> Users { get; set; }

    }
}
