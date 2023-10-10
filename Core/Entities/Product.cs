using System.ComponentModel.DataAnnotations;

namespace Core.Entities;

public class Product 
{
    private int _minimumQuantity;

    [Key]
    public int ProductCode { get; set; }
    public int MinimumQuantity
    {
        get { return _minimumQuantity; }
        set
        {
            if (value < 4)
            {
                throw new ArgumentException("Minimum quantity cannot be less than 4.");
            }
            _minimumQuantity = value;
        }
    }

    [Required,MaxLength(100)]
    public string Name { get; set; } = string.Empty;
    [Required, MaxLength(500)]

    public string Description { get; set; } = string.Empty;
    [Required]

    public decimal Price { get; set; }
    [Required, MaxLength(500)]
    public string PictureUrl { get; set; } = string.Empty;

    public int ProductCategoryId { get; set; }

    public ProductCategory ProductCategory { get; set; } 
    

}
