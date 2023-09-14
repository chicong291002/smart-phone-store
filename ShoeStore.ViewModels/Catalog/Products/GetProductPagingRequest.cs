using ShoeStore.ViewModels.Common;

namespace ShoeStore.ViewModels.Catalog.Products
{
    public class GetProductPagingRequest : PageResultBase
    {
        public string Keyword { get; set; }
        public int? CategoryId { get; set; }
    }
}
