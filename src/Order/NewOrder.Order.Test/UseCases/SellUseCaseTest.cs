using CSharpFunctionalExtensions;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace NewOrder.Order.Test
{
    public class SellUseCaseTest
    {
        private readonly IOrderDatabase _database;

        public SellUseCaseTest() =>
            _database = new FakeOrderDatabase();
        
        [Fact]
        public async ValueTask ShouldSellSuccessfully()
        {
            //Arrange
            var mockCustodyService = MockCustodyRemove(Result.Success());
            var mockAccountService = MockAccountCredit(Result.Success());
            var useCase = new SellUseCase(mockCustodyService, mockAccountService, _database);

            //Act
            var sellResult = await useCase.Sell(1, "TOYB4", 100, 10);

            //Assert
            Assert.True(sellResult.IsSuccess);
        }

        [Fact]
        public async ValueTask ShouldNotSellDueToInsuficientCustody()
        {
            //Arrange
            var mockCustodyService = MockCustodyRemove(Result.Failure(string.Empty));
            var mockAccountService = MockAccountCredit(Result.Success());
            var useCase = new SellUseCase(mockCustodyService, mockAccountService, _database);

            //Act
            var sellResult = await useCase.Sell(1, "TOYB4", 100, 10);

            //Assert
            Assert.True(sellResult.IsFailure);
        }

        private static IAccountService MockAccountCredit(Result result)
        {
            var mock = new Mock<IAccountService>();
            mock.Setup(s => s.Credit(It.IsAny<long>(), It.IsAny<decimal>()))
                .Returns(new ValueTask<Result>(result));

            return mock.Object;
        }

        private static ICustodyService MockCustodyRemove(Result result)
        {
            var mock = new Mock<ICustodyService>();
            mock.Setup(s => s.Remove(It.IsAny<long>(), It.IsAny<string>(), It.IsAny<int>()))
                .Returns(new ValueTask<Result>(result));

            return mock.Object;
        }
    }
}
