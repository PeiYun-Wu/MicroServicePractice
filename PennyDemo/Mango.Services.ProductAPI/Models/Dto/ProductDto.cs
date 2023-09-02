using System.ComponentModel.DataAnnotations;

namespace Mango.Services.ProductAPI.Models.Dto;

public class ProductDto
{
    //不需要數據註釋,因為沒有要填+進資料庫
    public int ProductId { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public string Description { get; set; }
    public string CategoryName { get; set; }
    public string ImageUrl { get; set; }
}
