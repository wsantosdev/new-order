using Microsoft.AspNetCore.Mvc;

namespace NewOrder.Custody.WebApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class RemoveController : ControllerBase
    {
        private readonly IRemoveUseCase _useCase;

        public RemoveController(IRemoveUseCase useCase) =>
            _useCase = useCase;

        [HttpPost]
        public IActionResult Remove([FromBody] RemoveRequest request)
        {
            var result = _useCase.Remove(request.AccountNumber, request.Symbol, request.Quantity);
            return result.IsSuccess
                    ? Ok()
                    : BadRequest(result.Error);
        }
    }
}
