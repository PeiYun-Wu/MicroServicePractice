using Mango.Services.ProductAPI.Models.Dto;

namespace Mango.Services.ProductAPI.Repositiry
{//當儲存庫
    public interface IProductRepository
    {
        //把產品拿出來
        Task<IEnumerable<ProductDto>> GetProducts();
        Task<ProductDto> GetProductById(int productId);
        Task<ProductDto> CreateUpdateProduct(ProductDto productDto);
        Task<bool> DeleteProduct(int productId);
    }
}
