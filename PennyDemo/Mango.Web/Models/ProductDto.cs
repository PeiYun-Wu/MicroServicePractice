using System.ComponentModel.DataAnnotations;

namespace Mango.Web.Models
{

    public class ProductDto
    {
        public ProductDto()
        {
            Count = 1;
        }
        //不需要數據註釋,因為沒有要填+進資料庫
        public int ProductId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public string CategoryName { get; set; }
        public string ImageUrl { get; set; }

        //add
        [Range(1,100)]
        public int Count { get; set; }
    }
}