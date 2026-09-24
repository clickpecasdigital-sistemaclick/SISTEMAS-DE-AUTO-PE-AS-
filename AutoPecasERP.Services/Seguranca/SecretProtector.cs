using System.Security.Cryptography;using System.Text;
namespace AutoPecasERP.Services.Seguranca;
public static class SecretProtector{
 public static string Protect(string value){if(string.IsNullOrEmpty(value))return "";var b=Encoding.UTF8.GetBytes(value);return Convert.ToBase64String(ProtectedData.Protect(b,null,DataProtectionScope.CurrentUser));}
 public static string Unprotect(string value){if(string.IsNullOrEmpty(value))return "";var b=Convert.FromBase64String(value);return Encoding.UTF8.GetString(ProtectedData.Unprotect(b,null,DataProtectionScope.CurrentUser));}
}