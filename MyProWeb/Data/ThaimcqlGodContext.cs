using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using MyProWeb.Models;
using MyProWeb.Models.Domain;

namespace MyProWeb.Data;

public partial class ThaimcqlGodContext : DbContext
{
    public ThaimcqlGodContext()
    {
    }

    public ThaimcqlGodContext(DbContextOptions<ThaimcqlGodContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Brand> Brands { get; set; }

    public virtual DbSet<CartItem> CartItems { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Favorite> Favorites { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=sql.bsite.net\\MSSQL2016; Database=thaimcql_GOD;User ID=thaimcql_god;Password=admin;Trusted_Connection=False;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Brand>(entity =>
        {
            entity.HasKey(e => e.Idbrand).HasName("PK__Brands__A8EBD9B7884CEE47");

            entity.Property(e => e.Idbrand).HasColumnName("IDBrand");
            entity.Property(e => e.ImgBrand).HasColumnType("text");
            entity.Property(e => e.NameBrand)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<CartItem>(entity =>
        {
            entity.HasKey(e => e.Idcart).HasName("PK__CartItem__43A2A4D20A88333B");

            entity.Property(e => e.Idcart).HasColumnName("IDCart");
            entity.Property(e => e.Idproduct).HasColumnName("IDProduct");

            entity.HasOne(d => d.IdproductNavigation).WithMany(p => p.CartItems)
                .HasForeignKey(d => d.Idproduct)
                .HasConstraintName("FK__CartItems__IDPro__48CFD27E");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Idcate).HasName("PK__Category__43A295268B0F822E");

            entity.ToTable("Category");

            entity.Property(e => e.Idcate).HasColumnName("IDCate");
            entity.Property(e => e.ImgCate).HasColumnType("text");
            entity.Property(e => e.NameCate)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Favorite>(entity =>
        {
            entity.HasKey(e => e.Idfavo).HasName("PK__Favorite__2067BDF391E667DA");

            entity.Property(e => e.Idfavo).HasColumnName("IDFavo");
            entity.Property(e => e.Idpro).HasColumnName("IDPro");

            entity.HasOne(d => d.IdproNavigation).WithMany(p => p.Favorites)
                .HasForeignKey(d => d.Idpro)
                .HasConstraintName("FK__Favorites__IDPro__4BAC3F29");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Idpro).HasName("PK__Products__98F92859F5A529FA");

            entity.Property(e => e.Idpro).HasColumnName("IDPro");
            entity.Property(e => e.Idbrand).HasColumnName("IDBrand");
            entity.Property(e => e.Idcate).HasColumnName("IDCate");
            entity.Property(e => e.Iduser).HasColumnName("IDUser");
            entity.Property(e => e.ImgPro).HasColumnType("text");
            entity.Property(e => e.NamePro)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdbrandNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.Idbrand)
                .HasConstraintName("FK__Products__IDBran__44FF419A");

            entity.HasOne(d => d.IdcateNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.Idcate)
                .HasConstraintName("FK__Products__IDCate__440B1D61");

            entity.HasOne(d => d.IduserNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.Iduser)
                .HasConstraintName("FK__Products__IDUser__45F365D3");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Iduser).HasName("PK__Users__EAE6D9DF5EC6F69A");

            entity.Property(e => e.Iduser).HasColumnName("IDUser");
            entity.Property(e => e.NameUser)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PassUser)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
