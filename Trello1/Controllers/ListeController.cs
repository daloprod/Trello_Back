using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Web.Http.Cors;
using Trello1.dto;
using Trello1.Models;

namespace Trello1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors(origins: "*", headers: "*", methods: "*")]

    public class ListeController : ControllerBase
    {
        private readonly PresidentContext _context;

        public ListeController(PresidentContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Post(dtoListe dto)
        {
            var Liste = new Liste
            {
                Nom = dto.Nom,
                IdProjet = dto.IdProjet
            };

            _context.Listes.Add(Liste);
            _context.SaveChanges();
            return Ok(Liste);
        }

     

        [HttpGet]
        public IActionResult Get()
        {
            var listes = _context.Listes.ToList();

            foreach (var liste in listes)
            {
                liste.Cartes = _context.Cartes
                    .Where(carte => carte.IdListe == liste.Id)
                    .Select(carte => new Carte
                    {
                        Id = carte.Id,
                        IdListe = liste.Id,
                        Titre = carte.Titre,
                        Description = carte.Description,
                        // Ajoutez d'autres propriétés de Carte au besoin
                    })
                    .ToList();
            }

            return Ok(listes);
        }





        [HttpDelete("{id}")]
       public IActionResult Delete(int id) 
        {
            var Liste = _context.Listes.SingleOrDefault(p => p.Id == id);

            if (Liste == null)
            {
                return NotFound($"le {id} est introuvable");
                
            }

            _context.Remove(Liste);
            _context.SaveChanges(true);
            return Ok(Liste);
        }


        [HttpPut("{id}")]
        public IActionResult Put(int id, dtoListe dto) 
        {
            var Liste = _context.Listes.SingleOrDefault(p => p.Id == id);
            if (Liste == null)
            {
                return NotFound($"le {id} est introuvable");

            }
            Liste.Nom = dto.Nom;    
           _context.SaveChanges();

            return Ok(Liste);

        }







   }
}
