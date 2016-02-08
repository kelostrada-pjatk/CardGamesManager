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

        public virtual CardInCardList AddCard(Card card)
        {
            var cardInList = CardInCardLists.FirstOrDefault(c => c.CardId == card.Id);
            if (cardInList == null)
            {
                cardInList = new CardInCardList {Card = card, CardList = this};
                CardInCardLists.Add(cardInList);
            }
            else
            {
                cardInList.Quantity++;
            }
            return cardInList;
        }
    }

    public class EditionCardList : CardList
    {
        public EditionCardList()
        {
            Public = true;
        }

        public virtual ICollection<Edition> Editions { get; set; }

        [NotMapped]
        public Edition Edition
        {
            get { return Editions.FirstOrDefault(); }
        }

        public CardInCardList AddCard(Card card, int number)
        {
            var cardInCardList = base.AddCard(card);
            cardInCardList.Number = number;
            return cardInCardList;
        }
    }

    public class Deck : Collection
    {
        
    }

    public class Collection : CardList
    {
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}