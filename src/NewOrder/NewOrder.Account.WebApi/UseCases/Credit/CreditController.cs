using Microsoft.AspNetCore.Mvc;

namespace NewOrder.Account.WebApi.UseCases.Credit
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreditController : ControllerBase
    {
        private readonly ICreditUseCase _useCase;

        public CreditController(ICreditUseCase useCase) =>
            _useCase = useCase;

        [HttpPost]
        public IActionResult Credit([FromBody] DebitRequest request)
        {
            var result = _useCase.Credit(request.AccountNumber, request.Amount);
            return result.IsSuccess
                    ? Ok()
                    : BadRequest(result.Error);
        }
    }
}
