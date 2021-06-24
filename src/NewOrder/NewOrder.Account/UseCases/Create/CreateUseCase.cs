﻿using CSharpFunctionalExtensions;

namespace NewOrder.Account
{
    public class CreateUseCase : ICreateUseCase
    {
        private readonly IAccountDatabase _database;

        public CreateUseCase(IAccountDatabase database) =>
            _database = database;

        public Result Create(long accountNumber, decimal initialDeposit)
        {
            var account = _database.Get(accountNumber);
            if (account != null)
                return Result.Failure($"The is already an account with number {accountNumber}.");

            account = Account.Create(accountNumber, initialDeposit);
            _database.Save(account);

            return Result.Success();
        }
    }
}