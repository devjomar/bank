using Account.Models;

namespace Account.Services.Interfaces;

public interface IStandardAccount {
  StandardAccountModel Create(StandardAccount account);
  string Recovery(string cpf);
  void Delete();
}

public class StandardAccount {
  public required string Name { get; set; }
  public required string Age { get; set; }
  public required string Cpf { get; set; }
  public required string Email { get; set; }
  public required string Password { get; set; }
  public required string Username { get; set; }
}