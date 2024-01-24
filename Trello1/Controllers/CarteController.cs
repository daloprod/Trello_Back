using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Trello1.Models;
using Trello1.dto;
using System;
using System.Linq;
using System.Web.Http.Cors;

namespace Trello1.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [Route("api/[controller]")]
    [ApiController]
    public class CarteController(PresidentContext context) : ControllerBase
    {
        private readonly PresidentContext _context = context ?? throw new ArgumentNullException(nameof(context));

        [HttpPost]
        public IActionResult Post(DtoPostCarte dto)
        {
            if (dto == null)
            {
                return BadRequest("Invalid model object");
            }

            var carte = new Carte()
            {
                IdListe = dto.IdListe,
                Titre = dto.Titre,
                Description = dto.Description,
                DateCreation = DateTime.Now
            };

            _context.Cartes.Add(carte);
            _context.SaveChanges();
            return Ok(carte);
        }

        [HttpGet]
        public IActionResult Get()
        {
            var Cartes = _context.Cartes.ToList();
            foreach (var carte in Cartes)
            {
                carte.Commentaires = _context.Commentaires
                    .Where(commentaire => commentaire.IdCarte == carte.Id)
                    .Select(commentaire => new Commentaire
                    {
                        Id = commentaire.Id,
                        IdCarte = carte.Id,
                        IdCarteNavigation = null,
                        Contenu = commentaire.Contenu,
                    })
                    .ToList();
            }

            return Ok(Cartes);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, DtoPostProjet dto)
        {
            if (dto == null)
            {
                return BadRequest("Invalid model object");
            }

            var Carte = _context.Cartes.SingleOrDefault(g => g.Id == id);

            if (Carte == null)
            {
                return NotFound($"No card was found with ID: {id}");
            }

            Carte.Titre = dto.Titre;
            Carte.Description = dto.Description;
            Carte.IdListe = dto.IdListe;
            Carte.DateCreation = DateTime.Now;

            _context.SaveChanges();
            return Ok(Carte);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var Carte = _context.Cartes.SingleOrDefault(g => g.Id == id);
            if (Carte == null)
            {
                return NotFound($"No card was found with ID: {id}");
            }

            _context.Remove(Carte);
            _context.SaveChanges();
            return Ok(Carte);
        }
    }
}
