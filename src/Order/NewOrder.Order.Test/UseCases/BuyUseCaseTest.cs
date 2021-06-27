using CSharpFunctionalExtensions;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace NewOrder.Order.Test
{
    public class BuyUseCaseTest
    {
        private readonly IOrderDatabase _database;

        public BuyUseCaseTest() =>
            _database = new FakeOrderDatabase();

        [Fact]
        public async ValueTask ShouldBuySuccessfully()
        {
            //Arrange
            var accountService = MockAccountDebit(Result.Success());
            var custodyService = MockCustodyAdd(Result.Success());
            var useCase = new BuyUseCase(accountService, custodyService, _database);

            //Act
            var buyResult = await useCase.Buy(1, "PETR4", 100, 10);

            //Assert
            Assert.True(buyResult.IsSuccess);
        }

        [Fact]
        public async ValueTask ShouldNotBuyDueToInsuficientFunds()
        {
            //Arrange
            var accountService = MockAccountDebit(Result.Failure(string.Empty));
            var custodyService = MockCustodyAdd(Result.Success());
            var useCase = new BuyUseCase(accountService, custodyService, _database);

            //Act
            var buyResult = await useCase.Buy(1, "PETR4", 100, 10);

            //Assert
            Assert.True(buyResult.IsFailure);
        }

        private static IAccountService MockAccountDebit(Result result)
        {
            var mock = new Mock<IAccountService>();
            mock.Setup(s => s.Debit(It.IsAny<long>(), It.IsAny<decimal>()))
                .Returns(new ValueTask<Result>(result));

            return mock.Object;
        }

        private static ICustodyService MockCustodyAdd(Result result)
        {
            var mock = new Mock<ICustodyService>();
            mock.Setup(s => s.Add(It.IsAny<long>(), It.IsAny<string>(), It.IsAny<int>()))
                .Returns(new ValueTask<Result>(result));

            return mock.Object;
        }
    }
}
