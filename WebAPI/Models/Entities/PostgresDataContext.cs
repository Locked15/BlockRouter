using Microsoft.EntityFrameworkCore;

namespace BlockRouter.WebAPI.Models.Entities;

public partial class PostgresDataContext : DbContext
{
    private const string ConnectionStringSecretKey = "DB:ConnectionString";

    public PostgresDataContext()
    {
    }

    public PostgresDataContext(DbContextOptions<PostgresDataContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Brand> Brands { get; set; }

    public virtual DbSet<Model> Models { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(GetConnectionString())
                      .UseLazyLoadingProxies();
    }

    private static string GetConnectionString()
    {
        var configurationBuilder = new ConfigurationBuilder().AddUserSecrets<Program>();
        var configuration = configurationBuilder.Build();

        return configuration.GetValue<string>(ConnectionStringSecretKey) ?? string.Empty;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Brand>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("brand_pkey");

            entity.ToTable("brand");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.Name).HasColumnName("name");
        });

        modelBuilder.Entity<Model>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("model_pkey");

            entity.ToTable("model");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BrandId).HasColumnName("brand_id");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.Name).HasColumnName("name");

            entity.HasOne(d => d.Brand).WithMany(p => p.Models)
                .HasForeignKey(d => d.BrandId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("model_brand_id_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
