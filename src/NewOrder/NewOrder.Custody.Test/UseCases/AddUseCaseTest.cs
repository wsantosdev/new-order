using Xunit;

namespace NewOrder.Custody.Test
{
    public class AddUseCaseTest
    {
        private readonly ICustodyDatabase _database;

        public AddUseCaseTest()
        {
            _database = new FakeCustodyDatabase();
        }

        [Fact]
        public void ShouldAddSuccessfully()
        {
            //Arrange
            var createResult = Custody.Create(1);
            _database.Save(createResult.Value);
            var useCase = new AddUseCase(_database);

            //Act
            var addResult = useCase.Add(1, "PETR4", 100);
            var custody = _database.Get(1);

            //Assert
            Assert.True(addResult.IsSuccess);
            Assert.Equal(100, custody.GetQuantity("PETR4"));
        }

        [Fact]
        public void ShouldNotAddDueToNotFoundCustody()
        {
            //Arrange
            var useCase = new AddUseCase(_database);

            //Act
            var addResult = useCase.Add(1, "TOYB4", 100);
            var custody = _database.Get(1);

            //Assert
            Assert.True(addResult.IsFailure);
            Assert.Null(custody);
        }

        [Fact]
        public void ShouldNotAddDueToInvalidEntry()
        {
            //Arrange
            var createResult = Custody.Create(1);
            _database.Save(createResult.Value);
            var useCase = new AddUseCase(_database);

            //Act
            var addResultEmptySymbol = useCase.Add(1, string.Empty, 100);
            var addResultZeroQuantity = useCase.Add(1, "PETR4", 0);
            var addResultNegativeQuantity = useCase.Add(1, "PETR4", -1);
            var custody = _database.Get(1);

            //Assert
            Assert.True(addResultEmptySymbol.IsFailure);
            Assert.True(addResultZeroQuantity.IsFailure);
            Assert.True(addResultNegativeQuantity.IsFailure);
            Assert.Equal(0, custody.GetQuantity("PETR4"));
        }
    }
}
