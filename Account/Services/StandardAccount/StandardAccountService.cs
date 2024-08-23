using Account.Models;
using Account.Services.Validations;
using Account.Services.Interfaces;

namespace Account.Services;

public class StandardAccountService : IStandardAccount {

  public StandardAccountModel Create(StandardAccount standard) {
    StandardAccountValidation validation = new StandardAccountValidation(standard.Name, standard.Age,
      standard.Cpf, standard.Email, standard.Username, standard.Password);

      StandardAccountModel account = new StandardAccountModel
      {
        Id = Guid.NewGuid(),
        Name = validation.Name,
        Age = validation.Age.ToShortDateString(),
        Cpf = validation.Cpf,
        Email = validation.Email,
        Password = validation.Password,
        Username = validation.Username
      };

      return account;
  }

  public string Recovery(string cpf) {
    return ""; // envia um link de recuperação para o email vinculado ao cpf
  }

  public void Delete() {}
}