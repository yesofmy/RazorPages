using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPagesLab.Pages
{
    public class QuickCalcModel : PageModel
    {
        public string Message { get; set; } = "";

        public void OnGet(decimal price, double discount)
        {
            decimal discountAmount = price * (decimal)(discount / 100);
            decimal finalPrice = price - discountAmount;

            Message = $"Original price: {price:F2} | " +
                      $"Discount: {discount:F2}% | " +
                      $"Final price: {finalPrice:F2}";
        }
    }
}