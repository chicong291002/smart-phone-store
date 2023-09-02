namespace ShoeStore.Application.Catalog.Products.DTOS
{
    public class ProductViewModel
    {
        public int Id { set; get; }
        public decimal Price { set; get; }
        public decimal OriginalPrice { set; get; }
        public DateTime DateCreated { set; get; }

        public string Name { set; get; }
        public string Description { set; get; }

        public string ThumbnailImage { get; set; }  

        public List<string> Categories { get; set; } = new List<string>();
    }
}