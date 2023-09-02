using Mango.Services.ProductAPI.Models.Dto;
using Mango.Services.ProductAPI.Repositiry;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace Mango.Services.ProductAPI.Controllers
{//改為ControllerBase 因為這是針對api的 (?)
    [Route("api/products")]  //下游
    public class ProductAPIController : ControllerBase
    {
        protected ResponseDto _response;
        private IProductRepository _productRepository;

        public ProductAPIController(IProductRepository productRepository)
        {
           _productRepository= productRepository;
            this._response = new ResponseDto();

        }

        [HttpGet]
        public async Task<object> Get()  //通用 => object
        {
            try 
            {
                IEnumerable<ProductDto> productDtos = await _productRepository.GetProducts();
                _response.Result= productDtos;
            }
            catch (Exception ex)
            {
                _response.IsSuccess= false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        
        }
        [HttpGet]
        [Route("{id}")]  //路由必須存在, 如果沒有就會變成無效的api,get導向到這裡會混淆
        public async Task<object> Get(int id)  //通用 => object
        {
            try
            {
                ProductDto productDto = await _productRepository.GetProductById(id);
                _response.Result = productDto;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;

        }

        [HttpPost]
       [Authorize]
        public async Task<object> Post([FromBody] ProductDto productDto)
        {
            try
            {
                ProductDto model = await _productRepository.CreateUpdateProduct(productDto);
                _response.Result = model;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }
        [HttpPut]
        [Authorize]
        public async Task<object> Put([FromBody] ProductDto productDto)
        {
            try
            {
                ProductDto model = await _productRepository.CreateUpdateProduct(productDto);
                _response.Result = model;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }
        [HttpDelete]
        [Authorize(Roles = "Admin")] //只有管理員adim可以刪除用戶
        [Route("{id}")]
        public async Task<object> Delete(int id)
        {
            try
            {
                bool isSuccess = await _productRepository.DeleteProduct(id);
                _response.Result = isSuccess;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }
    }
}
