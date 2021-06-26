using Xunit;

namespace NewOrder.Account.Test
{
    public class AccountTest
    {
        [Fact]
        public void ShouldCreateSuccessfullyWithInitialDeposit()
        {
            //Act
            var result = Account.Create(1, .01M);

            //Assert
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public void ShouldCreateSuccessfullyWithoutInitialDeposit()
        {
            //Act
            var result = Account.Create(1);

            //Assert
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public void ShouldNotCreateDueToInvalidAccountNumber()
        {
            //Act
            var createResultZero = Account.Create(0);
            var createResultNegative = Account.Create(-1);

            //Assert
            Assert.True(createResultZero.IsFailure);
            Assert.True(createResultNegative.IsFailure);
        }

        [Fact]
        public void ShouldNotCreateDueToInvalidInitialDeposit()
        {
            //Act
            var createResult = Account.Create(1, -1);

            //Assert
            Assert.True(createResult.IsFailure);
        }

        [Fact]
        public void ShouldDebitSuccessfully()
        {
            //Arrange
            var account = Account.Create(1, 1000).Value;

            //Act
            account.Debit(1000);

            //Assert
            Assert.Equal(0, account.Balance);
        }

        [Fact]
        public void ShouldNotDebitDueToInvalidAmount()
        {
            //Arrange
            var account = Account.Create(1).Value;

            //Act
            var debitResultZero = account.Debit(0);
            var debitResultNegative = account.Debit(-1);

            //Assert
            Assert.True(debitResultZero.IsFailure);
            Assert.True(debitResultNegative.IsFailure);
        }

        [Fact]
        public void ShouldNotDebitDueToInsuficientFunds()
        {
            //Arrange
            var account = Account.Create(1).Value;

            //Act
            var debitResult = account.Debit(100);

            //Assert
            Assert.False(debitResult.IsSuccess);
        }

        [Fact]
        public void ShouldCreditSuccessfully()
        {
            //Arrange
            var account = Account.Create(1).Value;

            //Act
            account.Credit(1000);

            //Assert
            Assert.Equal(1000, account.Balance);
        }

        [Fact]
        public void ShouldNotCreditDueToInvalidAmount()
        {
            //Arrange
            var account = Account.Create(1).Value;

            //Act
            var creditResultZero = account.Credit(0);
            var creditResultNegative = account.Credit(-1);

            //Assert
            Assert.False(creditResultZero.IsSuccess);
            Assert.False(creditResultNegative.IsSuccess);
        }
    }
}
