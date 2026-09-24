$ErrorActionPreference="Stop"
$root=Split-Path $PSScriptRoot -Parent
& "$PSScriptRoot\source-audit.ps1"
& "$PSScriptRoot\preflight.ps1"
& "$PSScriptRoot\publish-win-x64.ps1"
& "$PSScriptRoot\build-installer.ps1"
Write-Host "Pipeline concluido. Execute TESTE_INTEGRADO.md antes de distribuir."
