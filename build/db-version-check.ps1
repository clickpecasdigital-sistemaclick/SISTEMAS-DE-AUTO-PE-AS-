$root=Split-Path $PSScriptRoot -Parent
Write-Host "Schema controlado pelo DatabaseUpgradeService."
Write-Host "Antes de qualquer mudança de schema: incremente VersaoAtual, implemente o case correspondente e teste atualização sobre uma cópia do banco anterior."
Write-Host "Nunca use EnsureDeleted em produção."
