using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace ClassLibrary_LitFilmHub;

public partial class LiteratureAndFilmDbContext : IdentityDbContext<LiteratureAndFilmUser>
{
    public LiteratureAndFilmDbContext()
    {
    }   

    public LiteratureAndFilmDbContext(DbContextOptions<LiteratureAndFilmDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<Film> Films { get; set; }

    public virtual DbSet<Member> Members { get; set; }

    public virtual DbSet<Discussion> Discussions { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

        if (optionsBuilder.IsConfigured)
        {
            return;
        }

        IConfigurationBuilder builder = new ConfigurationBuilder().AddJsonFile("appsettings.json");
        IConfiguration configuration = builder.Build();
        optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Book>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Author).HasMaxLength(255);
            entity.Property(e => e.Genre).HasMaxLength(100);
            entity.Property(e => e.Isbn)
                .HasMaxLength(20)
                .HasColumnName("ISBN");
            entity.Property(e => e.Title).HasMaxLength(255);
        });


        modelBuilder.Entity<Film>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Director).HasMaxLength(255);
            entity.Property(e => e.Genre).HasMaxLength(100);
            entity.Property(e => e.Rating).HasMaxLength(50);
            entity.Property(e => e.Title).HasMaxLength(255);
        });


        modelBuilder.Entity<Member>(entity =>
        {
            entity.Property(e => e.MemberID).HasColumnName("MemberID");
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.MiddleInitial).HasMaxLength(1);
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.City).HasMaxLength(100);
            entity.Property(e => e.Country).HasMaxLength(100);
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.Username).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(255);

        });

        modelBuilder.Entity<Discussion>(entity =>
        {
            entity.Property(e => e.DiscussionID).HasColumnName("DiscussionID");
            entity.Property(e => e.Content).HasColumnType("text");

            // Define the relationship between Discussion and Member
            entity.HasOne(d => d.Member)
                .WithMany(m => m.Discussions)
                .HasForeignKey(d => d.MemberID)
                .OnDelete(DeleteBehavior.Cascade);
        });



        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
