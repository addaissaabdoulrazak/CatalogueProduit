using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Catalogue.Models
{
    public class Categorie
    {
        [Key]
      public int CategorieID { get; set; }
        public string NomCategorie { get; set; }
        //les Attribut d'assosiation son declarer Virtual table Produit intègré a la table categorie
        public virtual ICollection<Produit> Produits { get; set; }
    }
}
