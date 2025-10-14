using Microsoft.AspNetCore.Mvc;
using Solution.Core.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Solution.Api.Controllers
{
    public class CarController(ICarService carService): BaseController
    {

        [HttpGet]
        [Route("api/car/all")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await carService.GetAllAsync();

            return result.Match(
                result => Ok(result),
                errors => Problem(errors) 
            );
        }

        [HttpGet]
        [Route("api/car/{id}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute][Required] string id)
        {
            var result = await carService.GetByIdAsync(id);

            return result.Match(
                result => Ok(result),
                errors => Problem(errors)
            );
        }

        [HttpDelete]
        [Route("api/car/delete/{id}")]
        public async Task<IActionResult> DeleteByIdAsync([FromRoute][Required] string id)
        {
            var result = await carService.DeleteAsync(id);

            return result.Match(
                result => Ok(true),
                errors => Problem(errors)
            );
        }

        [HttpPost]
        [Route("api/car/create")]
        public async Task<IActionResult> CreateAsync([FromBody] [Required] CarModel model)
        {
            var result = await carService.CreateAsync(model);

            return result.Match(
                result => Ok(result),
                errors => Problem(errors)
            );
        }

        [HttpPut]
        [Route("api/car/update")]
        public async Task<IActionResult> UpdateAsync([FromBody][Required] CarModel model)
        {
            var result = await carService.UpdateAsync(model);

            return result.Match(
                result => Ok(new OkResult()),
                errors => Problem(errors)
            );
        }

        [HttpGet]
        [Route("api/car/page/{page}")]
        public async Task<IActionResult> GetPagedAsync([FromRoute] int page = 0)
        {
            var result = await carService.GetPagedAsync(page);
            return result.Match(
                result => Ok(result),
                errors => Problem(errors)
            );
        }
    }
}