using AutoPecasERP.Data.Context;using Microsoft.Data.Sqlite;
namespace AutoPecasERP.Services.Backup;
public class BackupService{
public async Task<string> CriarAsync(string destino){Directory.CreateDirectory(destino);var nome=$"AutoPecasERP_{DateTime.Now:yyyyMMdd_HHmmss}.db";var arq=Path.Combine(destino,nome);await using var db=new ErpDbContext();await db.Database.OpenConnectionAsync();var source=(SqliteConnection)db.Database.GetDbConnection();await using var target=new SqliteConnection($"Data Source={arq}");await target.OpenAsync();source.BackupDatabase(target);return arq;}
public string[] Listar(string pasta)=>Directory.Exists(pasta)?Directory.GetFiles(pasta,"AutoPecasERP_*.db").OrderByDescending(File.GetCreationTime).ToArray():Array.Empty<string>();
public void ValidarBackup(string arquivo){if(!File.Exists(arquivo))throw new FileNotFoundException("Backup não encontrado.");using var c=new SqliteConnection($"Data Source={arquivo};Mode=ReadOnly");c.Open();using var cmd=c.CreateCommand();cmd.CommandText="PRAGMA integrity_check;";var r=cmd.ExecuteScalar()?.ToString();if(r!="ok")throw new InvalidDataException("O arquivo não passou na verificação de integridade.");}
}