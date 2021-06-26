using Xunit;

namespace NewOrder.Custody.Test
{
    public class RemoveUseCaseTest
    {
        private readonly ICustodyDatabase _database;

        public RemoveUseCaseTest()
        {
            _database = new FakeCustodyDatabase();
        }

        [Fact]
        public void ShouldRemoveSuccessfully()
        {
            //Arrange
            var createResult = Custody.Create(1);
            createResult.Value.Add("PETR4", 100);
            _database.Save(createResult.Value);
            var useCase = new RemoveUseCase(_database);

            //Act
            var addResult = useCase.Remove(1, "PETR4", 100);
            var custody = _database.Get(1);

            //Assert
            Assert.True(addResult.IsSuccess);
            Assert.Equal(0, custody.GetQuantity("PETR4"));
        }

        [Fact]
        public void ShouldNotRemoveDueToNotFoundCustody()
        {
            //Arrange
            var useCase = new RemoveUseCase(_database);

            //Act
            var addResult = useCase.Remove(1, "TOYB4", 100);
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
            createResult.Value.Add("PETR4", 100);
            _database.Save(createResult.Value);
            var useCase = new RemoveUseCase(_database);

            //Act
            var removeResultEmptySymbol = useCase.Remove(1, string.Empty, 100);
            var removeResultZeroQuantity = useCase.Remove(1, "PETR4", 0);
            var removeResultNegativeQuantity = useCase.Remove(1, "PETR4", -1);
            var custody = _database.Get(1);

            //Assert
            Assert.True(removeResultEmptySymbol.IsFailure);
            Assert.True(removeResultZeroQuantity.IsFailure);
            Assert.True(removeResultNegativeQuantity.IsFailure);
            Assert.Equal(100, custody.GetQuantity("PETR4"));
        }
    }
}
