namespace Mango.Services.OrderAPI.Messages
{
    public class CheckoutHeaderDto
    {
        public int CartHeaderId { get; set; }
        public string UserId { get; set; }
        public string CouponCode { get; set; }
        //add
        public double OrderTotal { get; set; } //在web裡面運算

        //add in web
        public double DiscountTotal { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime PickupDateTime { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string CardNumber { get; set; }
        public string CVV { get; set; }
        public string ExpiryMonthYear { get; set; }
        //add
        public int CartTotalItems { get; set; }
        public IEnumerable<CartDetailsDto> CartDetails { get; set; }
    }
}
