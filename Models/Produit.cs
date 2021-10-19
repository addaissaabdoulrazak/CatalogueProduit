using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Catalogue.Models
{
    public class Produit
    {
        [Key]
        public int ProduitID { get; set; }
        [StringLength(70)]
        [Required]
        [MinLength(4),MaxLength(70)]
        public string Designation { get; set; }
        [Range(10,500000)]
        public double Prix { get; set; }
        public int Quantite { get; set; }
        public int CategorieID { get; set; }
//comme que nous avons une association entre Nos deux Table nous allons ttravailler en Mode Lazy d'ou l'utilisation du mot clé Virtual
        public virtual Categorie Categorie { get; set; }
    }
}
