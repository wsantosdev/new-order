using Xunit;

namespace NewOrder.Account.Test
{
    public class DebitUseCaseTest
    {
        private readonly IAccountDatabase _database;

        public DebitUseCaseTest()
        {
            _database = new FakeAccountDatabase();
        }

        [Fact]
        public void ShouldDebitSuccessfully()
        {
            //Arrange
            _database.Save(Account.Create(1, 100).Value);
            var useCase = new DebitUseCase(_database);

            //Act
            var result = useCase.Debit(1, 100);
            var account = _database.Get(1);

            //Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(0, account.Balance);
        }

        [Fact]
        public void ShouldNotDebitDueToInvalidAccountNumber()
        {
            //Arrange
            var useCase = new DebitUseCase(_database);

            //Act
            var result = useCase.Debit(1, 100);

            //Assert
            Assert.True(result.IsFailure);
        }

        [Fact]
        public void SouldNotDebitDueToInvalidAmount()
        {
            //Arrange
            _database.Save(Account.Create(1, 100).Value);
            var useCase = new DebitUseCase(_database);

            //Act
            var result = useCase.Debit(1, -1);
            var account = _database.Get(1);

            //Assert
            Assert.True(result.IsFailure);
            Assert.Equal(100, account.Balance);
        }
    }
}
