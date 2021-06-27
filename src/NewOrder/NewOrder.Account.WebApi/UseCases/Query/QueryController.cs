using Microsoft.AspNetCore.Mvc;

namespace NewOrder.Account.WebApi.UseCases.Query
{
    [Route("api/[controller]")]
    [ApiController]
    public class QueryController : ControllerBase
    {
        private readonly IQueryUseCase _useCase;

        public QueryController(IQueryUseCase useCase) =>
            _useCase = useCase;

        [HttpGet]
        [Route("{accountNumber}")]
        public IActionResult Query(long accountNumber)
        {
            var queryResult = _useCase.Query(accountNumber);
            return queryResult.IsSuccess
                    ? Ok(queryResult.Value)
                    : BadRequest(queryResult.Error);
        }
    }
}
