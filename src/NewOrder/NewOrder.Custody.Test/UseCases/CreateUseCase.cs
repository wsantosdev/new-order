using Xunit;

namespace NewOrder.Custody.Test
{
    public class CreateUseCaseTest
    {
        private readonly ICustodyDatabase _database;

        public CreateUseCaseTest()
        {
            _database = new FakeCustodyDatabase();
        }

        [Fact]
        public void ShouldCreateSuccessfully()
        {
            //Arrange
            var useCase = new CreateUseCase(_database);

            //Act
            var createResult = useCase.Create(1);
            var custody = _database.Get(1);

            //Assert
            Assert.True(createResult.IsSuccess);
            Assert.Equal(1, custody.AccountNumber);
        }

        [Fact]
        public void ShouldNotCreateDueToInvalidAccountNumber()
        {
            //Arrange
            var useCase = new CreateUseCase(_database);

            //Act
            var createResultZero = useCase.Create(0);
            var custodyZero = _database.Get(0);

            var createResultNegative = useCase.Create(-1);
            var custodyNegative = _database.Get(-1);

            //Assert
            Assert.True(createResultZero.IsFailure);
            Assert.Null(custodyZero);

            Assert.True(createResultNegative.IsFailure);
            Assert.Null(custodyNegative);
        }

        [Fact]
        public void ShouldNotCreateDueToBeAlreadyCreated()
        {
            //Arrange
            var result = Custody.Create(1);
            result.Value.Add("PETR4", 100);
            _database.Save(result.Value);
            var useCase = new CreateUseCase(_database);

            //Act
            var createResult = useCase.Create(1);
            var custody = _database.Get(1);

            //Assert
            Assert.True(createResult.IsFailure);
            Assert.Equal(100, custody.GetQuantity("PETR4"));
        }
    }
}
