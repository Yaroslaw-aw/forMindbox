namespace ProductsCategoriesManyWithMany.Db
{
    public class Category
    {
        public Guid CategoryId { get; set; }
        public string? CategoryName { get; set; }


        public virtual ICollection<Product>? Products { get; set; } = new List<Product>();

        [System.Text.Json.Serialization.JsonIgnore]
        public List<ProductCategory> ProductCategoriys { get; set; } = new List<ProductCategory>();
    }
}
