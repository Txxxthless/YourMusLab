using System.Text.Json;
using Domain.Entity;

namespace DAL
{
    public class AppDbContextSeed
    {
        public static async Task SeedAsync(AppDbContext context)
        {
            if (!context.Genres.Any())
            {
                Seed<Genre>(context, "../DAL/SeedData/genres.json");
            }
            if (!context.Albums.Any())
            {
                Seed<Album>(context, "../DAL/SeedData/albums.json");
            }
            if (!context.Authors.Any())
            {
                Seed<Author>(context, "../DAL/SeedData/authors.json");
            }
            if (!context.Tracks.Any())
            {
                Seed<Track>(context, "../DAL/SeedData/tracks.json");
            }

            if (context.ChangeTracker.HasChanges())
            {
                await context.SaveChangesAsync();
            }
        }

        private static void Seed<T>(AppDbContext context, string filePath)
            where T : BaseEntity
        {
            var data = JsonSerializer.Deserialize<List<T>>(File.ReadAllText(filePath));
            context.Set<T>().AddRange(data);
        }
    }
}
