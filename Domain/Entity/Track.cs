namespace Domain.Entity
{
    public class Track : BaseEntity
    {
        public string? Name { get; set; }
        public string? Author { get; set; }
        public string? Genre { get; set; }
        public string? FilePath { get; set; }
    }
}
