namespace AutoPecasERP.Services.Validacao;
public static class DocumentoValidator
{
    public static string Digitos(string? s) => new string((s ?? "").Where(char.IsDigit).ToArray());
    public static bool CpfCnpjFormatoValido(string? s)
    {
        var d = Digitos(s);
        return d.Length is 11 or 14;
    }
    public static bool UfValida(string? uf)
    {
        string[] ufs = { "AC","AL","AP","AM","BA","CE","DF","ES","GO","MA","MT","MS","MG","PA","PB","PR","PE","PI","RJ","RN","RS","RO","RR","SC","SP","SE","TO" };
        return ufs.Contains((uf ?? "").Trim().ToUpperInvariant());
    }
}