# Como gerar o executável e instalador no Windows
Pré-requisitos: Windows 10/11 x64, .NET 8 SDK e Inno Setup 6.

PowerShell, na raiz do projeto:
`powershell -ExecutionPolicy Bypass -File .\build\preflight.ps1`

Publicação:
`powershell -ExecutionPolicy Bypass -File .\build\publish-win-x64.ps1`

Instalador:
`powershell -ExecutionPolicy Bypass -File .\build\build-installer.ps1`

O instalador somente deve ser distribuído se o preflight, o build Release e o checklist de homologação forem aprovados. A emissão fiscal exige um provedor SEFAZ real e credenciais/certificado válidos.
