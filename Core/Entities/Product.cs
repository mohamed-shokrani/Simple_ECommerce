using System.ComponentModel.DataAnnotations;
using Core.Attributes;
using Core.Constants;//.DataAnnotations;

namespace Core.Entities;

public class Product 
{
    [Key]
    public int ProductCode { get; set; }

    [MinimumQuantity(ErrorMessage = "The minimum quantity cannot be less than 4.")]
    public int MinimumQuantity { get; set; }

    [Required,MaxLength(100)]
    public string Name { get; set; } = string.Empty;
    [Required, MaxLength(500)]

    public string Description { get; set; } = string.Empty;
    [Required]

    public decimal Price { get; set; }
    public decimal DiscountRate { get; set; } = ConstantForProducts.DiscountFifteenPercent;

    [Required, MaxLength(500)]
    public string PictureUrl { get; set; } = string.Empty;

    public int ProductCategoryId { get; set; }

    public ProductCategory ProductCategory { get; set; } 
    

}
