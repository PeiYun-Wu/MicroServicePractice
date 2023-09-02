using System.ComponentModel.DataAnnotations;

namespace Mango.Services.ShoppingCartAPI.Models.Dto
{
    public class CartHeaderDto
    {
        //刪除pk鍵 [Key]
        public int CartHeaderId { get; set; }
        public string UserId { get; set; }
        public string CouponCode { get; set; }
    }
}
