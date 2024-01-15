using System;
using System.Collections.Generic;

namespace Trello1.Models;

public partial class Liste
{
    public int Id { get; set; }

    public string Nom { get; set; } = null!;

    public int? IdProjet { get; set; }

    public virtual ICollection<Carte> Cartes { get; set; } = new List<Carte>();

    public virtual Projet? IdProjetNavigation { get; set; } = null;
}
