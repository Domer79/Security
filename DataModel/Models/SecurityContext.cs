using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using SystemTools;

namespace DataModel.Models
{
    public class SecurityContext : DbContext
    {
        public SecurityContext()
            : base(ApplicationCustomizer.ConnectionString)
        {
        }

        public SecurityContext(string connectionString)
            : base(connectionString)
        {
            
        }

        public virtual DbSet<SGrant> SGrant { get; set; }
        public virtual DbSet<SRole> SRole { get; set; }
        public virtual DbSet<AccessType> AccessType { get; set; }
        public virtual DbSet<Member> Member { get; set; }
        public virtual DbSet<SecObject> SecObject { get; set; }
        public virtual DbSet<SGroup> SGroup { get; set; }
        public virtual DbSet<SUser> SUser { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SRole>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<AccessType>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<AccessType>()
                .HasMany(e => e.SGrant)
                .WithRequired(e => e.AccessType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Member>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Member>()
                .HasOptional(e => e.SGroup)
                .WithRequired(e => e.Member)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Member>()
                .HasMany(e => e.SRole)
                .WithMany(e => e.Member)
                .Map(m => m.ToTable("MemberRole", "sec").MapLeftKey("IdMember").MapRightKey("idRole"));

            modelBuilder.Entity<SRole>()
                .HasMany(e => e.Member)
                .WithMany(e => e.SRole)
                .Map(m => m.ToTable("MemberTole", "sec").MapLeftKey("idRole").MapRightKey("idMember"));

            modelBuilder.Entity<SecObject>()
                .Property(e => e.ObjectName)
                .IsUnicode(false);

            modelBuilder.Entity<SecObject>()
                .Property(e => e.Type1)
                .IsUnicode(false);

            modelBuilder.Entity<SecObject>()
                .Property(e => e.Type2)
                .IsUnicode(false);

            modelBuilder.Entity<SecObject>()
                .Property(e => e.Type3)
                .IsUnicode(false);

            modelBuilder.Entity<SecObject>()
                .Property(e => e.Type4)
                .IsUnicode(false);

            modelBuilder.Entity<SecObject>()
                .Property(e => e.Type5)
                .IsUnicode(false);

            modelBuilder.Entity<SecObject>()
                .Property(e => e.Type6)
                .IsUnicode(false);

            modelBuilder.Entity<SecObject>()
                .Property(e => e.Type7)
                .IsUnicode(false);

            modelBuilder.Entity<SGroup>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<SUser>()
                .Property(e => e.Sid)
                .IsUnicode(false);

            modelBuilder.Entity<SUser>()
                .Property(e => e.DisplayName)
                .IsUnicode(false);

            modelBuilder.Entity<SUser>()
                .Property(e => e.Email)
                .IsUnicode(false);
        }
    }
}
