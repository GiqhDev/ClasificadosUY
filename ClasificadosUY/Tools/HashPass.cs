using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace ClasificadosUY.Tools
{
  public class HashPass
  {
    public static string HashPassword(string password)
    {
      using (SHA256 sha256Hash = SHA256.Create())
      {
        byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
        StringBuilder builder = new();
        foreach (byte b in bytes)
        {
          builder.Append(b.ToString("x2"));
        }
        return builder.ToString();
      }
    }

    public static bool VerifyPassword(string password, string base64Hash)
    {
      return HashPassword(password).Equals(base64Hash);
    }

    public static string CheckPasswordStrength(string password)
    {
      StringBuilder sb = new StringBuilder();
      if (password.Length < 8)
      {
        sb.Append("La contraseña debe tener minimo 8 caracteres. " + Environment.NewLine);
      }

      if (!(Regex.IsMatch(password, "[a-z]") && Regex.IsMatch(password, "[A-Z]") && Regex.IsMatch(password, "[0-9]")))
      {
        sb.Append("La contraseña debe contener mayusculas, minusculas y numeros " + Environment.NewLine);
      }
      return sb.ToString();
    }
  }
}
