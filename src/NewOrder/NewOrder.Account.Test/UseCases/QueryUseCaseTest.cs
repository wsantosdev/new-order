using Xunit;

namespace NewOrder.Account.Test
{
    public class QueryUseCaseTest
    {
        private readonly IAccountDatabase _database;

        public QueryUseCaseTest()
        {
            _database = new FakeAccountDatabase();
        }

        [Fact]
        public void ShouldQuerySuccessfully()
        {
            //Arrange
            var account = Account.Create(1).Value;
            _database.Save(account);
            var useCase = new QueryUseCase(_database);

            //Act
            var queryResult = useCase.Query(1);

            //Assert
            Assert.True(queryResult.IsSuccess);
            Assert.NotNull(queryResult.Value);
            Assert.Equal(1, queryResult.Value.Number);
        }

        [Fact]
        public void ShouldNotQueryDueToNonExistentAccount()
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
