using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace NewOrder.Account.WebApi
{
    public class CreateHostedService : IHostedService
    {
        private readonly ICreateUseCase _useCase;

        public CreateHostedService(ICreateUseCase useCase) =>
            _useCase = useCase;
        
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _useCase.Create(1, 100_000);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
