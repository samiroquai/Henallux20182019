using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ComparaisonEFCoreADONet
{
    public partial class StackOverflow2Context : DbContext
    {

        //Le contexte attend un ensemble d'options. Vous pouvez configurer ces dernières à l'aide d'une instance de DbContextOptionsBuilder<StackOverflow2Context>.
        // var builder=new DbContextOptionsBuilder<StackOverflow2Context>();
        // builder.UseSqlServer(config.GetConnectionString(CONNECTION_STRING_KEY));
        // using (var context = new StackOverflow2Context(builder.Options))
        // {
        //     context.Posts.Take(NUMBER_OF_RECORDS).ToList();
        // }
        public StackOverflow2Context(DbContextOptions<StackOverflow2Context> options)
            : base(options)
        {
            
        }

        public virtual DbSet<Badges> Badges { get; set; }
        public virtual DbSet<Comments> Comments { get; set; }
        public virtual DbSet<LinkTypes> LinkTypes { get; set; }
        public virtual DbSet<PostLinks> PostLinks { get; set; }
        public virtual DbSet<Posts> Posts { get; set; }
        public virtual DbSet<PostTypes> PostTypes { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Votes> Votes { get; set; }
        public virtual DbSet<VoteTypes> VoteTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Badges>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(40);
            });

            modelBuilder.Entity<Comments>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.Text)
                    .IsRequired()
                    .HasMaxLength(700);
            });

            modelBuilder.Entity<LinkTypes>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PostLinks>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreationDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<Posts>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Body).IsRequired();

                entity.Property(e => e.ClosedDate).HasColumnType("datetime");

                entity.Property(e => e.CommunityOwnedDate).HasColumnType("datetime");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.LastActivityDate).HasColumnType("datetime");

                entity.Property(e => e.LastEditDate).HasColumnType("datetime");

                entity.Property(e => e.LastEditorDisplayName).HasMaxLength(40);

                entity.Property(e => e.Tags).HasMaxLength(150);

                entity.Property(e => e.Title).HasMaxLength(250);
            });

            modelBuilder.Entity<PostTypes>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.DisplayName)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.Property(e => e.EmailHash).HasMaxLength(40);

                entity.Property(e => e.LastAccessDate).HasColumnType("datetime");

                entity.Property(e => e.Location).HasMaxLength(100);

                entity.Property(e => e.WebsiteUrl).HasMaxLength(200);
            });

            modelBuilder.Entity<Votes>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreationDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<VoteTypes>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });
        }
    }
}
