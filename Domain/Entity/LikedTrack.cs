namespace Domain.Entity
{
    public class LikedTrack : BaseEntity
    {
        public string? UserEmail { get; set; }
        public int TrackId { get; set; }
    }
}