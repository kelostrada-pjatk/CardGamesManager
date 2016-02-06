using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CardGames.MVC.Models.CardGames
{
    public class Edition
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ReleaseYear { get; set; }

        public int GameId { get; set; }
        public virtual Game Game { get; set; }

        public int EditionCardListId { get; set; }
        public virtual EditionCardList EditionCardList { get; set; }
    }
}