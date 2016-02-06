using System;
using System.Collections.Generic;
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
    }
}