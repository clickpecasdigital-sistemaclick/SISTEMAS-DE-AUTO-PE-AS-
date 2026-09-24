$ErrorActionPreference="Stop"
$root=Split-Path $PSScriptRoot -Parent;$bad=@()
$cs=Get-ChildItem "$root\src" -Recurse -Filter *.cs
foreach($f in $cs){$t=Get-Content $f.FullName -Raw
 if($t -match "\|\|await"){$bad+="Expressao invalida ||await: $($f.FullName)"}
 if($t -match "EntidadeId\s*="){$bad+="Auditoria usa EntidadeId inexistente: $($f.FullName)"}
}
$db=Get-Content "$root\src\AutoPecasERP.Data\Context\ErpDbContext.cs" -Raw
$names=[regex]::Matches($db,'DbSet<[^>]+>\s+(\w+)')|%{$_.Groups[1].Value}
$dups=$names|Group-Object|? Count -gt 1
foreach($d in $dups){$bad+="DbSet duplicado: $($d.Name)"}
if($bad.Count){$bad|%{Write-Error $_};exit 1}
Write-Host "Auditoria estatica estrutural aprovada."
