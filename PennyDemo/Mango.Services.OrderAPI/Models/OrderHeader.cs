namespace Mango.Services.OrderAPI.Models
{
    public class OrderHeader
    {
        //從checkoutheaderDto複製來的(再修改一些)
        public int OrderHeaderId { get; set; }
        public string UserId { get; set; }
        public string CouponCode { get; set; }
        //add
        public double OrderTotal { get; set; } //在web裡面運算

        //add in web
        public double DiscountTotal { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime PickupDateTime { get; set; }
        public DateTime OrderTime { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string CardNumber { get; set; }
        public string CVV { get; set; }
        public string ExpiryMonthYear { get; set; }
        //add
        public int CartTotalItems { get; set; }
        public List<OrderDetails> OrderDetails { get; set; }
        public bool PaymentStatus { get; set; }
    }
}
