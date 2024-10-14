using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace FlowersCareAPI.Data;

public class FlowersContext : DbContext
{
    public DbSet<Flower> Flowers { get; set; }

    public FlowersContext(DbContextOptions<FlowersContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Flower>()
            .HasKey(flower => flower.FId);
        modelBuilder.Entity<Flower>()
            .HasIndex(flower => flower.ScientificName).IsUnique();
        foreach (var property in typeof(Flower).GetProperties()
                     .Where(p => Attribute.IsDefined(p, typeof(RequiredAttribute))))
        {
            modelBuilder.Entity<Flower>()
                .Property(property.Name)
                .IsRequired();
        }

        modelBuilder.Entity<Flower>().HasData(new Flower(
                "Rosa",
                "Rose",
                "A beautiful and fragrant flower, often associated with love and romance.",
                false,
                3,
                LightLevel.High,
                12,
                "Requires regular watering and plenty of sunlight.",
                "https://example.com/roses.jpg"
            ),

            new Flower(
                "Cactaceae",
                "Cactus",
                "A succulent plant known for its ability to thrive in dry conditions.",
                false,
                7,
                LightLevel.Medium,
                36,
                "Needs minimal watering and bright, indirect light.",
                "https://example.com/cactus.jpg"
            ),

            new Flower(
                "Helianthus",
                "Sunflower",
                "A tall plant with a large, yellow flower head that follows the sun.",
                false,
                5,
                LightLevel.High,
                24,
                "Requires moderate watering and full sunlight.",
                "https://example.com/sunflower.jpg"
            ),

            new Flower(
                "Chrysanthemum",
                "Chrysanthemum",
                "A popular flower with a wide variety of colors and shapes, symbolizing joy and optimism.",
                true,
                4,
                LightLevel.Medium,
                18,
                "Needs moderate sunlight and regular watering.",
                "https://example.com/chrysanthemum.jpg"
            ),

            new Flower(
                "Aloe",
                "Aloe Vera",
                "A succulent plant known for its medicinal properties, especially for skin care.",
                false,
                14,
                LightLevel.Low,
                48,
                "Requires minimal watering and indirect sunlight.",
                "https://example.com/aloe.jpg"
            ),

            new Flower(
                "Tulipa",
                "Tulip",
                "A bulbous spring-flowering plant with vibrant flowers in various colors.",
                false,
                6,
                LightLevel.Medium,
                12,
                "Requires well-drained soil and moderate watering.",
                "https://example.com/tulip.jpg"
            ),

            new Flower(
                "Narcissus",
                "Daffodil",
                "A bright yellow spring flower often associated with renewal and hope.",
                false,
                4,
                LightLevel.High,
                24,
                "Needs full sunlight and moderate watering.",
                "https://example.com/daffodil.jpg"
            ),

            new Flower(
                "Lilium",
                "Lily",
                "A large, fragrant flower often used in gardens and floral arrangements.",
                true,
                5,
                LightLevel.Medium,
                12,
                "Requires partial shade and regular watering.",
                "https://example.com/lily.jpg"
            )
        );
    }
}