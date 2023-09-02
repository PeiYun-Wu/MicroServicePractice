using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mango.Services.ShoppingCartAPI.Models
{
    public class Product
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.None)] //不會自動生成id,pk值由用戶手動指定,每生產一筆的時候都要經過這手動+pk的值才不會出錯
        public int ProductId { get; set; }
        [Required]
        public string Name { get; set; }
        [Range(1, 1000)]
        public double Price { get; set; }
        public string Description { get; set; }
        public string CategoryName { get; set; }
        public string ImageUrl { get; set; }
    }
}
