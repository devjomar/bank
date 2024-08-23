using Account.Models;

namespace Account.Services.Interfaces;

public interface IMerchantAccount {
  MerchantAccountModel Create(MerchantAccount merchant);
  string Recovery(string cnpj);
  void Delete();
}

public class MerchantAccount {
  public required string Name { get; set; }
  public required string Age { get; set; }
  public required string Cpf { get; set; }
  public required string CompanyName { get; set; }
  public required string Cnpj { get; set; }
  public required string Email { get; set; }
  public required string Password { get; set; }
  public required string Username { get; set; }
}