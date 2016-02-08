using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CardGames.MVC.Models.CardGames
{
    public class Card
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<CardInCardList> CardInCardLists { get; set; }

        [NotMapped]
        public Edition Edition
        {
            get
            {
                return ((EditionCardList) CardInCardLists.First(cl => cl.CardList is EditionCardList).CardList).Edition;
            }
        }
    }
}