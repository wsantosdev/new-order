using Xunit;

namespace NewOrder.Custody.Test.UseCases
{
    public class QueryUseCaseTest
    {
        private readonly ICustodyDatabase _database;

        public QueryUseCaseTest() =>
            _database = new FakeCustodyDatabase();

        [Fact]
        public void ShouldQuerySuccessfully()
        {
            //Arrange
            var createResult = Custody.Create(1);
            _database.Save(createResult.Value);
            var useCase = new QueryUseCase(_database);

            //Act
            var queryResult = useCase.Query(1);

            //Assert
            Assert.True(queryResult.IsSuccess);
            Assert.Equal(1, queryResult.Value.AccountNumber);
        }

        [Fact]
        public void ShouldNotQueryDueToNonExistentCustody()
        {
            //Arrange
            var useCase = new QueryUseCase(_database);

            //Act
            var queryResult = useCase.Query(1);

            //Assert
            Assert.True(queryResult.IsFailure);
        }
    }
}
