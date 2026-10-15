using System.ComponentModel.DataAnnotations;

namespace HandmadeHub.Models;

public class Product
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Please enter the product title.")]
    [StringLength(100, ErrorMessage = "The title cannot exceed 100 characters.")]
    [Display(Name = "Product Title")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please select a category.")]
    [Display(Name = "Category")]
    public string Category { get; set; } = string.Empty;

    [Range(0.01, 1000000,
        ErrorMessage = "The price must be greater than zero.")]
    [Display(Name = "Price (UAH)")]
    public decimal Price { get; set; }

    [Display(Name = "Product Image")]
    public string? ImagePath { get; set; }
}