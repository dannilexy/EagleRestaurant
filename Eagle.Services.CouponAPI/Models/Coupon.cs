using System.ComponentModel.DataAnnotations;

namespace Eagle.Services.CouponAPI.Models
{
    public class Coupon
    {
        [Key]
        public int CouponId { get; set; }
        public string Code { get; set; }
        public double DiscountAmount { get; set; }
    }
}
