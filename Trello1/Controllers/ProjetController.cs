using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Trello1.Models;
using Trello1.dto;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Options;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Web.Http.Cors;

namespace Trello1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ProjetController : ControllerBase
    {
        private readonly PresidentContext _context;

        public ProjetController(PresidentContext context)
        {
            _context = context;
        }
    
        [HttpPost]
        public IActionResult Post(dtoPostProjet dto)
        {
            var projet = new Projet()
            {
                Nom = dto.Nom,
                Description = dto.Description,
            };
            projet.DateCreation = DateTime.Now;
            _context.Projets.Add(projet);
            _context.SaveChanges();
            return Ok(projet);
        }





        [HttpGet]
        public IActionResult Get()
        {
            var projets = _context.Projets.ToList();

            foreach (var projet in projets)
            {
                projet.Listes = _context.Listes
                    .Where(liste => liste.IdProjet == projet.Id)
                    .Select(liste => new Liste
                    {
                        Id = liste.Id,
                        IdProjet = projet.Id,
                        IdProjetNavigation = null,
                        Nom = liste.Nom,

                        Cartes = _context.Cartes
                            .Where(carte => carte.IdListe == liste.Id)
                            .Select(carte => new Carte
                            {
                                Id = carte.Id,
                                Titre = carte.Titre,
                                Description = carte.Description,
                                DateCreation = carte.DateCreation,
                                IdListe = carte.IdListe,
                                Commentaires = _context.Commentaires
                                    .Where(commentaire => commentaire.IdCarte == carte.Id)
                                    .ToList()
                            })
                            .ToList()
                    })
                    .ToList();
            }

            return Ok(projets);
        }



        [HttpPut("{id}")]
        public IActionResult Put(dtoPostProjet dto, int id)
    
        {
            var Projet = _context.Projets.SingleOrDefault(g => g.Id == id);

            if(Projet == null)
            {
                return NotFound($"No genre was found with ID: {id}");
            }
            Projet.Nom = dto.Nom;
           Projet.Description = dto.Description;
            _context.SaveChanges();
            return Ok(Projet);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var Projet = _context.Projets.SingleOrDefault(g => g.Id == id);
            if (Projet == null)
            {
                return NotFound($"No genre was found with ID: {id}");
            }
            _context.Remove(Projet);
            _context.SaveChanges(true);
            return Ok(Projet);

        }

    }
}
