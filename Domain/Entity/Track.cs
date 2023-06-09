namespace Domain.Entity
{
    public class Track : BaseEntity
    {
        public string? Name { get; set; }
        public Album? Album { get; set; }
        public int? AlbumId { get; set; }
        public Author? Author { get; set; }
        public int? AuthorId { get; set; }
        public Genre? Genre { get; set; }
        public int? GenreId { get; set; }
        public string? FilePath { get; set; }
    }
}
