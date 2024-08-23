using Account.Services.Validations;
using Account.Models;
using Account.Services.Interfaces;

namespace Account.Services;

public class MerchantAccountService : IMerchantAccount {

  public MerchantAccountModel Create(MerchantAccount merchant) {
    MerchantAccountValidation validation = new MerchantAccountValidation(merchant.Name, merchant.Age,
      merchant.Cpf, merchant.CompanyName, merchant.Cnpj, merchant.Email, merchant.Username, merchant.Password);

      MerchantAccountModel account = new MerchantAccountModel
      {
        Id = Guid.NewGuid(),
        Name = validation.Name,
        Age = validation.Age.ToShortDateString(),
        Cpf = validation.Cpf,
        CompanyName = validation.CompanyName,
        Cnpj = validation.Cnpj,
        Email = validation.Email,
        Password = validation.Password,
        Username = validation.Username
      };

      return account;
  }

  public string Recovery(string cnpj) {
    return ""; // envia um link de recuperação para o email vinculado ao cpf
  }

  public void Delete() {}
}