using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace PlayPal.DAL
{
    public partial class EFContext : DbContext
    {
        public EFContext()
            : base("name=BoardGameRental")
        {
        }

        public virtual DbSet<BoardGame> BoardGame { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<CoreUser> CoreUser { get; set; }
        public virtual DbSet<Notification> Notification { get; set; }
        public virtual DbSet<RentedBoardGame> RentedBoardGame { get; set; }
        public virtual DbSet<Review> Review { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<UserNotification> UserNotification { get; set; }
        public virtual DbSet<UsersDetail> UsersDetail { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BoardGame>()
                .Property(e => e.Title)
                .IsUnicode(false);

            modelBuilder.Entity<BoardGame>()
                .Property(e => e.Price)
                .HasPrecision(10, 2);

            modelBuilder.Entity<BoardGame>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<BoardGame>()
                .HasMany(e => e.RentedBoardGame)
                .WithRequired(e => e.BoardGame)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<BoardGame>()
                .HasMany(e => e.Review)
                .WithRequired(e => e.BoardGame)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Category>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Category>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Category>()
                .HasMany(e => e.BoardGame)
                .WithRequired(e => e.Category)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CoreUser>()
                .Property(e => e.Username)
                .IsUnicode(false);

            modelBuilder.Entity<CoreUser>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<CoreUser>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<CoreUser>()
                .HasMany(e => e.BoardGame)
                .WithRequired(e => e.CoreUser)
                .HasForeignKey(e => e.CreatedByUserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CoreUser>()
                .HasMany(e => e.BoardGame1)
                .WithRequired(e => e.CoreUser1)
                .HasForeignKey(e => e.UpdatedByUserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CoreUser>()
                .HasMany(e => e.Category)
                .WithRequired(e => e.CoreUser)
                .HasForeignKey(e => e.CreatedByUserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CoreUser>()
                .HasMany(e => e.Category1)
                .WithRequired(e => e.CoreUser1)
                .HasForeignKey(e => e.UpdatedByUserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CoreUser>()
                .HasMany(e => e.CoreUser1)
                .WithRequired(e => e.CoreUser2)
                .HasForeignKey(e => e.CreatedByUserId);

            modelBuilder.Entity<CoreUser>()
                .HasMany(e => e.CoreUser11)
                .WithRequired(e => e.CoreUser3)
                .HasForeignKey(e => e.UpdatedByUserId);

            modelBuilder.Entity<CoreUser>()
                .HasMany(e => e.Notification)
                .WithRequired(e => e.CoreUser)
                .HasForeignKey(e => e.CreatedByUserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CoreUser>()
                .HasMany(e => e.Notification1)
                .WithRequired(e => e.CoreUser1)
                .HasForeignKey(e => e.UpdatedByUserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CoreUser>()
                .HasMany(e => e.RentedBoardGame)
                .WithRequired(e => e.CoreUser)
                .HasForeignKey(e => e.CreatedByUserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CoreUser>()
                .HasMany(e => e.RentedBoardGame1)
                .WithRequired(e => e.CoreUser1)
                .HasForeignKey(e => e.UpdatedByUserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CoreUser>()
                .HasMany(e => e.RentedBoardGame2)
                .WithRequired(e => e.CoreUser2)
                .HasForeignKey(e => e.CoreUserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CoreUser>()
                .HasMany(e => e.Review)
                .WithRequired(e => e.CoreUser)
                .HasForeignKey(e => e.CreatedByUserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CoreUser>()
                .HasMany(e => e.Review1)
                .WithRequired(e => e.CoreUser1)
                .HasForeignKey(e => e.UpdatedByUserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CoreUser>()
                .HasMany(e => e.Review2)
                .WithRequired(e => e.CoreUser2)
                .HasForeignKey(e => e.CoreUserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CoreUser>()
                .HasMany(e => e.Role1)
                .WithRequired(e => e.CoreUser1)
                .HasForeignKey(e => e.CreatedByUserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CoreUser>()
                .HasMany(e => e.Role2)
                .WithRequired(e => e.CoreUser2)
                .HasForeignKey(e => e.UpdatedByUserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CoreUser>()
                .HasMany(e => e.UserNotification)
                .WithRequired(e => e.CoreUser)
                .HasForeignKey(e => e.CoreUserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CoreUser>()
                .HasMany(e => e.UserNotification1)
                .WithRequired(e => e.CoreUser1)
                .HasForeignKey(e => e.CreatedByUserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CoreUser>()
                .HasMany(e => e.UserNotification2)
                .WithRequired(e => e.CoreUser2)
                .HasForeignKey(e => e.UpdatedByUserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CoreUser>()
                .HasMany(e => e.UsersDetail)
                .WithRequired(e => e.CoreUser)
                .HasForeignKey(e => e.CreatedByUserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CoreUser>()
                .HasOptional(e => e.UsersDetail1)
                .WithRequired(e => e.CoreUser1);

            modelBuilder.Entity<CoreUser>()
                .HasMany(e => e.UsersDetail2)
                .WithRequired(e => e.CoreUser2)
                .HasForeignKey(e => e.UpdatedByUserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Notification>()
                .Property(e => e.Title)
                .IsUnicode(false);

            modelBuilder.Entity<Notification>()
                .Property(e => e.Message)
                .IsUnicode(false);

            modelBuilder.Entity<Notification>()
                .HasMany(e => e.UserNotification)
                .WithRequired(e => e.Notification)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Review>()
                .Property(e => e.Title)
                .IsUnicode(false);

            modelBuilder.Entity<Review>()
                .Property(e => e.Comment)
                .IsUnicode(false);

            modelBuilder.Entity<Role>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Role>()
                .HasMany(e => e.CoreUser)
                .WithRequired(e => e.Role)
                .HasForeignKey(e => e.RoleId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UsersDetail>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<UsersDetail>()
                .Property(e => e.LastName)
                .IsUnicode(false);

            modelBuilder.Entity<UsersDetail>()
                .Property(e => e.Address)
                .IsUnicode(false);
        }
    }
}
