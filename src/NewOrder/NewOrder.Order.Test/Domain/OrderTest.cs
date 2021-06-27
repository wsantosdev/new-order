using System;
using Xunit;

namespace NewOrder.Order.Test
{
    public class OrderTest
    {
        [Fact]
        public void ShouldCreateSuccessfully()
        {
            //Act
            var createResult = Order.Create(Guid.NewGuid(), 1, true, "PETR4", 100, 0.75M);

            //Assert
            Assert.True(createResult.IsSuccess);
        }

        [Fact]
        public void ShouldNotCreateDueToAbsentSymbol()
        {
            //Act
            var createResult = Order.Create(Guid.NewGuid(), 1, true, null, 100, 10M);

            //Assert
            Assert.True(createResult.IsFailure);
        }

        [Fact]
        public void ShouldNotCreateDueToInvalidQuantity()
        { 
            //Act
            var createResultZero = Order.Create(Guid.NewGuid(), 1, true, "TOYB4", 0, 10M);
            var createResultNegative = Order.Create(Guid.NewGuid(), 1, true, "TOYB4", -10, 10M);

            //Assert
            Assert.True(createResultZero.IsFailure);
            Assert.True(createResultNegative.IsFailure);
        }

        [Fact]
        public void ShouldNotCreateDueToInvalidPrice()
        {
            //Act
            var createResultZero = Order.Create(Guid.NewGuid(), 1, true, "TOYB4", 100, 0);
            var createResultNegative = Order.Create(Guid.NewGuid(), 1, true, "TOYB4", 100, -1M);

            //Assert
            Assert.True(createResultZero.IsFailure);
            Assert.True(createResultNegative.IsFailure);
        }
    }
}
