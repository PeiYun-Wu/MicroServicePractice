using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Mango.Services.OrderAPI.Models
{
    public class OrderDetails
    {
        //從cartdetailsDto複製來
        [Key]
        public int OrderDetailsId { get; set; }
        public int OrderHeaderId { get; set; }
        [ForeignKey("OrderHeaderId")]
        public virtual OrderHeader OrderHeader { get; set; }
        public int ProductId { get; set; }

        public int Count { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }
    }
}
