﻿namespace kurs_ASP_dotNET___Restaurant_API.Models;

public class DishDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
}