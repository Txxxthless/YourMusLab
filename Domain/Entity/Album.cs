namespace Domain.Entity
{
    public class Album : BaseEntity
    {
        public string? Name { get; set; }
        public Author? Author { get; set; }
        public int? AuthorId { get; set; }
        public Genre? Genre { get; set; }
        public int? GenreId { get; set; }
        public int? ReleaseYear { get; set; }
        public string? PictureUrl { get; set; }
    }
}
