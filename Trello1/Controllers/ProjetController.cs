using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Trello1.Models;
using Trello1.dto;
using System;
using System.Linq;
using System.Web.Http.Cors;

namespace Trello1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ProjetController(PresidentContext context) : ControllerBase
    {
        private readonly PresidentContext _context = context ?? throw new ArgumentNullException(nameof(context));

        [HttpPost]
        public IActionResult Post([FromBody] DtoPostProjet dto)
        {
            var projet = new Projet()
            {
                Nom = dto.Nom,
                Description = dto.Description,
                DateCreation = DateTime.Now
            };
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
        public IActionResult Put([FromBody] DtoPostProjet dto, int id)
        {
            var Projet = _context.Projets.SingleOrDefault(g => g.Id == id);

            if (Projet == null)
            {
                return NotFound($"No project was found with ID: {id}");
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
                return NotFound($"No project was found with ID: {id}");
            }
            _context.Remove(Projet);
            _context.SaveChanges(true);
            return Ok(Projet);
        }
    }
}
