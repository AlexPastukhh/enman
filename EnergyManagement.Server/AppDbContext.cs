Dusing System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Domain.EnergyManagement.DocumentManaging;
using Microsoft.EntityFrameworkCore;

namespace EnergyManagement.Server
{
    public class AppDbContext:DbContext
    {
        private readonly string _connString;
        public AppDbContext(string connectionString)
        {
            _connString = connectionString;
        }
        //public AppDbContext(){}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connString);
            base.OnConfiguring(optionsBuilder);
        }
        
        protected override void OnModelCreating(ModelBuilder mb)
        {
            mb.Entity<Client>(cb=>
            {
                cb.ToTable("Clients").HasKey(c=>c.Id);
                cb.Property(c=>c.Id)
                    .HasColumnName("ClientId")
                    .HasMaxLength(100);
                    
                cb.OwnsOne(c=>c.Email,eb=>
                {
                    eb.Property(e=>e.Value)
                        .HasColumnName("Email")
                        .HasMaxLength(100)
                        .IsRequired();
                });
                
                cb.OwnsOne(c=>c.Password,pb=>
                {
                    pb.Property(p=>p.Hash)
                        .HasColumnName("PasswordHash")
                        .HasMaxLength(100)
                        .IsRequired();
                        
                });
                
            }); 
            
            mb.Entity<IndividualClient>(ib=>
            {
                ib.ToTable("IndividualClients");
                
                ib.OwnsOne<FullName>("_fullName",b=>
                {
                    b.Property(fn=>fn.FirstName)
                        .HasColumnName("FirstName")
                        .HasMaxLength(100)
                        .IsRequired(false);
                    b.Property(fn=>fn.MiddleName)
                        .HasColumnName("MiddleName")
                        .HasMaxLength(100)
                        .IsRequired(false);
                    b.Property(fn=>fn.LastName)
                        .HasColumnName("LastName")
                        .HasMaxLength(100)
                        .IsRequired(false);
                });
                
                ib.OwnsOne<PhoneNumber>("_phoneNumber",pb=>
                {
                    pb.Property(ph=>ph.Value)
                        .HasColumnName("PhoneNumber")
                        .IsRequired(false);
                });

                
                ib.OwnsOne<PassportData>("_passportData",b=>
                {
                    b.Property(pd=>pd.Series)
                        .HasColumnName("Series")
                        .HasMaxLength(50)
                        .IsRequired(false);
                    b.Property(pd=>pd.Number)
                        .HasColumnName("Number")
                        .HasMaxLength(50)
                        .IsRequired(false);
                    b.Property(pd=>pd.IssuedBy)
                        .HasColumnName("IssuedBy")
                        .HasMaxLength(50)
                        .IsRequired(false);
                    b.Property(pd=>pd.IssuedAtTime)
                        .HasColumnName("IssuedAt")
                        .HasMaxLength(50)
                        .IsRequired(false);
                });
                
                ib.HasMany(i=>i.ClientRequests)
                    .WithOne(r=>r.Client)
                    .HasForeignKey("IndividualClientId");
                // ib.HasMany(i=>i.ConnectedObjects).WithOne();
            });
            
            mb.Entity<Manager>(mb=>
            {
                mb.ToTable("Managers").HasKey(m=>m.Id);
                
                mb.Property(m=>m.Password)
                    .HasColumnName("Password")
                    .HasConversion(
                        password=>password.Hash,
                        str=>Password.ConvertFromString(str))
                    .HasMaxLength(100)
                    .IsRequired();
                
                mb.Property(m=>m.Email)
                    .HasColumnName("Email")
                    .HasConversion(
                        email=>email.Value,
                        str=>Email.Create(str).Value)
                    .HasMaxLength(100)
                    .IsRequired();
            });
            
            mb.Entity<ClientRequest>(rb=>
            {
                rb.ToTable("Requests").HasKey(r=>r.Id);
                rb.HasDiscriminator<string>("ClientTypeDiscriminator")
                    .HasValue<IndividualRequest>("IndividualClientRequest");
                
                rb.Property(r=>r.Type)
                    .HasColumnName("RequestType")
                    .HasMaxLength(100)
                    .IsRequired();
                    
                rb.Property(r=>r.RequestDateTime)
                    .HasColumnName("RequestDateTime")
                    .HasMaxLength(100)
                    .IsRequired();
                    
                rb.Property(r=>r.RequestDetails)
                    .HasColumnName("RequestDetails")
                    .HasMaxLength(3000)
                    .IsRequired();
                    
                rb.HasMany(r=>r.Reviews)
                    .WithOne()
                    .HasForeignKey(rev=>rev.RequestId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
            
            mb.Entity<IndividualRequest>(rb=>
            {
                rb.ToTable("Requests");
                rb.HasOne(r=>r.Client)
                    .WithMany(c=>c.ClientRequests)
                    .HasForeignKey("IdividualClientId")
                    .OnDelete(DeleteBehavior.Cascade);
                    
                rb.OwnsOne(ir=>ir.Address,ab=>
                {
                    ab.Property(a=>a.PostalCode)
                        .HasColumnName("PostalCode")
                        .HasMaxLength(50)
                        .IsRequired();
                    
                    ab.Property(a=>a.Region)
                        .HasColumnName("Region")
                        .HasMaxLength(50)
                        .IsRequired();
                    
                    ab.Property(a=>a.City)
                        .HasColumnName("City")
                        .HasMaxLength(50)
                        .IsRequired();
                    
                    ab.Property(a=>a.Street)
                        .HasColumnName("Street")
                        .HasMaxLength(50)
                        .IsRequired();
                        
                    ab.Property(a=>a.House)
                        .HasColumnName("House")
                        .HasMaxLength(50)
                        .IsRequired();
                        
                      // ✅ Map to backing fields instead of properties
                    ab.Property<string?>("_building")
                        .HasColumnName("Building")
                        .IsRequired(false)
                        .HasMaxLength(50);
                        
                    ab.Property<string?>("_apartment")
                        .HasColumnName("Apartment")
                        .IsRequired(false)
                        .HasMaxLength(50);
                });
                
            });
            
            mb.Entity<RequestReview>(rb=>
            {
                rb.ToTable("RequestReviews")
                    .HasKey(r=>r.Id);
                
                rb.Property(r=>r.Id)
                    .HasColumnName("ReviewId")
                    .HasMaxLength(50);
                    
                rb.Property(r=>r.RequestId)
                    .HasColumnName("RequestId")
                    .HasMaxLength(50)
                    .IsRequired();
                    
                rb.Property(r=>r.ReviewDateTime)
                    .HasColumnName("ReviewTime")
                    .HasMaxLength(50)
                    .IsRequired();
                    
                rb.Property(r=>r.Commentary)
                    .HasColumnName("ReviewCommentary")
                    .HasMaxLength(3000)
                    .IsRequired(false);
                    
                rb.Property(r=>r.IsApproved)
                    .HasColumnName("IsApproved")
                    .HasMaxLength(50)
                    .IsRequired();
            });
            
            
            base.OnModelCreating(mb);
            // Additional model configuration can go here
            
        }
    }
}