using Domain.Entity;

namespace Domain.ViewModels
{
    public class TrackViewModel : BaseEntity
    {
        public string? Name { get; set; }
        public string? Author { get; set; }
        public string? Genre { get; set; }
        public string? Album { get; set; }
        public string? PicturePath { get; set; }
        public string? FilePath { get; set; }
    }
}
