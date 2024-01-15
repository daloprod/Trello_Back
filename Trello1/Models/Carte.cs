using System;
using System.Collections.Generic;

namespace Trello1.Models;

public partial class Carte
{
    public int Id { get; set; }

    public string Titre { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime? DateCreation { get; set; }

    public int? IdListe { get; set; }

    public virtual ICollection<Commentaire> Commentaires { get; set; } = new List<Commentaire>();

    public virtual Liste? IdListeNavigation { get; set; }

}
