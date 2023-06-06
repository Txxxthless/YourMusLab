using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Config
{
    public class TrackConfiguration : IEntityTypeConfiguration<Track>
    {
        public void Configure(EntityTypeBuilder<Track> builder)
        {
            builder.HasOne(track => track.Album).WithMany().HasForeignKey(track => track.AlbumId);
            builder.HasOne(track => track.Author).WithMany().HasForeignKey(track => track.AuthorId);
            builder.HasOne(track => track.Genre).WithMany().HasForeignKey(track => track.GenreId);
        }
    }
}
