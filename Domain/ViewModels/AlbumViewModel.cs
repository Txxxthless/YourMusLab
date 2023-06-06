using Domain.Entity;

namespace Domain.ViewModels
{
    public class AlbumViewModel : BaseEntity
    {
        public string? Name { get; set; }
        public string? Author { get; set; }
        public string? Genre { get; set; }
        public int? ReleaseYear { get; set; }
        public string? PictureUrl { get; set; }
    }
}
