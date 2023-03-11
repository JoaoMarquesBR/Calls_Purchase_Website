using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DAL;

public partial class TheFactory_Context : DbContext
{
    public TheFactory_Context()
    {
    }

    public TheFactory_Context(DbContextOptions<TheFactory_Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Form> Forms { get; set; }

    public virtual DbSet<Purchase> Purchases { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\ProjectModels;Database=TheFactoryWebSite;Persist Security Info=True;Encrypt=true;Trusted_Connection=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.accountID).HasName("PK__Account__F267253E500684FD");

            entity.ToTable("Account");

            entity.Property(e => e.accountName)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.groupPermission)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.password)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.username)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Form>(entity =>
        {
            entity.HasKey(e => e.formID).HasName("PK__Form__51BCB7CB708D91F1");

            entity.ToTable("Form");

            entity.Property(e => e.callDate).HasColumnType("date");
            entity.Property(e => e.callDesc)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.companyName)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.followUp)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.issueSolved)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.repName)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.timeLength)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.account).WithMany(p => p.Forms)
                .HasForeignKey(d => d.accountID)
                .HasConstraintName("forms_accountID_FK");
        });

        modelBuilder.Entity<Purchase>(entity =>
        {
            entity.HasKey(e => e.Purchase_ID).HasName("PK__Purchase__543E6DA3270D302A");

            entity.ToTable("Purchase");

            entity.Property(e => e.net).HasColumnType("money");
            entity.Property(e => e.productPrice).HasColumnType("money");
            entity.Property(e => e.purchaseDate).HasColumnType("date");
            entity.Property(e => e.reference).HasMaxLength(20);
            entity.Property(e => e.status)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.supplier).HasMaxLength(20);
            entity.Property(e => e.tax).HasColumnType("money");
            entity.Property(e => e.totalAfterTax).HasColumnType("money");

            entity.HasOne(d => d.account).WithMany(p => p.Purchases)
                .HasForeignKey(d => d.accountID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Purchase_Buyer_ID");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
