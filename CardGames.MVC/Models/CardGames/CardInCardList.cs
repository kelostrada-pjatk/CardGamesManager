using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CardGames.MVC.Models.CardGames
{
    public class CardInCardList
    {
        public CardInCardList()
        {
            Quantity = 1;
            Number = 1;
        }
        
        public int CardId { get; set; }
        public int CardListId { get; set; }

        public int Number { get; set; }
        public int Quantity { get; set; }

        public virtual ICollection<Card> Cards { get; set; }
        public virtual ICollection<CardList> CardLists { get; set; } 
    }
}