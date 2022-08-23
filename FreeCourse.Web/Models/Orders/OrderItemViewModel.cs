namespace FreeCourse.Web.Models.Orders
{
    public class OrderItemViewModel
    {
        public string ProductId { get; set; }  //CourseId için ProductId isimlendirmesini tercih ettik.
        public string ProductName { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }
    }
}
