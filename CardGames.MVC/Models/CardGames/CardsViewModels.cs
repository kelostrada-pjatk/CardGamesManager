using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CardGames.MVC.Models.CardGames
{
    public class CardViewModel
    {
        [Key]
        public int CardId { get; set; }

        [Required]
        [Display(Name = "Number")]
        public int Number { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required]
        public int EditionId { get; set; }
    }

    public class CollectionViewModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Is Public?")]
        public bool Public { get; set; }
        [Required]
        [Display(Name = "Is Deck?")]
        public bool Deck { get; set; }

        public Collection GetCollection()
        {
            if (Deck)
            {
                return new Deck
                {
                    Id = Id,
                    Name = Name,
                    Public = Public
                };
            }
            return new Collection
            {
                Id = Id,
                Name = Name,
                Public = Public
            };
        }

        public static CollectionViewModel FromCollection(Collection collection)
        {
            return new CollectionViewModel
            {
                Id = collection.Id,
                Name = collection.Name,
                Public = collection.Public,
                Deck = collection is Deck
            };
        }
    }
}