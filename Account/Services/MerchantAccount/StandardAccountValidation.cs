using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Account.Services.Validations;

public class MerchantAccountValidation {
  public string Name { get; private set; }
  public DateOnly Age { get; private set; }
  public string Cpf { get; private set; }
  public string CompanyName { get; private set; }
  public string Cnpj { get; private set; }
  public string Email { get; private set; }
  public string Username { get; private set; }
  public string Password { get; private set; }

  public MerchantAccountValidation(string name, string age, string cpf, string companyName, string cnpj, string email, string username, string password) {
    Name = name;
    Username = username;
    CompanyName = companyName;
    Age = DateOnly.Parse(age);
    Cpf = IsValidCpf(cpf);
    Cnpj = IsValidCnpj(cnpj);
    Email = IsValidEmail(email);
    Password = IsValidPassword(password);

    OrdinariesValidations();
  }

  private void OrdinariesValidations() {
    if (string.IsNullOrWhiteSpace(Name)) throw new ArgumentException("Name is required.", nameof(Name));

    if (string.IsNullOrWhiteSpace(Username)) throw new ArgumentException("Username is required", nameof(Username));

    if (string.IsNullOrWhiteSpace(CompanyName)) throw new ArgumentException("The company name is required", nameof(CompanyName));

    if (Age.Year < 1900) throw new ArgumentOutOfRangeException("Add a valid age", nameof(Age));

    if ((DateTime.Now.Year - Age.Year) < 18) throw new ArgumentException("To create an merchant account is need have at least 18 old", nameof(Age));
  }

  private string IsValidCpf(string cpf) {
    if (string.IsNullOrEmpty(cpf))
      throw new ArgumentException("CPF cannot be empty.", nameof(cpf));

    cpf = cpf.Replace(".", "").Replace("-", ""); // Remove caracteres especiais
    if (cpf.Length != 11 || !Regex.IsMatch(cpf, @"^\d{11}$"))
      throw new ArgumentException("Invalid CPF.", nameof(cpf));

    return cpf;
  }

  private string IsValidCnpj(string cnpj) {
    if (string.IsNullOrEmpty(cnpj))
      throw new ArgumentException("CNPJ cannot be empty.", nameof(cnpj));

    cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", ""); // Remove caracteres especiais
    if (cnpj.Length != 14 || !Regex.IsMatch(cnpj, @"^\d{14}$"))
      throw new ArgumentException("Invalid CNPJ.", nameof(cnpj));
    
    return cnpj;
  }

  public string IsValidEmail(string email) {
    if (string.IsNullOrEmpty(email))
      throw new ArgumentException("Email cannot be empty.", nameof(email));

    if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
      throw new ArgumentException("Invalid email format.", nameof(email));

    return email;
  }

  private string IsValidPassword(string password) {
    if (string.IsNullOrEmpty(password))
      throw new ArgumentException("Password can't be empty.", nameof(password));

    // Minimo de 8 caracteres, ao menos uma letra maiúscula, uma caixa minúscula, um número e um caracter especial
    if (!Regex.IsMatch(password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$"))
      throw new ArgumentException("Password doesn't meet the required security standards.", nameof(password));

    return HashPassword(password);
  }

  private string HashPassword(string password) {
    var hashedBytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));
    return Convert.ToBase64String(hashedBytes);
  }
}