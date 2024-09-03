using Fintech.API.Account.Domain;
using Fintech.API.Account.Services;

namespace Fintech.API.Account.UseCases;

public class AccountUseCase : IAccountUseCase
{
    private readonly IAccountService _createAccountService;

    public AccountUseCase(IAccountService createAccountService)
    {
        _createAccountService = createAccountService;
    }
    public static string GenerateRandom10DigitNumber()
    {
        Random random = new Random();
        string tenDigitNumber = random.Next(1, 10).ToString();

        for (int i = 1; i < 10; i++)
        {
            tenDigitNumber += random.Next(0, 10).ToString();
        }

        return tenDigitNumber;
    }

    public async Task<string> GenerateAccountNumberAsync()
    {
        string accountNumber;
        bool isUnique;

        do
        {
            accountNumber = GenerateRandom10DigitNumber();
            isUnique = await _createAccountService.IsAccountNumberUniqueAsync(accountNumber);
        } while (!isUnique);

        return accountNumber;
    }

    public async Task<CreateAccountResponse> CreateAccount(CreateAccountRequest account)
    {
        var accountNumber = await GenerateAccountNumberAsync();
        var clabe = $"014421{accountNumber}0";
        var accountModel = new AccountModel() {
            CustomerId = account.CustomerId,
            AccountNumber = accountNumber,
            Clabe = clabe,
            Balance = 0
        };
        await _createAccountService.CreateAccount(accountModel);
        return new CreateAccountResponse() {
            AccountNumber = accountModel.AccountNumber,
            Clabe = accountModel.Clabe,
            Balance = accountModel.Balance
        };
    }
}
