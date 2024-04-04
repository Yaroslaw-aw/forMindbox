namespace ProductsCategoriesManyWithMany.Db
{
    public class Product
    {
        public Guid ProductId { get; set; }
        public string? ProductName { get; set; }


        public virtual ICollection<Category>? Categories { get; set; } = new List<Category>();

        [System.Text.Json.Serialization.JsonIgnore]
        public List<ProductCategory> ProductCategories { get; set; } = new List<ProductCategory>();
    }
}
