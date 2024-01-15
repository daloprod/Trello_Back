namespace Trello1.dto
{
    public class dtoPostProjet
    {
        public string Nom { get; set; } = null!;

        public string? Titre { get; set; }
        public string? Description { get; set; }
        public int? IdListe { get; internal set; }
    }

    public class dtoGetProjet
    {
        public string Nom { get; set; } = null!;
        public string? Titre { get; set; }

        public string? Description { get; set; }


    }


}
