namespace Trello1.dto
{
    public class DtoPostProjet
    {
        public string Nom { get; set; } = null!;

        public required string Titre { get; set; }
        public string? Description { get; set; }
        public int? IdListe { get; internal set; }
    }

    public class DtoGetProjet
    {
        public string Nom { get; set; } = null!;
        public string? Titre { get; set; }

        public string? Description { get; set; }


    }


}
