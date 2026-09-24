$ErrorActionPreference="Stop"
$root=Split-Path $PSScriptRoot -Parent
& "$PSScriptRoot\preflight.ps1"
$desktop=Get-ChildItem "$root\src" -Filter *.csproj -Recurse | Where-Object {$_.FullName -match "Desktop"} | Select-Object -First 1
if(-not $desktop){ throw "Projeto Desktop nao encontrado." }
$out=Join-Path $root "publish\win-x64"
if(Test-Path $out){ Remove-Item $out -Recurse -Force }
dotnet publish $desktop.FullName -c Release -r win-x64 --self-contained true -o $out `
 /p:PublishSingleFile=false /p:DebugType=None /p:DebugSymbols=false
if($LASTEXITCODE -ne 0){ throw "Publish falhou." }
Write-Host "Publicado em: $out"
