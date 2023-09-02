namespace Mango.Services.ShoppingCartAPI.Models
{
    public class Cart //購物車容器
    {
        public CartHeader CartHeader { get; set; }
        public IEnumerable<CartDetails> CartDetails { get; set; }
    }
}
