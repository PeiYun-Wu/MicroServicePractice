using AutoMapper;
using Mango.Services.ProductAPI.DbContexts;
using Mango.Services.ProductAPI.Models;
using Mango.Services.ProductAPI.Models.Dto;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.ProductAPI.Repositiry
{
    //負責封裝對資料庫的操作，提供統一的介面供其他部分(服務或控制器)使用，以實現對數據的存取和操作
    public class ProductRepository : IProductRepository
    {
        //以DB為目標,需要依賴注入獲取應用程序DB下文
        private readonly ApplicationDbContext _db;
        private IMapper _mapper;

        //再加密獲取
        public ProductRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        //實作所有介面
        public async Task<ProductDto> CreateUpdateProduct(ProductDto productDto)
        {
            Product product = _mapper.Map<ProductDto, Product>(productDto);
            if (product.ProductId > 0) //已經有該產品
            {
                _db.Products.Update(product);
            }
            else
            {
                _db.Products.Add(product);
            }
            await _db.SaveChangesAsync();
            return _mapper.Map<Product, ProductDto>(product);
        }

        public async Task<bool> DeleteProduct(int productId)
        {
            try
            {
                Product product = await _db.Products.FirstOrDefaultAsync(u => u.ProductId == productId);
                if (product == null)
                {
                    return false;
                }
                _db.Products.Remove(product);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<ProductDto> GetProductById(int productId)
        {
            Product product = await _db.Products.Where(x=>x.ProductId == productId).FirstOrDefaultAsync();
            return _mapper.Map<ProductDto>(product);
        }

        public async Task<IEnumerable<ProductDto>> GetProducts()
        {
            //async 異部方法,可以在後台執行而不會阻塞主線程式的方法 搭配Task,await
            List<Product> productList = await _db.Products.ToListAsync();
            return _mapper.Map<List<ProductDto>>(productList);
        }
    }
}
