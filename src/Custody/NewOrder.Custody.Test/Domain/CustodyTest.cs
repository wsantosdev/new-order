using Xunit;

namespace NewOrder.Custody.Test
{
    public class CustodyTest
    {
        [Fact]
        public void ShouldCreateSuccessfully()
        {
            //Act
            var createResult = Custody.Create(1);

            //Assert
            Assert.True(createResult.IsSuccess);
            Assert.Equal(1, createResult.Value.AccountNumber);
        }

        [Fact]
        public void ShouldNotCreateDueToInvalidAccountNumber()
        {
            //Act
            var createResultZero = Custody.Create(0);
            var createResultNegative = Custody.Create(-1);

            //Assert
            Assert.True(createResultZero.IsFailure);
            Assert.True(createResultNegative.IsFailure);
        }

        [Fact]
        public void ShouldAddSuccessfully()
        {
            //Arrange
            var custody = Custody.Create(1).Value;
            var symbol = "PETR4";
            var quantity = 100;

            //Act
            var addResult = custody.Add(symbol, quantity);
            var quantityInCustody = custody.GetQuantity(symbol);

            //Assert
            Assert.True(addResult.IsSuccess);
            Assert.Equal(quantity, quantityInCustody);
        }

        [Fact]
        public void ShouldNotAddDueToEmptySymbol()
        {
            //Arrange
            var custody = Custody.Create(1).Value;
            
            //Act
            var addResult = custody.Add(string.Empty, 100);

            //Assert
            Assert.True(addResult.IsFailure);
        }

        [Fact]
        public void ShouldNotAddDueToInvalidQuantity()
        {
            //Arrange
            var custody = Custody.Create(1).Value;

            //Act
            var addResultZero = custody.Add("PETR4", 0);
            var addResultNegative = custody.Add("PETR4", -1);

            //Arrange
            Assert.True(addResultZero.IsFailure);
            Assert.True(addResultNegative.IsFailure);
        }

        [Fact]
        public void ShouldRemoveSuccessfully()
        {
            //Arrange
            var custody = Custody.Create(1).Value;
            var symbol = "PETR4";
            var quantity = 100;
            custody.Add(symbol, quantity);

            //Act
            var addResult = custody.Remove(symbol, quantity);
            var quantityInCustody = custody.GetQuantity(symbol);

            //Assert
            Assert.True(addResult.IsSuccess);
            Assert.Equal(0, quantityInCustody);
        }

        [Fact]
        public void ShouldNotRemoveDueToEmptySymbol()
        {
            //Arrange
            var custody = Custody.Create(1).Value;

            //Act
            var addResult = custody.Remove(string.Empty, 100);

            //Assert
            Assert.True(addResult.IsFailure);
        }

        [Fact]
        public void ShouldNotRemoveDueToInvalidQuantity()
        {
            //Arrange
            var custody = Custody.Create(1).Value;

            //Act
            var addResultZero = custody.Remove("PETR4", 0);
            var addResultNegative = custody.Remove("PETR4", -1);

            //Arrange
            Assert.True(addResultZero.IsFailure);
            Assert.True(addResultNegative.IsFailure);
        }
    }
}
