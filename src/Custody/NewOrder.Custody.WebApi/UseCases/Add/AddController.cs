using Microsoft.AspNetCore.Mvc;

namespace NewOrder.Custody.WebApi.UseCases.Add
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddController : ControllerBase
    {
        private readonly IAddUseCase _useCase;

        public AddController(IAddUseCase useCase) =>
            _useCase = useCase;

        [HttpPost]
        public IActionResult Add([FromBody] AddRequest request)
        {
            var result = _useCase.Add(request.AccountNumber, request.Symbol, request.Quantity);
            return result.IsSuccess
                    ? Ok()
                    : BadRequest(result.Error);
        }
    }
}
