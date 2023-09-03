using ShoeStore.Application.DTOS;

namespace ShoeStore.Application.Catalog.Products.DTOS
{
    public class GetPublicProductPagingRequest : PagingRequestBase
    {
        // thuoc tinh rieng
        public int? CategoryIds { get; set; }
    }
}
