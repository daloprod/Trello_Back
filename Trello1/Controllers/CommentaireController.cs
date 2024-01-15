

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
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [Route("api/[controller]")]
    [ApiController]
    public class CommentaireController : ControllerBase
    {
        private readonly PresidentContext _context;

        public CommentaireController(PresidentContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Post(dtocome dto)
        {

            var commentaire = new Commentaire()
            {
                Contenu = dto.Contenu,
                IdCarte = dto.IdCarte,
                Utilisateur = dto.Utilisateur,
                DateCreation = DateTime.Now
            };

            _context.Commentaires.Add(commentaire);
            _context.SaveChanges();
            return Ok(commentaire);

        }



        [HttpGet]
        public IActionResult Get()
        {
            var commentaires = _context.Commentaires.ToList();
            
            return Ok(commentaires);
        }





        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var commentaire = _context.Commentaires.SingleOrDefault(c => c.Id == id);
            if (commentaire == null)
            {
                return NotFound($"No comment was found with ID: {id}");
            }

            _context.Remove(commentaire);
            _context.SaveChanges(true);

            return Ok(commentaire);
        }






        [HttpPut("{id}")]
        public IActionResult Put(int id, dtocome dto)
        {
            var commentaire = _context.Commentaires.SingleOrDefault(c => c.Id == id);

            if (commentaire == null)
            {
                return NotFound($"No comment was found with ID: {id}");
            }

            commentaire.Contenu = dto.Contenu;
            _context.SaveChanges();

            return Ok(commentaire);
        }

    }
}
