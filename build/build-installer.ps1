$ErrorActionPreference="Stop"
$root=Split-Path $PSScriptRoot -Parent
& "$PSScriptRoot\publish-win-x64.ps1"
$iscc=(Get-Command ISCC.exe -ErrorAction SilentlyContinue)
if(-not $iscc){
 $paths=@("${env:ProgramFiles(x86)}\Inno Setup 6\ISCC.exe","${env:ProgramFiles}\Inno Setup 6\ISCC.exe")
 $exe=$paths|Where-Object{Test-Path $_}|Select-Object -First 1
}else{$exe=$iscc.Source}
if(-not $exe){throw "Inno Setup 6 nao encontrado. Instale-o e execute novamente."}
& $exe "$root\Installer\AutoPecasERP.iss"
if($LASTEXITCODE -ne 0){throw "Compilacao do instalador falhou."}
Write-Host "Instalador gerado em Installer\Output."
