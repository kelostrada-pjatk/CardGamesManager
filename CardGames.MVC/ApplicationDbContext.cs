using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using CardGames.MVC.Models;
using CardGames.MVC.Models.CardGames;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CardGames.MVC
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public virtual DbSet<Game> Games { get; set; }
        public virtual DbSet<Edition> Editions { get; set; }
        public virtual DbSet<CardList> CardLists { get; set; }
        public virtual DbSet<Card> Cards { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CardInCardList>()
                .HasKey(c => new { c.CardId, c.CardListId });

            modelBuilder.Entity<Card>()
                .HasMany(c => c.CardInCardLists)
                .WithRequired()
                .HasForeignKey(c => c.CardId);

            modelBuilder.Entity<CardList>()
                .HasMany(c => c.CardInCardLists)
                .WithRequired()
                .HasForeignKey(c => c.CardListId);

            base.OnModelCreating(modelBuilder);
        }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}