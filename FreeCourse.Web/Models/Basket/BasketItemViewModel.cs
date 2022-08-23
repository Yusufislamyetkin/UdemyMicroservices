namespace FreeCourse.Web.Models.Basket
{
    public class BasketItemViewModel
    {
        public int Quantity { get; set; } = 1;

        public string CourseId { get; set; }
        public string CourseName { get; set; }

        public decimal Price { get; set; }

        private decimal? DiscountAppliedPrice; //indirimli fiyat

        public decimal GetCurrentPrice // indirimli fiyat yoksa normal fiyat olsun
        {
            get => DiscountAppliedPrice != null ? DiscountAppliedPrice.Value : Price;
        }

        public void AppliedDiscount(decimal discountPrice) // kupon uygula
        {
            DiscountAppliedPrice = discountPrice;
        }
    }
}
