using Microsoft.EntityFrameworkCore;

namespace ProductsCategoriesManyWithMany.Db
{
    public partial class AppDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("products");
                entity.HasKey(p => p.ProductId).HasName("product_id");

                entity.Property(p => p.ProductName).HasColumnName("name");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("categories");
                entity.HasKey(c => c.CategoryId).HasName("catogory_id");

                entity.Property(p => p.CategoryName).HasColumnName("name");

                entity
                    .HasMany(category => category.Products)
                    .WithMany(category => category.Categories)
                    .UsingEntity<ProductCategory>(
                        product => product
                                    .HasOne(productCategory => productCategory.Product)
                                    .WithMany(product => product.ProductCategories)
                                    .HasForeignKey(productCategory => productCategory.ProductId),
                        category => category
                                    .HasOne(productCategory => productCategory.Category)
                                    .WithMany(category => category.ProductCategoriys)
                                    .HasForeignKey(productCategory => productCategory.CategoryId));
            });

            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
