using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace NewOrder.Order.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuyController : ControllerBase
    {
        private readonly IBuyUseCase _useCase;

        public BuyController(IBuyUseCase useCase) =>
            _useCase = useCase;

        [HttpPost]
        public async ValueTask<IActionResult> Buy([FromBody] BuyRequest request)
        {
            var buyResult = await _useCase.Buy(request.AccountNumber, request.Symbol, request.Quantity, request.Price)
                                          .ConfigureAwait(false);
            return buyResult.IsSuccess
                    ? Ok()
                    : BadRequest(buyResult.Error);
        }
    }
}