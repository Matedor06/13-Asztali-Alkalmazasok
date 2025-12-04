namespace Solution.Api.Controllers;

[Route("api/[controller]")]
public class BillItemController : BaseController
{
    private readonly IBillItemService _billItemService;

    public BillItemController(IBillItemService billItemService)
    {
        _billItemService = billItemService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _billItemService.GetAllAsync();

        return result.Match(
            items => Ok(items),
            errors => Problem(errors)
        );
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _billItemService.GetByIdAsync(id);

        return result.Match(
            item => Ok(item),
            errors => Problem(errors)
        );
    }

    [HttpGet("bill/{billId}")]
    public async Task<IActionResult> GetByBillId(int billId)
    {
        var result = await _billItemService.GetByBillIdAsync(billId);

        return result.Match(
            items => Ok(items),
            errors => Problem(errors)
        );
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] BillItemModel model)
    {
        var result = await _billItemService.CreateAsync(model);

        return result.Match(
            item => CreatedAtAction(nameof(GetById), new { id = item.Id }, item),
            errors => Problem(errors)
        );
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] BillItemModel model)
    {
        if (id != model.Id)
            return BadRequest("ID mismatch");

        var result = await _billItemService.UpdateAsync(model);

        return result.Match(
            item => Ok(item),
            errors => Problem(errors)
        );
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _billItemService.DeleteAsync(id);

        return result.Match(
            success => Ok(new OkResult()),
            errors => Problem(errors)
        );
    }
}
