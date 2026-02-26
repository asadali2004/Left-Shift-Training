using System;
using System.Collections.Generic;
using DBFirstDemo1.Models;
using Microsoft.EntityFrameworkCore;

namespace DBFirstDemo1.Data;

public partial class FoodieAppDbContext : DbContext
{
    public FoodieAppDbContext()
    {
    }

    public FoodieAppDbContext(DbContextOptions<FoodieAppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Restaurants> Restaurants { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=EFDemo;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Restaurants>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Restaura__3214EC0751DD00BF");

            entity.Property(e => e.City).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(150);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
