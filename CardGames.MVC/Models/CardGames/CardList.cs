using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CardGames.MVC.Models.CardGames
{
    public abstract class CardList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Public { get; set; }
    }

    public class EditionCardList : CardList
    {
        
    }

    public class Deck : CardList
    {
        
    }

    public class Collection : CardList
    {
        
    }
}