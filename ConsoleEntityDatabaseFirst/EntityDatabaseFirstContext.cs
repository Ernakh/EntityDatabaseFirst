using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace ConsoleEntityDatabaseFirst
{
    public partial class EntityDatabaseFirstContext : DbContext
    {
        public EntityDatabaseFirstContext()
        {
        }

        public EntityDatabaseFirstContext(DbContextOptions<EntityDatabaseFirstContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Email> Emails { get; set; }
        public virtual DbSet<Pessoa> Pessoas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=localhost;initial Catalog=EntityDatabaseFirst;User ID=teste;password=teste;language=Portuguese;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AS");

            modelBuilder.Entity<Email>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Email1)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.PessoaId).HasColumnName("pessoaId");

                entity.HasOne(d => d.Pessoa)
                    .WithMany(p => p.Emails)
                    .HasForeignKey(d => d.PessoaId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__Emails__pessoaId__267ABA7A");
            });

            modelBuilder.Entity<Pessoa>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("nome");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
