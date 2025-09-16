

namespace REST_Sample_01.Controllers
{
    public class CarController: ControllerBase
    {
        public List<string> Cars = [
            "Bmw 320d",
            "Opel Astra G",
            "Volkswagen Golf Mk4"];


        [HttpGet]
        [Route("car/{id}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute][Required] int id)
        {
            return Ok(Cars[id]);
        }



        [HttpGet]
        [Route("car")]
        public async Task<IActionResult> GetByQueryAsync([FromQuery][Required] int id)
        {
            return Ok(Cars[id]);
        }

        [HttpGet]
        [Route("car/all")]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(Cars);

        }

        [HttpPost]
        [Route("car/create")]
        public async Task<IActionResult> CreateAsync([FromBody][Required] string car)
        {
            Cars.Add(car);

            return Ok(Cars);

        }

        [HttpPut]
        [Route("car/update")]
        public async Task<IActionResult> UpdateAsync([FromBody][Required] CarUpdateModel model)
        {
            Cars[model.Id] = model.Name;

            return Ok(Cars);

        }

        [HttpDelete]
        [Route("car/id/{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute][Required] int id)
        {
            Cars.RemoveAt(id);

            return Ok(Cars);

        }

    }
}
