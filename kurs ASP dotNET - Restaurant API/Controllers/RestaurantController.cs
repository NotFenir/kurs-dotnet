using AutoMapper;
using kurs_ASP_dotNET___Restaurant_API.Models;
using kurs_ASP_dotNET___Restaurant_API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace RestaurantAPI.Controllers;

[Route("api/restaurant")]
[ApiController]
public class RestaurantController : ControllerBase
{
    private readonly IRestaurantService _restaurantService;

    public RestaurantController(IRestaurantService restaurantService)
    {
        _restaurantService = restaurantService;
    }

    [HttpPut("{id}")]
    public ActionResult Update([FromRoute]int id, [FromBody] UpdateRestaurantDto dto)
    {
        _restaurantService.Update(dto, id);
        return Ok();
    }
    
    [HttpDelete("{id}")]
    public ActionResult Delete([FromRoute] int id)
    {
        _restaurantService.Delete(id);
        return NoContent();
    }

    [HttpPost]
    public ActionResult CreateRestaurant([FromBody]CreateRestaurantDto dto)
    {
        var id = _restaurantService.Create(dto);
        return Created($"/api/restaurant/{id}", null);
    }
    
    [HttpGet]
    public ActionResult<IEnumerable<RestaurantDto>> GetAll()
    {
        var restaurantsDtos = _restaurantService.GetAll();
        return Ok(restaurantsDtos);
    }

    [HttpGet("{id}")]
    public ActionResult<RestaurantDto> Get([FromRoute]int id)
    {
        var restaurant = _restaurantService.GetById(id);
        return Ok(restaurant);
    }
}