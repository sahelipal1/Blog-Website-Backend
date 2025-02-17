using System;
using System.Collections.Generic;
using Blog_website.DAL.Entity;
using Microsoft.EntityFrameworkCore;

namespace Blog_website.DAL.DBContext;

public partial class ApplicationDbtContext : DbContext
{
    public ApplicationDbtContext()
    {
    }

    public ApplicationDbtContext(DbContextOptions<ApplicationDbtContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Post> Posts { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Database=Blog;Username=postgres;Password=nicdatabase");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.Entity<User>().HasQueryFilter(u => !u.IsDeleted);


        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("category_pkey");
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("posts_pkey");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.IsPublished).HasDefaultValue(true);

            entity.HasOne(d => d.CategoryNavigation).WithMany(p => p.Posts)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_category");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Posts).HasConstraintName("fk_created_by");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_pkey");

            entity.Property(e => e.Createddate).HasDefaultValueSql("CURRENT_DATE");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Updateddate).HasDefaultValueSql("CURRENT_DATE");

            entity.HasOne(d => d.CreatedbyNavigation).WithMany(p => p.InverseCreatedbyNavigation).HasConstraintName("fk_createdby");

            entity.HasOne(d => d.UpdatedbyNavigation).WithMany(p => p.InverseUpdatedbyNavigation).HasConstraintName("fk_updatedby");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
