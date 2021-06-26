using Xunit;

namespace NewOrder.Account.Test
{
    public class CreditUseCaseTest
    {
        private readonly IAccountDatabase _database;

        public CreditUseCaseTest()
        {
            _database = new FakeAccountDatabase();
        }

        [Fact]
        public void ShouldCreditSuccessfully()
        {
            //Arrange
            _database.Save(Account.Create(1).Value);
            var useCase = new CreditUseCase(_database);

            //Act
            var result = useCase.Credit(1, 1000);
            var account = _database.Get(1);

            //Arrange
            Assert.True(result.IsSuccess);
            Assert.Equal(1000, account.Balance);
        }

        [Fact]
        public void ShouldNotCreditDueToInvalidAccountNumber()
        {
            //Arrange
            var useCase = new CreditUseCase(_database);

            //Act
            var result = useCase.Credit(1, 100);

            //Assert
            Assert.True(result.IsFailure);
        }

        [Fact]
        public void ShouldNotCreditDueToInvalidAmount()
        {
            //Arrange
            _database.Save(Account.Create(1).Value);
            var useCase = new CreditUseCase(_database);

            //Act
            var resultZero = useCase.Credit(1, 0);
            var resultNegative = useCase.Credit(1, -100);
            var account = _database.Get(1);

            //Assert
            Assert.True(resultZero.IsFailure);
            Assert.True(resultNegative.IsFailure);
            Assert.Equal(0, account.Balance);
        }
    }
}
