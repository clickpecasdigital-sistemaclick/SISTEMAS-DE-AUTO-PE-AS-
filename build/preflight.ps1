$ErrorActionPreference="Stop"
Write-Host "=== Auto Pecas ERP - Preflight ==="
& "$PSScriptRoot\source-audit.ps1"
if(-not (Get-Command dotnet -ErrorAction SilentlyContinue)){ throw ".NET SDK nao encontrado. Instale .NET 8 SDK." }
$v=dotnet --version
Write-Host ".NET SDK: $v"
if(-not $v.StartsWith("8.")){ Write-Warning "Projeto foi definido para .NET 8. SDK atual: $v" }
$solution=Get-ChildItem -Path (Split-Path $PSScriptRoot -Parent) -Filter *.sln -Recurse | Select-Object -First 1
if(-not $solution){ throw "Arquivo .sln nao encontrado." }
Write-Host "Solution: $($solution.FullName)"
dotnet restore $solution.FullName
dotnet build $solution.FullName -c Release --no-restore
if($LASTEXITCODE -ne 0){ throw "Build falhou. Corrija os erros antes de publicar." }
Write-Host "Preflight concluido: build Release aprovado."
