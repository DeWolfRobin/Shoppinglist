using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Shoppinglist.Models
{
    public class shoppingItem
    {
        [Required]
        [Display(Name ="Beschrijving")]
        public string Naam { get; set; }
        [Range(typeof(int),"1","5")]
        public int Aantal { get; set; }

        public shoppingItem() {
        }

        public shoppingItem(string naam, int aantal) {
            Naam = naam;
            Aantal = aantal;
        }

    }
}
