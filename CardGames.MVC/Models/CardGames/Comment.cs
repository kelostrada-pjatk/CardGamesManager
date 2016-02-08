using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CardGames.MVC.Models.CardGames
{
    public class Comment
    {
        public int Id { get; set; }
        public DateTime DateAdded { get; set; }
        public string Content { get; set; }

        public int DeckId { get; set; }
        public virtual Deck Deck { get; set; }

        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}