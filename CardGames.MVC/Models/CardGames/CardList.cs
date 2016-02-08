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

        public virtual CardInCardList AddCard(Card card, int quantity = 1, int number = 1)
        {
            var cardInList = CardInCardLists.FirstOrDefault(c => c.CardId == card.Id);
            if (cardInList == null)
            {
                cardInList = new CardInCardList {Card = card, CardList = this, Number = number, Quantity = quantity};
                CardInCardLists.Add(cardInList);
            }
            else
            {
                cardInList.Quantity += quantity;
            }
            return cardInList;
        }

        public virtual CardInCardList RemoveCard(Card card, int quantity = 1)
        {
            var cardInList = CardInCardLists.First(c => c.CardId == card.Id);

            if (cardInList.Quantity <= quantity)
            {
                CardInCardLists.Remove(cardInList);
            }
            else
            {
                cardInList.Quantity -= quantity;
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

    }

    public class Deck : Collection
    {
        public virtual ICollection<Comment> Comments { get; set; } 
    }

    public class Collection : CardList
    {
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}