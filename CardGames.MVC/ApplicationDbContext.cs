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