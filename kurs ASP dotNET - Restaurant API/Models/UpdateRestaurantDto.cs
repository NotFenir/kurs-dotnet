using System.ComponentModel.DataAnnotations;

namespace kurs_ASP_dotNET___Restaurant_API.Models;

public class UpdateRestaurantDto
{
    [Required]
    [MaxLength(25)]
    public string? Name { get; set; }
    public string? Description { get; set; }
    public bool HasDelivery { get; set; }
}