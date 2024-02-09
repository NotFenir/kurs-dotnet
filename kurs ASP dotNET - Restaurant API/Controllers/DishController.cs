using kurs_ASP_dotNET___Restaurant_API.Models;
using kurs_ASP_dotNET___Restaurant_API.Services;
using Microsoft.AspNetCore.Mvc;

namespace kurs_ASP_dotNET___Restaurant_API.Controllers;

[Route("api/restaurant/{restaurantId}/dish")]
[ApiController]
public class DishController : ControllerBase
{
    private readonly IDishService _dishService;

    public DishController(IDishService dishService)
    {
        _dishService = dishService;
    }
    
    [HttpPost]
    public ActionResult Post([FromRoute] int restaurantId, [FromBody] CreateDishDto dto)
    {
        var newDishId = _dishService.Create(restaurantId, dto);
        return Created($"/api/restaurant/{restaurantId}/dish/{newDishId}", null);
    }

    [HttpGet("{dishId}")]
    public ActionResult<DishDto> Get([FromRoute] int restaurantId, [FromRoute] int dishId)
    {
        var dishDto = _dishService.GetById(restaurantId, dishId);
        return Ok(dishDto);
    }
    
    [HttpGet]
    public ActionResult<List<DishDto>> GetAll([FromRoute] int restaurantId, [FromRoute] int dishId)
    {
        var result = _dishService.GetAll(restaurantId);
        return Ok(result);
    }

    [HttpDelete]
    public ActionResult DeleteAll([FromRoute] int restaurantId)
    {
        _dishService.RemoveAll(restaurantId);
        return NoContent();
    }

    [HttpDelete("{dishId}")]
    public ActionResult Delete([FromRoute] int restaurantId, [FromRoute] int dishId)
    {
        _dishService.RemoveById(restaurantId, dishId);
        return NoContent();
    }

    [HttpPut("{dishId}")]
    public ActionResult Update([FromRoute] int restaurantId, [FromRoute] int dishId, [FromBody] UpdateDishDto dto)
    {
        _dishService.Update(restaurantId, dishId, dto);
        return Ok();
    }
}