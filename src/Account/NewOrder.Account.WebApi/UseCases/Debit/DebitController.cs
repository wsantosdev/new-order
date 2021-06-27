using Microsoft.AspNetCore.Mvc;

namespace NewOrder.Account.WebApi.UseCases.Credit
{
    [Route("api/[controller]")]
    [ApiController]
    public class DebitController : ControllerBase
    {
        private readonly IDebitUseCase _useCase;

        public DebitController(IDebitUseCase useCase) =>
            _useCase = useCase;

        [HttpPost]
        public IActionResult Credit([FromBody] DebitRequest request)
        {
            var result = _useCase.Debit(request.AccountNumber, request.Amount);
            return result.IsSuccess
                    ? Ok()
                    : BadRequest(result.Error);
        }
    }
}
