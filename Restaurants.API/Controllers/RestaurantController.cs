using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Restaurants;

namespace Restaurants.API.Controllers
{
    [Route("api/restaurants")]
    [ApiController]
    public class RestaurantController(IRestaurantsService restaurantsService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll() 
        {
            var restaurants = await restaurantsService.GetAll();
            return Ok(restaurants);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute]int id)
        {
            var restaurant = await restaurantsService.GetById(id);

            if (restaurant is null)
                return NotFound();

            return Ok(restaurant);
        }

        // POST api/<RestaurantController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<RestaurantController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<RestaurantController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
