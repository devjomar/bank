using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Account.Services.Validations;

public class StandardAccountValidation {
  public string Name { get; private set; }
  public DateOnly Age { get; private set; }
  public string Cpf { get; private set; }
  public string Email { get; private set; }
  public string Username { get; private set; }
  public string Password { get; private set; }

  public StandardAccountValidation(string name, string age, string cpf, string email, string username, string password) {
    Name = name;
    Username = username;
    Age = DateOnly.Parse(age);
    Cpf = IsValidCpf(cpf);
    Email = IsValidEmail(email);
    Password = IsValidPassword(password);

    OrdinariesValidations();
  }

  private void OrdinariesValidations() {
    if (string.IsNullOrWhiteSpace(Name)) throw new ArgumentException("Name is required.", nameof(Name));

    if (string.IsNullOrWhiteSpace(Username)) throw new ArgumentException("Username is required", nameof(Username));

    if (Age.Year < 1900) throw new ArgumentOutOfRangeException("Add a valid age", nameof(Age));

    if ((DateTime.Now.Year - Age.Year) < 16) throw new ArgumentException("To create an account is need have at least 16 old or more", nameof(Age));
  }

  private string IsValidCpf(string cpf) {
    if (string.IsNullOrEmpty(cpf))
      throw new ArgumentException("CPF cannot be empty.", nameof(cpf));

    cpf = cpf.Replace(".", "").Replace("-", ""); // Remove caracteres especiais
    if (cpf.Length != 11 || !Regex.IsMatch(cpf, @"^\d{11}$"))
      throw new ArgumentException("Invalid CPF.", nameof(cpf));

    return cpf;
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