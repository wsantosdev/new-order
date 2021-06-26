using Xunit;

namespace NewOrder.Account.Test
{
    public class CreateUseCaseTest
    {
        private readonly IAccountDatabase _database;
        
        public CreateUseCaseTest()
        {
            _database = new FakeAccountDatabase();
        }
        
        [Fact]
        public void ShouldCreateAndPersistSuccessfully()
        {
            //Arrange
            var useCase = new CreateUseCase(_database);

            //Act
            var result = useCase.Create(1, 100);
            var account = _database.Get(1);

            //Aseert
            Assert.True(result.IsSuccess);
            Assert.NotNull(account);
            Assert.Equal(100, account.Balance);
        }

        [Fact]
        public void ShouldNotCreateDueToInvalidAccountNumber()
        {
            //Arrange
            var useCase = new CreateUseCase(_database);

            //Act
            var result = useCase.Create(-1);
            var account = _database.Get(-1);

            //Assert
            Assert.True(result.IsFailure);
            Assert.Null(account);
        }

        [Fact]
        public void ShouldNotCreateDueToInvalidAmount()
        {
            //Arrange
            var useCase = new CreateUseCase(_database);

            //Act
            var result = useCase.Create(1, -1);
            var account = _database.Get(1);

            //Arrange
            Assert.True(result.IsFailure);
            Assert.Null(account);
        }
    }
}
