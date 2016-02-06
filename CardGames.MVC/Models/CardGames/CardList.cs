using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CardGames.MVC.Models.CardGames
{
    public abstract class CardList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Public { get; set; }

        public virtual ICollection<CardInCardList> CardInCardLists { get; set; }

        public virtual void AddCard(Card card)
        {
            var cardInList = CardInCardLists.FirstOrDefault(c => c.CardId == card.Id);
            if (cardInList == null)
            {
                CardInCardLists.Add(new CardInCardList {CardId = card.Id, CardListId = Id});
            }
            else
            {
                cardInList.Quantity++;
            }
        }
    }

    public class EditionCardList : CardList
    {
        public EditionCardList()
        {
            Public = true;
        }
    }

    public class Deck : CardList
    {
        
    }

    public class Collection : CardList
    {
        
    }
}