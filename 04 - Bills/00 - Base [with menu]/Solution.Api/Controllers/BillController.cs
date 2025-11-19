using Solution.Services.Services.Bill.Dtos;

namespace Solution.Api.Controllers;

[Route("api/[controller]")]
public class BillController : BaseController
{
    private readonly IBillService _billService;

    public BillController(IBillService billService)
    {
        _billService = billService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _billService.GetAllAsync();

        return result.Match(
            bills => Ok(bills),
            errors => Problem(errors)
        );
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _billService.GetByIdAsync(id);

        return result.Match(
            bill => Ok(bill),
            errors => Problem(errors)
        );
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateBillDto dto)
    {
        var result = await _billService.CreateAsync(dto);

        return result.Match(
            bill => CreatedAtAction(nameof(GetById), new { id = bill.Id }, bill),
            errors => Problem(errors)
        );
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateBillDto dto)
    {
        var result = await _billService.UpdateAsync(id, dto);

        return result.Match(
            bill => Ok(bill),
            errors => Problem(errors)
        );
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _billService.DeleteAsync(id);

        return result.Match(
            success => Ok(new OkResult()),
            errors => Problem(errors)
        );
    }
}
