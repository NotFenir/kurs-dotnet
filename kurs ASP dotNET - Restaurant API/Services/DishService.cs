using AutoMapper;
using kurs_ASP_dotNET___Restaurant_API.Controllers;
using kurs_ASP_dotNET___Restaurant_API.Entities;
using kurs_ASP_dotNET___Restaurant_API.Exceptions;
using kurs_ASP_dotNET___Restaurant_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace kurs_ASP_dotNET___Restaurant_API.Services;

public interface IDishService
{
    int Create(int restaurantId, CreateDishDto dto);
    DishDto GetById(int restaurantId, int dishId);
    List<DishDto> GetAll(int restaurantId);
    void RemoveAll(int restaurantId);
    void RemoveById(int restaurantId, int dishId);
    void Update(int restaurantId, int dishId, UpdateDishDto dto);
}

public class DishService : IDishService
{
    private readonly RestaurantDbContext _context;
    private readonly IMapper _mapper;

    public DishService(RestaurantDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public int Create(int restaurantId, CreateDishDto dto)
    {
        var restaurant = GetRestaurantById(restaurantId);
        var dishEntity = _mapper.Map<Dish>(dto);
        dishEntity.RestaurantId = restaurantId;
        
        _context.Dishes?.Add(dishEntity);
        _context.SaveChanges();

        return dishEntity.Id;
    }

    public DishDto GetById(int restaurantId, int dishId)
    {
        var restaurant = GetRestaurantById(restaurantId);
        var dish = GetDishById(dishId, restaurantId);

        var dishDto = _mapper.Map<DishDto>(dish);
        return dishDto;
    }

    public List<DishDto> GetAll(int restaurantId)
    {
        var restaurant = GetRestaurantById(restaurantId);
        var dishDtos = _mapper.Map<List<DishDto>>(restaurant.Dishes);
        return dishDtos;
    }

    public void RemoveAll(int restaurantId)
    {
        var restaurant = GetRestaurantById(restaurantId);
        _context.RemoveRange(restaurant!.Dishes!);
        _context.SaveChanges();
    }

    public void RemoveById(int restaurantId, int dishId)
    {
        var restaurant = GetRestaurantById(restaurantId);
        var dish = GetDishById(dishId, restaurantId);

        _context.Dishes?.Remove(dish);
        _context.SaveChanges();
    }

    public void Update(int restaurantId, int dishId, UpdateDishDto dto)
    {
        var restaurant = GetRestaurantById(restaurantId);
        var dish = GetDishById(dishId, restaurantId);

        _mapper.Map<UpdateDishDto, Dish>(dto, dish);
        _context.SaveChanges();
    }

    private Restaurant? GetRestaurantById(int restaurantId)
    {
        var restaurant = _context
            .Restaurants?
            .Include(r => r.Dishes)
            .FirstOrDefault(r => r.Id == restaurantId);

        if (restaurant is null)
        {
            throw new NotFoundException("Restaurant not found");
        }
        
        return restaurant;
    }
    
    private Dish? GetDishById(int dishId, int restaurantId)
    {
        var dish = _context.Dishes?.FirstOrDefault(d => d.Id == dishId);
        if (dish is null || dish.RestaurantId != restaurantId)
        {
            throw new NotFoundException("Dish not found");
        }

        return dish;
    }
}