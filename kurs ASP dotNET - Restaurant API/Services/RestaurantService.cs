﻿using AutoMapper;
using kurs_ASP_dotNET___Restaurant_API.Entities;
using kurs_ASP_dotNET___Restaurant_API.Exceptions;
using kurs_ASP_dotNET___Restaurant_API.Models;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Controllers;

namespace kurs_ASP_dotNET___Restaurant_API.Services;

public interface IRestaurantService
{
    RestaurantDto? GetById(int id);
    IEnumerable<RestaurantDto> GetAll();
    int Create(CreateRestaurantDto dto);
    public void Delete(int id);
    public void Update(UpdateRestaurantDto dto, int id);
}

public class RestaurantService : IRestaurantService
{
    private readonly RestaurantDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly ILogger<RestaurantService> _logger;

    public RestaurantService(RestaurantDbContext dbContext, IMapper mapper, ILogger<RestaurantService> logger)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _logger = logger;
    }

    public void Update(UpdateRestaurantDto dto, int id)
    {
        var restaurant = _dbContext
            .Restaurants!
            .FirstOrDefault(r => r.Id == id);

        if (restaurant is null)
        {
            throw new NotFoundException("Restaurant not found");
        }

        restaurant.Name = dto.Name;
        restaurant.Description = dto.Description;
        restaurant.HasDelivery = dto.HasDelivery;
        _dbContext.SaveChanges();
    }

    public void Delete(int id)
    {
        _logger.LogError($"Restaurant with id: {id} DELETE action invoked");
        
        var restaurant = _dbContext
            .Restaurants!
            .FirstOrDefault(r => r.Id == id);

        Console.WriteLine(restaurant is null);
        
        if (restaurant is null)
        {
            throw new NotFoundException("Restaurant not found");
        }

        _dbContext.Restaurants?.Remove(restaurant);
        _dbContext.SaveChanges();
    }
    
    public RestaurantDto? GetById(int id)
    {
        var restaurant = _dbContext
            .Restaurants!
            .Include(r => r.Address)
            .Include(r => r.Dishes)
            .FirstOrDefault(r => r.Id == id);

        if (restaurant is null)
        {
            throw new NotFoundException("Restaurant not found");
        }

        var result = _mapper.Map<RestaurantDto>(restaurant);
        return result;
    }

    public IEnumerable<RestaurantDto> GetAll()
    {
        var restaurants = _dbContext
            .Restaurants!
            .Include(r => r.Address)
            .Include(r => r.Dishes)
            .ToList();

        var restaurantDtos = _mapper.Map<List<RestaurantDto>>(restaurants);
        return restaurantDtos;
    }

    public int Create(CreateRestaurantDto dto)
    {
        var restaurant = _mapper.Map<Restaurant>(dto);
        _dbContext.Restaurants?.Add(restaurant);
        _dbContext.SaveChanges();

        return restaurant.Id;
    }
}