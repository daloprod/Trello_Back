namespace Trello1.dto
{
    public class DtoPostCarte
    {
         
        public required string Titre { get; set; }

        public string? Description { get; set; }

        public int? IdListe { get; set; }



    }

    public class DtoGetCarte
    {
        public string? Titre { get; set; }

        public string? Description { get; set; }

        public int? IdListe { get; set; }



    }


}
