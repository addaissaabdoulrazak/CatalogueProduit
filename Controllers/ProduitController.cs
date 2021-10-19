using Catalogue.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalogue.Controllers
{
    public class ProduitController : Controller
    {
        //verification si cette Demarche Fonctionne
        public ApplicationDbContext _db =new ApplicationDbContext();
        
        //GET : /Produit/Index

        //Nous allons ajouter une Pagination
        public IActionResult Index(int page=0, int size=5,string motCle="" )
        {
            int position = page*size;
            IEnumerable<Produit> produits =
                _db
                .Produits
                .Where(p=>p.Designation.Contains(motCle))
                .Skip(position).Take(size).
                //inclure p.categorie dans la requête pour pouvoir afficher le nom des categorie au lieu de l'identifiant de la clé etrangère
                Include(p=>p.Categorie).
                ToList();
          //la page Courrente est la page numero zéro(0) 
            ViewBag.currentPage = page;
            //si le nombre de page contient des virgule il faut ajouter une nouvelle Pages pour stocker le reste
            int NbreProduits = _db.Produits.Where(p=>p.Designation.Contains(motCle)).ToList().Count();
            int totalPages;
                if ((NbreProduits % size) == 0)
                {
                totalPages = NbreProduits / size;
                }
            else
            {
              totalPages = 1+(NbreProduits/size) ;
            }
            ViewBag.motCle = motCle;
            ViewBag.TotalPages=totalPages;
            return View("Produits",produits); 

 
        }
        public IActionResult FormulaireProduit()
        {
            //liste des categorie
            IEnumerable<Categorie> categories = _db.Categories.ToList();      
            ViewBag.categories = categories;
  
            return View();
        }
        [HttpPost]
        public IActionResult SaveProduit(Produit produit)
        {
            if(ModelState.IsValid)
            {
                _db.Produits.Add(produit);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            //si vous retourner le formulaireProduit ainsi que le produit  il faut de même tenir en compte la liste des categorie qui est presente au niveau
            //de notre formulaire et donc il faudrait le mentionné dans votre ViewBag
            //liste des categorie
            IEnumerable<Categorie> categories = _db.Categories.ToList();
            ViewBag.categories = categories;
            return View("FormulaireProduit",produit);
        }
    }
}
