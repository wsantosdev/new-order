using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace NewOrder.Order.WebApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class SellController : ControllerBase
    {
        private readonly ISellUseCase _useCase;

        public SellController(ISellUseCase useCase) =>
            _useCase = useCase;

        [HttpPost]
        public async ValueTask<IActionResult> Buy([FromBody] SellRequest request)
        {
            var sellResult = await _useCase.Sell(request.AccountNumber, request.Symbol, request.Quantity, request.Price)
                                           .ConfigureAwait(false);
            return sellResult.IsSuccess
                    ? Ok()
                    : BadRequest(sellResult.Error);
        }
    }
}