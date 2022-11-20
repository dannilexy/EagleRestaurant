namespace Eagle.Services.CouponAPI.Models.Dtos
{
    public class CouponDto
    {
        public int CouponId { get; set; }
        public string Code { get; set; }
        public double DiscountAmount { get; set; }
    }
}
